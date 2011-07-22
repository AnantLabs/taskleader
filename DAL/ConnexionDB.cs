using System;
using System.Data.SQLite;
using System.Configuration;

namespace TaskLeader.DAL
{
	// Structure listant les différentes informations liées à une entité de la base (Contexte, Destinataire ...)
    public struct DBentity 
    {
		public String mainTable; // Nom de la table principale
		public String viewColName; // Nom de la colonne dans vueActions
		public String allColName; // Nom de la colonne "All" dans la table Filtre
		
		public DBentity(String view, String table, String all)
		{
			mainTable = table;
			viewColName = view;
			allColName = all;
		}
    }

    public class DB
    {
        // Variable locale pour stocker une référence vers l'instance
        private static DB instance = null;
        public static DB Instance
        {
            get
            {
                // Si pas d'instance existante on en crée une...
                if (instance == null)
                    instance = new DB();

                // On retourne l'instance de MonSingleton
                return instance;
            }
        }

        // Chemin d'accès à la base
        private String cheminDB = ConfigurationManager.AppSettings["cheminDB"];  

        // Connexion à la base SQLite
        private SQLiteConnection SQLC = null;
        // Renvoie la connection
        public SQLiteConnection getConnection()
        {
            if (SQLC == null)
                SQLC = new SQLiteConnection("Data Source=" + cheminDB);

            if (SQLC.State == System.Data.ConnectionState.Closed)
                SQLC.Open();

            return SQLC;
        }

        // Ferme la connection
        public void closeConnection()
        {
            SQLC.Close();
        }

        // "Schéma de base"
        public DBentity contexte = new DBentity("Contexte", "Contextes", "AllCtxt");
        public DBentity sujet = new DBentity("Sujet", "Sujets", "AllSuj");
        public DBentity destinataire = new DBentity("Destinataire", "Destinataires", "AllDest");
        public DBentity statut = new DBentity("Statut", "Statuts", "AllStat");
		public DBentity filtre = new DBentity("","Filtres","");
    }
}
