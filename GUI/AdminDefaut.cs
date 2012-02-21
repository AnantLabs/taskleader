using System;
using System.Configuration;
using System.Windows.Forms;
using System.Collections;
using TaskLeader.DAL;

namespace TaskLeader.GUI
{
    public partial class AdminDefaut : Form
    {
        String empty = "-- Aucun --";

        // Variables locales identifiant la base courante
        private String dbName;
        private DB db { get { return TrayIcon.dbs[dbName]; } }

        public AdminDefaut(String database)
        {
            InitializeComponent();
            this.dbName = database;
        }

        private void AdminDefaut_Load(object sender, EventArgs e)
        {        
            //Remplissage des combos
            ctxtListBox.Items.Add(empty);
            ctxtListBox.Items.AddRange(db.getTitres(DB.contexte));
            destListBox.Items.Add(empty);
            destListBox.Items.AddRange(db.getTitres(DB.destinataire));
            statutListBox.Items.Add(empty);
            statutListBox.Items.AddRange(db.getTitres(DB.statut));
            filterCombo.Items.Add(empty);
            filterCombo.Items.AddRange(db.getTitres(DB.filtre));

            //Sélection des valeurs par défaut

            ctxtListBox.Text = db.getDefault(DB.contexte);
            if (ctxtListBox.Text == "")
                ctxtListBox.SelectedIndex = 0; // Sélection de la ligne "Aucun"

            this.updateSujet(sender, e);

            destListBox.Text = db.getDefault(DB.destinataire);
            if (destListBox.Text == "")
                destListBox.SelectedIndex = 0;

            statutListBox.Text = db.getDefault(DB.statut);
            if (statutListBox.Text == "")
                statutListBox.SelectedIndex = 0;

            filterCombo.Text = db.getDefault(DB.filtre);
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
                sujetListBox.Items.AddRange(db.getSujets(ctxtListBox.Text));

                // Sélection du sujet par défaut
                sujetListBox.Text = db.getDefault(DB.sujet);
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
                updatedValues.Add(new DBvalue(DB.contexte, ctxtListBox.Text));

            if (sujetListBox.SelectedIndex > 0)
                updatedValues.Add(new DBvalue(DB.sujet, sujetListBox.Text));

            if (destListBox.SelectedIndex > 0)
                updatedValues.Add(new DBvalue(DB.destinataire, destListBox.Text));

            if (statutListBox.SelectedIndex > 0)
                updatedValues.Add(new DBvalue(DB.statut, statutListBox.Text));

            if (filterCombo.SelectedIndex > 0)
                updatedValues.Add(new DBvalue(DB.filtre, filterCombo.Text));

            // Sauvegarde
            db.insertDefaut(updatedValues.ToArray());
            // On affiche un message de statut sur la TrayIcon
            TrayIcon.afficheMessage("Bilan création/modification", "Valeurs par défaut mises à jour");

            //Fermeture de la Form
            this.Close();
        }
    }
}
