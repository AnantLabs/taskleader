using System;
using System.Drawing;
using System.Windows.Forms;

namespace TaskLeader.GUI
{
    // Interface pour le contenu du ComplexToolTip
    public interface IComplexToolTipContent
    {
        event EventHandler ClosureRequested; // Evénement levé quand le contenu demande la fermeture du tooltip
        Control control {get;} // Object Control du contenu
    }

    public class ComplexTooltip
    {
        // Déclaration du ToolStripDown sous-jacent
        private ToolStripDropDown popup = new ToolStripDropDown();

        /// <summary>
        /// Affichage d'un tooltip élaboré
        /// </summary>
        /// <param name="content">Contenu du tooltip</param>
        public ComplexTooltip(IComplexToolTipContent content)
        {
            // Changement de la police du contrôle pour permettre un calcul correct de la taille
            content.control.Font = ToolStripDropDown.DefaultFont;

            // ToolStripDown initialisation
            this.popup.Margin = Padding.Empty;
            this.popup.Padding = Padding.Empty;

            // ToolStripItem initialisation
            ToolStripControlHost host = new ToolStripControlHost(content.control);
            host.Margin = Padding.Empty;
            host.Padding = Padding.Empty;
            this.popup.Items.Add(host);

            // Closure request treatment
            content.ClosureRequested += new EventHandler(content_ClosureRequested);
        }

        // Affichage de la pop-up
        public void Show()
        {
            this.popup.Show(Cursor.Position);
        }

        // Fermeture de la pop-up
        private void content_ClosureRequested(object sender, EventArgs e)
        {
            this.popup.Close();
        }
    }

    /// <summary>
    /// Le constructeur de base du Label étant trop pauvre, cette classe factorise certains attributs
    /// </summary>
    public class SimpleLabel : Label
    {
        public SimpleLabel(String texte, FontStyle style = FontStyle.Regular)
            : base()
        {
            this.AutoSize = true;
            this.Text = texte;
            this.Font = new Font(this.Font, style);
            this.Margin = new Padding(0, 3, 0, 0);
            this.BackColor = Color.Transparent;
        }
    }
}
