 using System;
using System.Data;
using System.Collections;
using System.Collections.Specialized;
using System.Configuration;
using System.Windows.Forms;
using System.Drawing;
using TaskLeader.DAL;
using TaskLeader.BO;
using TaskLeader.BLL;

namespace TaskLeader.GUI
{
    public partial class Toolbox : Form
    {
        public Toolbox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Chargement des différents composants au lancement de la toolbox
        /// </summary>
        private void Toolbox_Load(object sender, EventArgs e)
        {
            // Mention du nom de la version dans le titre de la fenêtre
            this.Text += " v"+Application.ProductVersion;

            // Remplissage de la combo des filtres
            filterCombo.Items.AddRange(ReadDB.Instance.getFiltersName());

            // Remplissage de la ListBox des statuts + menu contextuel
            foreach (object item in ReadDB.Instance.getStatut())
            {
                statutListBox.Items.Add(item, true); // Sélection des statuts par défaut
                statutTSMenuItem.DropDown.Items.Add(item.ToString(), null, this.changeStat);
            }

            // Création de la colonne mail
            DataGridViewImageColumn mailCol = new DataGridViewImageColumn();
            mailCol.Name = "Mail";
            mailCol.DataPropertyName = "IDMail";
            mailCol.Visible = false;
            grilleData.Columns.Add(mailCol);

            // On rajoute les lignes qu'il faut dans le contextMenu de la liste d'actions
            NameValueCollection section = (NameValueCollection)ConfigurationManager.GetSection("ExportSection");
            // Affichage de l'item dans le menu uniquement si une valeur d'export
            this.exportMenuItem.Visible = (section.Count > 0);
            foreach (string key in section)
                this.exportMenuItem.DropDown.Items.Add(key, null, this.exportRow);

            // Remplissage des dernières ListBox
            this.miseAjour(sender, e);
        }
        
        // Rafraîchissement de la page
        public void miseAjour(object sender, EventArgs e)
        {
            // Vidage de toutes les ListBox
            this.ctxtListBox.Items.Clear();
            this.destListBox.Items.Clear();           

            // Remplissage de la ListBox des contextes
            foreach (object item in ReadDB.Instance.getCtxt())
                ctxtListBox.Items.Add(item, true); // Sélection des contextes par défaut

            // Remplissage de la ListBox des destinataires
            foreach (object item in ReadDB.Instance.getDest())
                destListBox.Items.Add(item, true); // Sélection des destinataires par défaut

            // Si un filtre est actif on l'affiche
            if (Filtre.CurrentFilter != null)
                this.showFilter(Filtre.CurrentFilter);  
        }

        // Ouverture de la gui création d'action
        private void ajoutAction(object sender, EventArgs e)
        {
            TLaction action = new TLaction();
            action.freezeInitState();

            ManipAction fenetre = new ManipAction(action);
            fenetre.Disposed += new EventHandler(this.miseAjour); // Sur fermeture de ManipAction on update la Toolbox
            fenetre.Show();
        }

        // Méthode permettant de récupérer un objet TLaction à partir d'une collection de cellules
        private TLaction getActionFromRow(DataGridViewCellCollection ligne)
        {
            TLaction action = new TLaction(ligne["Titre"].Value.ToString());
            action.ID = ligne["id"].Value.ToString();
            action.Contexte = ligne["Contexte"].Value.ToString();
            action.Sujet = ligne["Sujet"].Value.ToString();
            action.parseDueDate(ligne["Deadline"].Value.ToString());
            action.Destinataire = ligne["Destinataire"].Value.ToString();
            action.Statut = ligne["Statut"].Value.ToString();
            
            // Récupération des informations du mail lié le cas échéant
            if(ligne["Mail"].Value.ToString() != "")
                action.mail = new Mail(ligne["Mail"].Value.ToString()); // Quand on formatte une cellule on ne modifie pas sa Value
            
            return action;
        }

        // Ouverture de la gui édition d'action
        private void modifAction(object sender, EventArgs e)
        {
            // Récupération de l'action correspondant à la ligne
            TLaction action = getActionFromRow(grilleData.SelectedRows[0].Cells);
            action.freezeInitState();

            ManipAction fenetre = new ManipAction(action);
            fenetre.Disposed += new EventHandler(this.miseAjour); // Sur fermeture de ManipAction on update la Toolbox
            fenetre.Show();
        }

        // Mise à jour du statut d'une action via le menu contextuel
        private void changeStat(object sender, EventArgs e)
        {
            // Récupération de l'action
            TLaction action = getActionFromRow(grilleData.SelectedRows[0].Cells);
            action.freezeInitState();

            // On récupère le nouveau statut
            action.Statut = ((ToolStripItem)sender).Text;

            // On met à jour le statut de l'action que s'il a changé
            if (action.statusHasChanged)
                DataManager.Instance.saveAction(action);

            this.miseAjour(null, null);
        }

