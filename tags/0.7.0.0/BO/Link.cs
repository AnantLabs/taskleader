using System;
using System.Data;
using TaskLeader.DAL;
using System.Diagnostics;
using TaskLeader.GUI;

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
            this.v_link = linkData["Path"].ToString();
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
            try
            {
                Process.Start(@v_link); // Ouverture du lien avec le programme par défaut
            }
            catch (Exception e)
            {
                TrayIcon.afficheMessage("Erreur d'ouverture", e.Message);
            }     
        }

        // Stockage du mail
        public override String store()
        {
            return WriteDB.Instance.insertLink(this);
        }
    }
}
