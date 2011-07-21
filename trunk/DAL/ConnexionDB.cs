using System;
using System.Data.SQLite;
using System.Configuration;

namespace TaskLeader.DAL
{
	// Structure listant les différentes informations liées à une donnée (Contexte, Destinataire ...)
    public struct TLData 
    {
		public String mainTable; // Nom de la table principale
		public String viewColName; // Nom de la colonne dans vueActions
		public String allColName; // Nom de la colonne "All" dans la table Filtre
		
		public TLData(String table, String view, String all)
		{
			mainTable = table;
			viewColName = view;
			allColName = all;
		}
    }

    public class ConnexionDB
    {
        // Variable locale pour stocker une référence vers l'instance
        private static ConnexionDB instance = null;

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

        // "Schéma de base"
        // 0=contexte, 1=sujet, 2=destinataire, 3=statut
		private TLData[] v_schema;
        public TLData[] schema { get { return v_schema; } }

        // Chemin d'accès à la base
        private String cheminDB = ConfigurationManager.AppSettings["cheminDB"];

        // Renvoie l'instance ou la crée
        public static ConnexionDB Instance
        {
            get
            {
                // Si pas d'instance existante on en crée une...
                if (instance == null)
                    instance = new ConnexionDB();

                // On retourne l'instance de MonSingleton
                return instance;
            }
        }       

		// Constructeur
		public ConnexionDB ()
		{
			// 0=contexte, 1=sujet, 2=destinataire, 3=statut
			v_schema[0] = new TLData("Contexte", "Contextes","AllCtxt");
			v_schema[1] = new TLData("Sujet", "Sujets", "AllSuj");
			v_schema[2] = new TLData("Destinataire", "Destinataires", "AllDest");
			v_schema[2] = new TLData("Statut","Statuts","AllStat");
		}
		
        // Ferme la connection
        public void closeConnection()
        {
            SQLC.Close();
        }
    }
}