        // Méthode appelée quand checks des contextes changent
        private void updateSujet(object sender, EventArgs e)
        {     
            // Dans tous les cas de changements de séléction on vide la liste
            sujetListBox.Items.Clear();

            // On n'affiche la liste des sujets que si un seul contexte est tické
            if (ctxtListBox.CheckedItems.Count == 1)
            {
                //Activation de la checkBox all
                allSujt.Enabled = true;
                //Remplissage de la liste
                foreach (object item in ReadDB.Instance.getSujet((String)ctxtListBox.CheckedItems[0].ToString()))
                    sujetListBox.Items.Add(item, true);
            }
            else
            {
                //Dans tous les autres cas on grise la checkbox All
                allSujt.Enabled = false;
            }
        }
        
        // Mise en forme des cellules sous certaines conditions
        private void grilleData_CellFormatting(object sender,DataGridViewCellFormattingEventArgs e)
        {
            DateTime dateValue;
            
            // Si la deadline est dépassée, la case est affichée sur fond rouge
            if (grilleData.Columns[e.ColumnIndex].Name.Equals("Deadline") && DateTime.TryParse(e.Value.ToString(), out dateValue) && dateValue.CompareTo(DateTime.Now) < 0)
            {
                    // On affiche la date en rouge et en gras
                    e.CellStyle.ForeColor = Color.Red;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);

                    // Et en darkRed sur sélection
                    e.CellStyle.SelectionForeColor = Color.DarkRed;
            }
            
            // Si un mail est attaché, on affiche l'image de mail
            if (grilleData.Columns[e.ColumnIndex].Name.Equals("Mail"))
            {
                if (e.Value as String != null)
                    e.Value = TaskLeader.Properties.Resources.outlook;                  
                else
                    e.CellStyle.NullValue = null;
            }
            
        }
                
        // Méthode générique d'affichage de la liste d'actions à partir d'un filtre
        private void afficheActions(Filtre filtre)
        {
            // Si le résultat d'une recherche est affiché, on efface l'étiquette
            if (searchFlowLayoutPanel.Visible)
                searchFlowLayoutPanel.Visible = false;

            // Si le filtre a un nom, on l'affiche
            filterCombo.Text = filtre.nom;

            // Récupération des résultats et association au tableau
            DataTable liste = DataManager.Instance.getActions(filtre);                     
            grilleData.DataSource = liste;

            // Réorganisation des colonnes
            grilleData.Columns["Mail"].DisplayIndex = 4;
            grilleData.Columns["Mail"].Visible = true;

            if (liste.Rows.Count == 0) // A voir si pas mieux en BalloonTip
                MessageBox.Show("Aucun résultat ne correspond au filtre", "Filtre d'actions", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);                  
        }

        // Affichage des actions sur filtre manuel
        private void filtreAction(object sender, EventArgs e)
        {
            // Si le résultat d'une recherche est affiché, on efface l'étiquette
            if (searchFlowLayoutPanel.Visible)
                searchFlowLayoutPanel.Visible = false;

            Filtre filtre = new Filtre(allCtxt.Checked,ctxtListBox.CheckedItems,allSujt.Checked,sujetListBox.CheckedItems,allDest.Checked,destListBox.CheckedItems,allStat.Checked,statutListBox.CheckedItems);
            // Pas de nom de filtre, il s'agit d'un filtre manuel
            this.afficheActions(filtre);
        }

        // Fermeture de la Form si minimisée
        private void Toolbox_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.Close();
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

