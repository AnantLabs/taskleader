using System;
using System.Windows.Forms;
using System.Configuration;
using TaskLeader.DAL;
using TaskLeader.BLL;

namespace TaskLeader.GUI
{
    public partial class ManipAction : Form
    {
        int type;
        String idAction;
        String initialStatus;
        String mailID;

        // Partie commune au 2 constructeurs
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
            dateChosen.Checked = false;

            // On reset les destinataires
            destBox.Text = "";
            destBox.Items.Clear();
        }

        // Constructeur pour une création from scratch
        public ManipAction()
        {
            InitializeComponent();

            // On est dans le cas 1: création d'une nouvelle action
            type = 1;
            this.Text += "Ajouter une action";

            // Chargement des composants
            this.loadWidgets();

            // On sélectionne le statut par défaut
            statutBox.Text = ReadDB.Instance.getDefaultStatus();
        }

        // Constructeur pour une création depuis Outlook
        public ManipAction(String sujet, String IDMail)
        {
            InitializeComponent();

            // On est dans le cas 1: création d'une nouvelle action
            type = 1;
            this.Text += "Ajouter une action";

            // Chargement des composants
            this.loadWidgets();

            // On sélectionne le statut par défaut
            statutBox.Text = ReadDB.Instance.getDefaultStatus();

            // On affiche le sujet du mail dans la case action
            desField.Text = sujet;

            // On active le lien "Source Outlook"
            lienMail.Visible = true;
            this.mailID = IDMail;
        }
        
        // Constructeur pour un update
        public ManipAction(DataGridViewCellCollection cells)
        {
            
            InitializeComponent();

            // On est dans le cas 2: modification d'une action
            type = 2;
            this.Text += "Modifier une action";

            // Chargement des composants
            this.loadWidgets();

            // On remplit tous les champs avec les données de la datagridview
            contexteBox.Text = cells["Contexte"].Value.ToString();
            sujetBox.Text = cells["Sujet"].Value.ToString();
            updateSujet(); // Mise à jour de la liste des sujets
            desField.Text = cells["Titre"].Value.ToString();

            // Récupération de la dueDate entrée
            String date = cells["Deadline"].Value.ToString();
            if (date != "")
                actionDatePicker.Value = DateTime.Parse(date);
            else
                dateChosen.Checked = true;

            destBox.Text = cells["Destinataire"].Value.ToString();

            // On sauvegarde le statut initial
            this.initialStatus = cells["Statut"].Value.ToString();
            // On sélectionne le statut
            statutBox.SelectedItem = this.initialStatus;
        
            // On récupère l'id de l'action qu'on veut modifier
            this.idAction = cells["id"].Value.ToString();
            
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
            String action = desField.Text;
            bool statusChanged;

            switch (type)
            {
            case 1:
                    // On crée une nouvelle action à partir des données rentrées
                    DataManager.Instance.createAction(contexteBox.Text, sujetBox.Text, action, dateChosen.Checked, actionDatePicker.Value, destBox.Text, mailID,statutBox.Text);                    
                    if (ConfigurationManager.AppSettings["newActionChained"] == "true")
                    {
                        // On simule la fermeture de la form pour rafraîchir la Toolbox
                        this.OnFormClosed(new FormClosedEventArgs(CloseReason.None));
                        
                        // On reset tous les champs
                        this.clearAllFields();
                        // Et on recharge
                        this.loadWidgets();   
                    }
                    else
                        this.Close();// Fermeture de la fenêtre
                    break;
                
            case 2:
                statusChanged = !(statutBox.Text==this.initialStatus);
                // Update de l'action avec les données entrées
                DataManager.Instance.updateAction(contexteBox.Text, sujetBox.Text, action, dateChosen.Checked, actionDatePicker.Value, destBox.Text, statusChanged,statutBox.Text, idAction); 
                // Fermeture de la fenêtre
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
