using System;
using System.Drawing;
using System.Diagnostics;
using System.Runtime.InteropServices;
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

        private TLaction v_action;
        public String ID { get { return v_action.ID; } }

        // Préparation des widgets
        private void loadWidgets()
        {
            this.Icon = TaskLeader.Properties.Resources.task_coach;

            //Ajout des contextes à la combobox
            foreach (String item in ReadDB.Instance.getTitres(DB.Instance.contexte))
                contexteBox.Items.Add(item);

            // Ajout des destinataires à la combobox
            foreach (String item in ReadDB.Instance.getTitres(DB.Instance.destinataire))
                destBox.Items.Add(item);

            // On remplit la liste des statuts
            foreach (String item in ReadDB.Instance.getTitres(DB.Instance.statut))
                statutBox.Items.Add(item);
        }

        // Ajout d'un lien à la ListView
        private void addPJToView(Enclosure pj)
        {
            // Ajout de l'image correspondant au lien dans la bibliothèque
            if (!biblio.Images.Keys.Contains(pj.IconeKey))
                this.biblio.Images.Add(pj.IconeKey, pj.Icone);

            // Ajout du lien à la ListView
            ListViewItem item = new ListViewItem(pj.Titre, pj.IconeKey);
            item.Tag = pj;

            // Ajout du lien à la listView
            linksView.Items.Add(item);
        }

        public ManipAction(TLaction action)
        {
            InitializeComponent();

            // On mémorise l'action
            this.v_action = action;
            // Chargement des widgets
            this.loadWidgets();

            Array links = action.PJ;

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
            if (contexteBox.Text != "")
                updateSujet(); // Mise à jour de la liste des sujets

            foreach (Enclosure link in links)
                this.addPJToView(link);

            this.linksView.Visible = (links.Length > 0);

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

            foreach (String item in ReadDB.Instance.getSujets(contexteBox.Text))
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
            v_action.save();

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

        private void pj_Click(object sender, EventArgs e)
        {
            // On ouvre le lien
            ((Enclosure)linksView.SelectedItems[0].Tag).open();
        }

        public void addPJToForm(Enclosure pj)
        {
            // Ajout du lien à l'action
            v_action.addPJ(pj);
            // Ajout à la linksView
            this.addPJToView(pj);
            // Affichage de la linksView
            this.linksView.Visible = true;
        }

        private void ajouterLink_Click(object sender, EventArgs e)
        {
            this.TopMost = false; // Passage de la fenêtre ManipAction en arrière plan temporairement

            SaveLink saveForm = new SaveLink();
            if (saveForm.ShowDialog() == DialogResult.OK) // Affichage de la fenêtre SaveLink
                this.addPJToForm(saveForm.lien);

            this.linksView.Visible = true;
            this.TopMost = true;
        }

        private void addPJBut_Click(object sender, EventArgs e)
        {
            this.addLinksMenu.Show(Cursor.Position);// Affichage du menu d'ajout des liens
        }

        private void mailItem_Click(object sender, EventArgs e)
        {
            // Mise en valeur de la fenêtre Outlook
            if (!OutlookIF.Instance.addMailInProgress)
            {
                this.AddMailLabel.Text = "Sélectionner le mail à ajouter";
                this.AddMailLabel.ForeColor = SystemColors.HotTrack;
                this.AddMailLabel.Visible = true;
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
            if (linksView.InvokeRequired)
                linksView.Invoke(new NewMailEventHandler(addMail), new object[] { sender, e }); // Gestion des appels depuis un autre thread
            else
            {
                this.addPJToForm(e.Mail); // Ajout de mail à l'action
                this.AddMailLabel.Visible = false; // Disparition du label de statut
                OutlookIF.Instance.NewMail -= new NewMailEventHandler(addMail); // Inscription à l'event NewMail
            }
        }

        // Nettoyage sur fermeture de la fenêtre
        private void ManipAction_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Désinscription de l'event NewMail
            OutlookIF.Instance.NewMail -= new NewMailEventHandler(addMail);
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
            v_action.removePJ((Enclosure)linksView.SelectedItems[0].Tag);

            // Suppression de la pj de la vue
            linksView.Items.Remove(linksView.SelectedItems[0]);
            if (linksView.Items.Count == 0)
                linksView.Visible = false;

        }

    }
}
