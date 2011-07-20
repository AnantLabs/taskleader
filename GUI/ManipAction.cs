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
            foreach (String item in ReadDB.Instance.getCtxt())
                contexteBox.Items.Add(item);

            // Ajout des destinataires à la combobox
            foreach (String item in ReadDB.Instance.getDest())
                destBox.Items.Add(item);

            // On remplit la liste des statuts
            foreach (String item in ReadDB.Instance.getStatut())
                statutBox.Items.Add(item);

            // Remplissage de la liste des images
            this.images.Images.Add(TaskLeader.Properties.Resources.outlook);
        }

        public ManipAction(TLaction action)
        {
            InitializeComponent();

            // On mémorise l'action
            this.v_action = action;
            // Chargement des widgets
            this.loadWidgets();

            Array links;

            if (action.isScratchpad)
            {
                this.Text += "Ajouter une action";
                links = action.Links; // Dans le cas où la création d'action vient d'une interface
            }
            else
            {
                this.Text += "Modifier une action";

                destBox.Text = action.Destinataire;

                if (action.hasDueDate)
                    actionDatePicker.Value = action.DueDate;
                else
                    noDueDate.Checked = true;

                contexteBox.Text = action.Contexte;
                sujetBox.Text = action.Sujet;
                updateSujet(); // Mise à jour de la liste des sujets

                // Récupération des différents liens
                links = ReadDB.Instance.getLinks(action.ID);
            }

            // Affichage des liens le cas échéant
            if (links.Length > 0)
            {
                ListViewItem linkItem;

                foreach (Enclosure link in links)
                {
                    // Définition du label du lien
                    linkItem = new ListViewItem(link.Titre, link.Type);
                    linkItem.Tag = link;

                    // Ajout du lien à la listView
                    linksView.Items.Add(linkItem);
                }

                // Affichage de la ListView
                this.linksLabel.Visible = true;
                this.linksView.Visible = true;
            }

            // On affiche le sujet du mail dans la case action
            desField.Text = action.Texte;
            desField.Select(0, 0);

            // On sélectionne le statut
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
