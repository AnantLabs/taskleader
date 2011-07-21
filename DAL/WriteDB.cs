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
                using (SQLiteCommand SQLCmd = new SQLiteCommand(DB.Instance.getConnection()))
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
            using (SQLiteTransaction mytransaction = DB.Instance.getConnection().BeginTransaction())
            {
                using (SQLiteCommand SQLCmd = new SQLiteCommand(DB.Instance.getConnection()))
                {
                    // On insère le nom du filtre
                    String nomFiltre = "'" + filtre.nom.Replace("'", "''") + "'"; // Le titre du filtre ne doit pas contenir de quote
                    SQLCmd.CommandText = "INSERT INTO Filtres (Titre) VALUES ("+nomFiltre+");";
                    SQLCmd.ExecuteNonQuery();

                    // On insère ensuite dans les tables annexes les données sélectionnées
                    foreach (Criterium critere in filtre.criteria)
                    {
                        String selection;
                        String table = critere.entity.mainTable;

                        // On crée la requête pour insertion des critères dans les tables annexes
                        String requete = "INSERT INTO Filtres_cont VALUES (";
                        requete += "(SELECT max(id) FROM Filtres),'"+table+"',(SELECT id FROM "+table+" WHERE Titre=@Titre));";
                        // On récupère le rowid du filtre frâichement créé

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
                        SQLCmd.CommandText = "UPDATE Filtres SET " + critere.entity.allColName + "=0 WHERE id = (SELECT max(id) FROM Filtres)";
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
                            " SELECT C.id, " + sujet +
                            " FROM Contextes C" +
                            " WHERE C.Titre = " + ctxt;

            return execSQL(requete);
        }

        // Insertion d'un nouveau destinataire en base
        public int insertDest(String destinataire)
        {
            String dest = "'" + destinataire.Replace("'", "''") + "'";

            String requete = "INSERT INTO Destinataires (Titre) VALUES (" + dest + ")";

            return execSQL(requete);
        }
    
        // Insertion d'un nouveau mail en base
        public String insertMail(Mail mail)
        {
            String EncID = "";
            String titre = "'" + mail.Titre.Replace("'", "''") + "'";

            using (SQLiteTransaction mytransaction = DB.Instance.getConnection().BeginTransaction())
            {
                using (SQLiteCommand SQLCmd = new SQLiteCommand(DB.Instance.getConnection()))
                {
                    // Insertion du mail
                    SQLCmd.CommandText = "INSERT INTO Mails (Titre,StoreID,EntryID,MessageID) ";
                    SQLCmd.CommandText += "VALUES(" + titre + "," + mail.StoreIDSQL + "," + mail.EntryIDSQL + "," + mail.MessageIDSQL + ");";
                    SQLCmd.ExecuteNonQuery();

                    // Récupération du EncID
                    SQLCmd.CommandText = "SELECT max(id) FROM Mails";
                    EncID = SQLCmd.ExecuteScalar().ToString();
                }
                mytransaction.Commit();
            }

            return EncID;
        }

        // Insertion d'une nouvelle action
        public void insertAction(TLaction action)
        {
            using (SQLiteTransaction mytransaction = DB.Instance.getConnection().BeginTransaction())
            {
                using (SQLiteCommand SQLCmd = new SQLiteCommand(DB.Instance.getConnection()))
                {
                    //Syntaxe: INSERT INTO Actions (nom des colonnes avec ,) VALUES(valeurs avec ' et ,)     

                    // Préparation des différents morceaux de la requête
                    String insertPart = "INSERT INTO Actions (";
                    String valuePart = " VALUES (";

                    if (action.Contexte != "")
                    {
                        insertPart += "CtxtID,";
                        valuePart += "(SELECT id FROM Contextes WHERE Titre = " + action.ContexteSQL + "),";
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
                        valuePart += "(SELECT id FROM Destinataires WHERE Titre =" + action.DestinataireSQL + "),";
                    }

                    insertPart += "StatID)";
                    valuePart += "(SELECT id FROM Statuts WHERE Titre=" + action.StatutSQL + "))";

                    SQLCmd.CommandText = insertPart + valuePart;
                    SQLCmd.ExecuteNonQuery();

                    // Insertion des infos des pièces jointes
                    foreach (Enclosure link in action.Links)
                    {
                        //Enregistrement de la PJ dans la bonne table et récupération de l'ID
                        String EncID = link.store();
                        
                        // Création de la requête
                        String requete = "INSERT INTO Enclosures VALUES(";
                        requete += "(SELECT max(id) FROM Actions),";
                        requete += link.TypeSQL+",";
                        requete += "'" + EncID + "');";

                        SQLCmd.CommandText = requete;
                        SQLCmd.ExecuteNonQuery();
                    }
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
                ctxtPart = "CtxtID=(SELECT id FROM Contextes WHERE Titre=" + action.ContexteSQL+"),";

            String sujetPart = "";
            if (action.sujetHasChanged)
                sujetPart = "SujtID=(SELECT id FROM Sujets WHERE Titre=" + action.SujetSQL + "),";

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
                destPart = "DestID=(SELECT id FROM Destinataires WHERE Titre=" + action.DestinataireSQL + "),";

            String statPart = "";
            if (action.statusHasChanged)
                statPart = "StatID=(SELECT id FROM Statuts WHERE Titre=" + action.StatutSQL + "),";
                // Il y a volontairement une virgule à la fin dans le cas où le statut n'a pas été mis à jour

            String updatePart = ctxtPart + sujetPart + actionPart + datePart + destPart + statPart;

            String requete;
            if (updatePart.Length > 0)
            {
                requete = "UPDATE Actions SET " + updatePart.Substring(0, updatePart.Length - 1) + " WHERE id='" + action.ID + "'";
                return execSQL(requete);
            }
            else
                return 0;           
        }
    }
}
