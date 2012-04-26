using System;
using System.Collections;
using System.Configuration;
using System.Data;
using TaskLeader.GUI;
using TaskLeader.DAL;

namespace TaskLeader.BO
{
    public class Criterium
    {
        private DBentity v_champ;
        public DBentity entity { get { return v_champ; } }
 
        private ArrayList v_selected = new ArrayList();
        public ArrayList selected { get { return v_selected; } }

        public Criterium(DBentity entity, IList criteres)
        {
            this.v_champ = entity;

            if (criteres!=null)
                v_selected.AddRange(criteres);
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
        private ArrayList v_criteria = new ArrayList();
        public Object[] criteria { get { return v_criteria.ToArray(); } }

        // Nom du filtre
        private String v_nomFiltre = "";
        public String nom { get { return v_nomFiltre; } set { v_nomFiltre = value; } }        

        // Constructeur complet
        public Filtre(String nomDB, bool allCtxt, bool allSuj, bool allDest, bool allStat, IList ctxt = null, IList suj = null, IList dest = null, IList stat = null)
        {
            this.v_type = 1;
            this.dbName = nomDB;

            if (!allCtxt)
                this.v_criteria.Add(new Criterium(DB.contexte, ctxt));

            if (ctxt != null && ctxt.Count == 1 && !allSuj)
                this.v_criteria.Add(new Criterium(DB.sujet, suj));

            if (!allDest)
                this.v_criteria.Add(new Criterium(DB.destinataire, dest));

            if (!allStat)
                this.v_criteria.Add(new Criterium(DB.statut, stat));
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
                this.v_criteria.Add(critere);
        }

        /// <summary>
        /// Retourne une DataTable contenant les actions du filtre
        /// </summary>
        public DataTable getActions()
        {
            DataTable data = new DataTable();

            // Récupération des actions
            switch (this.type)
            {
                case (1):
                    data = db.getActions(this.criteria).Copy();
                    break;
                case (2):
                    data = db.searchActions(this.nom).Copy();
                    break;
            }

            // Ajout d'une colonne contenant le nom de la DB de ce filtre
            DataColumn col = new DataColumn("DB", typeof(String));
            col.DefaultValue = this.dbName;
            data.Columns.Add(col);

            // Ajout d'une colonne formalisant une ref pour chaque action
            data.Columns.Add("Ref", typeof(String), "DB+'" + Environment.NewLine + "#'+ID");
            data.Columns["Ref"].SetOrdinal(0); //TODO: c'est de la présentation çà, doit être dans Grille

            // Ajout d'une nouvelle colonne date typé, clone de Deadline
            data.Columns.Add("Date", typeof(DateTime), "Deadline");

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
    }
}
