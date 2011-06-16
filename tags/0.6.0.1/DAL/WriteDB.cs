using System;
using System.Collections;
using System.Data.SQLite;
using System.Windows.Forms;
using TaskLeader.BO;
using TaskLeader.GUI;

namespace TaskLeader.DAL
{
    public class WriteDB
    {
        // Variable locale pour stocker une référence vers l'instance
        private static WriteDB instance = null;

        // Renvoie l'instance ou la crée
        public static WriteDB Instance
        {
            get
            {
                // Si pas d'instance existante on en crée une...
                if (instance == null)
                    instance = new WriteDB();

                // On retourne l'instance de MonSingleton
                return instance;
            }
        }

        // Méthode générique pour exécuter une requête sql fournie en paramètre, retourne le nombre de lignes modifiées
        public int execSQL(String requete)
        {
            // Si le mode debug est activé, on affiche les requêtes
            if (System.Configuration.ConfigurationManager.AppSettings["debugMode"] == "true")
                MessageBox.Show(requete, "Requête", MessageBoxButtons.OK, MessageBoxIcon.Information);

            try
            {
                using (SQLiteCommand SQLCmd = new SQLiteCommand(ConnexionDB.Instance.getConnection()))
                {
                    // Création d'une nouvelle commande à partir de la connexion
                    SQLCmd.CommandText = requete;
                    //Exécution de la commande
                    return SQLCmd.ExecuteNonQuery();
                }
            }
            catch (Exception Ex)
            {
                // On affiche l'erreur.
                MessageBox.Show(Ex.Message, "Exception sur execSQL", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return -1;
            }
        }

        // Insertion en base d'un nouveau filtre
        public void insertFiltre(Filtre filtre)
        {
            using (SQLiteTransaction mytransaction = ConnexionDB.Instance.getConnection().BeginTransaction())
            {
                using (SQLiteCommand SQLCmd = new SQLiteCommand(ConnexionDB.Instance.getConnection()))
                {
                    // On insère le nom du filtre
                    String nomFiltre = "'" + filtre.nom.Replace("'", "''") + "'"; // Le titre du filtre ne doit pas contenir de quote
                    SQLCmd.CommandText = "INSERT INTO Filtres (Titre) VALUES ("+nomFiltre+");";
                    SQLCmd.ExecuteNonQuery();

                    // On insère ensuite dans les tables annexes les données sélectionnées
                    foreach (Criterium critere in filtre.criteria)
                    {
                        String selection;

                        // On crée la requête pour insertion des critères dans les tables annexes
                        String requete = "INSERT INTO " + ConnexionDB.Instance.schema[critere.champ, 3];
                        requete += " VALUES ((SELECT max(rowid) FROM Filtres),"; // On récupère le rowid du filtre frâichement créé
                        requete += "(SELECT rowid FROM " + ConnexionDB.Instance.schema[critere.champ, 1];
                        requete += " WHERE " + ConnexionDB.Instance.schema[critere.champ, 2] + "=@Titre));";

                        SQLCmd.CommandText = requete;
                        SQLiteParameter p_Titre = new SQLiteParameter("@Titre");
                        SQLCmd.Parameters.Add(p_Titre);

                        foreach (String item in critere.selected)
                        {
                            selection = item.Replace("'", "''"); // On gère les simple quote
                            p_Titre.Value = selection;
                            SQLCmd.ExecuteNonQuery();
                        }

                        // On précise que la case ALL n'a pas été sélectionnée pour ce critère
                        SQLCmd.CommandText = "UPDATE Filtres SET " + ConnexionDB.Instance.schema[critere.champ, 4] + "=0 WHERE rowid = (SELECT max(rowid) FROM Filtres)";
                        SQLCmd.ExecuteNonQuery();
                    }
                }
                mytransaction.Commit();
            }

            // On affiche un message de statut sur la TrayIcon
            TrayIcon.afficheMessage("Bilan création/modification","Nouveau filtre ajouté: "+filtre.nom);

        }

        // Insertion d'un nouveau contexte en base
        public int insertContexte(String contexte)
        {
            String ctxt = "'" + contexte.Replace("'", "''") + "'";
            String requete = "INSERT INTO Contextes (Titre) VALUES (" + ctxt + ")";
            return execSQL(requete);
        }

        // Insertion d'un nouveau sujet en base
        public int insertSujet(String contexte, String subject)
        {
            String ctxt = "'" + contexte.Replace("'", "''") + "'";
            String sujet = "'" + subject.Replace("'", "''") + "'";

            String requete = "INSERT INTO Sujets (CtxtID,Titre)" +
                            " SELECT C.rowid, " + sujet +
                            " FROM Contextes C" +
                            " WHERE C.Titre = " + ctxt;

            return execSQL(requete);
        }

        // Insertion d'un nouveau destinataire en base
        public int insertDest(String destinataire)
        {
            String dest = "'" + destinataire.Replace("'", "''") + "'";

            String requete = "INSERT INTO Destinataires (Nom) VALUES (" + dest + ")";

            return execSQL(requete);
        }
    
        // Insertion d'une nouvelle action
        public void insertAction(TLaction action)
        {
            using (SQLiteTransaction mytransaction = ConnexionDB.Instance.getConnection().BeginTransaction())
            {
                using (SQLiteCommand SQLCmd = new SQLiteCommand(ConnexionDB.Instance.getConnection()))
                {
                    // Insertion des infos du mail joint si nécessaire
                    if (action.hasMailAttached)
                    {
                        SQLCmd.CommandText = "INSERT INTO Mails VALUES(" + action.mail.StoreIDSQL + "," + action.mail.EntryIDSQL + "," + action.mail.MessageIDSQL + ");";
                        SQLCmd.ExecuteNonQuery();
                    }

                    //Syntaxe: INSERT INTO Actions (nom des colonnes avec ,) VALUES(valeurs avec ' et ,)     

                    // Préparation des différents morceaux de la requête
                    String insertPart = "INSERT INTO Actions (";
                    String valuePart = " VALUES (";

                    if (action.Contexte != "")
                    {
                        insertPart += "CtxtID,";
                        valuePart += "(SELECT rowid FROM Contextes WHERE Titre = " + action.ContexteSQL + "),";
                    }

                    if (action.Sujet != "")
                    {
                        insertPart += "SujtID,";
                        valuePart += "(SELECT id FROM VueSujets WHERE Contexte=" + action.ContexteSQL + " AND Titre=" + action.SujetSQL + "),";
                    }

                    insertPart += "Titre,"; // On a déjà vérifier que la chaîne n'était pas nulle
                    valuePart += action.TexteSQL + ",";

                    if (action.hasDueDate)
                    {
                        insertPart += "DueDate,";
                        valuePart += action.DueDateSQL + ",";
                    }

                    if (action.Destinataire != "")
                    {
                        insertPart += "DestID,";
                        valuePart += "(SELECT rowid FROM Destinataires WHERE Nom =" + action.DestinataireSQL + "),";
                    }

                    if (action.hasMailAttached)
                    {
                        insertPart += "IDMail,";
                        valuePart += "(SELECT max(rowid) FROM Mails),";
                    }

                    insertPart += "StatID)";
                    valuePart += "(SELECT rowid FROM Statuts WHERE Titre=" + action.StatutSQL + "))";

                    SQLCmd.CommandText = insertPart + valuePart;
                    SQLCmd.ExecuteNonQuery();
                }
                mytransaction.Commit();
            }
        }

        // Mise à jour d'une action (flexible)
        public int updateAction(TLaction action)
        {
            // Préparation des sous requêtes
            String ctxtPart = "";
            if (action.ctxtHasChanged)
                ctxtPart = "CtxtID=(SELECT rowid FROM Contextes WHERE Titre=" + action.ContexteSQL+"),";

            String sujetPart = "";
            if (action.sujetHasChanged)
                sujetPart = "SujtID=(SELECT rowid FROM Sujets WHERE Titre=" + action.SujetSQL + "),";

            String actionPart = "";
            if (action.texteHasChanged)
                actionPart = "Titre=" + action.TexteSQL + ",";

            String datePart = "";
            if (action.dueDateHasChanged)
            {
                if (action.hasDueDate)
                    datePart = "DueDate=" + action.DueDateSQL + ",";
                else
                    datePart = "DueDate=NULL,";
            }             

            String destPart = "";
            if (action.destHasChanged)
                destPart = "DestID=(SELECT rowid FROM Destinataires WHERE Nom=" + action.DestinataireSQL + "),";

            String statPart = "";
            if (action.statusHasChanged)
                statPart = "StatID=(SELECT rowid FROM Statuts WHERE Titre=" + action.StatutSQL + "),";
                // Il y a volontairement une virgule à la fin dans le cas où le statut n'a pas été mis à jour

            String updatePart = ctxtPart + sujetPart + actionPart + datePart + destPart + statPart;

            String requete;
            if (updatePart.Length > 0)
            {
                requete = "UPDATE Actions SET " + updatePart.Substring(0, updatePart.Length - 1) + " WHERE rowid='" + action.ID + "'";
                return execSQL(requete);
            }
            else
                return 0;           
        }
    }
}
