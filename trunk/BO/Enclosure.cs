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
        private Bitmap _iconeBig;
        private Bitmap _iconeSmall;
        public Image BigIcon { get { return _iconeBig; } }
        public Image SmallIcon { get { return _iconeSmall; } }
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
                    _iconeBig = TaskLeader.Properties.Resources.outlook;
                    _iconeSmall = TaskLeader.Properties.Resources.outlook;
                    break;
				case "Links":
					_iconeBig = TaskLeader.Properties.Resources.link32;
                    _iconeSmall = TaskLeader.Properties.Resources.link;
                    break;
            }          
        }

        // Méthode obligatoire permettant d'ouvrir le lien
        public abstract void open();
    }
}
