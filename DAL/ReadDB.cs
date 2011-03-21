 using System;
using System.Collections;
using System.Data;
using System.Data.SQLite;
using System.Windows.Forms;
using TaskLeader.BO;

namespace TaskLeader.DAL
{
    public class ReadDB
    {
        // Variable locale pour stocker une référence vers l'instance
        private static ReadDB instance = null;

        // Renvoie l'instance ou la crée
        public static ReadDB Instance
        {
            get
            {
                // Si pas d'instance existante on en crée une...
                if (instance == null)
                    instance = new ReadDB();

                // On retourne l'instance de MonSingleton
                return instance;
            }
        }      

        // Méthode générique pour récupérer une seule colonne
        private object[] getList(String requete)
        {
            // Si le mode debug est activé, on affiche les requêtes
            if(System.Configuration.ConfigurationManager.AppSettings["debugMode"] == "true")
                MessageBox.Show(requete, "Requête", MessageBoxButtons.OK, MessageBoxIcon.Information);

            ArrayList liste = new ArrayList();

            try
            {
                using (SQLiteCommand SQLCmd = new SQLiteCommand(ConnexionDB.Instance.getConnection()))
                {
                    // Création d'une nouvelle commande à partir de la connexion
                    SQLCmd.CommandText = requete;

                    using (SQLiteDataReader SQLDReader = SQLCmd.ExecuteReader())
                    {
                        // La méthode Read() lit l'entrée actuelle puis renvoie true tant qu'il y a des entrées à lire.
                        while (SQLDReader.Read())
                            liste.Add(SQLDReader[0]); // On retourne la seule et unique colonne
                    }
                }
            }
            catch (Exception Ex)
            {
                // On affiche l'erreur.
                MessageBox.Show(Ex.Message, "Exception sur getList", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return liste.ToArray();
        }

        // Méthode générique pour récupérer une table
        private DataTable getTable(String requete)
        {
            // Si le mode debug est activé, on affiche les requêtes
            if (System.Configuration.ConfigurationManager.AppSettings["debugMode"] == "true")
                MessageBox.Show(requete, "Requête", MessageBoxButtons.OK, MessageBoxIcon.Information);

            // Création de la DataTable
            DataTable data = new DataTable();

            try
            {
                using (SQLiteDataAdapter SQLAdap = new SQLiteDataAdapter(requete, ConnexionDB.Instance.getConnection()))
                {
                    // Remplissage avec les données de l'adaptateur
                    SQLAdap.Fill(data);
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Exception sur getTable", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }

            return data;
        }

        // Méthode générique pour récupérer un entier
        private int getInteger(String requete)
        {
            // Si le mode debug est activé, on affiche les requêtes
            if (System.Configuration.ConfigurationManager.AppSettings["debugMode"] == "true")
                MessageBox.Show(requete, "Requête", MessageBoxButtons.OK, MessageBoxIcon.Information);

            try
            {
                using (SQLiteCommand SQLCmd = new SQLiteCommand(ConnexionDB.Instance.getConnection()))
                {
                    SQLCmd.CommandText = requete;
                    return Convert.ToInt32(SQLCmd.ExecuteScalar());
                }
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Exception sur getInteger", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return -1;
            }
        }

        // Vérification si un numéro de version est présent dans la table des compatibilités
        public bool isVersionComp(String version)
        {
            // Si une ligne matche la base est compatible
            return (getInteger("SELECT count(rowid) FROM VerComp WHERE Num='" + version + "'") == 1); 
        }

        // On vérifie la version la plus haute compatible avec cette base
        public String getLastVerComp()
        {
            return (String)getList("SELECT Num FROM VerComp WHERE rowid=(SELECT max(rowid) FROM VerComp)")[0];
        }

        // Renvoie un tableau de tous les contextes présents en base
        public object[] getCtxt()
        {
            return getList("SELECT Titre FROM Contextes");
        }

        // Renvoie un tableau de tous les sujets correspondant au contexte
        public object[] getSujet(String contexte)
        {
            return getList("SELECT Titre FROM VueSujets WHERE Contexte ='" + contexte + "'");
        }
        
        // Renvoie un tableau de tous les destinataires présents dans la base
        public object[] getDest()
        {
            return getList("SELECT Nom FROM Destinataires ORDER BY Nom ASC"); // On trie les noms dans l'ordre alphabétique
        }

        // Renvoie un tableau de tous les statuts présents dans la base
        public object[] getStatut()
        {
            return getList("SELECT Titre FROM Statuts");
        }

        // Renvoie le nom du statut par défaut
        public String getDefaultStatus()
        {
            return (String)getList("SELECT Titre FROM Statuts WHERE Defaut='1'")[0]; // Il n'y a qu'un seul statut par défaut
        }

        // Renvoie un tableau de tous les filtres présents en base
        public object[] getFiltersName()
        {
            return getList("SELECT Titre FROM Filtres ORDER BY Titre ASC"); // On trie dans l'ordre alphabétique
        }
       
        // Renvoie un DataTable de toutes les actions
        public DataTable getActions(Object[] criteria)
        {
            // Création de la requête de filtrage
            String requete = "SELECT id, Contexte, Sujet, Titre, Deadline, Destinataire, Statut FROM VueActions";

            String selection, nomColonne;

            if (criteria.Length > 0) // Il n'y a de WHERE que si au moins un criterium a été entré
            {
                requete += " WHERE ";

                foreach (Criterium critere in criteria) // On boucle sur tous les critères du filtre
                {
                    // On récupère le nom de la colonne correspondant au critère
                    nomColonne = ConnexionDB.Instance.schema[critere.champ, 0];

                    if (critere.selected.Count > 0) // Requête SQL si au moins un élément a été sélectionné
                    {
                        requete += nomColonne + " IN (";
                        foreach (String item in critere.selected)
                        {
                            selection = item.Replace("'", "''"); // On gère les simple quote
                            requete += "'" + selection + "', ";
                        }
                        requete = requete.Substring(0, requete.Length - 2); // On efface la dernière virgule et le dernier space en trop
                        requete += ")";
                    }
                    else
                        requete += nomColonne + " IS NULL";

                    requete += " AND ";
                }

                requete = requete.Substring(0, requete.Length - 5); // On enlève le dernier AND en trop
            }   
                          
            return getTable(requete);
        }

        // Récupère un filtre en fonction de son titre
        public Filtre getFilter(String name){

            // On récupère d'abord les checkbox all
            String titre = "'" + name.Replace("'", "''") + "'";
            String requete = "SELECT AllCtxt, AllSuj, AllDest, AllStat FROM Filtres WHERE Titre=" + titre;
            DataTable resultat = getTable(requete);
            
            // On crée le filtre correspondant
            Filtre filtre = new Filtre(resultat);
            object[] liste;

            // On récupère les sélections si nécessaire
            foreach (Criterium critere in filtre.criteria)
            {
                // Création de la requête
                requete = "SELECT TP."+ConnexionDB.Instance.schema[critere.champ, 2]+" FROM " + ConnexionDB.Instance.schema[critere.champ, 1]+" TP, "+ConnexionDB.Instance.schema[critere.champ, 3] + " TF, Filtres F ";
                requete += "WHERE F.Titre =" + titre + " AND TF.FiltreID=F.rowid AND TF.SelectedID=TP.rowid";
                // Récupération de la liste
                liste = getList(requete);

                // On met à jour le critère du filtre correspondant
                foreach(object item in liste)
                    critere.selected.Add(item);            
            }

            return filtre;
        }

        // Vérification de l'existence du nom du filtre
        public bool isNvoFiltre(String nom)
        {
            String name = "'" + nom.Replace("'", "''") + "'";

            // On compte le nombre d'occurences de ce filtre dans la table
            String requete = "SELECT count(rowid) FROM Filtres WHERE Titre=" + name;

            return (getInteger(requete) == 0);
        }

        // Vérification de la présence d'un nouveau contexte
        public bool isNvoContexte(String contexte)
        {
            String ctxt = "'" + contexte.Replace("'", "''") + "'"; // C'est spécifique SQL donc dans la DAL

            // On compte le nombre d'occurences de ce contexte dans la table
            String requete = "SELECT count(rowid) FROM Contextes WHERE Titre=" + ctxt;

            if (this.getInteger(requete)==0)
                return true;
            else
                return false;
        }
    
        // Vérification de la présence d'un nouveau sujet
        public bool isNvoSujet(String contexte, String subject)
        {
            String ctxt = "'" + contexte.Replace("'", "''") + "'";
            String sujet = "'" + subject.Replace("'", "''") + "'";

            String requete = "SELECT count(Titre) FROM VueSujets WHERE Contexte = " + ctxt + " AND Titre = " + sujet;

            if (this.getInteger(requete) == 0)
                return true;
            else
                return false;
        }

        // Vérification de la présence d'un nouveau destinataire
        public bool isNvoDest(String destinataire)
        {
            String dest = "'" + destinataire.Replace("'", "''") + "'";

            String requete = "SELECT count(Nom) FROM Destinataires WHERE Nom=" + dest;

            if (this.getInteger(requete) == 0)
                return true;
            else
                return false;
        }
        
        // Récupération des informations d'un mail à partir de son ID
        public DataTable getMailData(String id)
        {
            String requete = "SELECT * FROM Mails WHERE rowid='"+id+"'";
            return getTable(requete);
        }
    }
}
