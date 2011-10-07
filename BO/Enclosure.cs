using System;
using System.Drawing;
using TaskLeader.DAL;

namespace TaskLeader.BO
{
    public abstract class Enclosure
    {
        // ID de la pj
        private String v_id = "";
        public String ID { get { return v_id; } }

        // Titre de la pièce jointe
        private String v_titre = "";
        public String Titre { get { return v_titre; } set { v_titre = value; } }

        // Icône de la pièce jointe
        private Bitmap v_icone;
        public Image Icone { get { return v_icone; } }
        public String IconeKey { get { return type_string; } }

        // Type de la pièce jointe
        private String type_string = "";
        public String Type { get { return type_string; } }

        // Constructeur
        public Enclosure(String id, String titre, String type)
        {
            v_id = id;
            type_string = type;
            v_titre = titre;

            switch (type)
            {
                case "Mails":
                    v_icone = TaskLeader.Properties.Resources.outlook;
                    break;
				case "Links":
					v_icone = TaskLeader.Properties.Resources.shortcut;
                    break;
            }          
        }

        // Méthode obligatoire permettant d'ouvrir le lien
        public abstract void open();
    }
}
