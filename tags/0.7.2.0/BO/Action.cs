using System;
using System.Configuration;
using System.Collections;
using System.Data;
using TaskLeader.DAL;
using TaskLeader.GUI;

namespace TaskLeader.BO
{
    public class TLaction
    {
        // Méthode privée pour fabriquer des string compatible sql
        private String sqlFactory(String original) { return "'" + original.Replace("'", "''") + "'"; }

        // Membre privé permettant de détecter des updates
        private bool initialStateFrozen = false;

        // DB d'où provient l'action
        public String dbName = TrayIcon.defaultDB.name;
        private DB db { get { return TrayIcon.dbs[this.dbName]; } }

        // ID de l'action dans la base TaskLeader
        private String v_TLID = "";
        public String ID { get { return v_TLID; } }
        public bool isScratchpad { get { return (v_TLID == ""); } }

        // Contexte de l'action
        private String v_ctxt = TrayIcon.defaultDB.getDefault(DB.contexte);
        public bool ctxtHasChanged = false;
        public String Contexte {
            get { return v_ctxt; }
            set {
                if (value != v_ctxt)
                {
                    ctxtHasChanged = this.initialStateFrozen;
                    v_ctxt = value;
                }               
            }
        }
        public String ContexteSQL { get { return sqlFactory(v_ctxt); } }        

        // Sujet de l'action
        private String v_sujt = TrayIcon.defaultDB.getDefault(DB.sujet);
        public bool sujetHasChanged = false;
        public String Sujet
        {
            get { return v_sujt; }
            set {
                if (value != v_sujt)
                {
                    sujetHasChanged = this.initialStateFrozen;
                    v_sujt = value;
                }               
            }
        }
        public String SujetSQL { get { return sqlFactory(v_sujt); } }    

        // Libéllé de l'action
        private String v_texte = "";
        public bool texteHasChanged = false;
        public String Texte
        {
            get { return v_texte; }
            set {
                if (value != v_texte)
                {
                    texteHasChanged = this.initialStateFrozen;
                    v_texte = value;
                }               
            }
        }
        public String TexteSQL { get { return sqlFactory(v_texte); } }    

        // DueDate de l'action
        private DateTime v_dueDate = DateTime.MinValue;
        public bool dueDateHasChanged = false;
        public bool hasDueDate { get { return (v_dueDate != DateTime.MinValue); } }
        public DateTime DueDate
        {
            get { return v_dueDate; }
            set
            {
                if (value != v_dueDate)
                {
                    dueDateHasChanged = this.initialStateFrozen;
                    v_dueDate = value;
                }
            }
        }
        public void parseDueDate(String date){ DateTime.TryParse(date,out v_dueDate); }
        public String DueDateSQL { get { return "'"+v_dueDate.ToString("yyyy-MM-dd")+"'"; } }

        // Destinataire de l'action
        private String v_dest = TrayIcon.defaultDB.getDefault(DB.destinataire);
        public bool destHasChanged = false;
        public String Destinataire
        {
            get { return v_dest; }
            set
            {
                if (value != v_dest)
                {
                    destHasChanged = this.initialStateFrozen;
                    v_dest = value;
                }
            }
        }
        public String DestinataireSQL { get { return sqlFactory(v_dest); } } 

        // Statut de l'action
        private String v_stat = TrayIcon.defaultDB.getDefault(DB.statut); // Le statut est initialisé avec la valeur par défaut
        public bool statusHasChanged = false;
        public String Statut
        {
            get { return v_stat; }
            set
            {
                if (value != v_stat)
                {
                    statusHasChanged = this.initialStateFrozen;
                    v_stat = value;
                }
            }
        }
        public String StatutSQL { get { return sqlFactory(v_stat); } }       

        // PJ à l'action
        private ArrayList v_links = new ArrayList();
        private ArrayList added_links = new ArrayList();
        private ArrayList removed_links = new ArrayList();
        public void addPJ(Enclosure link) { v_links.Add(link); added_links.Add(link); }
        public void removePJ(Enclosure link) { v_links.Remove(link); removed_links.Add(link); }
        public bool hasPJ { get { return (v_links.Count > 0); } }
        public Array PJ { get { return v_links.ToArray(); } }

        /// <summary>
        /// Constructeur permettant d'initialiser les valeurs par défaut
        /// </summary>
        public TLaction() { this.initialStateFrozen = true; }
		
        /// <summary>
        /// Constructeur à partir de l'ID de stockage de l'action
        /// </summary>
        /// <param name="ID">ID dans la base d'actions</param>
        /// <param name="database">Nom de la base d'actions</param>
        public TLaction(String ID, String database)
		{
            this.dbName = database;
			this.v_TLID = ID;
		
			//Récupération des données de l'action
			DataRow data = db.getAction(ID);
			
			this.v_ctxt = data["Contexte"] as String;
			this.v_sujt = data["Sujet"] as String;
			this.v_texte = data["Titre"] as String;
			this.parseDueDate(data["Deadline"] as String);
			this.v_dest = data["Destinataire"] as String;
			this.v_stat = data["Statut"] as String;
			
			//Récupération des liens
			this.v_links.AddRange(db.getPJ(ID));

            this.initialStateFrozen = true;
		}

