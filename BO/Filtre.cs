using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using TaskLeader.GUI;
using TaskLeader.DAL;

namespace TaskLeader.BO
{
    public class Criterium : IEquatable<Criterium>
    {
        private DBentity _champ;
        public DBentity entity { get { return _champ; } }

        private List<String> _selected = new List<String>();
        public List<String> valuesSelected { get { return _selected; } }

        public Criterium(DBentity entity, IList criteres)
        {
            this._champ = entity;

            if (criteres as IEnumerable<String> != null)
                _selected.AddRange(criteres as IEnumerable<String>);
        }

        public override String ToString()
        {
            return String.Join(" + ", _selected);
        }

        #region Implémentation de IEquatable http://msdn.microsoft.com/en-us/library/ms131190.aspx

        /// <summary>
        /// Méthode permettant la comparaison de 2 Criterium
        /// </summary>
        /// <param name="compCriter">Filtre à comparer</param>
        public bool Equals(Criterium compCriter)
        {
            if (compCriter == null)
                return false;

            return ((this._champ.nom == compCriter._champ.nom) && this._selected.SequenceEqual(compCriter._selected));
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Criterium compCriter = obj as Criterium;
            if (compCriter == null)
                return false;
            else
                return Equals(compCriter);
        }

        #endregion
    }

    public class Filtre : IEquatable<Filtre>
    {
        /// <summary>
        /// Type du filtre: 1=Critères, 2=Recherche
        /// </summary>
        public int type { get { return _type; } }
        private int _type;

        // DB d'application de ce filtre
        public String dbName;
        private DB db { get { return TrayIcon.dbs[dbName]; } }

        // Tableau qui donne la liste des critères sélectionnés autre que ALL        
        private Dictionary<DBentity, Criterium> _criteriaList = new Dictionary<DBentity, Criterium>();
        public List<Criterium> criteria { get { return _criteriaList.Values.ToList<Criterium>(); } }

        // Nom du filtre
        private String v_nomFiltre = "";
        public String nom { get { return v_nomFiltre; } set { v_nomFiltre = value; } }

        #region Constructeurs

        // Constructeur complet
        public Filtre(String nomDB, bool allCtxt, bool allSuj, bool allDest, bool allStat, IList ctxt = null, IList suj = null, IList dest = null, IList stat = null)
        {
            this._type = 1;
            this.dbName = nomDB;

            if (!allCtxt)
                this._criteriaList.Add(DB.contexte, new Criterium(DB.contexte, ctxt));

            if (ctxt != null && ctxt.Count == 1 && !allSuj)
                this._criteriaList.Add(DB.sujet, new Criterium(DB.sujet, suj));

            if (!allDest)
                this._criteriaList.Add(DB.destinataire, new Criterium(DB.destinataire, dest));

            if (!allStat)
                this._criteriaList.Add(DB.statut, new Criterium(DB.statut, stat));
        }

        /// <summary>
        /// Constructeur pour une recherche
        /// </summary>
        /// <param name="recherche">Chaîne à rechercher</param>
        /// <param name="nomDB">Nom de la base</param>
        public Filtre(String recherche, String nomDB)
        {
            this._type = 2;
            this.dbName = nomDB;
            this.v_nomFiltre = recherche;
        }

        public Filtre(String nomDB)
        {
            this._type = 1;
            this.dbName = nomDB;
        }

        #endregion

        public void addCriterium(Criterium critere)
        {
            if (critere != null)
                this._criteriaList.Add(critere.entity, critere);
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
                if (this._criteriaList.ContainsKey(entity))
                    description.Add(entity.nom, this._criteriaList[entity].ToString());
                else
                    description.Add(entity.nom, "");

            return description;
        }

        #region Implémentation de IEquatable http://msdn.microsoft.com/en-us/library/ms131190.aspx

        /// <summary>
        /// Méthode permettant la comparaison de 2 Filtres
        /// </summary>
        /// <param name="compFilter">Filtre à comparer</param>
        public bool Equals(Filtre compFilter)
        {
            if (compFilter == null)
                return false;

            // Les filtres ont des types différents
            if (this._type != compFilter.type)
                return false;

            // Les 2 filtres sont des recherches
            if (this._type == 2)
                return ((compFilter.nom == this.nom) && (compFilter.dbName == this.dbName));

            // Les 2 filtres sont des filtres enregistrés
            if (!String.IsNullOrEmpty(this.nom) && !String.IsNullOrEmpty(compFilter.nom))
                return ((compFilter.nom == this.nom) && (compFilter.dbName == this.dbName));

            // Un des 2 filtres est un fitre manuel, comparaison des critères
            return this._criteriaList.SequenceEqual(compFilter._criteriaList);
        }

        public override bool Equals(Object obj)
        {
            if (obj == null)
                return false;

            Filtre compFilter = obj as Filtre;
            if (compFilter == null)
                return false;
            else
                return Equals(compFilter);
        }

        #endregion
    }
}
