using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using TaskLeader.GUI;
using TaskLeader.DAL;

namespace TaskLeader.BO
{
    public class Criterium
    {
        private DBentity v_champ;
        public DBentity entity { get { return v_champ; } }
 
        private ArrayList v_selected = new ArrayList(); //TODO: à remplacer par une List<String>
        public ArrayList selected { get { return v_selected; } }

        public Criterium(DBentity entity, IList criteres)
        {
            this.v_champ = entity;

            if (criteres!=null)
                v_selected.AddRange(criteres);
        }

        public override String ToString()
        {
            List<String> liste = new List<String>();
            foreach(String valeur in this.v_selected)
                liste.Add(valeur);
            return String.Join(" + ", liste);
        }
    }

    public class Filtre
    {
        /// <summary>
        /// Type du filtre: 1=Critères, 2=Recherche
        /// </summary>
        public int type { get { return v_type; } }
        private int v_type;

        // DB d'application de ce filtre
        public String dbName;
        private DB db { get { return TrayIcon.dbs[dbName]; } }

        // Tableau qui donne la liste des critères sélectionnés autre que ALL        
        private Dictionary<DBentity, Criterium> criteriaList = new Dictionary<DBentity, Criterium>();
        public List<Criterium> criteria { get { return criteriaList.Values.ToList<Criterium>(); } }

        // Nom du filtre
        private String v_nomFiltre = "";
        public String nom { get { return v_nomFiltre; } set { v_nomFiltre = value; } }        

        // Constructeur complet
        public Filtre(String nomDB, bool allCtxt, bool allSuj, bool allDest, bool allStat, IList ctxt = null, IList suj = null, IList dest = null, IList stat = null)
        {
            this.v_type = 1;
            this.dbName = nomDB;

            if (!allCtxt)
                this.criteriaList.Add(DB.contexte, new Criterium(DB.contexte, ctxt));

            if (ctxt != null && ctxt.Count == 1 && !allSuj)
                this.criteriaList.Add(DB.sujet,new Criterium(DB.sujet, suj));

            if (!allDest)
                this.criteriaList.Add(DB.destinataire, new Criterium(DB.destinataire, dest));

            if (!allStat)
                this.criteriaList.Add(DB.statut, new Criterium(DB.statut, stat));
        }


        /// <summary>
        /// Constructeur pour une recherche
        /// </summary>
        /// <param name="recherche">Chaîne à rechercher</param>
        /// <param name="nomDB">Nom de la base</param>
        public Filtre(String recherche, String nomDB)
        {
            this.v_type = 2;
            this.dbName = nomDB;
            this.v_nomFiltre = recherche;
        }


        public Filtre(String nomDB)
        {
            this.v_type = 1;
            this.dbName = nomDB;
        }

        public void addCriterium(Criterium critere)
        {
            if (critere != null)
                this.criteriaList.Add(critere.entity, critere);
        }

        /// <summary>
        /// Retourne une DataTable contenant les actions du filtre
        /// </summary>
        public DataTable getActions()
        {
            // Récupération des actions
            DataTable dbData;
            switch (this.type)
            {
                case (1):
                    dbData = db.getActions(this.criteria);
                    break;
                case (2):
                    dbData = db.searchActions(this.nom);
                    break;
                default:
                    dbData = new DataTable();
                    break;
            }

            // Typage des colonnes pour éviter les problèmes de Merge
            DataTable data = dbData.Clone(); // Copie du schéma uniquement
            foreach (DataColumn column in data.Columns)
                column.DataType = typeof(String);
            data.Columns["Deadline"].DataType = typeof(DateTime);
            foreach (DataRow row in dbData.Rows)
                data.ImportRow(row);

            // Ajout d'une colonne contenant le nom de la DB de ce filtre
            DataColumn col = new DataColumn("DB", typeof(String));
            col.DefaultValue = this.dbName;
            data.Columns.Add(col);

            // Ajout d'une colonne formalisant une ref pour chaque action
            data.Columns.Add("Ref", typeof(String), "DB+'" + Environment.NewLine + "#'+ID");

            // Création de la clé primaire à partir des colonnes DB et ID
            DataColumn[] keys = new DataColumn[2];
            keys[0] = data.Columns["DB"];
            keys[1] = data.Columns["ID"];
            data.PrimaryKey = keys;

            return data;
        }
 
        /// <summary>
        /// Retourne le nom du filtre
        /// </summary>
        /// <returns>Nom du filtre</returns>
        public override String ToString()
        {
            return this.v_nomFiltre; //Donc rien pour les filtres manuels.
        }

        /// <summary>
        /// Retourne un Dictionnaire DBentity => Valeur décrivant le filtre.
        /// Valeur = "" si All sélectionné.
        /// </summary>
        public Dictionary<String, String> getDescription()
        {
            Dictionary<String, String> description = new Dictionary<string, string>();

            foreach (DBentity entity in DB.entities)
                if (this.criteriaList.ContainsKey(entity))
                    description.Add(entity.nom, this.criteriaList[entity].ToString());
                else
                    description.Add(entity.nom, "");

            return description;
        }
    }
}
