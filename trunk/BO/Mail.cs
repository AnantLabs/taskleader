using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TaskLeader.DAL;
using TaskLeader.BLL;

namespace TaskLeader.BO
{
    public class Mail : Enclosure
    {
        // Méthode privée pour fabriquer des string compatible sql
        private String sqlFactory(String original) { return "'" + original.Replace("'", "''") + "'"; }

        // storeID du dossier contenant le mail
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

        /// <summary>
        /// Création d'un objet Mail à partir de son ID de stockage
        /// </summary>
        public Mail(String ID):base("","Mails")
        {
            DataRow mailData = ReadDB.Instance.getMailData(ID);

            base.Titre = mailData["Titre"].ToString();
            this.v_storeID = mailData["StoreID"].ToString();
            this.v_entryID = mailData["EntryID"].ToString();
            this.v_messageID = mailData["MessageID"].ToString();
        }

        /// <summary>
        /// Création d'un objet Mail à partir des différents ID
        /// </summary>
        public Mail(String titre,String storeID, String entryID, String messageID):base(titre,"Mails")
        {
            this.v_storeID = storeID;
            this.v_entryID = entryID;
            this.v_messageID = messageID;
        }

        // Ouverture du mail
        public override void open()
        {
            OutlookIF.Instance.displayMail(this);
        }

        // Stockage du mail
        public override String store()
        {
            return WriteDB.Instance.insertMail(this);
        }
    }
}
