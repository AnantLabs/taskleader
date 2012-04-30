using System.Windows.Forms;
using TaskLeader.BO;

namespace TaskLeader.GUI
{
    public partial class Etiquette : UserControl
    {
        private Filtre v_filtre;
        public Filtre filtre { get { return v_filtre; } }

        public Etiquette()
        {
            InitializeComponent();
        }

        public Etiquette(Filtre filtre)
        {
            InitializeComponent();

            this.v_filtre = filtre;

            switch (filtre.type)
            {
                case (2): // C'est une recherche

                    // Affichage de l'étiquette correspondant à la recherche
                    typeLabel.Text = "Recherche:";
                    valeurLabel.Text = "'" + filtre.nom + "' [" + filtre.dbName + "]";
                    break;

                case (1):

                    // Affichage de l'étiquette correspondant au filtre
                    typeLabel.Text = "Filtre:";
                    if (filtre.nom != "")
                        valeurLabel.Text = filtre.ToString() + " [" + filtre.dbName + "]";
                    else
                        valeurLabel.Text = "manuel [" + filtre.dbName + "]";
                    break;
            }
        }

        /// <summary>
        /// Suppression de l'étiquette du contrôle parent quand l'étiquette est fermée
        /// </summary>
        private void exitSearchBut_Click(object sender, System.EventArgs e)
        {
            this.Parent.Controls.Remove(this);
        }
        
        /// <summary>
        ///  Affichage d'un panel décrivant le filtre
        /// </summary>
        private void infoBox_Click(object sender, System.EventArgs e)
        {
            new ComplexTooltip(
                new EditFilter(this.filtre)
            ).Show();
        }
    }
}
