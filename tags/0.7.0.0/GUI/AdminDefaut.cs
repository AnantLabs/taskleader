using System;
using System.Windows.Forms;
using System.Collections;
using TaskLeader.DAL;

namespace TaskLeader.GUI
{
    public partial class AdminDefaut : Form
    {
        String empty = "-- Aucun --";

        public AdminDefaut()
        {
            InitializeComponent();
        }

        private void AdminDefaut_Load(object sender, EventArgs e)
        {        
            //Remplissage des combos
            ctxtListBox.Items.Add(empty);
            ctxtListBox.Items.AddRange(ReadDB.Instance.getTitres(DB.Instance.contexte));
            destListBox.Items.Add(empty);
            destListBox.Items.AddRange(ReadDB.Instance.getTitres(DB.Instance.destinataire));
            statutListBox.Items.Add(empty);
            statutListBox.Items.AddRange(ReadDB.Instance.getTitres(DB.Instance.statut));
            filterCombo.Items.Add(empty);
            filterCombo.Items.AddRange(ReadDB.Instance.getTitres(DB.Instance.filtre));

            //Sélection des valeurs par défaut

            ctxtListBox.Text = ReadDB.Instance.getDefault(DB.Instance.contexte);
            if (ctxtListBox.Text == "")
                ctxtListBox.SelectedIndex = 0; // Sélection de la ligne "Aucun"

            this.updateSujet(sender, e);

            destListBox.Text = ReadDB.Instance.getDefault(DB.Instance.destinataire);
            if (destListBox.Text == "")
                destListBox.SelectedIndex = 0;

            statutListBox.Text = ReadDB.Instance.getDefault(DB.Instance.statut);
            if (statutListBox.Text == "")
                statutListBox.SelectedIndex = 0;

            filterCombo.Text = ReadDB.Instance.getDefault(DB.Instance.filtre);
            if (filterCombo.Text == "")
                filterCombo.SelectedIndex = 0;
        }

        private void updateSujet(object sender, EventArgs e)
        {
            // Remise à zéro de la liste
            sujetListBox.Items.Clear();
            sujetListBox.Items.Add(empty);
            sujetListBox.Enabled = true;

            if (ctxtListBox.SelectedIndex > 0) // Uniquement si contexte différent de "Aucun"
            {
                // Remplissage de la liste
                sujetListBox.Items.AddRange(ReadDB.Instance.getSujets(ctxtListBox.Text));

                // Sélection du sujet par défaut
                sujetListBox.Text = ReadDB.Instance.getDefault(DB.Instance.sujet);
                if (sujetListBox.Text == "")
                    sujetListBox.SelectedIndex = 0;
            }
            else
                sujetListBox.Enabled = false;
        }

        private void saveBut_Click(object sender, EventArgs e)
        {
            // Récupération de la liste des valeurs mise à jour
            ArrayList updatedValues = new ArrayList();

            if (ctxtListBox.SelectedIndex > 0)
                updatedValues.Add(new DBvalue(DB.Instance.contexte, ctxtListBox.Text));

            if (sujetListBox.SelectedIndex > 0)
                updatedValues.Add(new DBvalue(DB.Instance.sujet, sujetListBox.Text));

            if (destListBox.SelectedIndex > 0)
                updatedValues.Add(new DBvalue(DB.Instance.destinataire, destListBox.Text));

            if (statutListBox.SelectedIndex > 0)
                updatedValues.Add(new DBvalue(DB.Instance.statut, statutListBox.Text));

            if (filterCombo.SelectedIndex > 0)
                updatedValues.Add(new DBvalue(DB.Instance.filtre, filterCombo.Text));

            // Sauvegarde
            WriteDB.Instance.insertDefaut(updatedValues.ToArray());
            // On affiche un message de statut sur la TrayIcon
            TrayIcon.afficheMessage("Bilan création/modification", "Valeurs par défaut mises à jour");

            //Fermeture de la Form
            this.Close();
        }
    }
}
