using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Configuration;

namespace ActionsList
{
    public partial class ManipAction : Form
    {
        ActionsDBservices services;
        Toolbox parent;
        
        int type;
        String idAction;

        // Constructeur pour une création
        public ManipAction(ActionsDBservices servicesDB, Toolbox form)
        {
            InitializeComponent();
            services = servicesDB;
            parent = form;

            // On est dans le cas 1: création d'une nouvelle action
            type = 1;
            this.Text = "Ajouter une action";

            // On n'affiche pas les widgets de sélection du statut, la nouvelle action sera forcemment ouverte
            label6.Visible = false;
            statutBox.Visible = false;
        }

        // Constructeur pour un update
        public ManipAction(ActionsDBservices servicesDB, Toolbox form, DataGridViewCellCollection cells)
        {
            InitializeComponent();
            services = servicesDB;
            parent = form;

            // On est dans le cas 2: modification d'une action
            type = 2;
            this.Text = "Modifier une action";
            
            // On remplit tous les champs avec les données de la datagridview
            contexteBox.Text = cells["Contexte"].Value.ToString();
            sujetBox.Text = cells["Sujet"].Value.ToString();
            updateSujet(); // Mise à jour de la liste des sujets
            desField.Text = cells["Titre"].Value.ToString();

            // Récupération de la dueDate entrée
            String date = cells["Date limite"].Value.ToString();
            if (date != "")
                actionDatePicker.Value = DateTime.Parse(date);
            else
                dateChosen.Checked = true;

            destBox.Text = cells["Destinataire"].Value.ToString();

            // On remplit la liste des statuts
            foreach (String item in services.getStatut())
                statutBox.Items.Add(item);
            // On sélectionne le statut
            statutBox.SelectedItem = cells["Statut"].Value.ToString();

            // On récupère l'id de l'action qu'on veut modifier
            idAction = cells["id"].Value.ToString();
        }

        // Partie commune entre les 2 constructeurs
        private void ManipAction_Load(object sender, EventArgs e)
        {
            //Ajout des contextes à la combobox
            foreach (String item in services.getCtxt())
                contexteBox.Items.Add(item);

            // Ajout des destinataires à la combobox
            foreach (String item in services.getDest())
                destBox.Items.Add(item);
        }

        // Mise à jour de la combobox présentant les sujets
        private void updateSujet()
        {
            // On vide les sujets correspondants au contexte actuel
            sujetBox.Items.Clear();

            foreach (String item in services.getSujet(contexteBox.Text))
                sujetBox.Items.Add(item);
        }

        // Sauvegarde de l'action
        private void sauveAction(object sender, EventArgs e)
        {
            String bilan ="";
            String action = desField.Text;

            switch (type)
            {
                case 1:
                    // On crée une nouvelle action à partir des données rentrées

                    bilan+=services.editAction(1,contexteBox.Text, sujetBox.Text, action, dateChosen.Checked, actionDatePicker.Value, destBox.Text,"","0");
                    parent.miseAjour(bilan);
                    this.Close();
                    if (ConfigurationManager.AppSettings["newActionChained"] == "true")
                    {
                        ManipAction window = new ManipAction(services, parent);
                        window.Show();
                    }                       
                    break;

                case 2:
                    // On met à jour l'action avec les données entrées
                    bilan += services.editAction(2,contexteBox.Text, sujetBox.Text, action, dateChosen.Checked, actionDatePicker.Value, destBox.Text, statutBox.Text, idAction);
                    parent.miseAjour(bilan);    
                    // On ferme la fenêtre
                    this.Close();
                    break;
            }        
        }

        // Demande de mise à jour des sujets quand le contexte sélectionné change
        private void contexteBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateSujet();
        }

        // Mise à jour du widget date en fonction de la sélection de la checkbox
        private void dateChosen_CheckedChanged(object sender, EventArgs e)
        {
            actionDatePicker.Enabled = !dateChosen.Checked;
        }

    }
}
