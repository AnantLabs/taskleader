 using System;
using System.Data;
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
        DataGridViewImageColumn linkCol = new DataGridViewImageColumn();

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
            this.loadFilters();

            // Remplissage de la ListBox des statuts + menu contextuel du tableau
            foreach (object item in ReadDB.Instance.getTitres(DB.Instance.statut))
            {
                statutListBox.Items.Add(item, true); // Sélection de tous les statuts par défaut
                statutTSMenuItem.DropDown.Items.Add(item.ToString(), null, this.changeStat);
            }

            // Création de la colonne des liens
            linkCol.Name = "Liens";
            linkCol.DataPropertyName = "Liens";

            // On rajoute les lignes qu'il faut dans le contextMenu de la liste d'actions
            NameValueCollection section = (NameValueCollection)ConfigurationManager.GetSection("Export");
            // Affichage de l'item dans le menu uniquement si une valeur d'export
            this.exportMenuItem.Visible = (section.Count > 0);
            foreach (string key in section)
                this.exportMenuItem.DropDown.Items.Add(key, null, this.exportRow);

            // Remplissage des dernières ListBox
            this.miseAjour(sender, e);
        }
        
        // Chargement des filtres
        private void loadFilters()
        {
            filterCombo.Items.Add("Sélectionner...");
            filterCombo.Items.AddRange(ReadDB.Instance.getTitres(DB.Instance.filtre));
            filterCombo.SelectedIndex = 0;
        }

        // Rafraîchissement de la page
        public void miseAjour(object sender, EventArgs e)
        {
            // Vidage de toutes les ListBox
            this.ctxtListBox.Items.Clear();
            this.destListBox.Items.Clear();           

            // Remplissage de la ListBox des contextes
            foreach (object item in ReadDB.Instance.getTitres(DB.Instance.contexte))
                ctxtListBox.Items.Add(item, true); // Sélection des contextes par défaut

            // Remplissage de la ListBox des destinataires
            foreach (object item in ReadDB.Instance.getTitres(DB.Instance.destinataire))
                destListBox.Items.Add(item, true); // Sélection des destinataires par défaut

            // Si un filtre est actif on l'affiche
            if (Filtre.CurrentFilter != null)
                this.showFilter(Filtre.CurrentFilter);  
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
                foreach (object item in ReadDB.Instance.getSujets((String)ctxtListBox.CheckedItems[0].ToString()))
                    sujetListBox.Items.Add(item, true);
            }
            else
            {
                //Dans tous les autres cas on grise la checkbox All
                allSujt.Enabled = false;
            }
        }

		// Fermeture de la Form si minimisée
        private void Toolbox_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.Close();
        }
		
        // Ouverture de la gui création d'action
        private void ajoutAction(object sender, EventArgs e)
        {
            TrayIcon.displayNewAction(new TLaction());
        }

        // Ouverture de la gui édition d'action
        private void modifAction(object sender, EventArgs e)
        {
            TrayIcon.displayNewAction(new TLaction(grilleData.SelectedRows[0].Cells["id"].Value.ToString()));
        }

        // Mise à jour du statut d'une action via le menu contextuel
        private void changeStat(object sender, EventArgs e)
        {
            // Récupération de l'action
			TLaction action = new TLaction(grilleData.SelectedRows[0].Cells["id"].Value.ToString());

            // On récupère le nouveau statut
            action.Statut = ((ToolStripItem)sender).Text;

            // On met à jour le statut de l'action que s'il a changé
            if (action.statusHasChanged)
                action.save();

            this.miseAjour(null, null);
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
            if (grilleData.Columns[e.ColumnIndex].Name.Equals("Liens"))
            {
                String valeur = e.Value as String;
                if (valeur == null) // Pas de lien associé
                    e.CellStyle.NullValue = null;
                else if (valeur == "*") // Plusieurs liens associés
                    e.Value = TaskLeader.Properties.Resources.PJ;
                else
                    e.Value = new genericEnc(valeur).Icone;
            }    
        }
		
		// Gestion des clicks sur le tableau d'actions
        private void grilleData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                //Sélection de la ligne
                grilleData.Rows[e.RowIndex].Selected = true;

                //Affichage du menu contextuel
                listeContext.Show(Cursor.Position);
            }

            if (e.Button == MouseButtons.Left && // Click gauche
                grilleData.Columns[e.ColumnIndex].Name.Equals("Liens") && // Colonne "Liens"
                grilleData[e.ColumnIndex, e.RowIndex].Value.ToString() != "") // Cellule non vide
            {
				//Récupération des différents liens
                Array links = ReadDB.Instance.getPJ(grilleData.SelectedRows[0].Cells["id"].Value.ToString());

                if (links.Length == 1) // Un lien seulement
                    ((Enclosure)links.GetValue(0)).open(); // Ouverture directe
                else // Plusieurs liens
                {
                    linksContext.Items.Clear(); // Remise à zéro de la liste

                    foreach(Enclosure link in links){
                        ToolStripButton item = new ToolStripButton(link.Titre,link.Icone,this.linksContext_ItemClicked); // Création du lien avec le titre et l'icône
                        item.Tag = link; // Association du link
                        linksContext.Items.Add(item); // Ajout au menu
                    }                   

                    linksContext.Show(Cursor.Position); // Affichage du menu contextuel de liste
                }
            }
        }

        // Ouverture du lien
        private void linksContext_ItemClicked(object sender, EventArgs e)
        {
            ((Enclosure)((ToolStripButton)sender).Tag).open();
        }

        // Affichage d'un curseur doigt si mail attaché.
        private void grilleData_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (grilleData.Columns[e.ColumnIndex].Name.Equals("Liens") &&
                e.RowIndex >=0 &&
                grilleData[e.ColumnIndex, e.RowIndex].Value.ToString() != "")
                this.Cursor = Cursors.Hand;
        }

        private void grilleData_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (grilleData.Columns[e.ColumnIndex].Name.Equals("Liens") &&
                e.RowIndex >= 0 &&
                grilleData[e.ColumnIndex, e.RowIndex].Value.ToString() != "")
                this.Cursor = Cursors.Default;
        }
                
        // Méthode générique d'affichage de la liste d'actions à partir d'un filtre
        private void afficheActions(Filtre filtre)
        {
            // Ajout si nécessaire de la colonne Mail
            if (!grilleData.Columns.Contains("Liens"))        
                grilleData.Columns.Add(linkCol);  

            // Récupération des résultats et association au tableau
            DataTable liste = filtre.getActions();                     
            grilleData.DataSource = liste;

            grilleData.Columns["Liens"].DisplayIndex = 4;

            // Définition du label de résultat
            if (liste.Rows.Count == 0)
                resultLabel.Text = "Aucune action trouvée";
            else if (liste.Rows.Count == 1)
                resultLabel.Text = "1 action trouvée";
            else
                resultLabel.Text = liste.Rows.Count.ToString() + " actions trouvées";
            // Affichage du label
            resultLabel.Visible = true;

            grilleData.Focus();
        }

        // Affichage des actions sur filtre manuel
        private void filtreAction(object sender, EventArgs e)
        {
            Filtre filtre = new Filtre(allCtxt.Checked, allSujt.Checked, allDest.Checked, allStat.Checked, ctxtListBox.CheckedItems, sujetListBox.CheckedItems, destListBox.CheckedItems, statutListBox.CheckedItems);            

            if (saveFilterCheck.Checked) //Sauvegarde du filtre si checkbox cochée
            {
                // Désélection de la checkbox
                saveFilterCheck.Checked = false;

                String nomFiltre = "";

                if ((new SaveFilter()).getFilterName(ref nomFiltre) == DialogResult.OK)// Affichage de la Fom SaveFilter
                {
                    //Sauvegarde du filtre
                    filtre.nom = nomFiltre;
                    WriteDB.Instance.insertFiltre(filtre);                   

                    // On vide la liste des filtres                
                    filterCombo.Items.Clear();

                    // On la remplit à nouveau
                    this.loadFilters();
                }
            }

            // Quoiqu'il arrive, affichage du filtre
            this.showFilter(filtre);
        }       

        // Copie de l'action dans le presse-papier
        private void exportRow(object sender, EventArgs e)
        {
            Export.Instance.clipAction(((ToolStripItem)sender).Text, grilleData.SelectedRows[0]);
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
            // Reset des champs recherches et filtres enregistrés
            filterCombo.SelectedIndex = 0;
            searchBox.Text = ""; 

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
        /// Application d'un filtre sur les différents widgets + affichage des actions correspondantes
        /// </summary>
        private void showFilter(Filtre filtre)
        {
            // Reset des checkedlistbox de filtre
            razFiltres();      

            switch(filtre.type)
            {
                case (2): // C'est une recherche

                    // Affichage de l'étiquette correspondant à la recherche
                    typeLabel.Text = "Recherche:";
                    searchedText.Text = filtre.nom;                                    
                    break;

                case (1):

                    // Affichage de l'étiquette correspondant au filtre
                    typeLabel.Text = "Filtre:";
                    if (filtre.nom != "")
                        searchedText.Text = filtre.nom;
                    else
                        searchedText.Text = "manuel";

                    CheckBox box = new CheckBox();
                    CheckedListBox list = new CheckedListBox();

                    // Tickage des bons critères
                    foreach (Criterium critere in filtre.criteria)
                    {
                        String table = critere.entity.mainTable;

                        if (table == DB.Instance.contexte.mainTable) { box = allCtxt; list = ctxtListBox; }
                        if (table == DB.Instance.sujet.mainTable) { box = allSujt; list = sujetListBox; }
                        if (table == DB.Instance.destinataire.mainTable) { box = allDest; list = destListBox; }
                        if (table == DB.Instance.statut.mainTable) { box = allStat; list = statutListBox; }

                        box.Checked = false; // La checkbox "Tous" n'est pas sélectionnée
                        for (int i = 0; i < list.Items.Count; i++) // Parcours de la ListBox
                        {
                            int index = critere.selected.IndexOf(list.Items[i]); // Recherche de l'item dans le filtre
                            list.SetItemChecked(i, !(index == -1));
                        }
                    }

                break;
            }

            // Affichage de l'étiquette
            searchFlowLayoutPanel.Visible = true;

            // Application du filtre
            afficheActions(filtre);
        }

        // Ouverture d'un filtre enregistré
        private void openFilter(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex > 0)
                this.showFilter(ReadDB.Instance.getFilter(filterCombo.Text));
        }

        // Validation de la recherche après click sur OK
        private void searchButton_Click(object sender, EventArgs e)
        {
            if (searchBox.Text != "")
                this.showFilter(new Filtre(searchBox.Text));
            else
                MessageBox.Show("Veuillez entrer un mot clé pour la recherche", "Recherche", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        // Permet la validation de la recherche par la touche ENTER
        private void searchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                this.searchButton_Click(sender,e);
        }

        // Suppression de la recherche après click sur l'étiquette
        private void exitSearchBut_Click(object sender, EventArgs e)
        {    
            // Si un filtre était actif avant la (ou les) recherche(s), on l'affiche
            if (Filtre.CurrentFilter.type == 2 && Filtre.OldFilter != null)
                this.showFilter(Filtre.OldFilter);
            else
            {
                // On cache l'étiquette de recherche et le label de résultat
                searchFlowLayoutPanel.Visible = false;
                resultLabel.Visible = false;
                grilleData.DataSource = null; // Suppression des lignes du tableau
                razFiltres();
            }
        }

        private void defaultValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AdminDefaut().Show();
        }
    }
}
