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
            
            if (action.isScratchpad)
                this.Text += "Ajouter une action";               
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
            }

            // On affiche le sujet du mail dans la case action
            desField.Text = action.Texte;

            // On sélectionne le statut
            statutBox.SelectedItem = action.Statut; 			
			
            // Récupération des différents liens
            DataTable links = ReadDB.Instance.getLinks(action.ID);
			
           ListViewItem link = new ListViewItem();
			
            foreach (DataRow linkData in links.Rows)
            {
				// Définition du label du lien
				link.Text = linkData["Titre"].ToString();
				
				// Définition de l'image correspondante au lien
				switch(link["EncType"].ToString())
				{
					case "Mails":
						link.ImageIndex = 0;
						break;
				}
				
				// Ajout des infos additionnelles
				link.SubItems.Clear();
				link.SubItems.Add(link["EncType"].ToString());
				link.SubItems.Add(link["EncID"].ToString());
				
				// Ajout du lien à la listeView
				linksView.Items.Add(link);
			}
			
			if(links.Rows.Count > 0)
			{
                // Affichage de la ListView
                this.linksLabel.Visible = true;
                this.linksView.Visible = true;
            }		
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
		// A SUPPRIMER
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
			
			// A SUPPRIMER avec les raccourcis clavier
            if (v_action.isScratchpad && ConfigurationManager.AppSettings["newActionChained"] == "true")
            {
                // On simule la fermeture de la form pour rafraîchir la Toolbox
                //this.Disposed(new EventArgs());
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
		
		private void link_Click( object sender, EventArgs e )
		{
			// Récupération du lien sélectionné
			ListViewItem link = linksView.SelectedItems[0];
			
			DataManager.Instance.openLink(link.SubItems[0].Text, link.SubItems[1].Text);
			// OutlookIF.Instance.displayMail(v_action.mail);
		}
    }
}
