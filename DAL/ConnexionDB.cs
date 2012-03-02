using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Configuration;
using TaskLeader.BO;

namespace TaskLeader.DAL
{
    // Structure listant les différentes informations liées à une entité de la base (Contexte, Destinataire ...)
    public class DBentity
    {

        public String nom; // Nom de l'entité pour IHM
        public String mainTable; // Nom de la table principale
        public String viewColName; // Nom de la colonne dans vueActions
        public String allColName; // Nom de la colonne "All" dans la table Filtre

        public DBentity(String name, String view, String table, String all)
        {
            this.nom = name;
            this.mainTable = table;
            this.viewColName = view;
            this.allColName = all;
        }

        public DBentity parent;
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

            //TODO: ne pas harcoder les différents types
            sujet.parent = contexte;
            this.NewValue.Add(contexte.nom, null);
            this.NewValue.Add(sujet.nom, null);
            this.NewValue.Add(destinataire.nom, null);
            this.NewValue.Add(statut.nom, null);
            this.NewValue.Add(filtre.nom, null);
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
        private void closeConnection()
        {
            v_connex.Close();
        }

        // "Schéma de base"
        public static DBentity contexte = new DBentity("Contextes", "Contexte", "Contextes", "AllCtxt");
        public static DBentity sujet = new DBentity("Sujets", "Sujet", "Sujets", "AllSuj");
        public static DBentity destinataire = new DBentity("Destinataires", "Destinataire", "Destinataires", "AllDest");
        public static DBentity statut = new DBentity("Statuts", "Statut", "Statuts", "AllStat");
        public static DBentity filtre = new DBentity("Filtres", "", "Filtres", "");
        public static DBentity[] entities = { contexte, sujet, destinataire, statut};

        // Gestion des évènements NewValue - http://msdn.microsoft.com/en-us/library/z4ka55h8(v=vs.80).aspx
        private Dictionary<String, Delegate> NewValue = new Dictionary<String, Delegate>();
        public void subscribe_NewValue(DBentity entity, EventHandler value) { this.NewValue[entity.nom] = (EventHandler)this.NewValue[entity.nom] + value; }
        public void unsubscribe_NewValue(DBentity entity, EventHandler value) { this.NewValue[entity.nom] = (EventHandler)this.NewValue[entity.nom] - value; }
        private void onNewValue(DBentity entity)
        {
            EventHandler handler;
            if (null != (handler = (EventHandler)this.NewValue[entity.nom]))
                handler(this, new EventArgs());
        }
    }
}
