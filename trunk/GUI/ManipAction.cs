using System;
using System.Windows.Forms;
using System.Configuration;
using TaskLeader.DAL;
using TaskLeader.BLL;
using TaskLeader.BO;

namespace TaskLeader.GUI
{
    public partial class ManipAction : Form
    {
        private TLaction v_action;

        // Remplissage des combobox
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
        }

        // Constructeur pour une création depuis Outlook
        public ManipAction(TLaction action)
        {
            InitializeComponent();

            // On mémorise l'action
            this.v_action = action;

            this.loadWidgets();
            
            if (action.isScratchpad)
                this.Text += "Ajouter une action";               
            else
            {
                this.Text += "Modifier une action";

                // On remplit tous les champs avec les données de l'action
                contexteBox.Text = action.Contexte;
                sujetBox.Text = action.Sujet;
                updateSujet(); // Mise à jour de la liste des sujets

                // Récupération de la dueDate entrée
                if (action.hasDueDate)
                    actionDatePicker.Value = action.DueDate;
                else
                    noDueDate.Checked = true;              
            }

            // On active le lien "Source Outlook" si nécessaire
            lienMail.Visible = action.hasMailAttached;

            // On affiche le sujet du mail dans la case action
            desField.Text = action.Texte;

            // On affiche le destinataire
            destBox.Text = action.Destinataire;

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

        // Remise à zéro de tous les champs sauf le statut
        private void clearAllFields()
        {
            // On efface les contextes
            contexteBox.Text = "";
            contexteBox.Items.Clear();

            //On efface les sujets
            sujetBox.Text = "";
            sujetBox.Items.Clear();

            // On efface l'action
            desField.Text = "";

            // On reset la date
            actionDatePicker.Value = DateTime.Now;
            noDueDate.Checked = false;

            // On reset les destinataires
            destBox.Text = "";
            destBox.Items.Clear();
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

            if (v_action.isScratchpad && ConfigurationManager.AppSettings["newActionChained"] == "true")
            {
                // On simule la fermeture de la form pour rafraîchir la Toolbox
                this.OnFormClosed(new FormClosedEventArgs(CloseReason.None));
                // On reset tous les champs
                this.clearAllFields();
                // Et on recharge
                this.loadWidgets();
                return;
            }

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

        private void lienMail_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            OutlookIF outlook = new OutlookIF();
            outlook.searchMail(this.v_action);
        }
    }
}
