using System;
using System.Drawing;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Configuration;
using System.Collections.Specialized;
using System.Windows.Forms;
using TaskLeader.DAL;
using TaskLeader.BO;
using TaskLeader.BLL;

namespace TaskLeader.GUI
{
    public partial class ManipAction : Form
    {
        //Import de l'API Win32 'SetForegroundWindow'
        [DllImportAttribute("User32.dll")]
        private static extern IntPtr SetForegroundWindow(IntPtr hWnd);

        private TLaction _action;
        private DB db { get { return TrayIcon.dbs[_action.dbName]; } }

        public String ID { get { return _action.ID; } }

        // Préparation des widgets
        private void loadWidgets()
        {
            // Contextes
            this.contexteBox.Items.Clear();
            contexteBox.Items.AddRange(db.getTitres(DB.contexte));
            contexteBox.Text = _action.Contexte;

            // Sujets
            if (contexteBox.Text != "")
                updateSujet(); // Mise à jour de la liste des sujets
            sujetBox.Text = _action.Sujet;

            // Destinataires
            this.destBox.Items.Clear();
            destBox.Items.AddRange(db.getTitres(DB.destinataire));
            destBox.Text = _action.Destinataire;

            // Statuts
            this.statutBox.Items.Clear();
            statutBox.Items.AddRange(db.getTitres(DB.statut)); // On remplit la liste des statuts
            statutBox.SelectedItem = _action.Statut;
        }

        /// <summary>Constructeur de la fenêtre</summary>
        /// <param name="action">Action à afficher</param>
        public ManipAction(TLaction action)
        {
            InitializeComponent();
            this.Icon = TaskLeader.Properties.Resources.task_coach;

            // On mémorise l'action
            this._action = action;

            // Remplissage de la liste des bases disponibles
            foreach (String dbName in TrayIcon.dbs.Keys)
                dbsBox.Items.Add(dbName);
            dbsBox.Text = _action.dbName;

            if (action.isScratchpad)
                this.Text += "Ajouter une action - TaskLeader";
            else
            {
                this.Text += "Modifier une action - TaskLeader";

                this.dbsBox.Enabled = false;

                if (action.hasDueDate) // Attribut géré à part car pas de valeur par défaut
                    actionDatePicker.Value = action.DueDate;
                else
                    noDueDate.Checked = true;
            }

            // Chargement des widgets
            this.loadWidgets();

            // Affichage des pièces jointes
            foreach (Enclosure link in action.PJ)
                this.addPJToView(link);

            this.linksView.Visible = (action.PJ.Count > 0);

            // Affichage du descriptif de l'action
            desField.Text = action.Texte;
            desField.Select(desField.Text.Length, 0); // Curseur placé à la fin par défaut

        }

        // Mise à jour de la combobox présentant les sujets
        private void updateSujet()
        {
            // On vide les sujets correspondants au contexte actuel
            sujetBox.Items.Clear();

            foreach (String item in db.getTitres(DB.sujet, contexteBox.Text))
                sujetBox.Items.Add(item);
        }

        // Sauvegarde de l'action
        private void sauveAction(object sender, EventArgs e)
        {
            //TODO: griser le bouton Sauvegarder si rien n'a été édité

            // Erreur lorsque le champ descriptif est vide
            if (String.IsNullOrWhiteSpace(this.desField.Text))
            {
                this.errorLabel.Visible = true;
                return;
            }

            // Update de l'action avec les nouveaux champs
            _action.updateDefault(contexteBox.Text, sujetBox.Text, desField.Text, destBox.Text, statutBox.Text);

            // Update de la DueDate que si c'est nécessaire
            if (noDueDate.Checked)
                _action.DueDate = DateTime.MinValue; // Remise à zéro de la dueDate
            else
                _action.DueDate = actionDatePicker.Value;

            // On sauvegarde l'action
            _action.save();

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

        /// <summary>
        /// Ajoute une PJ au formulaire et à l'action correspondante
        /// </summary>
        /// <param name="pj">PJ à inclure</param>
        public void addPJToForm(Enclosure pj)
        {
            // Ajout du lien à l'action
            _action.addPJ(pj);
            // Ajout à la linksView
            this.addPJToView(pj);
            // Affichage de la linksView
            this.linksView.Visible = true;
        }

        // Nettoyage sur fermeture de la fenêtre
        private void ManipAction_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (this.addMailRequested)
                OutlookIF.Instance.NewMail -= new NewMailEventHandler(addMail); // Désinscription de l'event NewMail
        }

        #region linksView

