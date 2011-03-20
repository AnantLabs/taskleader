using System;
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
        private String v_ctxt = "";
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
        private String v_sujt = "";
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
        private String v_dest = "";
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
        private String v_stat = ReadDB.Instance.getDefaultStatus(); // Le statut est initialisé avec la valeur par défaut
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

        // booléen pour savoir si l'action est reliée à un mail
        public bool hasMailAttached
        {
            get
            {
                if (v_entryID != "" && v_storeID != "" && v_messageID != "")
                    return true;
                else
                    return false;
            } 
        }

        // storeID du dossier contenant le mail si relié à l'action
        private String v_storeID = "";
        public String StoreID { get { return v_storeID; } set { v_storeID = value; } }
        public String StoreIDSQL { get { return sqlFactory(v_storeID); } } 

        // entryID du mail si relié à l'action
        private String v_entryID = "";
        public String EntryID { get { return v_entryID; } set { v_entryID = value; } }
        public String EntryIDSQL { get { return sqlFactory(v_entryID); } } 

        // messageID du mail si relié à l'action
        private String v_messageID = "";
        public String MessageID { get { return v_messageID; } set { v_messageID = value; } }
        public String MessageIDSQL { get { return sqlFactory(v_messageID); } } 

        //Constructeur simple
        public TLaction(String sujet)
        {
            this.v_texte = sujet;
        }

        // Constructeur permettant d'initialiser les valeurs par défaut
        public TLaction(){}

        // Méthode permettant d'updater les champs principaux
        public void updateDefault(String contexte, String subject, String desAction, String destinataire,  String stat)
        {
            this.Contexte = contexte;
            this.Sujet = subject;
            this.Texte = desAction;
            this.Destinataire = destinataire;
            this.Statut = stat;       
        }
    }
}
