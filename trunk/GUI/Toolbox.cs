using System;
using System.Data;
using System.ComponentModel;
using System.Collections.Generic;
using System.Windows.Forms;
using TaskLeader.DAL;
using TaskLeader.BO;

namespace TaskLeader.GUI
{
    public partial class Toolbox : Form
    {
        private Grille data = new Grille();

        public Toolbox()
        {
            InitializeComponent();
        }

        /// <summary>Chargement des différents composants au lancement de la toolbox</summary>
        private void Toolbox_Load(object sender, EventArgs e)
        {
            // Configuration du dbSelect pour afficher 
            this.dbSelect.setForDB();

            // Remplissage de la liste des bases d'action disponibles
            foreach (DB db in TrayIcon.dbs.Values)
            {
                if (TrayIcon.activeDBs.Contains(db.name)) // Si la base est active
                    this.addDB(db);

                // Menu admin
                ToolStripMenuItem dbItem = new ToolStripMenuItem(db.name);
                dbItem.Checked = TrayIcon.activeDBs.Contains(db.name);
                dbItem.CheckOnClick = true;
                dbItem.CheckedChanged += new EventHandler(changeActiveDBs);
                this.baseToolStripMenuItem.DropDown.Items.Add(dbItem);
            }

            TrayIcon.activeDBs.ListChanged += new ListChangedEventHandler(updateDBs);

            // -------------------
            // Tab 'filtre manuel'
            // -------------------

            // Création des MultipleSelect
            // TODO: la création devrait boucler sur les DBentity de la classe DB
            CritereSelect ctxt = new CritereSelect(DB.contexte);
            CritereSelect sujt = new CritereSelect(DB.sujet);
            sujt.addParent(ctxt);
            this.selectPanel.Controls.Add(ctxt);
            this.selectPanel.Controls.Add(sujt);
            this.selectPanel.Controls.Add(new CritereSelect(DB.destinataire));
            this.selectPanel.Controls.Add(new CritereSelect(DB.statut));

            this.manuelDBcombo.Text = TrayIcon.defaultDB.name;
            // ATTENTION: déclenche la mise à jour de toutes les CritereSelect!!

            // -------
            // Grille
            // -------
            this.data.Dock = DockStyle.Fill;
            this.mainTableLayout.Controls.Add(this.data, 0, 2);
            this.mainTableLayout.SetColumnSpan(this.data, 4);
        }

        /// <summary>
        /// Ajoute la nouvelle DB aux widgets concernés
        /// </summary>
        /// <param name="db">DB à rajouter</param>
        private void addDB(DB db)
        {
            this.manuelDBcombo.Items.Add(db); // Filtre manuel
            //this.manuelDBcombo.Text = TrayIcon.defaultDB.name; // C'est à la combo de le gérer elle même
            this.dbSelect.addDB(db); // Recherche
            this.filtersPanel.Controls.Add(new FiltreSelect(db)); // Filtres enregistrés
        }

        private void updateDBs(object sender, ListChangedEventArgs e)
        {
            String dbName = ((BindingList<String>)sender)[e.NewIndex];
            //TODO: impossible d'accéder à l'item supprimé !!

            if (e.ListChangedType == ListChangedType.ItemAdded)
                this.addDB(TrayIcon.dbs[dbName]);
            else if (e.ListChangedType == ListChangedType.ItemDeleted)
            {
                this.manuelDBcombo.Items.Remove(TrayIcon.dbs[dbName]);
                this.manuelDBcombo.Text = TrayIcon.defaultDB.name; // C'est à la combo de le gérer elle même
                this.dbSelect.removeDB(TrayIcon.dbs[dbName]);
                this.filtersPanel.Controls.RemoveByKey(dbName);
            }
        }

        #region Menu admin

        /// <summary>
        /// Modifie la liste des bases actives
        /// </summary>
        private void changeActiveDBs(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Checked) // La base vient d'être activée
                TrayIcon.activeDBs.Add(sender.ToString()); // Ajout à la liste globale des bases actives
            else // La base vient d'être désactivée
                TrayIcon.activeDBs.Remove(sender.ToString()); // Suppression de la liste globale des bases actives
        }

