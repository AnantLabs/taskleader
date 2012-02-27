using System;
using System.Data;
using System.Collections;
using System.Collections.Generic;
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
        // Variables locales identifiant la base courante
        //private String dbName { get { return TrayIcon.currentDB; } }
        private DB db { get { return TrayIcon.defaultDB; } }

        private DataGridViewImageColumn linkCol = new DataGridViewImageColumn();
        private int P1length = Int32.Parse(ConfigurationManager.AppSettings["P1length"]);

        public String selectedActionID { set { grilleData.Tag = value; } }

        /// <summary>
        /// Dictionnaire des widgets de sélection des critères
        /// </summary>
        private Dictionary<string, MultipleSelect> criteresSelect = new Dictionary<string, MultipleSelect>();

        public Toolbox()
        {
            InitializeComponent();
        }

        /// <summary>Chargement des différents composants au lancement de la toolbox</summary>
        private void Toolbox_Load(object sender, EventArgs e)
        {
            // Remplissage de la liste des bases d'action disponibles
            foreach (String nomBase in TrayIcon.dbs.Keys)
            {
                manuelDBcombo.Items.Add(TrayIcon.dbs[nomBase]);
                ToolStripMenuItem dbItem = new ToolStripMenuItem(nomBase);
                dbItem.Checked = TrayIcon.activeDBs.Contains(nomBase);
                dbItem.CheckOnClick = true;
                dbItem.CheckedChanged += new EventHandler(changeActiveDBs);
                this.baseToolStripMenuItem.DropDown.Items.Add(dbItem);
            }

            // -------------------
            // Tab 'filtre manuel'
            // -------------------

            // Création des MultipleSelect
            this.criteresSelect.Add("contextes", new MultipleSelect("Contextes", DB.contexte));
            this.criteresSelect.Add("sujets", new MultipleSelect("Sujets", DB.sujet));
            this.criteresSelect["sujets"].addParent(this.criteresSelect["contextes"]);
            this.criteresSelect.Add("destinataires", new MultipleSelect("Destinataires", DB.destinataire));
            this.criteresSelect.Add("statuts", new MultipleSelect("Statuts", DB.statut));
            foreach(Control control in this.criteresSelect.Values)
                this.selectPanel.Controls.Add(control);

            this.manuelDBcombo.Text = TrayIcon.defaultDB.name;
            // ATTENTION: déclenche la mise à jour de toutes les MuitipleSelect!!

            // Remplissage de la combo des filtres
            this.loadFilters();

            // Remplissage de la ListBox des statuts + menu contextuel du tableau
            foreach (object item in db.getTitres(DB.statut))
                statutTSMenuItem.DropDown.Items.Add(item.ToString(), null, this.changeStat);

            ((ToolStripDropDownMenu)statutTSMenuItem.DropDown).ShowImageMargin = false;

            // Création de la colonne des liens
            linkCol.Name = "Liens";
            linkCol.DataPropertyName = "Liens";

            // On rajoute les lignes qu'il faut dans le contextMenu de la liste d'actions
            NameValueCollection section = (NameValueCollection)ConfigurationManager.GetSection("Export");
            // Affichage de l'item dans le menu uniquement si une valeur d'export
            this.exportMenuItem.Visible = (section.Count > 0);
            foreach (string key in section)
                this.exportMenuItem.DropDown.Items.Add(key, null, this.exportRow);
            ((ToolStripDropDownMenu)exportMenuItem.DropDown).ShowImageMargin = false;

            // Remplissage des dernières ListBox
            //this.miseAjour(true);
        }

        /// <summary>
        /// Modifie la liste des bases actives
        /// </summary>
        private void changeActiveDBs(object sender, EventArgs e)
        {
            // Mise à jour de la liste des bases actives
            if (((ToolStripMenuItem)sender).Checked) // La base vient d'être activée
                TrayIcon.activeDBs.Add(sender.ToString());
            else
                TrayIcon.activeDBs.Remove(sender.ToString());

            // Mise à jour de la toolbox
            this.razTableau(); //TODO: Pourquoi? 
            this.loadFilters();
            this.miseAjour(true); //TODO: Pourquoi?
        }

        /// <summary>
        /// Met à jour la liste des filtres pour la DB courante
        /// </summary>
        private void loadFilters()
        {
            // Suppression des valeurs courantes
            this.dbsTree.BeginUpdate();
            this.dbsTree.Nodes.Clear();
            
            // Récupération des filtres des bases actives         
            foreach (String nomDB in TrayIcon.activeDBs)
            {
                ArrayList filtres = new ArrayList();
                foreach (Filtre filtre in TrayIcon.dbs[nomDB].getFilters())
                {
                    TreeNode node = new TreeNode(filtre.nom);
                    node.Tag = filtre;
                    filtres.Add(node);
                }
                dbsTree.Nodes.Add(new TreeNode(nomDB, (TreeNode[])filtres.ToArray(typeof(TreeNode))));
            }

            this.dbsTree.EndUpdate();
        }

        /// <summary>
        /// Rafraîchissment de la toolbox
        /// </summary>
        /// <param name="fullUpdate">true si mise à jour des contextes et destinataires</param>
        public void miseAjour(bool fullUpdate)
        {
            if (fullUpdate)
            {
                // Mise à jour des contextes
                this.criteresSelect["contextes"].maj(db);
                // Mise à jour des destinataires
                this.criteresSelect["destinataires"].maj(db);
            }

            // Si un filtre est actif on l'affiche
            if (this.db.CurrentFilter != null)
                this.showFilter(this.db.CurrentFilter);
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
            TrayIcon.displayNewAction(new TLaction(grilleData.SelectedRows[0].Cells["id"].Value.ToString(), grilleData.SelectedRows[0].Cells["DB"].Value.ToString()));
        }

        // Mise à jour du statut d'une action via le menu contextuel
        private void changeStat(object sender, EventArgs e)
        {
            // Récupération de l'action
            TLaction action = new TLaction(grilleData.SelectedRows[0].Cells["id"].Value.ToString(), grilleData.SelectedRows[0].Cells["DB"].Value.ToString());

            // On récupère le nouveau statut
            action.Statut = ((ToolStripItem)sender).Text;

            // On met à jour le statut de l'action que s'il a changé
            if (action.statusHasChanged)
                action.save();

            this.selectedActionID = grilleData.SelectedRows[0].Cells["id"].Value.ToString();
            this.miseAjour(false);
        }

        // Mise en forme des cellules sous certaines conditions
        private void grilleData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DateTime date;

            // Association du tooltip
            if (grilleData.Columns[e.ColumnIndex].Name.Equals("Deadline"))
                grilleData[e.ColumnIndex, e.RowIndex].ToolTipText = "Modifier la date";

            // Gestion de la colonne Deadline
            if (grilleData.Columns[e.ColumnIndex].Name.Equals("Deadline") && DateTime.TryParse(e.Value.ToString(), out date))
            {
                // Récupération du delta en jours
                int diff = (date.Date - DateTime.Now.Date).Days;

                // Modification du contenu des cellules
                if (diff == 0) // Aujourd'hui
                    e.Value = e.Value.ToString() + Environment.NewLine + "Today"; // Valeur modifiée      
                else if (diff > 0)// Dans le futur
                    e.Value = e.Value.ToString() + Environment.NewLine + "+ " + diff.ToString() + " jours"; // Valeur modifiée

                // Modification de la mise en forme des cellules
                if (diff < 0) // En retard
                {
                    e.CellStyle.ForeColor = Color.Red; // Affichage de la date en rouge
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold); // en gras
                    e.CellStyle.SelectionForeColor = Color.DarkRed; // en darkRed sur séléction                    
                }
                else if (diff == 0) // Jour même
                {
                    e.CellStyle.ForeColor = Color.DarkOrange; // Affichage de la date en orange
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold);
                    e.CellStyle.SelectionForeColor = Color.SaddleBrown; // en darkRed sur séléction 
                }
                else if (diff > 0 && diff <= P1length) // Dans le futur "proche"
                {
                    e.CellStyle.ForeColor = Color.DarkGreen;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold); // en gras
                }
            }

            // Gestion de la colonne PJ
            if (grilleData.Columns[e.ColumnIndex].Name.Equals("Liens"))
            {
                switch (e.Value.ToString())
                {
                    case ("0"):
                        e.Value = null; // Vidage la cellule
                        e.CellStyle.NullValue = null; // Aucun affichage si cellule vide
                        break;
                    case ("1"):
                        // Récupération de la PJ
                        Enclosure pj = (Enclosure)db.getPJ(grilleData.Rows[e.RowIndex].Cells["id"].Value.ToString()).GetValue(0);
                        e.Value = pj.Icone; // Affichage de la bonne icône
                        grilleData[e.ColumnIndex, e.RowIndex].ToolTipText = pj.Titre; // Modification du tooltip de la cellule
                        grilleData.Rows[e.RowIndex].Tag = pj; // Tag de la DataGridRow
                        break;
                    default:
                        // On diffère la récupération de liste
                        e.Value = TaskLeader.Properties.Resources.PJ;
                        break;
                }
            }
        }

        // Gestion des clicks sur le tableau d'actions
        private void grilleData_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && // Click droit
                e.RowIndex >= 0) // Ce n'est pas la ligne des headers
            {
                grilleData.Rows[e.RowIndex].Selected = true; // Sélection de la ligne           
                listeContext.Show(Cursor.Position); // Affichage du menu contextuel
            }

            if (e.Button == MouseButtons.Left && // Click gauche
                grilleData.Columns[e.ColumnIndex].Name.Equals("Liens") && // Colonne "Liens"
                e.RowIndex >= 0 && // Ce n'est pas la ligne des headers
                grilleData[e.ColumnIndex, e.RowIndex].Value.ToString() != "0") // Cellule non vide
            {
                if (grilleData[e.ColumnIndex, e.RowIndex].Value.ToString() == "1") // Un lien seulement
                    ((Enclosure)grilleData.Rows[e.RowIndex].Tag).open(); // Ouverture directe
                else // Plusieurs liens
                {
                    Array links = db.getPJ(grilleData.SelectedRows[0].Cells["id"].Value.ToString()); //Récupération des différents liens
                    linksContext.Items.Clear(); // Remise à zéro de la liste

                    foreach (Enclosure link in links)
                    {
                        ToolStripMenuItem item = new ToolStripMenuItem(link.Titre, link.Icone, this.linksContext_ItemClicked); // Création du lien avec le titre et l'icône
                        item.Tag = link; // Association du link
                        linksContext.Items.Add(item); // Ajout au menu
                    }

                    linksContext.Show(Cursor.Position); // Affichage du menu contextuel de liste
                }
            }

            if (e.Button == MouseButtons.Left && // Click gauche
                grilleData.Columns[e.ColumnIndex].Name.Equals("Deadline") && // Colonne "Liens"
                e.RowIndex >= 0) // Ce n'est pas la ligne des headers // Cellule non vide
            {
                grilleData.Cursor = Cursors.Default;
                DatePickerPopup popup = new DatePickerPopup(new TLaction(grilleData.SelectedRows[0].Cells["id"].Value.ToString(), grilleData.SelectedRows[0].Cells["DB"].Value.ToString()));
                popup.Closed += new ToolStripDropDownClosedEventHandler(popup_Closed);
                popup.Show();
            }
        }

        // Gestion de la fermeture de la pop-up changement de date
        private void popup_Closed(Object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
            {
                // Mémorisation de ligne sélectionnée
                this.selectedActionID = grilleData.SelectedRows[0].Cells["id"].Value.ToString();
                this.miseAjour(false);
            }
        }

        // Ouverture du lien
        private void linksContext_ItemClicked(object sender, EventArgs e)
        {
            ((Enclosure)((ToolStripMenuItem)sender).Tag).open();
        }

        // Affichage d'un curseur doigt si mail attaché.
        private void grilleData_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            bool pjActivated =
                grilleData.Columns[e.ColumnIndex].Name.Equals("Liens") &&
                e.RowIndex >= 0 &&
                grilleData[e.ColumnIndex, e.RowIndex].Value.ToString() != "0";

            bool dateActivated =
                grilleData.Columns[e.ColumnIndex].Name.Equals("Deadline") &&
                e.RowIndex >= 0;

            if (pjActivated || dateActivated)
                grilleData.Cursor = Cursors.Hand;
        }

        private void grilleData_CellMouseLeave(object sender, DataGridViewCellEventArgs e)
        {
            bool pjActivated =
                grilleData.Columns[e.ColumnIndex].Name.Equals("Liens") &&
                e.RowIndex >= 0 &&
                grilleData[e.ColumnIndex, e.RowIndex].Value.ToString() != "0";

            bool dateActivated =
                grilleData.Columns[e.ColumnIndex].Name.Equals("Deadline") &&
                e.RowIndex >= 0;

            if (pjActivated || dateActivated)
                grilleData.Cursor = Cursors.Default;
        }

        // Méthode générique d'affichage de la liste d'actions à partir d'un filtre
        private void afficheActions(Filtre filtre)
        {
            // Ajout si nécessaire de la colonne Mail
            if (!grilleData.Columns.Contains("Liens"))
                grilleData.Columns.Add(linkCol);

            // Récupération des résultats du filtre et association au tableau
            DataTable liste = filtre.getActions();
            grilleData.DataSource = liste;

            // Mise en forme du tableau
            grilleData.Columns["id"].Visible = false;
            grilleData.Columns["DB"].Visible = false;
            grilleData.Columns["Liens"].DisplayIndex = 5;
            grilleData.Columns["Titre"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Définition du label de résultat
            if (liste.Rows.Count == 0)
                resultLabel.Text = "Aucune action trouvée";
            else if (liste.Rows.Count == 1)
                resultLabel.Text = "1 action trouvée";
            else
                resultLabel.Text = liste.Rows.Count.ToString() + " actions trouvées";
            // Affichage du label
            resultLabel.Visible = true;

            grilleData.Focus(); // Focus au tableau pour permettre le scroll direct

            // Sélection de l'action si refresh suite à modification d'action
            if (grilleData.Tag != null && grilleData.Tag.ToString() != "") // ID de l'action stocké dans le tag
            {
                DataRow[] rows = liste.Select("id=" + grilleData.Tag.ToString());
                if (rows.Length == 1)
                    grilleData.Rows[liste.Rows.IndexOf(rows[0])].Selected = true;
                grilleData.Tag = null; // Remise à zéro du tag
            }
        }

        // Affichage des actions sur filtre manuel
        private void filtreAction(object sender, EventArgs e)
        {
            Filtre filtre = new Filtre(manuelDBcombo.Text);
            foreach (MultipleSelect widget in this.criteresSelect.Values)
                filtre.addCriterium(widget.getCriterium());

            if (saveFilterCheck.Checked) //Sauvegarde du filtre si checkbox cochée
            {
                if (nameBox.Text == "")
                {
                    errorLabel.Text = "Le nom du filtre ne peut être vide";
                    errorLabel.Visible = true;
                }
                else
                {
                    DB db = (DB)manuelDBcombo.Items[manuelDBcombo.SelectedIndex];
                    if (!db.isNvo(DB.filtre, nameBox.Text))
                    {
                        errorLabel.Text = "Ce nom de filtre existe déjà.";
                        errorLabel.Visible = true;
                    }
                    else
                    {
                        // Sauvegarde du filtre
                        filtre.nom = nameBox.Text;
                        db.insertFiltre(filtre);

                        // Raz du formulaire
                        saveFilterCheck.Checked = false;
                        nameBox.Text = "";

                        // Mise à jour de la liste des filtres
                        this.loadFilters();

                        // Affichage du filtre
                        this.showFilter(filtre);
                    }
                }
            }
            else
                this.showFilter(filtre);            
        }

        // Copie de l'action dans le presse-papier
        private void exportRow(object sender, EventArgs e)
        {
            Export.Instance.clipAction(((ToolStripItem)sender).Text, grilleData.SelectedRows[0]);
        }

        // Remise à zéro du tableau d'actions
        private void razTableau()
        {
            // On cache l'étiquette de recherche et le label de résultat
            //TODO: supprimer toutes les étiquettes
            resultLabel.Visible = false;
            grilleData.DataSource = null; // Suppression des lignes du tableau
        }

        // Ouverture d'un filtre enregistré
        private void openFilter(object sender, EventArgs e)
        {
            if (((ComboBox)sender).SelectedIndex > 0) // Pour éviter le "Sélectionner..."
                this.showFilter(((Filtre)((ComboBox)sender).SelectedItem)); // La combobox contient des objets Filtre
        }

        /// <summary>
        /// Application d'un filtre: affichage des actions correspondantes
        /// </summary>
        private void showFilter(Filtre filtre)
        {
            statusPanel.Controls.Add(new Etiquette(filtre));

            // Application du filtre
            afficheActions(filtre);
        }

        // Validation de la recherche après click sur OK
        private void searchButton_Click(object sender, EventArgs e)
        {
            if (searchBox.Text != "")
                this.showFilter(new Filtre(searchBox.Text, this.db.name));
            else
                MessageBox.Show("Veuillez entrer un mot clé pour la recherche", "Recherche", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        // Permet la validation de la recherche par la touche ENTER
        private void searchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                this.searchButton_Click(sender, e);
        }

        // Suppression de la recherche après click sur l'étiquette
        private void exitSearchBut_Click(object sender, EventArgs e)
        {
            // Si un filtre était actif avant la (ou les) recherche(s), on l'affiche
            //if (((Filtre)this.typeLabel.Tag).type == 2 && this.db.OldFilter != null)
            //    this.showFilter(this.db.OldFilter);
            //else
            //{
            //    this.db.CurrentFilter = null;
            //    razTableau();
            //}
        }

        private void defaultValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new AdminDefaut(this.db.name).Show();
        }

        private void hideCollapse(object sender, EventArgs e)
        {
            String state = this.button1.Text;

            if (state == "^" && sender.GetType() == typeof(Button)) // Bandeau déployé
            {
                this.tabControl1.Appearance = TabAppearance.FlatButtons;
                RowStyle small = new RowStyle(SizeType.Absolute, 30);
                this.mainTableLayout.RowStyles[0] = small;
                this.button1.Text = "v";
            }
            else // Bandeau replié
            {
                this.tabControl1.Appearance = TabAppearance.Normal;
                RowStyle big = new RowStyle(SizeType.Absolute, 155);
                this.mainTableLayout.RowStyles[0] = big;
                this.button1.Text = "^";
            }
        }

        /// <summary>
        /// Changement de DB dans le tab 'Filtre manuel'
        /// </summary>
        private void manuelDBcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(MultipleSelect widget in this.criteresSelect.Values)
                widget.maj((DB)manuelDBcombo.Items[manuelDBcombo.SelectedIndex]);
        }

        private void saveFilterCheck_CheckedChanged(object sender, EventArgs e)
        {
            this.nameBox.Enabled = this.saveFilterCheck.Checked;
        }

        private void nameBox_Enter(object sender, EventArgs e)
        {
            errorLabel.Visible = false;
        }
    }
}
