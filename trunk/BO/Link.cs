using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using TaskLeader.DAL;
using TaskLeader.BLL;
using System.Diagnostics;

namespace TaskLeader.BO
{
    public class Link : Enclosure
    {
        // Méthode privée pour fabriquer des string compatible sql
        private String sqlFactory(String original) { return "'" + original.Replace("'", "''") + "'"; }

        // storeID du dossier contenant le mail
        private String v_link = "";
        public String url { get { return v_link; } set { v_link = value; } }
        public String urlSQL { get { return sqlFactory(v_link); } }

        /// <summary>
        /// Création d'un objet Raccourci à partir de son ID de stockage
        /// </summary>
        public Link(String ID):base("","Links")
        {
            DataRow linkData = ReadDB.Instance.getLinkData(ID);

            base.Titre = linkData["Titre"].ToString();
            this.v_link = mailData["Path"].ToString();
        }

        /// <summary>
        /// Création d'un objet Raccourci à partir de son chemin
        /// </summary>
        public Link(String titre,String path):base(titre,"Links")
        {
            this.v_link = path;
        }

        // Ouverture du mail
        public override void open()
        {
            Process.Start(@v_link); //@ génant pour les url web ?
        }

        // Stockage du mail
        public override String store()
        {
            return WriteDB.Instance.insertLink(this);
        }
    }
}