        private void defaultValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:new AdminDefaut(this.db.name).Show();
        }

        // Ouverture de la gui création d'action
        private void ajoutAction(object sender, EventArgs e)
        {
            TrayIcon.displayNewAction(new TLaction());
        }

        #endregion
 
        #region Onglet Recherche

        // Validation de la recherche après click sur OK
        private void searchButton_Click(object sender, EventArgs e)
        {
            if (searchBox.Text != "")
                foreach (DB db in this.dbSelect.getDBs())
                    this.tagsPanel.Controls.Add(new Etiquette(new Filtre(searchBox.Text, db.name)));
            else
            {
                this.erreurSearch.Text = "Entrer un mot clé pour la recherche";
                this.erreurSearch.Visible = true;
            }
        }

        // Permet la validation de la recherche par la touche ENTER
        private void searchBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Enter)
                this.searchButton_Click(sender, e);
        }

        private void searchBox_Enter(object sender, EventArgs e)
        {
            this.erreurSearch.Visible = false;
        }

        #endregion

        #region comportement Toolbox

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

        // Fermeture de la Form si minimisée
        private void Toolbox_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
                this.Close();
        }

        #endregion

        #region Onglet Filtre manuel

        /// <summary>
        /// Changement de DB dans le tab 'Filtre manuel'
        /// </summary>
        private void manuelDBcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (CritereSelect widget in this.selectPanel.Controls)
                widget.changeDB((DB)manuelDBcombo.Items[manuelDBcombo.SelectedIndex]);
        }

        /// <summary>
        /// Click sur la checkbox de sauvegarde du filtre
        /// </summary>
        private void saveFilterCheck_CheckedChanged(object sender, EventArgs e)
        {
            this.nameBox.Enabled = this.saveFilterCheck.Checked;
        }

        /// <summary>
        /// Click dans la textbox du nom du filtre
        /// </summary>
        private void nameBox_Enter(object sender, EventArgs e)
        {
            this.errorLabel.Visible = false;
        }

        // Affichage des actions sur filtre manuel
        private void filtreManuel(object sender, EventArgs e)
        {
            Filtre filtre = new Filtre(manuelDBcombo.Text);
            foreach (CritereSelect widget in this.selectPanel.Controls)
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

                        // Mise à jour de la liste des filtres de la base correspondante
                        ((FiltreSelect)this.filtersPanel.Controls[db.name]).maj();

                        // Affichage du filtre
                        this.tagsPanel.Controls.Add(new Etiquette(filtre));
                    }
                }
            }
            else
                this.tagsPanel.Controls.Add(new Etiquette(filtre));
        }

        #endregion

        #region TagsPanel

        /// <summary>
        /// Quand une étiquette est ajoutée au panel, on ajoute le filtre à la Grille
        /// </summary>
        private void tagsPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            int nombre = data.add(((Etiquette)e.Control).filtre);
            this.afficheNombre(nombre);
        }

        /// <summary>
        /// Quant une étiquette est supprimée du panel, on supprimer le filtre de la Grille
        /// </summary>
        private void tagsPanel_ControlRemoved(object sender, ControlEventArgs e)
        {
            // On efface la grille dans tous les cas
            data.raz();

            if (tagsPanel.Controls.Count > 0) // S'il reste au moins une étiquette
            {
                int nombre = 0;

                // On recalcule le tableau (pas top en perfo mais résout le pb des intersections entre filtres)
                foreach (Etiquette tag in tagsPanel.Controls)
                    nombre = data.add(tag.filtre);

                this.afficheNombre(nombre);
            }
            else
                this.resultLabel.Visible = false;
        }

        #endregion

        #region resultLabel

        /// <summary>
        /// Met à jour le label d'affichage des nombre d'actions trouvées
        /// </summary>
        /// <param name="nombre">Nombre d'actions affichées par la liste</param>
        private void afficheNombre(int nombre)
        {
            // Définition du label de résultat
            if (nombre == 0)
                this.resultLabel.Text = "Aucune action trouvée";
            else if (nombre == 1)
                this.resultLabel.Text = "1 action trouvée";
            else
                this.resultLabel.Text = nombre.ToString() + " actions trouvées";

            // Affichage du label
            resultLabel.Visible = true;
        }

        #endregion

        

    }
}
