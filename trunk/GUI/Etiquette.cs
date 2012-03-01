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
                    searchedText.Text = filtre.nom;
                    break;

                case (1):

                    // Affichage de l'étiquette correspondant au filtre
                    typeLabel.Text = "Filtre:";
                    if (filtre.nom != "")
                        searchedText.Text = filtre.ToString();
                    else
                        searchedText.Text = "manuel";
                    break;
            }
        }

        /// <summary>
        /// Suppression du contrôle du contrôle parent quand l'étiquette est fermée
        /// </summary>
        private void exitSearchBut_Click(object sender, System.EventArgs e)
        {
            this.Parent.Controls.Remove(this);
        }
    }
}
