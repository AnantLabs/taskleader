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
        private Object[] v_criteria;
        public Object[] criteria { get { return v_criteria; } }

        // Nom du filtre
        private String v_nomFiltre = "";
        public String nom { get { return v_nomFiltre; } set { v_nomFiltre = value; } }        

        // Constructeur complet
        public Filtre(String nomDB, bool allCtxt, bool allSuj, bool allDest, bool allStat, IList ctxt = null, IList suj = null, IList dest = null, IList stat = null)
        {
            this.v_type = 1;
            this.dbName = nomDB;
            
            ArrayList criteres = new ArrayList();

            if (!allCtxt)
                criteres.Add(new Criterium(DB.contexte, ctxt));

            if (ctxt != null && ctxt.Count == 1 && !allSuj)
                criteres.Add(new Criterium(DB.sujet, suj));

            if (!allDest)
                criteres.Add(new Criterium(DB.destinataire, dest));

            if (!allStat)
                criteres.Add(new Criterium(DB.statut, stat));

            this.v_criteria = criteres.ToArray();
        }

        /// <summary>
        /// Constructeur pour une recherche
        /// </summary>
        public Filtre(String recherche, String nomDB)
        {
            this.v_type = 2;
            this.dbName = nomDB;
            this.v_nomFiltre = recherche;
        }

        public DataTable getActions()
        {
            // Stockage du filtre
            this.db.CurrentFilter = this;

            DataTable data = new DataTable();

            switch (this.type)
            {
                case (1):
                    data = db.getActions(this.criteria);
                    break;
                case (2):
                    data = db.searchActions(this.nom);
                    break;
            }

            return data;
        }
    
    }
}
