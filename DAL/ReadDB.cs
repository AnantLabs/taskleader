 using System;
using System.Collections;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using TaskLeader.BO;

namespace TaskLeader.DAL
{
    public class ReadDB
    {
        // Variable locale pour stocker une référence vers l'instance
        private static ReadDB instance = null;

        // Renvoie l'instance ou la crée
        public static ReadDB Instance
        {
            get
            {
                // Si pas d'instance existante on en crée une...
                if (instance == null)
                    instance = new ReadDB();

                // On retourne l'instance de MonSingleton
                return instance;
            }
        }      

        // Méthode générique pour récupérer une seule colonne
        private object[] getList(String requete)
        {
            // Si le mode debug est activé, on affiche les requêtes
            if(System.Configuration.ConfigurationManager.AppSettings["debugMode"] == "true")
                MessageBox.Show(requete, "Requête", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ArrayList liste = new ArrayList();

            try
            {
                using (SQLiteCommand SQLCmd = new SQLiteCommand(DB.Instance.getConnection()))
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
                using (SQLiteDataAdapter SQLAdap = new SQLiteDataAdapter(requete, DB.Instance.getConnection()))
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
                using (SQLiteCommand SQLCmd = new SQLiteCommand(DB.Instance.getConnection()))
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
		
        // Vérification si un numéro de version est présent dans la table des compatibilités
        public bool isVersionComp(String version) //TODO=========================================
        {
            // Si une ligne matche la base est compatible
            return (getInteger("SELECT count(rowid) FROM Properties WHERE ActionsDBVer='" + version + "'") == 1);
        }

        // On vérifie la version la plus haute compatible avec cette base
        public String getLastVerComp() //TODO=========================================
        {
            return (String)getList("SELECT Num FROM VerComp WHERE rowid=(SELECT max(rowid) FROM VerComp)")[0];
        }

		// =====================================================================================
		
		// Vérification de la présence d'une nouvelle valeur d'une entité
		public bool isNvo(DBentity entity, String title)
		{
			String titre = "'" + title.Replace("'", "''") + "'";
            String requete = "SELECT count(id) FROM "+entity.mainTable+" WHERE Titre=" + titre;

            return (getInteger(requete) == 0);
		}
    
        // Vérification de la présence d'un nouveau sujet
        public bool isNvoSujet(String contexte, String subject)
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
		
		// Récupération de la liste des valeurs d'une entité. Obsolète: getCtxt, getDest, getStatut, getFilters(
		public object[] getTitres(DBentity entity)
		{
			return getList("SELECT Titre FROM "+entity.mainTable+" ORDER BY Titre ASC");
		}

        // Renvoie un tableau de tous les sujets correspondant au contexte
        public object[] getSujet(String contexte)
        {
            return getList("SELECT Titre FROM VueSujets WHERE Contexte ='" + contexte + "' ORDER BY Titre ASC");
        }

        // Renvoie le nom du statut par défaut
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
            DataRow resultat = getTable(requete).Rows[0];
            
            // On crée le filtre correspondant
            Filtre filtre = new Filtre((bool)resultat["AllCtxt"],(bool)resultat["AllSuj"],(bool)resultat["AllDest"],(bool)resultat["AllStat"]);
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
                foreach(object item in liste)
                    critere.selected.Add(item);            
            }

            return filtre;
        }
            
        // Renvoie un DataTable de toutes les actions
        public DataTable getActions(Object[] criteria)
        {
            // Création de la requête de filtrage
            String requete = "SELECT * FROM VueActions";

            String selection, nomColonne;

            if (criteria.Length > 0) // Il n'y a de WHERE que si au moins un criterium a été entré
            {
                requete += " WHERE ";

                foreach (Criterium critere in criteria) // On boucle sur tous les critères du filtre
                {
                    // On récupère le nom de la colonne correspondant au critère
                    nomColonne = critere.entity.viewColName;

                    if (critere.selected.Count > 0) // Requête SQL si au moins un élément a été sélectionné
                    {
                        requete += nomColonne + " IN (";
                        foreach (String item in critere.selected)
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

		// Renvoie les données liées à une action
		public DataRow getAction(String ID)
		{
			DataTable result = getTable("SELECT * FROM VueActions WHERE id='"+ID+"'");
			return result.Rows[0];
		}	
		
        // Récupération des liens attachés à une action
        public Array getLinks(String actionID)
        {
            DataTable linksData = getTable("SELECT EncType,EncID FROM Enclosures WHERE ActionID=" + actionID);
            ArrayList liste = new ArrayList();

            foreach (DataRow link in linksData.Rows)
                switch (link["EncType"].ToString())
                {
                    case("Mails"):
                        liste.Add(new Mail(link["EncID"].ToString()));
                        break;
                }

            return liste.ToArray();
        }

        // Récupération des informations d'un mail à partir de son ID
        public DataRow getMailData(String id)
        {
			DataTable result = getTable("SELECT * FROM Mails WHERE id='" + id + "'");
            return result.Rows[0];
        }
		
		// Récupération des informations d'un lien à partir de son ID
		public DataRow getLinkData(String id)
		{
			DataTable result = getTable("SELECT * FROM Links WHERE id='" + id + "'");
            return result.Rows[0];
		}

        // Recherche de mots clés dans la colonne Action
        public DataTable searchActions(String keywords)
        {
            String requete = "SELECT * FROM VueActions WHERE Titre LIKE '%"+keywords.Replace("'", "''")+"%';";
            return getTable(requete);
        }
    }
}
