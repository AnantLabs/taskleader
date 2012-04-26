using System;
using System.Windows.Forms;
using TaskLeader.BO;

namespace TaskLeader.GUI
{
    public partial class DatePickerPopup : UserControl
    {
        // Déclaration du ToolStripDown sous-jacent
        private ToolStripDropDown popup = new ToolStripDropDown();
        public event ToolStripDropDownClosedEventHandler Closed
        {
            add { popup.Closed += value; }
            remove { popup.Closed -= value; }
        }

        // Déclaration de l'action associée
        private TLaction v_action;
        public String ID { get { return v_action.ID; } }

        public DatePickerPopup(TLaction action)
        {
            InitializeComponent();
            v_action = action;

            // Initialisation de la pop-up
            popup.Margin = Padding.Empty;
            popup.Padding = Padding.Empty;
            ToolStripControlHost host = new ToolStripControlHost(this);
            host.Margin = Padding.Empty;
            host.Padding = Padding.Empty;
            popup.Items.Add(host);

            // Initialisation du composant calendar
            if (action.hasDueDate)
                calendar.SelectionStart = action.DueDate;
            else
                this.noDueDate.Checked = true;
        }

        // Affichage de la pop-up
        new public void Show()
        {
            popup.Show(Cursor.Position);
        }

        // Désactivation du calendrier si nécessaire
        private void noDueDate_CheckStateChanged(object sender, System.EventArgs e)
        {
            calendar.Visible = !noDueDate.Checked;
        }

        // Sauvegarde de la nouvelle deadline
        private void validBut_Click(object sender, System.EventArgs e)
        {
            // Update de la DueDate que si c'est nécessaire
            if (noDueDate.Checked)
                v_action.DueDate = DateTime.MinValue; // Remise à zéro de la dueDate
            else
                v_action.DueDate = calendar.SelectionStart;

            // On sauvegarde l'action
            v_action.save();

            // Fermeture de la fenêtre
            this.popup.Close(ToolStripDropDownCloseReason.ItemClicked); //TODO: la raison de fermeture ne sera finalement pas utile
        }

        private void closeBut_Click(object sender, EventArgs e)
        {
            this.popup.Close(ToolStripDropDownCloseReason.CloseCalled); //TODO: la raison de fermeture ne sera finalement pas utile
        }
    }
}