            if (e.Button == MouseButtons.Left &&
                grilleData.Columns[e.ColumnIndex].Name.Equals("Mail") &&
                grilleData[e.ColumnIndex, e.RowIndex].Value.ToString() != "")
                OutlookIF.Instance.displayMail(new Mail(grilleData.Rows[e.RowIndex].Cells["Mail"].Value.ToString()));
        }

        // Affichage d'un curseur doigt si mail attaché.
        private void grilleData_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (grilleData.Columns[e.ColumnIndex].Name.Equals("Mail") &&
                e.RowIndex >=0 &&
                grilleData[e.ColumnIndex, e.RowIndex].Value.ToString() != "")
                this.Cursor = Cursors.Hand;
        }

        private void grilleData_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (grilleData.Columns[e.ColumnIndex].Name.Equals("Mail") &&
                e.RowIndex >= 0 &&
                grilleData[e.ColumnIndex, e.RowIndex].Value.ToString() != "")
                this.Cursor = Cursors.Default;
        }

        // Copie de l'action dans le presse-papier
        private void exportRow(object sender, EventArgs e)
        {
            Export.Instance.clipAction(((ToolStripItem)sender).Text, grilleData.SelectedRows[0]);
        }

        // Enregistrement du filtre renseigné
        private void saveFilterBut_Click(object sender, EventArgs e)
        {
            if (filterCombo.Text != "")
            {
                Filtre filtre = new Filtre(allCtxt.Checked, ctxtListBox.CheckedItems, allSujt.Checked, sujetListBox.CheckedItems, allDest.Checked, destListBox.CheckedItems, allStat.Checked, statutListBox.CheckedItems);
                filtre.nom = filterCombo.Text;
                if (ReadDB.Instance.isNvoFiltre(filtre))
                {
                    WriteDB.Instance.insertFiltre(filtre);
                    // On vide la liste des filtres et on efface la sélection
                    filterCombo.Items.Clear();
                    filterCombo.Text = "";
                    // On la remplit à nouveau
                    filterCombo.Items.AddRange(ReadDB.Instance.getFiltersName()); 
                }                  
                else
                    MessageBox.Show("Ce nom de filtre existe déjà.", "Nom du filtre", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);               
            }
            else
                MessageBox.Show("Le nom du filtre ne peut être vide", "Sauvegarde de filtre", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);     
        }

        //Routine générique permettant de (dé)sélectionner tous les items
        private void allBox_Click(object sender, EventArgs e)
        {
            CheckedListBox list = new CheckedListBox();

            switch (((Control)sender).Name)
            {
                case ("allCtxt"): //Contexte
                    list = ctxtListBox;
                    break;
                case ("allSujt"): //Contexte
                    list = sujetListBox;
                    break;
                case ("allDest"): //Contexte
                    list = destListBox;
                    break;
                case ("allStat"): //Contexte
                    list = statutListBox;
                    break;
            }

            for (int i = 0; i < list.Items.Count; i++)
                list.SetItemChecked(i, ((CheckBox)sender).Checked);
        }
        
        //Routine générique pour activation checkbox all
        private void listBox_Click(object sender, EventArgs e)
        {
            CheckBox box = new CheckBox();

            switch (((Control)sender).Name)
            {
                case ("ctxtListBox"):
                    //updateSujet(sender,e);
                    box = allCtxt;
                    break;
                case ("sujetListBox"):
                    box = allSujt;
                    break;
                case ("destListBox"): 
                    box = allDest;
                    break;
                case ("statutListBox"):
                    box = allStat;
                    break;
            }

            if (box.Checked)
                box.Checked = false;
        }

        //Remise à zéro de tous les filtres
        private void razFiltres()
        {
            // Contextes
            allCtxt.Checked = true;
            allBox_Click(allCtxt, new EventArgs());

            // Sujets
            updateSujet(new Object(), new EventArgs());

            // Destinataires
            allDest.Checked = true;
            allBox_Click(allDest, new EventArgs());

            // Statuts
            allStat.Checked = true;
            allBox_Click(allStat, new EventArgs());
        }

        /// <summary>
        /// Application d'un filtre sur les différents widgets + affichage
        /// </summary>
        private void showFilter(Filtre filtre)
        {
            razFiltres();

            CheckBox box = new CheckBox();
            CheckedListBox list = new CheckedListBox();

            // Tickage des bons critères
            foreach (Criterium critere in filtre.criteria)
            {
                switch (critere.champ)
                {
                    case(0): //Contexte
                        box = allCtxt;
                        list = ctxtListBox;                         
                        break;
                    case(1): //Sujet
                        box = allSujt;
                        list = sujetListBox;                         
                        break;
                    case(2): //Destinataire
                        box = allDest;
                        list = destListBox;                         
                        break;
                    case(3): //Statut
                        box = allStat;
                        list = statutListBox;
                        break;
                }

                box.Checked = false; // La checkbox "Tous" n'est pas sélectionnée
                for (int i = 0; i < list.Items.Count; i++) // Parcours de la ListBox
                {
                    int index = critere.selected.IndexOf(list.Items[i]); // Recherche de l'item dans le filtre
                    list.SetItemChecked(i, !(index == -1));
                }
            }

            // Application du filtre
            afficheActions(filtre);
        }

        // Ouverture d'un filtre enregistré
        private void openFilterBut_Click(object sender, EventArgs e)
        {
            if (filterCombo.Text != "")
                this.showFilter(ReadDB.Instance.getFilter(filterCombo.Text));
            else
                MessageBox.Show("Veuillez entrer un nom de filtre", "Application d'un filtre", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        // Affichage du résultat de la recherche
        private void searchButton_Click(object sender, EventArgs e)
        {
            // Remise à zéro de tous les filtres + Nom du filtre
            razFiltres();
            filterCombo.Text = "";

            // Récupération des résultats et association au tableau
            DataTable liste = ReadDB.Instance.searchActions(searchBox.Text);
            grilleData.DataSource = liste;

            // Affichage de l'étiquette correspondant à la recherche
            searchedText.Text = searchBox.Text;
            searchFlowLayoutPanel.Visible = true;

            // Remise à zéro de la textbox de recherche
            searchBox.Text = "";

            // Réorganisation des colonnes
            grilleData.Columns["Mail"].DisplayIndex = 4;
            grilleData.Columns["Mail"].Visible = true;

            if (liste.Rows.Count == 0) // A voir si pas mieux en BalloonTip
                MessageBox.Show("Aucun résultat", "Recherche d'actions", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        // Permet la validation de la recherche par la touche ENTER
        private void searchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                searchButton_Click(sender, e);
        }

        // Suppression de la recherche après click sur l'étiquette
        private void exitSearchBut_Click(object sender, EventArgs e)
        {
            // On cache l'étiquette de recherche
            searchFlowLayoutPanel.Visible = false;

            // Si un filtre est actif on l'affiche
            if (Filtre.CurrentFilter != null)
                this.showFilter(Filtre.CurrentFilter);
            // Sinon ?
        }
    }
}
