using System;
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
            // ToolStripDown initialisation
            this.popup.Margin = Padding.Empty;
            this.popup.Padding = Padding.Empty;

            // ToolStripItem initialisation

            ToolStripControlHost host = new ToolStripControlHost(content.control);
            host.AutoSize = true;
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
}
