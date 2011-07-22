using System;
using System.Windows.Forms;
using System.Configuration;
using TaskLeader.DAL;
using TaskLeader.BLL;
using TaskLeader.BO;
using System.Data;

namespace TaskLeader.GUI
{
    public partial class ManipAction : Form
    {
        private TLaction v_action;

        // Préparation des widgets
        private void loadWidgets()
        {
            //Ajout des contextes à la combobox
            foreach (String item in ReadDB.Instance.getFiltres(DB.Instance.contexte))
                contexteBox.Items.Add(item);

            // Ajout des destinataires à la combobox
            foreach (String item in ReadDB.Instance.getTitres(DB.Instance.destinataire))
                destBox.Items.Add(item);

            // On remplit la liste des statuts
            foreach (String item in ReadDB.Instance.getTitres(DB.Instance.statut))
                statutBox.Items.Add(item);
        }

        public ManipAction(TLaction action)
        {
            InitializeComponent();

            // On mémorise l'action
            this.v_action = action;
            // Chargement des widgets
            this.loadWidgets();

            Array links = action.Links;

            if (action.isScratchpad)
                this.Text += "Ajouter une action";
            else
            {
                this.Text += "Modifier une action";

                if (action.hasDueDate) // Attribut géré à part car pas de valeur par défaut
                    actionDatePicker.Value = action.DueDate;
                else
                    noDueDate.Checked = true;
            }

            destBox.Text = action.Destinataire;

            contexteBox.Text = action.Contexte;

            sujetBox.Text = action.Sujet;
            if(contexteBox.Text != "")
                updateSujet(); // Mise à jour de la liste des sujets

            // Affichage des liens le cas échéant
            if (links.Length > 0)
            {
                ListViewItem linkItem;

                foreach (Enclosure link in links)
                {
                    // Ajout de l'image correspondant au lien dans la bibliothèque
                    this.images.Images.Clear();
                    this.images.Images.Add(link.Icone);

                    // Ajout du lien à la ListView
                    linkItem = new ListViewItem(link.Titre, 0);
                    linkItem.Tag = link;

                    // Ajout du lien à la listView
                    linksView.Items.Add(linkItem);
                }

                // Affichage de la ListView
                this.linksLabel.Visible = true;
                this.linksView.Visible = true;
            }

            // Affichage du descriptif de l'action
            desField.Text = action.Texte;
            desField.Select(desField.Text.Length, 0); // Curseur placé à la fin par défaut

            // Sélection du statut
            statutBox.SelectedItem = action.Statut;
        }

        // Mise à jour de la combobox présentant les sujets
        private void updateSujet()
        {
            // On vide les sujets correspondants au contexte actuel
            sujetBox.Items.Clear();

            foreach (String item in ReadDB.Instance.getSujet(contexteBox.Text))
                sujetBox.Items.Add(item);
        }

        // Sauvegarde de l'action
        private void sauveAction(object sender, EventArgs e)
        {
            //TODO: griser le bouton Sauvegarder si rien n'a été édité

            // Update de l'action avec les nouveaux champs
            v_action.updateDefault(contexteBox.Text, sujetBox.Text, desField.Text, destBox.Text, statutBox.Text);

            // Update de la DueDate que si c'est nécessaire
            if (noDueDate.Checked)
                v_action.DueDate = DateTime.MinValue; // Remise à zéro de la dueDate
            else
                v_action.DueDate = actionDatePicker.Value;

            // On sauvegarde l'action
            DataManager.Instance.saveAction(v_action);

            // Fermeture de la fenêtre
            this.Close();
        }

        // Demande de mise à jour des sujets quand le contexte sélectionné change
        private void contexteBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateSujet();
        }

        // Mise à jour du widget date en fonction de la sélection de la checkbox
        private void dateChosen_CheckedChanged(object sender, EventArgs e)
        {
            actionDatePicker.Enabled = !noDueDate.Checked;
        }

        private void link_Click(object sender, EventArgs e)
        {
            // On ouvre le lien
            ((Enclosure)linksView.SelectedItems[0].Tag).open();
        }
    }
}