        /// <summary>
        /// Mise à jour des champs principaux
        /// </summary>
        /// <param name="contexte">Nouveau contexte</param>
        /// <param name="subject">Nouveau sujet</param>
        /// <param name="desAction">Nouvelle description</param>
        /// <param name="destinataire">Nouveau destinataire</param>
        /// <param name="stat">Nouveau statut</param>
        public void updateDefault(String contexte, String subject, String desAction, String destinataire,  String stat)
        {
			// Utilisation volontaire des attributs publics pour détecter les changements
            this.Contexte = contexte;
            this.Sujet = subject;
            this.Texte = desAction;
            this.Destinataire = destinataire;
            this.Statut = stat;       
        }

        /// <summary>
        /// Sauvegarde de l'action dans la base correspondante
        /// </summary>
        public void save()
        {
            String bilan = "";
            int resultat;

            // On rajoute une ligne d'historique si le statut est différent de Ouverte et si le statut a changé
            if (this.Statut != db.getDefault(DB.statut) && this.statusHasChanged)
                this.Texte += Environment.NewLine + "Action " + this.Statut + " le: " + DateTime.Now.ToString("dd-MM-yyyy");

            // Vérification des nouveautés
            if (this.ctxtHasChanged) // Test uniquement si contexte entré
                if (db.isNvo(DB.contexte, this.Contexte)) // Si on a un nouveau contexte
                {
                    resultat = db.insert(DB.contexte,this.Contexte); // On récupère le nombre de lignes insérées
                    if (resultat == 1)
                        bilan += "Nouveau contexte enregistré\n";
                }

            if (this.sujetHasChanged)
                if (db.isNvoSujet(this.Contexte, this.Sujet)) //TODO: il y a un cas foireux si le contexte est vide
                {
                    resultat = db.insertSujet(this.Contexte, this.Sujet);
                    if (resultat == 1)
                        bilan += "Nouveau sujet enregistré\n";
                }

            if (this.destHasChanged)
                if (db.isNvo(DB.destinataire, this.Destinataire))
                {
                    resultat = db.insert(DB.destinataire,this.Destinataire);
                    if (resultat == 1)
                        bilan += "Nouveau destinataire enregistré\n";
                }

            if (this.Texte != "")
            {
                if (this.isScratchpad)
                {
                    this.v_TLID = db.insertAction(this); // Sauvegarde de l'action
                    
                    bilan += "Nouvelle action enregistrée\n";
                    if (this.hasPJ)
                    {
                        db.insertPJ(this.v_TLID, this.PJ); // Sauvegarde des PJ
                        bilan += v_links.Count.ToString()+" PJ enregistrée";
                        if (v_links.Count > 1) bilan += "s";
                        bilan += "\n";
                    }
                }
                else
                {
                    resultat = db.updateAction(this);
                    if (resultat == 1)
                        bilan += "Action mise à jour\n";

                    // Insertion des pj
                    int nbAdded = this.added_links.Count;
                    if (nbAdded > 0)
                    {
                        db.insertPJ(this.v_TLID, this.added_links.ToArray()); // Sauvegarde des PJ
                        bilan += nbAdded.ToString() + " PJ enregistrée"; // Préparation du bilan
                        if (nbAdded > 1) bilan += "s";
                        bilan += "\n";
                    }

                    // Suppression des pj
                    int nbSupp = this.removed_links.Count;
                    if (nbSupp > 0)
                    {
                        db.removePJ(this.removed_links.ToArray());
                        bilan += nbSupp.ToString() + " PJ supprimée"; // Préparation du bilan
                        if (nbSupp > 1) bilan += "s";
                        bilan += "\n";
                    }
                }
            }

            // On affiche un message de statut sur la TrayIcon
            if (bilan.Length > 0) // On n'affiche un bilan que s'il s'est passé qqchose
                TrayIcon.afficheMessage("Bilan sauvegarde", bilan.Substring(0, bilan.Length - 1)); // On supprime le dernier \n            
        }

        /// <summary>
        /// Change la base correspondant à l'action scratchpad
        /// </summary>
        /// <param name="nomDB">Nom de la nouvelle base</param>
        public void changeDB(String nomDB)
        {
            // Changement du nom de la base
            this.dbName = nomDB;

            // Changement des valeurs par défaut
            this.v_ctxt = TrayIcon.dbs[dbName].getDefault(DB.contexte);
            this.v_sujt = TrayIcon.dbs[dbName].getDefault(DB.sujet);
            this.v_dest = TrayIcon.dbs[dbName].getDefault(DB.destinataire);
            this.v_stat = TrayIcon.dbs[dbName].getDefault(DB.statut);
        }
    }
}
