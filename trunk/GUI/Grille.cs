using System;
using System.Configuration;
using System.Data;
using System.Collections.Specialized;
using System.Linq;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using TaskLeader.BO;
using TaskLeader.DAL;
using TaskLeader.BLL;

namespace TaskLeader.GUI
{
    public partial class Grille : UserControl
    {
        // DataSet contenant toutes les DataTable correspondant aux filtres affichés
        private Dictionary<Filtre, DataTable> data = new Dictionary<Filtre, DataTable>();
        /// <summary>
        /// Retourne le nom des tables affichant des actions de la base en paramètre
        /// </summary>
        /// <param name="db">Nom de la base</param>
        private Filtre[] getTables(String db) //TODO: nom pas adapté
        {
            // Source: http://stackoverflow.com/questions/2968356/linq-transform-dictionarykey-value-to-dictionaryvalue-key
            // ou http://stackoverflow.com/questions/146204/duplicate-keys-in-net-dictionaries
            return this.data
                .ToLookup(kp => kp.Key.dbName, kp => kp.Key)[db] // Inversion key/value avec plusieurs keys identiques (dbName) + select la clé db
                .ToArray();
        }

        // Récupération de la DataSource de grilleData
        private DataTable mergeTable { get { return this.grilleData.DataSource as DataTable; } }

        private int P1length = Int32.Parse(ConfigurationManager.AppSettings["P1length"]);
        private DataGridViewImageColumn linkCol = new DataGridViewImageColumn();

        public Grille()
        {
            InitializeComponent();

            this.grilleData.DataSource = new DataTable();

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

        # region Business

        /// <summary>
        /// Ajoute les actions retournées par le filtre dans le tableau.
        /// </summary>
        /// <param name="filtre">Filtre ajouté</param>
        public int add(Filtre filtre)
        {
            // Ajout si nécessaire de la colonne Mail
            if (!grilleData.Columns.Contains("Liens"))
                grilleData.Columns.Add(linkCol);

            // Récupération des résultats du filtre et association au tableau
            data.Add(filtre, filtre.getActions());
            this.mergeTable.Merge(this.data[filtre]);

            this.mergeTable.DefaultView.Sort = "Date ASC";

            // Mise en forme du tableau
            grilleData.Columns["id"].Visible = false;
            grilleData.Columns["DB"].Visible = false;
            grilleData.Columns["Deadline"].Visible = false;
            grilleData.Columns["Liens"].DisplayIndex = 5;
            grilleData.Columns["Date"].DisplayIndex = 6;
            grilleData.Columns["Titre"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Hook des éditions d'actions de la base correspondante
            TrayIcon.dbs[filtre.dbName].ActionEdited += new EventHandler(actionEdited);
            //this.DBdisplayed.Add(filtre.getUniqueName(), filtre.dbName);

            return this.mergeTable.Rows.Count;
        }

        /// <summary>
        /// Supprime les actions retournées par le filtre du tableau
        /// </summary>
        /// <param name="filtre">Filtre à supprimer</param>
        public int remove(Filtre filtre)
        {
            this.data.Remove(filtre); // Suppression de la table du DataSet

            TrayIcon.dbs[filtre.dbName].ActionEdited -= new EventHandler(actionEdited);
            //this.DBdisplayed.Remove(filtre.getUniqueName());

            this.mergeTable.Clear(); // Efface toutes les données de la table merge
            foreach (DataTable table in this.data.Values)
                this.mergeTable.Merge(table); // Merge des tables restants dans le dataset

            //TODO: le header de la table restera dans ce cas là

            return this.mergeTable.Rows.Count;
        }

        /// <summary>
        /// Mise à jour du contenu de la table quand une action est créée/modifiée
        /// </summary>
        private void actionEdited(object sender, EventArgs e)
        {
            //TODO: mettre à jour toutes les DataTable de la base correspondante
            foreach (Filtre filtre in this.getTables(((TLaction)sender).dbName))
                this.data[filtre] = filtre.getActions().Copy();

            this.mergeTable.Clear();
            foreach (DataTable table in this.data.Values)
                this.mergeTable.Merge(table); // Merge des tables restants dans le dataset

            //TODO: sélectionner l'action dans le tableau si présente
        }

        /// <summary>
        /// Remise à zéro du tableau d'actions
        /// </summary>
        public void raz()
        {
            this.data = null; // Suppression des lignes du tableau
            //TODO: il faut aussi vider le dataset
        }

        // Ouverture de la gui édition d'action
        private void modifAction(object sender, EventArgs e)
        {
            new ManipAction(new TLaction(this.grilleData.SelectedRows[0].Cells["id"].Value.ToString(), this.grilleData.SelectedRows[0].Cells["DB"].Value.ToString())).Show();
            //TrayIcon.displayNewAction(action);//TODO: à supprimer par la suite
        }

        #endregion

        #region grilleData

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

        private void selection(String id = null)
        {
            grilleData.Focus(); // Focus au tableau pour permettre le scroll direct

            // Sélection de l'action si refresh suite à modification d'action 
            if (grilleData.Tag != null && grilleData.Tag.ToString() != "") // ID de l'action stocké dans le tag
            {
                DataRow[] rows = this.mergeTable.Select("id=" + grilleData.Tag.ToString());
                if (rows.Length == 1)
                    grilleData.Rows[this.mergeTable.Rows.IndexOf(rows[0])].Selected = true;
                grilleData.Tag = null; // Remise à zéro du tag
            }
        }

        #endregion

        #region listeContext

        private void listeContext_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Remplissage du menu des différents statuts
            statutTSMenuItem.DropDown.Items.Clear();

            DB db = TrayIcon.dbs[grilleData.SelectedRows[0].Cells["DB"].Value.ToString()];
            foreach (object item in db.getTitres(DB.statut)) //TODO: mettre en cache la valeur des statuts
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

            //this.selectedActionID = grilleData.SelectedRows[0].Cells["id"].Value.ToString(); //TODO: à supprimer
            //this.miseAjour(false); //TODO: à supprimer
        }

        // Copie de l'action dans le presse-papier
        private void exportRow(object sender, EventArgs e)
        {
            Export.Instance.clipAction(((ToolStripItem)sender).Text, grilleData.SelectedRows[0]);
        }

        #endregion

        #region linksContext

        // Ouverture du lien
        private void linksContext_ItemClicked(object sender, EventArgs e)
        {
            ((Enclosure)((ToolStripMenuItem)sender).Tag).open();
        }

        #endregion

        #region DatePickerPopup

        // Gestion de la fermeture de la pop-up changement de date
        private void popup_Closed(Object sender, ToolStripDropDownClosedEventArgs e)
        {
            if (e.CloseReason == ToolStripDropDownCloseReason.ItemClicked)
            {
                // Mémorisation de ligne sélectionnée
                //this.selectedActionID = grilleData.SelectedRows[0].Cells["id"].Value.ToString(); //TODO: à supprimer
                //TODO: this.miseAjour(false);
            }
        }

        #endregion

    }
}
