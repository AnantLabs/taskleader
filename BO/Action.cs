using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskLeader.BO
{
    public class TLaction
    {
        // Méthode privée pour fabriquer des string compatible sql
        private String sqlFactory(String original) { return "'" + original.Replace("'", "''") + "'"; }

        // ID de l'action dans la base TaskLeader
        private int v_TLID = 0;
        public int ID { get { return v_TLID; } set { v_TLID = value; } }
        public bool isScratchpad { get { return (v_TLID == 0); } }

        // Contexte de l'action
        private String v_ctxt = "";
        public String Contexte { get { return v_ctxt; } set { v_ctxt = value; } }
        public String ContexteSQL { get { return sqlFactory(v_ctxt); } }        

        // Sujet de l'action
        private String v_sujt = "";
        public String Sujet { get { return v_sujt; } set { v_sujt = value; } }
        public String SujetSQL { get { return sqlFactory(v_sujt); } }    

        // Libéllé de l'action
        private String v_texte = "";
        public String Texte { get { return v_texte; } set { v_texte = value; } }
        public String TexteSQL { get { return sqlFactory(v_texte); } }    

        // DueDate de l'action
        private DateTime v_dueDate = new DateTime(); //Affectation de la valeur MinValue
        public bool hasDueDate { get { return (v_dueDate != DateTime.MinValue); } }
        public DateTime DueDate { get { return v_dueDate; } set { v_dueDate = value; } }
        public void parseDueDate(String date){ DateTime.TryParse(date,out v_dueDate); }
        public String DueDateSQL { get { return "'"+v_dueDate.ToString("yyyy-MM-dd")+"'"; } }

        // Destinataire de l'action
        private String v_dest = "";
        public String Destinataire { get { return v_dest; } set { v_dest = value; } }
        public String DestinataireSQL { get { return sqlFactory(v_dest); } } 

        // Statut de l'action
        private String v_stat = "";
        public bool statusHasChanged = false;
        public String Statut { get { return v_stat; } set {v_stat = value;} }
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

        // entryID du mail si relié à l'action
        private String v_entryID = "";
        public String EntryID { get { return v_entryID; } set { v_entryID = value; } }

        // messageID du mail si relié à l'action
        private String v_messageID = "";
        public String MessageID { get { return v_messageID; } set { v_messageID = value; } }       

        //Constructeur simple
        public TLaction(String sujet)
        {
            this.v_texte = sujet;
        }

        // Constructeur permettant d'initialiser les valeurs par défaut
        public TLaction() { }

        // Méthode permettant d'updater les champs principaux
        public void updateDefault(String contexte, String subject, String desAction, String destinataire,  String stat)
        {
            v_ctxt = contexte;
            v_sujt = subject;
            v_texte = desAction;
            v_dest = destinataire;
            this.Statut = stat; // On utilise volontairement l'attribut publique pour détecter le chgt
        }
    }
}
