using System;
using System.Data;
using System.Windows.Forms;
using System.Configuration;

namespace ActionsList
{
    public partial class Toolbox : Form
    {
        public ActionsDBservices services;

        public Toolbox(ActionsDBservices baseSQL)
        {
            InitializeComponent();
            services = baseSQL;
        }

        private void Toolbox_Load(object sender, EventArgs e)
        {
            this.Text += " v"+Application.ProductVersion;

            foreach (String item in services.getCtxt())
                ctxtListBox.Items.Add(item,true); // Remplissage de la combobox contextes en sélectionnant par défaut

            foreach (String item in services.getDest())
                destListBox.Items.Add(item,true); // Remplissage de la combobox destinataires en sélectionnant par défaut

            foreach (String item in services.getStatut())
                statutListBox.Items.Add(item,true); // Remplissage de la combobox statuts en sélectionnant par défaut
        }
                
        public void miseAjour(String bilan)
        {
            // Récupération de la durée d'affichage de la tooltip
            int duree = int.Parse(ConfigurationManager.AppSettings["dureeTooltip"]);
            
            // Affichage du bilan en tooltip de la tray icon
            trayIcon.ShowBalloonTip(duree,"Ajout/Modification d'action",bilan,ToolTipIcon.Info);
            
            // Mise à jour de la liste avec les filtres enregistrés
            if(services.filterStored)
                grilleData.DataSource = services.getList().DefaultView;

            //Mise à jour de la liste des contextes
            if(bilan.Contains("Nouveau contexte"))
                foreach (String item in services.getCtxt())
                    if (!ctxtListBox.Items.Contains(item))
                        ctxtListBox.Items.Add(item);

            // Mise à jour de la liste des sujets
            if(ctxtListBox.CheckedItems.Count==1 && bilan.Contains("Nouveau sujet"))
                foreach (String item in services.getSujet((String)ctxtListBox.CheckedItems[0]))
                    if (!sujetListBox.Items.Contains(item))
                        sujetListBox.Items.Add(item);

            //Mise à jour de la liste des destinataires
            if (bilan.Contains("Nouveau destinataire"))
                foreach (String item in services.getDest())
                    if (!destListBox.Items.Contains(item))
                        destListBox.Items.Add(item);
        }

        private void ajoutAction(object sender, EventArgs e)
        {
            // On passe en paramètre l'objet contenant la connection sql
            ManipAction fenetre = new ManipAction(services, this);
            fenetre.Show();
        }

        private void Toolbox_FormClosed(object sender, FormClosedEventArgs e)
        {
            // Fermeture du fichier
            services.closeConnex();
        }

        private void modifAction(object sender, DataGridViewCellEventArgs e)
        {
            // Ouverture de la fenêtre ManipAction en mode édition
            ManipAction fenetre = new ManipAction(services, this, grilleData.Rows[e.RowIndex].Cells);
            fenetre.Show();
        }

        private void updateSujet(object sender, EventArgs e)
        {
            
            // Dans tous les cas de changements de séléction on vide la liste
            sujetListBox.Items.Clear();

            // On n'affiche la liste des sujets que si un seul contexte est sélectionné
            if (ctxtListBox.CheckedItems.Count == 1)
            {
                //Activation des radio buttons
                SujSelRadio.Enabled = true;
                SujAllRadio.Enabled = true;
                //Remplissage de la liste
                foreach (String item in services.getSujet((String)ctxtListBox.CheckedItems[0].ToString()))
                    sujetListBox.Items.Add(item, true);

            }
            else
            {
                //Dans tous les autres cas on grise les radio buttons
                SujSelRadio.Enabled = false;
                SujAllRadio.Enabled = false;
                //Et on active "Sélection"
                SujSelRadio.Checked = true;
            }

        }

        private void filtreAction(object sender, EventArgs e)
        {
            // Stockage des filtres
            services.updateFilters(ctxtListBox.CheckedItems, sujetListBox.CheckedItems, destListBox.CheckedItems, statutListBox.CheckedItems);
            //Récupération de la DataTable de résultats
            DataTable liste = services.getList();

            if (liste.Rows.Count > 0)
                grilleData.DataSource = liste.DefaultView;
            else
            {
                MessageBox.Show("Aucun résultat ne correspond au filtre", "Filtre d'actions", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                grilleData.Enabled = false;
            }

        }

        private void refreshBout_Click(object sender, EventArgs e)
        {
            miseAjour("");
        }

        private void Toolbox_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.Hide();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            maximItem_Click(sender, e);
        }

