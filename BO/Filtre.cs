using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Data;

namespace ActionsList.BO
{
    public class Criterium
    {
        private int v_champ; //0=contexte, 1=sujet, 2=destinataire, 3=statut
        public int champ { get { return v_champ; } }
 
        private ArrayList v_selected = new ArrayList();
        public ArrayList selected { get { return v_selected; } }

        // Constructeur quand on connaît les critères
        public Criterium(int table, IList criteres)
        {
            this.v_champ = table;
            this.v_selected.AddRange(criteres);
        }

        // Constructeur quand on connaît uniquement les champs
        public Criterium(int table)
        {
            this.v_champ = table;
        }
    }

    public class Filtre
    {
        // Tableau qui donne la liste des critères sélectionnés autre que ALL        
        private Object[] v_criteria;
        public Object[] criteria { get { return v_criteria; } }

        // Variable locale pour stocker une référence vers le filtre en cours
        private static Filtre v_currentFilter = null;

        // Renvoie le filtre courant ou le crée
        public static Filtre CurrentFilter
        {
            get { return v_currentFilter; }
            set { v_currentFilter = value; }
        }
        
        // Constructeur de filtre à partir de la GUI
        public Filtre(bool allCtxt,IList ctxt, bool allSuj, IList suj, bool allDest, IList dest, bool allStat, IList stat)
        {
            ArrayList criteres = new ArrayList();

            if (!allCtxt) // Champ = 0 pour les contextes
                criteres.Add(new Criterium(0,ctxt));

            if (ctxt.Count == 1 && !allSuj) // Champ = 1 pour les sujets
                criteres.Add(new Criterium(1, suj));

            if (!allDest)
                criteres.Add(new Criterium(2, dest));

            if (!allStat)
                criteres.Add(new Criterium(3, stat));

            this.v_criteria = criteres.ToArray();
        }
    
        // Constructeur pour récupération base
        public Filtre(DataTable allSelected)
        {
            ArrayList criteres = new ArrayList();

            // On récupère la ligne de la table
            //Col 0=AllCtxt 1=AllSuj 2=AllDest 3=AllStat
            DataRow allList = allSelected.Rows[0];

            // On boucle sur tous les champs et on remplit 
            for (int i = 0; i < 4; i++)
                if (!(bool)allList[i])
                    criteres.Add(new Criterium(i));

            this.v_criteria = criteres.ToArray();
        }
    }
}
