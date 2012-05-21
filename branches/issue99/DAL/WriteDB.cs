using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Windows.Forms;
using TaskLeader.BO;
using TaskLeader.GUI;

namespace TaskLeader.DAL
{
    public struct DBvalue
    {
        public DBentity entity; // Nom de l'entité DB
        public String value; // Valeur

        public DBvalue(DBentity table, String valeur)
        {
            entity = table;
            value = valeur;
        }
    }

    public partial class SQLiteDB
    {
        // Méthode générique pour exécuter une requête sql fournie en paramètre, retourne le nombre de lignes modifiées
        public int execSQL(String requete)
        {
            // Si le mode debug est activé, on affiche les requêtes
            if (System.Configuration.ConfigurationManager.AppSettings["debugMode"] == "true")
                MessageBox.Show(requete, "Requête", MessageBoxButtons.OK, MessageBoxIcon.Information);

            try
            {
                using (SQLiteCommand SQLCmd = new SQLiteCommand(this.SQLC))
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

        // Insertion d'une valeur par défaut
        public void insertDefaut(object[] values)
        {
            using (SQLiteTransaction mytransaction = this.SQLC.BeginTransaction())
            {
                using (SQLiteCommand SQLCmd = new SQLiteCommand(this.SQLC))
                {
                    // On efface toutes les valeurs par défaut
                    SQLCmd.CommandText = "UPDATE Contextes SET Defaut=NULL WHERE Defaut=1;";
                    SQLCmd.CommandText += "UPDATE Sujets SET Defaut=NULL WHERE Defaut=1;";
                    SQLCmd.CommandText += "UPDATE Destinataires SET Defaut=NULL WHERE Defaut=1;";
                    SQLCmd.CommandText += "UPDATE Statuts SET Defaut=NULL WHERE Defaut=1;";
                    SQLCmd.CommandText += "UPDATE Filtres SET Defaut=NULL WHERE Defaut=1;";
                    SQLCmd.ExecuteNonQuery();

                    // On insère les valeurs par défaut sélectionnées
                    foreach (DBvalue DBvalue in values)
                    {
                        SQLCmd.CommandText += "UPDATE " + DBvalue.entity.mainTable + " SET Defaut=1 WHERE Titre='" + DBvalue.value + "';";
                        SQLCmd.ExecuteNonQuery();
                    }
                }

                mytransaction.Commit();
            }
        }

        // Insertion en base d'un nouveau filtre
        public void insertFiltre(Filtre filtre)
        {
            using (SQLiteTransaction mytransaction = this.SQLC.BeginTransaction())
            {
                using (SQLiteCommand SQLCmd = new SQLiteCommand(this.SQLC))
                {
                    // On insère le nom du filtre
                    String nomFiltre = "'" + filtre.nom.Replace("'", "''") + "'"; // Le titre du filtre ne doit pas contenir de quote
                    SQLCmd.CommandText = "INSERT INTO Filtres (Titre) VALUES (" + nomFiltre + ");";
                    SQLCmd.ExecuteNonQuery();

                    // On insère ensuite dans les tables annexes les données sélectionnées
                    foreach (Criterium critere in filtre.criteria)
                    {
                        String selection;
                        String table = critere.entity.mainTable;

                        // On crée la requête pour insertion des critères dans les tables annexes
                        String requete = "INSERT INTO Filtres_cont VALUES (";
                        requete += "(SELECT max(id) FROM Filtres),'" + table + "',(SELECT id FROM " + table + " WHERE Titre=@Titre));";
                        // On récupère le rowid du filtre frâichement créé

                        SQLCmd.CommandText = requete;
                        SQLiteParameter p_Titre = new SQLiteParameter("@Titre");
                        SQLCmd.Parameters.Add(p_Titre);

                        foreach (String item in critere.valuesSelected)
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


            this.OnNewValue(DB.filtre);
            // On affiche un message de statut sur la TrayIcon
            TrayIcon.afficheMessage("Bilan création/modification", "Nouveau filtre ajouté: " + filtre.nom);

        }

        // Méthode générique d'insertion de certaine DBentity
        public int insert(DBentity entity, String value)
        {
            String sqlValue = "'" + value.Replace("'", "''") + "'";
            String requete = "INSERT INTO " + entity.mainTable + " (Titre) VALUES (" + sqlValue + ")";

            int result = execSQL(requete);
            if (result == 1)
                this.OnNewValue(contexte);

            return result;
        }

        // Insertion d'un nouveau sujet en base
        public int insertSujet(String contexte, String value)
        {
            String ctxt = "'" + contexte.Replace("'", "''") + "'";
            String sjt = "'" + value.Replace("'", "''") + "'";

            String requete = "INSERT INTO Sujets (CtxtID,Titre)" +
                            " SELECT C.id, " + sjt +
                            " FROM Contextes C" +
                            " WHERE C.Titre = " + ctxt;

            int result = execSQL(requete);
            if (result == 1)
                this.OnNewValue(sujet, contexte);

            return result;
        }

        // Insertion d'un nouveau mail en base
        public String insertMail(Mail mail)
        {
            String EncID = "";
            String titre = "'" + mail.Titre.Replace("'", "''") + "'";

            using (SQLiteTransaction mytransaction = this.SQLC.BeginTransaction())
            {
                using (SQLiteCommand SQLCmd = new SQLiteCommand(this.SQLC))
                {
                    // Insertion du mail
                    SQLCmd.CommandText = "INSERT INTO Mails (Titre,StoreID,EntryID,MessageID) ";
                    SQLCmd.CommandText += "VALUES(" + titre + "," + mail.StoreIDSQL + "," + mail.EntryIDSQL + "," + mail.MessageIDSQL + ");";
                    SQLCmd.ExecuteNonQuery();

                    // Récupération du EncID
                    SQLCmd.CommandText = "SELECT max(id) FROM Mails;";
                    EncID = SQLCmd.ExecuteScalar().ToString();
                }
                mytransaction.Commit();
            }

            return EncID;
        }

        // Insertion d'un nouveau lien en base
        public String insertLink(Link lien)
        {
            String EncID = "";
            String titre = "'" + lien.Titre.Replace("'", "''") + "'";

            using (SQLiteTransaction mytransaction = this.SQLC.BeginTransaction())
            {
                using (SQLiteCommand SQLCmd = new SQLiteCommand(this.SQLC))
                {
                    // Insertion du mail
                    SQLCmd.CommandText = "INSERT INTO Links (Titre,Path) ";
                    SQLCmd.CommandText += "VALUES(" + titre + "," + lien.urlSQL + ");";
                    SQLCmd.ExecuteNonQuery();

                    // Récupération du EncID
                    SQLCmd.CommandText = "SELECT max(id) FROM Links;";
                    EncID = SQLCmd.ExecuteScalar().ToString();
                }
                mytransaction.Commit();
            }

            return EncID;
        }

        // Insertion des PJ
        public void insertPJ(String actionID, List<Enclosure> PJ)
        {
            String requete;

            foreach (Enclosure enc in PJ)
            {
                //Enregistrement de la PJ dans la bonne table et récupération de l'ID
                String EncID = "";

                switch (enc.Type)
                {
                    case ("Mails"):
                        EncID = this.insertMail((Mail)enc);
                        break;
                    case ("Links"):
                        EncID = this.insertLink((Link)enc);
                        break;
                }

                // Création de la requête
                requete = "INSERT INTO Enclosures VALUES(";
                requete += actionID + ",";
                requete += "'" + enc.Type + "',";
                requete += EncID + ");";

                execSQL(requete);
                this.OnActionEdited(actionID);
            }
        }

        // Suppression d'une liste de PJs de la base (toutes liées à la même action)
        public void removePJ(String actionID, List<Enclosure> PJ)
        {
            foreach (Enclosure pj in PJ)
            {
                using (SQLiteTransaction mytransaction = this.SQLC.BeginTransaction())
                {
                    using (SQLiteCommand SQLCmd = new SQLiteCommand(this.SQLC))
                    {
                        // Suppression de la pj dans la table correspondante
                        SQLCmd.CommandText = "DELETE FROM " + pj.Type + " WHERE id=" + pj.ID + ";";
                        SQLCmd.ExecuteNonQuery();

                        // Suppression de l'entrée dans la table de correspondance PJ/Action
                        SQLCmd.CommandText = "DELETE FROM Enclosures WHERE EncID=" + pj.ID + ";";
                        SQLCmd.ExecuteNonQuery();
                    }
                    mytransaction.Commit();
                }
            }

            if (PJ.Count > 0)
                this.OnActionEdited(actionID);
        }

        // Insertion d'une nouvelle action
        // Renvoie l'ID de stockage de l'action
        public String insertAction(TLaction action)
        {
            String actionID;

            using (SQLiteTransaction mytransaction = this.SQLC.BeginTransaction())
            {
                using (SQLiteCommand SQLCmd = new SQLiteCommand(this.SQLC))
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

                    insertPart += "Titre,"; // On a déjà vérifié que la chaîne n'était pas nulle
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

                    SQLCmd.CommandText = "SELECT max(id) FROM Actions;";
                    actionID = SQLCmd.ExecuteScalar().ToString();
                }
                mytransaction.Commit();
            }

            this.OnActionEdited(actionID);
            return actionID;
        }

        // Mise à jour d'une action (flexible)
        public int updateAction(TLaction action)
        {
            // Préparation des sous requêtes
            String ctxtPart = "";
            if (action.ctxtHasChanged)
                ctxtPart = "CtxtID=(SELECT id FROM Contextes WHERE Titre=" + action.ContexteSQL + "),";

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

                int result = execSQL(requete);
                this.OnActionEdited(action.ID);
                return result;
            }
            else
                return 0;
        }
    }
}