        // Fermeture de l'appli depuis le tray
        private void closeItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // Affichage maximisé de la Toolbox depuis le tray
        private void maximItem_Click(object sender, EventArgs e)
        {
            this.Show();
            this.WindowState = FormWindowState.Maximized;
        }

        // Affichage du menu contextuel sur le tableau d'action
        private void grilleData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //Sélection de la ligne
                grilleData.Rows[e.RowIndex].Selected = true;
                //Affichage du menu contextuel
                listeContext.Show(Cursor.Position);
            }
        }

        // Copie de l'action dans le presse papier avec le format SEnS => couche business
        private void copySens(object sender, EventArgs e)
        {
            clipAction(ConfigurationManager.AppSettings["sensTemplate"]);
        }

        // Copie de l'action dans le presse papier avec le format PRM SBA => couche business
        private void copySBA(object sender, EventArgs e)
        {
            clipAction(ConfigurationManager.AppSettings["sbaTemplate"]);
        }

        // Routine commune aux exports presse-papier
        private void clipAction(String template)
        {
            // Remplacement des caractères spéciaux
            String resultat = template.Replace("(VIDE)", "");
            resultat = resultat.Replace("(TAB)", "\t");

            // Remplacement du sujet (Attention les sauts de ligne ne sont pas gérés)
            resultat = resultat.Replace("(Sujet)", grilleData.SelectedRows[0].Cells["Sujet"].Value.ToString());

            // Remplacement de l'action en rentrant une formule excel pour les passages à la ligne
            String action = grilleData.SelectedRows[0].Cells["Titre"].Value.ToString().Replace("\"","\"\"");
            action = action.Replace("\r\n", "\"&CAR(10)&\""); // Attention compatible avec la version fr de excel seulement
            action = "=\"" + action + "\"";
            resultat = resultat.Replace("(Action)", action);

            // Remplacement du statut, de la due date et de la date courante
            resultat = resultat.Replace("(Statut)", grilleData.SelectedRows[0].Cells["Statut"].Value.ToString());
            resultat = resultat.Replace("(DueDate)", grilleData.SelectedRows[0].Cells["Date limite"].Value.ToString());
            resultat = resultat.Replace("(NOW)", DateTime.Now.ToString("dd-MM-yyyy"));         

            //Copie dans le presse-papier
            Clipboard.SetText(resultat);
        }

        private void saveFilterBut_Click(object sender, EventArgs e)
        {
            String resultat;

            if (filterCombo.Text != "")
            {
                resultat = services.addFilter(filterCombo.Text, ctxtListBox.CheckedItems, sujetListBox.CheckedItems, destListBox.CheckedItems, statutListBox.CheckedItems);
                this.miseAjour(resultat);
            }
            else
                MessageBox.Show("Le nom du filtre ne peut être vide", "Sauvegarde de filtre", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        //Routine générique permettant de griser les listes et sélectionnant tous les items
        private void DisableAndSelectAll(CheckedListBox list, RadioButton radio)
        {
            for (int i = 0; i < list.Items.Count; i++)
                list.SetItemChecked(i, true);

            list.Enabled = !radio.Checked;
        }

        //Application sur liste contexte
        private void CtxtAllRadio_CheckedChanged(object sender, EventArgs e)
        {
            DisableAndSelectAll(ctxtListBox, (RadioButton)sender);
        }

        //Application sur liste destinataires
        private void destAllRadio_CheckedChanged(object sender, EventArgs e)
        {
            DisableAndSelectAll(destListBox, (RadioButton)sender);
        }

        //Application sur liste sujets
        private void SujAllRadio_CheckedChanged(object sender, EventArgs e)
        {
            DisableAndSelectAll(sujetListBox, (RadioButton)sender);
        }

        //Application sur liste statuts
        private void statAllRadio_CheckedChanged(object sender, EventArgs e)
        {
            DisableAndSelectAll(statutListBox, (RadioButton)sender);
        }


    }
}
