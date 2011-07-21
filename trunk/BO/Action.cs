using System;
using System.Collections;
using System.Data;
using TaskLeader.DAL;

namespace TaskLeader.BO
{
    public class TLaction
    {
        // Méthode privée pour fabriquer des string compatible sql
        private String sqlFactory(String original) { return "'" + original.Replace("'", "''") + "'"; }

        // Membre privé permettant de détecter des updates
        private bool initialStateFrozen = false;
        public void freezeInitState() { this.initialStateFrozen = true; }

        // ID de l'action dans la base TaskLeader
        private String v_TLID = "";
        public String ID { get { return v_TLID; } set { v_TLID = value; } }
        public bool isScratchpad { get { return (v_TLID == ""); } }

        // Contexte de l'action
        private String v_ctxt = ReadDB.Instance.getDefault(DB.Instance.contexte);
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
        private String v_sujt = ReadDB.Instance.getDefault(DB.Instance.sujet);
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
        private String v_dest = ReadDB.Instance.getDefault(DB.Instance.destinataire);
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
        private String v_stat = ReadDB.Instance.getDefault(DB.Instance.statut); // Le statut est initialisé avec la valeur par défaut
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
        public void addLink(Enclosure link) { v_links.Add(link); }
        public bool hasLinks { get { return (v_links.Count > 0); } }
        public Array Links { get { return v_links.ToArray(); } }

        /// <summary>
        /// Constructeur permettant d'initialiser les valeurs par défaut
        /// </summary>
        public TLaction(){}
		
		/// <summary>
        /// Constructeur à partir de l'ID de stockage de l'action
        /// </summary>
        // Constructeur permettant de créer une action à partir de son ID
        public TLaction(String ID)
		{
			this.v_TLID = ID;
		
			//Récupération des données de l'action
			DataRow data = ReadDB.Instance.getAction(ID);
			
			this.v_ctxt = data["Contexte"] as String;
			this.v_sujt = data["Sujet"] as String;
			this.v_texte = data["Titre"] as String;
			this.parseDueDate(data["Deadline"] as String);
			this.v_dest = data["Destinataire"] as String;
			this.v_stat = data["Statut"] as String;
			
			//Récupération des liens
			v_links.AddRange(ReadDB.Instance.getLinks(ID));
		}

        // Méthode permettant d'updater les champs principaux
        public void updateDefault(String contexte, String subject, String desAction, String destinataire,  String stat)
        {
			// Utilisation volontaire des attributs publics pour détecter les changements
            this.Contexte = contexte;
            this.Sujet = subject;
            this.Texte = desAction;
            this.Destinataire = destinataire;
            this.Statut = stat;       
        }
    }
}
