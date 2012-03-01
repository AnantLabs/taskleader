using System;
using System.Data.SQLite;
using System.Configuration;
using TaskLeader.BO;

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

    public partial class DB
    {
        // Caractéristiques de la DB
        public String path = "";
        public String name = "";

        /// <summary>
        /// Retourne le nom de la base
        /// </summary>
        public override string ToString()
        {
            return this.name;
        }

        public DB(String chemin, String nom)
        {
            this.path = chemin;
            this.name = nom;
        }

        // Connexion à la base SQLite
        private SQLiteConnection v_connex = null;
        // Renvoie la connection
        private SQLiteConnection SQLC
        {
            get
            {
                if (v_connex == null)
                    v_connex = new SQLiteConnection("Data Source=" + path);

                if (v_connex.State == System.Data.ConnectionState.Closed)
                    v_connex.Open();

                return v_connex;
            }
        }

        // Ferme la connection
        public void closeConnection()
        {
            v_connex.Close();
        }

        // "Schéma de base"
        public static DBentity contexte = new DBentity("Contexte", "Contextes", "AllCtxt");
        public static DBentity sujet = new DBentity("Sujet", "Sujets", "AllSuj");
        public static DBentity destinataire = new DBentity("Destinataire", "Destinataires", "AllDest");
        public static DBentity statut = new DBentity("Statut", "Statuts", "AllStat");
		public static DBentity filtre = new DBentity("","Filtres","");
    }
}
