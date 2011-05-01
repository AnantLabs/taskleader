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
            this.Text += " v"+Application.ProductVersion;

            filterCombo.Items.AddRange(ReadDB.Instance.getFiltersName()); 
            
            foreach (object item in ReadDB.Instance.getCtxt())
                ctxtListBox.Items.Add(item,true); // Remplissage de la combobox contextes en sélectionnant par défaut

            foreach (object item in ReadDB.Instance.getDest()) // C'est pas bien mais il n'y a pas de code business
                destListBox.Items.Add(item,true); // Remplissage de la combobox destinataires en sélectionnant par défaut

            foreach (object item in ReadDB.Instance.getStatut()) // C'est pas bien mais il n'y a pas de code business
            {
                statutListBox.Items.Add(item, true); // Remplissage de la combobox statuts en sélectionnant par défaut
                statutTSMenuItem.DropDown.Items.Add(item.ToString(),null,this.changeStat); // Remplissage du DropDownMenu sur la liste
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

            // Si un filtre est actif on l'affiche
            if (Filtre.CurrentFilter != null)
                this.showFilter(Filtre.CurrentFilter);           
        }
        
        // Rafraîchissement de la page
        public void miseAjour(object sender, EventArgs e)
        {           
            // Mise à jour de la liste avec le dernier filtre joué
            if(Filtre.CurrentFilter != null)
                afficheActions(Filtre.CurrentFilter);

            /*
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
             */
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

            this.miseAjour(null,null);
        }

        // Méthode appelée quand checks des contextes changent
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
                foreach (object item in ReadDB.Instance.getSujet((String)ctxtListBox.CheckedItems[0].ToString()))
                    sujetListBox.Items.Add(item, true);
            }
            else
            {
                //Dans tous les autres cas on grise les radio buttons
                SujSelRadio.Enabled = false;
                SujAllRadio.Enabled = false;
                //Et on active "Sélection"
                //SujSelRadio.Checked = true;
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
            Filtre filtre = new Filtre(CtxtAllRadio.Checked,ctxtListBox.CheckedItems,SujAllRadio.Checked,sujetListBox.CheckedItems,destAllRadio.Checked,destListBox.CheckedItems,statAllRadio.Checked,statutListBox.CheckedItems);
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

        // Affichade d'un curseur doigt si mail attaché.
        private void grilleData_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            if (grilleData.Columns[e.ColumnIndex].Name.Equals("Mail") &&
                grilleData[e.ColumnIndex, e.RowIndex].Value.ToString() != "")
                this.Cursor = Cursors.Hand;
        }

        private void grilleData_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            if (grilleData.Columns[e.ColumnIndex].Name.Equals("Mail") &&
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
                Filtre filtre = new Filtre(CtxtAllRadio.Checked,ctxtListBox.CheckedItems,SujAllRadio.Checked,sujetListBox.CheckedItems,destAllRadio.Checked,destListBox.CheckedItems,statAllRadio.Checked,statutListBox.CheckedItems);
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

        //Routine générique permettant de griser les listes et sélectionner tous les items
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

        // Méthode générique utilisée pour appliquer un filtre dans les CheckedListBox
        private void checkSelection(RadioButton selRadio,CheckedListBox liste, IList criteres)
        {
            selRadio.Checked = true; // Sélection du radio Sélection
            for (int i = 0; i < liste.Items.Count; i++) // Parcours de la ListBox
            {
                int index = criteres.IndexOf(liste.Items[i]); // Recherche de l'item dans le filtre
                liste.SetItemChecked(i, !(index == -1));
            }    
        }

        /// <summary>
        /// Application d'un filtre sur les différents widgets + affichage
        /// </summary>
        private void showFilter(Filtre filtre)
        {              
            // Passage de toutes les checkboxes à ALL
            CtxtAllRadio.Checked = true;
            destAllRadio.Checked = true;
            statAllRadio.Checked = true;

            // Tickage des bons critères
            foreach (Criterium critere in filtre.criteria)
            {
                switch (critere.champ)
                {
                    case(0): //Contexte
                        checkSelection(CtxtSelRadio, ctxtListBox, critere.selected);                          
                        break;
                    case(1): //Sujet
                        checkSelection(SujSelRadio, sujetListBox, critere.selected);                          
                        break;
                    case(2): //Destinataire
                        checkSelection(destSelRadio, destListBox, critere.selected);                          
                        break;
                    case(3): //Statut
                        checkSelection(statSelRadio, statutListBox, critere.selected);
                        break;
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

        
    }
}
