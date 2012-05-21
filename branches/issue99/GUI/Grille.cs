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
        /// <summary>
        /// Dictionnaire filtre affiché => DataTable associé
        /// </summary>
        private Dictionary<Filtre, DataTable> data = new Dictionary<Filtre, DataTable>();
        /// <summary>
        /// Retourne le nom des tables affichant des actions de la base en paramètre
        /// </summary>
        /// <param name="db">Nom de la base</param>
        private Filtre[] getFiltersFromDB(String db)
        {
            // Source: http://stackoverflow.com/questions/2968356/linq-transform-dictionarykey-value-to-dictionaryvalue-key
            // ou http://stackoverflow.com/questions/146204/duplicate-keys-in-net-dictionaries
            return this.data
                .ToLookup(kp => kp.Key.dbName, kp => kp.Key)[db] // Key= Nom de la base, Value= Filtres correspondant à cette base
                .ToArray();
        }

        // Récupération de la DataSource de grilleData
        private DataTable mergeTable { get { return this.grilleData.DataSource as DataTable; } }

        /// <summary>
        /// Retourne une DataGridViewTextBoxColumn à partir du nom de la colonne fournie
        /// </summary>
        private DataGridViewTextBoxColumn createSimpleColumn(String name)
        {
            DataGridViewTextBoxColumn col = new DataGridViewTextBoxColumn();
            col.Name = name;
            col.DataPropertyName = name;

            return col;
        }

        public Grille()
        {
            InitializeComponent();

            this.grilleData.AutoGenerateColumns = false; //Les colonnes sont créées manuellement

            grilleData.Columns.Insert(0, this.createSimpleColumn("Ref"));
            grilleData.Columns.Insert(1, this.createSimpleColumn("Contexte"));
            grilleData.Columns.Insert(2, this.createSimpleColumn("Sujet"));

            grilleData.Columns.Insert(3, this.createSimpleColumn("Titre"));
            grilleData.Columns["Titre"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleLeft;

            // Création de la colonne des liens
            DataGridViewImageColumn linkCol = new DataGridViewImageColumn();
            linkCol.Name = "Liens";
            linkCol.DataPropertyName = "Liens";
            grilleData.Columns.Insert(4, linkCol);

            grilleData.Columns.Insert(5, this.createSimpleColumn("Deadline"));
            grilleData.Columns.Insert(6, this.createSimpleColumn("Destinataire"));
            grilleData.Columns.Insert(7, this.createSimpleColumn("Statut"));

            this.grilleData.DataSource = new DataTable();

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
            this.data.Add(filtre, filtre.getActions()); // Récupération des résultats du filtre et association au tableau
            TrayIcon.dbs[filtre.dbName].ActionEdited += new ActionEditedEventHandler(actionEdited); // Hook des éditions d'actions de la base correspondante

            this.mergeTable.Merge(this.data[filtre]);

            this.mergeTable.DefaultView.Sort = "Deadline ASC"; // Tri sur les dates à chaque ajout (même si un autre filtre est en place)
            this.grilleData.Focus();

            return this.mergeTable.Rows.Count;
        }

        /// <summary>
        /// Supprime les actions retournées par le filtre du tableau
        /// </summary>
        /// <param name="filtre">Filtre à supprimer</param>
        public int remove(Filtre filtre)
        {
            this.data.Remove(filtre); // Suppression de la table du DataSet
            TrayIcon.dbs[filtre.dbName].ActionEdited -= new ActionEditedEventHandler(actionEdited);

            this.mergeTable.Clear(); // Efface toutes les données de la table merge
            foreach (DataTable table in this.data.Values)
                this.mergeTable.Merge(table); // Merge des tables restants dans le dataset

            return this.mergeTable.Rows.Count;
        }

        /// <summary>
        /// Mise à jour du contenu de la table quand une action est créée/modifiée
        /// </summary>
        private void actionEdited(String db, String id)
        {
            // Mise à jour des DataTables liées à la base de l'action
            foreach (Filtre filtre in this.getFiltersFromDB(db))
                this.data[filtre] = filtre.getActions().Copy();

            // Rafraîchissement de la mergeTable
            this.mergeTable.Clear();
            foreach (DataTable table in this.data.Values)
                this.mergeTable.Merge(table);

            this.grilleData.Focus(); // Focus sur tableau pour permettre le scroll direct

            // Sélection de la bonne ligne
            this.grilleData.Rows.Cast<DataGridViewRow>()
                .Where(r => r.Cells["Ref"].Value.ToString().Equals(db + Environment.NewLine + "#" + id))
                .ToList().ForEach(r => r.Selected = true);
        }

        // Ouverture de la gui édition d'action
        private void modifAction(object sender, EventArgs e)
        {
            new ManipAction(
                new TLaction(
                    this.getDataFromRow(this.grilleData.SelectedRows[0].Index, "id"),
                    this.getDataFromRow(this.grilleData.SelectedRows[0].Index, "DB")
                )
            ).Show();
        }

        #endregion

        #region grilleData

        /// <summary>
        /// Retourne une donnée de la DataTable à partir de l'index dans la grille
        /// </summary>
        /// <param name="index">Index de la ligne dans la grille</param>
        /// <param name="col">Nom du champ</param>
        private String getDataFromRow(int index, String col)
        {
            return ((DataRowView)grilleData.Rows[index].DataBoundItem).Row[col].ToString();
        }

        // Mise en forme des cellules sous certaines conditions
        private void grilleData_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            DateTime date;

            // Association du tooltip
            if (grilleData.Columns[e.ColumnIndex].Name.Equals("Deadline"))
                grilleData[e.ColumnIndex, e.RowIndex].ToolTipText = "Modifier la date";

            #region Gestion de la colonne Deadline
            if (grilleData.Columns[e.ColumnIndex].Name.Equals("Deadline") && DateTime.TryParse(e.Value.ToString(), out date))
            {
                // Récupération du delta en jours
                int diff = (date.Date - DateTime.Now.Date).Days;

                // Modification du contenu des cellules
                if (diff == 0) // Aujourd'hui
                    e.Value = date.ToShortDateString() + Environment.NewLine + "Today"; // Valeur modifiée      
                else if (diff > 0)// Dans le futur
                    e.Value = date.ToShortDateString() + Environment.NewLine + "+ " + diff.ToString() + " jours"; // Valeur modifiée

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
                else if (diff > 0 && diff <= Int32.Parse(ConfigurationManager.AppSettings["P1length"])) // Dans le futur "proche"
                {
                    e.CellStyle.ForeColor = Color.DarkGreen;
                    e.CellStyle.Font = new Font(e.CellStyle.Font, FontStyle.Bold); // en gras
                }
            }
            #endregion

            #region Gestion de la colonne PJ
            if (grilleData.Columns[e.ColumnIndex].Name.Equals("Liens"))
            {
                switch (e.Value.ToString())
                {
                    case ("0"):
                        e.Value = null; // Vidage la cellule
                        e.CellStyle.NullValue = null; // Aucun affichage si cellule vide
                        grilleData[e.ColumnIndex, e.RowIndex].ToolTipText = String.Empty;
                        break;
                    case ("1"):
                        // Récupération de la PJ
                        DB db = TrayIcon.dbs[this.getDataFromRow(e.RowIndex, "DB")];
                        Enclosure pj = db.getPJ(this.getDataFromRow(e.RowIndex, "id"))[0];
                        e.Value = pj.Icone; // Affichage de la bonne icône
                        grilleData[e.ColumnIndex, e.RowIndex].ToolTipText = pj.Titre; // Modification du tooltip de la cellule
                        grilleData.Rows[e.RowIndex].Tag = pj; // Tag de la DataGridRow
                        break;
                    default:
                        // On diffère la récupération de liste
                        e.Value = TaskLeader.Properties.Resources.PJ;
                        grilleData[e.ColumnIndex, e.RowIndex].ToolTipText = String.Empty;
                        break;
                }
            }
            #endregion
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
                    DB db = TrayIcon.dbs[this.getDataFromRow(e.RowIndex, "DB")];
                    List<Enclosure> links = db.getPJ(this.getDataFromRow(e.RowIndex, "id")); //Récupération des différents liens
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
                grilleData.Columns[e.ColumnIndex].Name.Equals("Deadline") && // Colonne "Deadline"
                e.RowIndex >= 0) // Ce n'est pas la ligne des headers // Cellule non vide
            {
                grilleData.Cursor = Cursors.Default;
                new ComplexTooltip(
                    new DatePickerPopup(
                        new TLaction(this.getDataFromRow(e.RowIndex, "id"), this.getDataFromRow(e.RowIndex, "DB"))
                    )
                ).Show();
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

        #endregion

        #region listeContext

        private void listeContext_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Remplissage du menu des différents statuts
            statutTSMenuItem.DropDown.Items.Clear();

            DB db = TrayIcon.dbs[this.getDataFromRow(grilleData.SelectedRows[0].Index, "DB")];
            foreach (object item in db.getTitres(DBField.statut)) //TODO: mettre en cache la valeur des statuts
                statutTSMenuItem.DropDown.Items.Add(item.ToString(), null, this.changeStat);

            ((ToolStripDropDownMenu)statutTSMenuItem.DropDown).ShowImageMargin = false;
        }

        // Mise à jour du statut d'une action via le menu contextuel
        private void changeStat(object sender, EventArgs e)
        {
            // Récupération de l'action
            TLaction action = new TLaction(
                this.getDataFromRow(grilleData.SelectedRows[0].Index, "id"),
                this.getDataFromRow(grilleData.SelectedRows[0].Index, "DB")
            );

            // On récupère le nouveau statut
            action.Statut = ((ToolStripItem)sender).Text;

            // On met à jour le statut de l'action que s'il a changé
            if (action.statusHasChanged)
                action.save();
        }

        // Copie de l'action dans le presse-papier
        private void exportRow(object sender, EventArgs e)
        {
            new TLaction(
                this.getDataFromRow(grilleData.SelectedRows[0].Index, "id"),
                this.getDataFromRow(grilleData.SelectedRows[0].Index, "DB")
            ).clip(((ToolStripItem)sender).Text);
        }

        #endregion

        #region linksContext

        // Ouverture du lien
        private void linksContext_ItemClicked(object sender, EventArgs e)
        {
            ((Enclosure)((ToolStripMenuItem)sender).Tag).open();
        }

        #endregion

    }
}
