using System;
using System.Data.SQLite;
using System.Configuration;

namespace TaskLeader.DAL
{
    public class Schema
    {
        //TODO:rendre plus propre la déclaration du schema
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
        // Colonne dans vueActions, Nom table principale, Nom champ titre, Nom table de filtre, Colonne All dans Filtres
        // 0=contexte, 1=sujet, 2=destinataire, 3=statut
        private string[,] v_schema = new string[4, 5]
        { { "Contexte", "Contextes", "Titre", "Filtres_Ctxt","AllCtxt" }, { "Sujet", "Sujets", "Titre","Filtres_Suj","AllSuj" },
        { "Destinataire", "Destinataires", "Nom","Filtres_Dest","AllDest" },{"Statut","Statuts","Titre", "Filtres_Stat","AllStat"} };
        // 3ème colonne inutile => label harmonisé, 4ème colonne inutile => Tables supprimées
        public string[,] schema { get { return v_schema; } }

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

        

        // Ferme la connection
        public void closeConnection()
        {
            SQLC.Close();
        }
    }
}
