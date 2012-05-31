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
        /// <summary>
        /// Nom de l'entité pour IHM, !!doit être unique !!
        /// </summary>
        public String nom;
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

    public delegate void ParentValueEventHandler(String parentValue);
    public delegate void ActionEditedEventHandler(String dbName, String actionID);

    public partial class DB //TODO: détecter les ouvertures de fichier pour les limiter
    {
        // Caractéristiques de la DB
        public String path = "";
        public String name = "";

        /// <summary>
        /// Retourne le nom de la base
        /// </summary>
        public override string ToString() { return this.name; }

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

        private String _connectionString { get { return "Data Source=" + this.path + ";FailIfMissing=True;Version=3;Pooling=True;Max Pool Size=100;"; } }

        // "Schéma de base" = Nom de l'entité pour IHM, Nom de la colonne dans vueActions, Nom de la table principale, Nom de la colonne "All" dans la table Filtre
        public static DBentity contexte = new DBentity("Contextes", "Contexte", "Contextes", "AllCtxt");
        public static DBentity sujet = new DBentity("Sujets", "Sujet", "Sujets", "AllSuj");
        public static DBentity destinataire = new DBentity("Destinataires", "Destinataire", "Destinataires", "AllDest");
        public static DBentity statut = new DBentity("Statuts", "Statut", "Statuts", "AllStat");
        public static DBentity filtre = new DBentity("Filtres", "", "Filtres", "");
        public static DBentity[] entities = { contexte, sujet, destinataire, statut };

        #region Events

        // Gestion des évènements NewValue - http://msdn.microsoft.com/en-us/library/z4ka55h8(v=vs.80).aspx
        private Dictionary<String, Delegate> NewValue = new Dictionary<String, Delegate>();
        public void subscribe_NewValue(DBentity entity, ParentValueEventHandler value) { this.NewValue[entity.nom] = (ParentValueEventHandler)this.NewValue[entity.nom] + value; }
        public void unsubscribe_NewValue(DBentity entity, ParentValueEventHandler value) { this.NewValue[entity.nom] = (ParentValueEventHandler)this.NewValue[entity.nom] - value; }
        /// <summary>
        /// Génération de l'évènement NewValue
        /// </summary>
        /// <param name="entity">DBentity concernée</param>
        /// <param name="parentValue">La valeur courante de la DBentity parente</param>
        private void OnNewValue(DBentity entity, String parentValue = null)
        {
            ParentValueEventHandler handler;
            if (null != (handler = (ParentValueEventHandler)this.NewValue[entity.nom]))
                handler(parentValue);
        }

        // Gestion de l'évènement ActionEdited
        public event ActionEditedEventHandler ActionEdited;
        /// <summary>
        /// Génération de l'évènement ActionEdited
        /// </summary>
        /// <param name="action">Action ayant généré l'event</param>
        private void OnActionEdited(String actionID)
        {
            if (this.ActionEdited != null)
                this.ActionEdited(this.name, actionID); //Invoque le délégué
        }

        #endregion
    }
}
