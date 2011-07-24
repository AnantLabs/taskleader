using System;
using System.Collections;
using System.Data;
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
        // Type du filtre: 1=Critères, 2=Recherche
        private int v_type;
        public int type { get { return v_type; } }

        // Tableau qui donne la liste des critères sélectionnés autre que ALL        
        private Object[] v_criteria;
        public Object[] criteria { get { return v_criteria; } }

        // Nom du filtre
        private String v_nomFiltre = "";
        public String nom { get { return v_nomFiltre; } set { v_nomFiltre = value; } }

        // Variable locale pour stocker une référence vers le filtre en cours et le précédent de type 1
        private static Filtre v_currentFilter = ReadDB.Instance.getFilter(ReadDB.Instance.getDefault(DB.Instance.filtre));
        private static Filtre v_oldFilter = null;
        public static Filtre CurrentFilter
        {
            get { return v_currentFilter; }
            set {
                if (v_currentFilter != null && v_currentFilter.type == 1)
                    v_oldFilter = v_currentFilter; // Mémorisation du dernier filtre de type 1
                v_currentFilter = value;
            }
        }
        public static Filtre OldFilter{get { return v_oldFilter; }}

        // Constructeur complet
        public Filtre(bool allCtxt, bool allSuj, bool allDest, bool allStat, IList ctxt = null, IList suj = null, IList dest = null, IList stat = null)
        {
            this.v_type = 1;
            
            ArrayList criteres = new ArrayList();

            if (!allCtxt)
                criteres.Add(new Criterium(DB.Instance.contexte, ctxt));

            if (ctxt != null && ctxt.Count == 1 && !allSuj)
                criteres.Add(new Criterium(DB.Instance.sujet, suj));

            if (!allDest)
                criteres.Add(new Criterium(DB.Instance.destinataire, dest));

            if (!allStat)
                criteres.Add(new Criterium(DB.Instance.statut, stat));

            this.v_criteria = criteres.ToArray();
        }

        /// <summary>
        /// Constructeur pour une recherche
        /// </summary>
        public Filtre(String recherche)
        {
            this.v_type = 2;
            this.v_nomFiltre = recherche;
        }

        public DataTable getActions()
        {
            // Stockage du filtre
            CurrentFilter = this;

            DataTable data = new DataTable();

            switch (this.type)
            {
                case (1):
                    data = ReadDB.Instance.getActions(this.criteria);
                    break;
                case (2):
                    data = ReadDB.Instance.searchActions(this.nom);
                    break;
            }

            return data;
        }
    
    }
}
