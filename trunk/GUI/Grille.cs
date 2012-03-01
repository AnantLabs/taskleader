using System;
using System.Configuration;
using System.Data;
using System.Collections.Specialized;
using System.Drawing;
using System.Windows.Forms;
using TaskLeader.BO;
using TaskLeader.DAL;
using TaskLeader.BLL;

namespace TaskLeader.GUI
{
    public partial class Grille : UserControl
    {
        private DataTable data;

        private int P1length = Int32.Parse(ConfigurationManager.AppSettings["P1length"]);
        private DataGridViewImageColumn linkCol = new DataGridViewImageColumn();

        public String selectedActionID { set { grilleData.Tag = value; } }

        public Grille()
        {
            InitializeComponent();

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

        }

        # region  Business

        /// <summary>
        /// Ajoute les actions retournées par le filtre dans le tableau.
        /// </summary>
        /// <param name="filtre">Filtre demandé</param>
        public int add(Filtre filtre)
        {
            // Ajout si nécessaire de la colonne Mail
            if (!grilleData.Columns.Contains("Liens"))
                grilleData.Columns.Add(linkCol);

            // Récupération des résultats du filtre et association au tableau
            DataTable liste = filtre.getActions();

            if (this.data == null) // Premier appel à la fonction add
            {
                this.data = liste;
                this.grilleData.DataSource = data;
            }
            else
                this.data.Merge(liste);

            //TODO: il faudrait retrier sur la date
            data.DefaultView.Sort = "Date ASC";

            // Mise en forme du tableau
            grilleData.Columns["id"].Visible = false;
            grilleData.Columns["DB"].Visible = false;
            grilleData.Columns["Deadline"].Visible = false;
            grilleData.Columns["Liens"].DisplayIndex = 5;
            grilleData.Columns["Date"].DisplayIndex = 6;
            grilleData.Columns["Titre"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            grilleData.Focus(); // Focus au tableau pour permettre le scroll direct

            // Sélection de l'action si refresh suite à modification d'action
            if (grilleData.Tag != null && grilleData.Tag.ToString() != "") // ID de l'action stocké dans le tag
            {
                DataRow[] rows = liste.Select("id=" + grilleData.Tag.ToString());
                if (rows.Length == 1)
                    grilleData.Rows[liste.Rows.IndexOf(rows[0])].Selected = true;
                grilleData.Tag = null; // Remise à zéro du tag
            }

            return data.Rows.Count;
        }

        // Remise à zéro du tableau d'actions
        public void raz()
        {
            this.data = null; // Suppression des lignes du tableau
            this.grilleData.DataSource = null;
        }

         #endregion

        #region Widgets events

        // ----------- grilleData -----------

        // Mise en forme des cellules sous certaines conditions
        private void grilleData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DateTime date;

            // Association du tooltip
            if (grilleData.Columns[e.ColumnIndex].Name.Equals("Date"))
                grilleData[e.ColumnIndex, e.RowIndex].ToolTipText = "Modifier la date";

            // Gestion de la colonne Deadline
            if (grilleData.Columns[e.ColumnIndex].Name.Equals("Date") && DateTime.TryParse(e.Value.ToString(), out date))
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
                        DB db = TrayIcon.dbs[grilleData.SelectedRows[0].Cells["DB"].Value.ToString()];
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
                    DB db = TrayIcon.dbs[grilleData.SelectedRows[0].Cells["DB"].Value.ToString()];
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
                grilleData.Columns[e.ColumnIndex].Name.Equals("Date") && // Colonne "Date"
                e.RowIndex >= 0) // Ce n'est pas la ligne des headers // Cellule non vide
            {
                grilleData.Cursor = Cursors.Default;
                DatePickerPopup popup = new DatePickerPopup(new TLaction(grilleData.SelectedRows[0].Cells["id"].Value.ToString(), grilleData.SelectedRows[0].Cells["DB"].Value.ToString()));
                popup.Closed += new ToolStripDropDownClosedEventHandler(popup_Closed);
                popup.Show();
            }
        }

        // Affichage d'un curseur doigt si mail attaché.
        private void grilleData_CellMouseEnter(object sender, DataGridViewCellEventArgs e)
        {
            bool pjActivated =
                grilleData.Columns[e.ColumnIndex].Name.Equals("Liens") &&
                e.RowIndex >= 0 &&
                grilleData[e.ColumnIndex, e.RowIndex].Value.ToString() != "0";

            bool dateActivated =
                grilleData.Columns[e.ColumnIndex].Name.Equals("Date") &&
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
                grilleData.Columns[e.ColumnIndex].Name.Equals("Date") &&
                e.RowIndex >= 0;

            if (pjActivated || dateActivated)
                grilleData.Cursor = Cursors.Default;
        }

        // Ouverture de la gui édition d'action
        private void modifAction(object sender, DataGridViewCellEventArgs e)
        {
            TrayIcon.displayNewAction(new TLaction(grilleData.SelectedRows[0].Cells["id"].Value.ToString(), grilleData.SelectedRows[0].Cells["DB"].Value.ToString()));
        }

        // ------------- listeContext -------------

        private void listeContext_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Remplissage du menu des différents statuts
            DB db = TrayIcon.dbs[grilleData.SelectedRows[0].Cells["DB"].Value.ToString()];
            foreach (object item in db.getTitres(DB.statut))
                statutTSMenuItem.DropDown.Items.Add(item.ToString(), null, this.changeStat);

            ((ToolStripDropDownMenu)statutTSMenuItem.DropDown).ShowImageMargin = false;
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
            //this.miseAjour(false);
        }

        // Copie de l'action dans le presse-papier
        private void exportRow(object sender, EventArgs e)
        {
            Export.Instance.clipAction(((ToolStripItem)sender).Text, grilleData.SelectedRows[0]);
        }

        // ------------- linksContext -------------

        // Ouverture du lien
        private void linksContext_ItemClicked(object sender, EventArgs e)
        {
            ((Enclosure)((ToolStripMenuItem)sender).Tag).open();
        }

        // ------------- DatePickerPopup -------------

        // Gestion de la fermeture de la pop-up changement de date
        private void popup_Closed(Object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
            {
                // Mémorisation de ligne sélectionnée
                this.selectedActionID = grilleData.SelectedRows[0].Cells["id"].Value.ToString();
                //TODO: this.miseAjour(false);
            }
        }

        #endregion
    
    }
}