        // Ajout d'un lien à la ListView
        private void addPJToView(Enclosure pj)
        {
            // Ajout de l'image correspondant au lien dans la bibliothèque
            if (!biblio.Images.Keys.Contains(pj.IconeKey))
                this.biblio.Images.Add(pj.IconeKey, pj.BigIcon);

            // Ajout du lien à la ListView
            ListViewItem item = new ListViewItem(pj.Titre, pj.IconeKey);
            item.Tag = _action.PJ.IndexOf(pj);

            // Ajout du lien à la listView
            linksView.Items.Add(item);
        }

        // Gestion de l"ouverture de menu contextuel sur la linksView
        private void linksViewMenu_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // On annule l'affichage si aucun lien n'est sélectionné
            if (linksView.SelectedItems.Count == 0)
                e.Cancel = true;
        }

        // Suppression d'une PJ
        private void deleteEncItem_Click(object sender, EventArgs e)
        {
            // Suppression de la PJ sélectionnée de l'action associée
            _action.removePJ((Enclosure)linksView.SelectedItems[0].Tag);

            // Suppression de la pj de la vue
            linksView.Items.Remove(linksView.SelectedItems[0]);
            if (linksView.Items.Count == 0)
                linksView.Visible = false;
        }

        // Passage de l'item en mode édition
        private void renameEncItem_Click(object sender, EventArgs e)
        {
            linksView.SelectedItems[0].BeginEdit();
        }

        // Renommage de la PJ
        private void linksView_AfterLabelEdit(object sender, LabelEditEventArgs e)
        {
            if (!String.IsNullOrEmpty(e.Label))
                _action.renamePJ((int)linksView.Items[e.Item].Tag, e.Label);
        }

        private void pj_Click(object sender, EventArgs e)
        {
            // On ouvre le lien
            ((Enclosure)linksView.SelectedItems[0].Tag).open();
        }

        #endregion

        // Sélection d'une autre DB
        private void changeDB(object sender, EventArgs e)
        {
            if (dbsBox.Text != _action.dbName)
            {
                // Mise à jour de l'action
                _action.changeDB(dbsBox.Text);

                // Mise à jour des widgets
                this.loadWidgets();
            }
        }

        private void desField_Enter(object sender, EventArgs e)
        {
            this.errorLabel.Visible = false;
        }

        #region Ajout de PJs

        private void addLinkBut_Click(object sender, EventArgs e)
        {
            this.TopMost = false; // Passage de la fenêtre ManipAction en arrière plan temporairement

            SaveLink saveForm = new SaveLink();
            if (saveForm.ShowDialog() == DialogResult.OK) // Affichage de la fenêtre SaveLink
                this.addPJToForm(saveForm.lien);

            this.linksView.Visible = true;
            this.TopMost = true;
        }

        // Gestion de la demande d'ajout de mail
        private bool addMailRequested = false;
        private void addMailBut_Click(object sender, EventArgs e)
        {
            // Mise en valeur de la fenêtre Outlook
            if (!OutlookIF.Instance.addMailInProgress)
            {
                // Mise en place de l'IHM
                this.addMailRequested = true;
                this.AddMailLabel.Text = "Sélectionner le mail à ajouter";
                this.AddMailLabel.ForeColor = SystemColors.HotTrack;
                this.addLinkBut.Visible = false;
                this.addMailBut.Visible = false;
                this.AddMailLabel.Visible = true;

                // Affichage en premier plan de la fenêtre Outlook
                Process[] p = Process.GetProcessesByName("OUTLOOK");
                if (p.Length > 0)
                    SetForegroundWindow(p[0].MainWindowHandle);

                // Récupération de l'évènement "Nouveau mail"
                OutlookIF.Instance.NewMail += new NewMailEventHandler(addMail);
            }
            else
            {
                this.AddMailLabel.Text = "Ajout de mail déjà en cours";
                this.AddMailLabel.ForeColor = Color.Red;
                this.AddMailLabel.Visible = true;
            }
        }

        // Gestion de l'arrivée des mails
        private void addMail(object sender, NewMailEventArgs e)
        {
            if (linksView.InvokeRequired) // Gestion des appels depuis un autre thread
                linksView.Invoke(new NewMailEventHandler(addMail), new object[] { sender, e });
            else
            {
                this.addMailRequested = false;
                this.addPJToForm(e.Mail); // Ajout de mail à l'action
                this.AddMailLabel.Visible = false; // Disparition du label de statut
                this.addLinkBut.Visible = true;
                this.addMailBut.Visible = true;
                OutlookIF.Instance.NewMail -= new NewMailEventHandler(addMail); // Inscription à l'event NewMail
            }
        }

        #endregion
    }
}
