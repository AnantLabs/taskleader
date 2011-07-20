using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TaskLeader.BO
{
    public abstract class Enclosure
    {
        // Titre de la pièce jointe
        private String v_titre = "";
        public String Titre { get { return v_titre; } set { v_titre = value; } }

        // Type de la pièce jointe
        private String v_type;
        public int Type
        {
            get
            {
                switch (v_type)
                {
                    case "Mails":
                        return 0;
                    default:
                        return -1; // Impossile de déterminer le type du lien
                }
            }
        }
        public String TypeSQL { get { return "'" + v_type + "'"; } }

        // Constructeur
        public Enclosure(String titre, String type)
        {
            v_titre = titre;
            v_type = type;
        }

        // Méthode obligatoire permettant d'ouvrir le lien
        public abstract void open();

        // Méthode obligatoire permettant de stocker le lien
        // Retourne l'ID de stockage
        public abstract String store();
    }
}
