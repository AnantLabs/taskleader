 using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using TaskLeader.BO;
using TaskLeader.GUI;

namespace TaskLeader.DAL
{

    public partial class SQLiteDB
    {    
        // Méthode générique pour récupérer une seule colonne
        private object[] getList(String requete)
        {
            // Si le mode debug est activé, on affiche les requêtes
            if(System.Configuration.ConfigurationManager.AppSettings["debugMode"] == "true")
                MessageBox.Show(requete, "Requête", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ArrayList liste = new ArrayList();

            try
            {
                using (SQLiteCommand SQLCmd = new SQLiteCommand(this.SQLC))
                {
                    // Création d'une nouvelle commande à partir de la connexion
                    SQLCmd.CommandText = requete;

                    using (SQLiteDataReader SQLDReader = SQLCmd.ExecuteReader())
                    {
                        // La méthode Read() lit l'entrée actuelle puis renvoie true tant qu'il y a des entrées à lire.
                        while (SQLDReader.Read())
                            liste.Add(SQLDReader[0]); // On retourne la seule et unique colonne
                    }
                }
            }
            catch (Exception Ex)
            {
                // On affiche l'erreur.
                MessageBox.Show(Ex.Message, "Exception sur getList", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return liste.ToArray();
        }

        // Méthode générique pour récupérer une table
        private DataTable getTable(String requete)
        {
            // Si le mode debug est activé, on affiche les requêtes
            if (System.Configuration.ConfigurationManager.AppSettings["debugMode"] == "true")
                MessageBox.Show(requete, "Requête", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Création de la DataTable
            DataTable data = new DataTable();

            try
            {
                using (SQLiteDataAdapter SQLAdap = new SQLiteDataAdapter(requete,this.SQLC))
                {
                    // Remplissage avec les données de l'adaptateur
                    SQLAdap.Fill(data);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Exception sur getTable", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return data;
        }

        // Méthode générique pour récupérer un entier
        private int getInteger(String requete)
        {
            // Si le mode debug est activé, on affiche les requêtes
            if (System.Configuration.ConfigurationManager.AppSettings["debugMode"] == "true")
                MessageBox.Show(requete, "Requête", MessageBoxButtons.OK, MessageBoxIcon.Information);

            try
            {
                using (SQLiteCommand SQLCmd = new SQLiteCommand(this.SQLC))
                {
                    SQLCmd.CommandText = requete;
                    return Convert.ToInt32(SQLCmd.ExecuteScalar());
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Exception sur getInteger", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return -1;
            }
        }

		// =====================================================================================
		
        // Vérification si la table est bien compatible
        public bool isVersionComp(String version)
        {
            if (this.SQLC.GetSchema("Tables").Select("Table_Name = 'Properties'").Length > 0) // Version >= 0.7
                return (getList("SELECT rowid FROM Properties WHERE Cle='ActionsDBVer' AND Valeur='"+version+"';").Length > 0);
            else
                return false;
        }

        // On vérifie la version la plus haute compatible avec cette base
        public String getLastVerComp()
        {
            if (this.SQLC.GetSchema("Tables").Select("Table_Name = 'Properties'").Length > 0) // Version >= 0.7
                return getList("SELECT Valeur FROM Properties WHERE Cle='ActionsDBVer';")[0].ToString();
            else // Version < 0.7
                return (String)getList("SELECT Num FROM VerComp WHERE rowid=(SELECT max(rowid) FROM VerComp)")[0];
        }

		// =====================================================================================
		
		// Vérification de la présence d'une nouvelle valeur d'une entité
		public override bool isNvo(DBField entity, String title)
		{
			String titre = "'" + title.Replace("'", "''") + "'";
            String requete = "SELECT count(id) FROM "+entity.mainTable+" WHERE Titre=" + titre;

            return (getInteger(requete) == 0);
		}
    
        // Vérification de la présence d'un nouveau sujet
        public override bool isNvoSujet(String contexte, String subject)
        {
            String ctxt = "'" + contexte.Replace("'", "''") + "'";
            String sujet = "'" + subject.Replace("'", "''") + "'";

            String requete = "SELECT count(Titre) FROM VueSujets WHERE Contexte = " + ctxt + " AND Titre = " + sujet;

            if (this.getInteger(requete) == 0)
                return true;
            else
                return false;
        }
        
		// =====================================================================================
		
		// Récupération de la liste des valeurs d'une entité. Obsolète
		public object[] getTitres(DBentity entity,String key=null)
		{
            if(entity.mainTable!="Sujets")
			    return getList("SELECT Titre FROM "+entity.mainTable+" ORDER BY Titre ASC");
            else
                return getList("SELECT Titre FROM VueSujets WHERE Contexte ='" + key + "' ORDER BY Titre ASC");
		}

        /// <summary>
        /// Récupération des valeurs par défaut
        /// </summary>
        /// <param name="entity">DBFields.contexte, DBFields.destinataire ...</param>
        /// <returns>Valeur par défaut de l'entité</returns>
        public String getDefault(DBentity entity)
        {
            Object[] resultat = getList("SELECT Titre FROM " + entity.mainTable + " WHERE Defaut='1'");

            if (resultat.Length == 1)
                return (String)resultat[0];
            else
                return "";
        }
   
        // Récupère un filtre en fonction de son titre
        public Filtre getFilter(String name){

            // On récupère d'abord les checkbox all
            String titre = "'" + name.Replace("'", "''") + "'";
            String requete = "SELECT AllCtxt, AllSuj, AllDest, AllStat FROM Filtres WHERE Titre=" + titre;
            DataRowCollection results = getTable(requete).Rows;

            if (results.Count == 0)
                return null;

            DataRow resultat = results[0];
            
            // On crée le filtre correspondant
            Filtre filtre = new Filtre(this.name,(bool)resultat["AllCtxt"],(bool)resultat["AllSuj"],(bool)resultat["AllDest"],(bool)resultat["AllStat"]);
            filtre.nom = name;
            object[] liste;

            // On récupère les sélections si nécessaire
            foreach (Criterium critere in filtre.criteria)
            {
                // Récupération du nom de la table correspondante
                String table = critere.entity.mainTable;
                // Création de la requête
                requete = "SELECT TP.Titre FROM "+table+" TP, Filtres_cont TF, Filtres F ";
                requete += "WHERE F.Titre =" + titre + " AND TF.FiltreID=F.rowid AND TF.FiltreType='"+table+"' AND TF.SelectedID=TP.rowid";
                // Récupération de la liste
                liste = getList(requete);

                // On met à jour le critère du filtre correspondant
                foreach(String item in liste)
                    critere.valuesSelected.Add(item);            
            }

            return filtre;
        }
        
        /// <summary>
        /// Obtient un tableau de tous les filtres de la base
        /// </summary>
        /// <returns>Tableau de 'Filtre'</returns>
        public object[] getFilters()
        {
            ArrayList liste = new ArrayList();

            foreach (String filtreName in this.getTitres(DB.filtre))
                liste.Add(this.getFilter(filtreName));

            return liste.ToArray();
        }

        // Renvoie un DataTable de toutes les actions
        public DataTable getActions(List<Criterium> criteria)
        {
            // Création de la requête de filtrage
            String requete = "SELECT * FROM VueActions";

            String selection, nomColonne;

            if (criteria.Count > 0) // Il n'y a de WHERE que si au moins un criterium a été entré
            {
                requete += " WHERE ";

                foreach (Criterium critere in criteria) // On boucle sur tous les critères du filtre
                {
                    // On récupère le nom de la colonne correspondant au critère
                    nomColonne = critere.entity.viewColName;

                    if (critere.valuesSelected.Count > 0) // Requête SQL si au moins un élément a été sélectionné
                    {
                        requete += nomColonne + " IN (";
                        foreach (String item in critere.valuesSelected)
                        {
                            selection = item.Replace("'", "''"); // On gère les simple quote
                            requete += "'" + selection + "', ";
                        }
                        requete = requete.Substring(0, requete.Length - 2); // On efface la dernière virgule et le dernier space en trop
                        requete += ")";
                    }
                    else
                        requete += nomColonne + " IS NULL";

                    requete += " AND ";
                }

                requete = requete.Substring(0, requete.Length - 5); // On enlève le dernier AND en trop
            }

            return getTable(requete);
        }
        
        /// <summary>Renvoie les données liées à une action</summary>
        /// <param name="ID">ID de l'action dans la base</param>
		public DataRow getAction(String ID)
		{
			DataTable result = getTable("SELECT * FROM VueActions WHERE id='"+ID+"'");
			return result.Rows[0];
		}

        /// <summary>Récupération des liens attachés à une action</summary>
        public List<Enclosure> getPJ(String actionID)
        {
            DataTable linksData = getTable("SELECT EncType,EncID FROM Enclosures WHERE ActionID=" + actionID);
            List<Enclosure> liste = new List<Enclosure>();

            foreach (DataRow pj in linksData.Rows)
                switch (pj["EncType"].ToString())
                {
                    case("Mails"):
                        liste.Add(new Mail(pj["EncID"].ToString(),this));
                        break;
                    case("Links"):
                        liste.Add(new Link(pj["EncID"].ToString(),this));
                        break;
                }

            return liste;
        }

        // Récupération des informations d'un mail à partir de son ID
        public DataRow getMailData(String id)
        {
			DataTable result = getTable("SELECT * FROM Mails WHERE id=" + id);
            return result.Rows[0];
        }
		
		// Récupération des informations d'un lien à partir de son ID
		public DataRow getLinkData(String id)
		{
			DataTable result = getTable("SELECT * FROM Links WHERE id=" + id);
            return result.Rows[0];
		}

        // Recherche de mots clés dans la colonne Action
        public DataTable searchActions(String keywords)
        {
            String words = keywords.Replace("'", "''");

            String requete = "SELECT * FROM VueActions WHERE Titre LIKE '%"+words+"%' OR ";
            requete += "id IN("; // Recherche dans les titres de mail
            requete += "   SELECT E.ActionID FROM Mails M, Enclosures E";
            requete += "      WHERE M.Titre LIKE '%" + words + "%'";
            requete += "      AND M.id=E.EncID";
            requete += "      AND E.EncType='Mails'";
            requete += ") OR ";
            requete += "id IN("; // Recherche dans les titres ou chemins des liens
            requete += "   SELECT E.ActionID FROM Links L, Enclosures E";
            requete += "      WHERE L.Titre LIKE '%" + words + "%'";
            requete += "      AND L.id=E.EncID";
            requete += "      AND E.EncType='Links'";
            requete += ");";

            return getTable(requete);
        }
    }
}
