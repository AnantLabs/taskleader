using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Configuration;
using System.Windows.Forms;

namespace TaskLeader.BLL
{
    public class Export
    {
        // Variable locale pour stocker une référence vers l'instance
        private static Export v_instance = null;

        // Renvoie l'instance ou la crée
        public static Export Instance
        {
            get
            {
                // Si pas d'instance existante on en crée une...
                if (v_instance == null)
                    v_instance = new Export();

                // On retourne l'instance de MonSingleton
                return v_instance;
            }
        }

        /// <summary>
        /// Export vers presse-papier à partir du template de la clé fournie
        /// </summary>
        public void clipAction(String key,DataGridViewRow data)
        {
            //Récupération des templates d'export
            NameValueCollection section = (NameValueCollection)ConfigurationManager.GetSection("ExportSection");
            String template = section[key];

            // Remplacement des caractères spéciaux
            String resultat = template.Replace("(VIDE)", "");
            resultat = resultat.Replace("(TAB)", "\t");

            // Remplacement du sujet (Attention les sauts de ligne ne sont pas gérés)
            resultat = resultat.Replace("(Sujet)", data.Cells["Sujet"].Value.ToString());

            // Remplacement de l'action en rentrant une formule excel pour les passages à la ligne
            String action = data.Cells["Titre"].Value.ToString().Replace("\"", "\"\"");
            action = action.Replace(Environment.NewLine, "\"&CAR(10)&\""); // Attention compatible avec la version fr de excel seulement
            action = "=\"" + action + "\"";
            resultat = resultat.Replace("(Action)", action);

            // Remplacement du statut, de la due date et de la date courante
            resultat = resultat.Replace("(Statut)", data.Cells["Statut"].Value.ToString());
            resultat = resultat.Replace("(DueDate)", data.Cells["Deadline"].Value.ToString());
            resultat = resultat.Replace("(NOW)", DateTime.Now.ToString("dd-MM-yyyy"));

            //Copie dans le presse-papier
            Clipboard.SetText(resultat);
        }

    }
}
