using System;
using System.Data;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Windows.Forms;
using TaskLeader.DAL;
using TaskLeader.BO;

namespace TaskLeader.GUI
{
    public partial class Toolbox : Form
    {
        private Grille data = new Grille();
        private DBSelect dbSelect = new DBSelect();

        public Toolbox()
        {
            InitializeComponent();
        }

        /// <summary>Chargement des différents composants au lancement de la toolbox</summary>
        private void Toolbox_Load(object sender, EventArgs e)
        {
            // Création des CritereSelect
            CritereSelect widget;
            this.selectPanel.SuspendLayout();
            foreach (DBentity entity in DB.entities)
            {
                widget = new CritereSelect(entity);
                if (entity.parent != null)
                    widget.addParent(this.selectPanel.Controls[entity.parent.nom] as CritereSelect);
                this.selectPanel.Controls.Add(widget);
            }
            this.selectPanel.ResumeLayout();
            this.selectPanel.SuspendLayout(); // Etrange mais çà accélère l'affichage
            foreach (Control control in this.selectPanel.Controls)
                this.selectPanel.SetFlowBreak(control, true);
            this.selectPanel.ResumeLayout();


            // Remplissage de la liste des bases d'action disponibles
            foreach (DB db in TrayIcon.dbs.Values)
            {
                if (TrayIcon.activeDBs.Contains(db.name)) // Si la base est active
                    this.addDB(db);

                // Menu admin
                ToolStripMenuItem activeItem = new ToolStripMenuItem("Active");
                activeItem.Checked = TrayIcon.activeDBs.Contains(db.name);
                activeItem.CheckOnClick = true;
                activeItem.CheckedChanged += new EventHandler(this.changeActiveDBs);

                this.adminItem.DropDownItems.Add(new ToolStripMenuItem(db.name,TaskLeader.Properties.Resources.database,new ToolStripMenuItem[]{
                    activeItem,
                    new ToolStripMenuItem("Valeurs par défaut",TaskLeader.Properties.Resources.bullets,this.defaultValuesToolStripMenuItem_Click),
                }));
            }

            this.manuelDBcombo.Text = TrayIcon.defaultDB.name; // ATTENTION: déclenche la mise à jour de toutes les CritereSelect!!
            TrayIcon.activeDBs.CollectionChanged += new NotifyCollectionChangedEventHandler(activeDBs_CollectionChanged);

            // -------------------
            // Tab 'recherche'
            // -------------------
            this.searchPanel.Controls.Add(this.dbSelect);

            // -------
            // Grille
            // -------
            this.data.Dock = DockStyle.Fill;
            this.mainTableLayout.Controls.Add(this.data, 0, 2);
            this.mainTableLayout.SetColumnSpan(this.data, 4);
        }

        #region Gestion de la liste des DBs

        /// <summary>
        /// Ajoute la nouvelle DB aux widgets concernés
        /// </summary>
        /// <param name="db">DB à rajouter</param>
        private void addDB(DB db)
        {
            this.manuelDBcombo.Items.Add(db); // Filtre manuel
            this.dbSelect.addDB(db); // Recherche

            this.filtersPanel.Controls.Add(new FiltreSelect(db));
            this.filtersPanel.SetFlowBreak(
                this.filtersPanel.Controls[this.filtersPanel.Controls.Count - 1], //Dernier contrôle ajouté
                true
            );
        }

        private void activeDBs_CollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (e.Action == NotifyCollectionChangedAction.Add)
            {
                this.addDB(TrayIcon.dbs[(String)e.NewItems[0]]);
                this.manuelDBcombo.Text = TrayIcon.defaultDB.name; // ATTENTION: déclenche la mise à jour de toutes les CritereSelect!!
            }
            else if (e.Action == NotifyCollectionChangedAction.Remove)
            {
                this.manuelDBcombo.Items.Remove(TrayIcon.dbs[(String)e.OldItems[0]]);
                this.manuelDBcombo.Text = TrayIcon.defaultDB.name; // C'est à la combo de le gérer elle même
                this.dbSelect.removeDB(TrayIcon.dbs[(String)e.OldItems[0]]);
                this.filtersPanel.Controls.RemoveByKey((String)e.OldItems[0]);
            }
        }

        #endregion

        #region Menu admin

        /// <summary>
        /// Modifie la liste des bases actives
        /// </summary>
        private void changeActiveDBs(object sender, EventArgs e)
        {
            ToolStripDropDownMenu menu = ((ToolStripMenuItem)sender).GetCurrentParent() as ToolStripDropDownMenu;

            if (((ToolStripMenuItem)sender).Checked) // La base vient d'être activée
                TrayIcon.activeDBs.Add(menu.OwnerItem.Text); // Ajout à la liste globale des bases actives
            else // La base vient d'être désactivée
                TrayIcon.activeDBs.Remove(menu.OwnerItem.Text); // Suppression de la liste globale des bases actives
        }

        private void defaultValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ToolStripDropDownMenu menu = ((ToolStripMenuItem)sender).GetCurrentParent() as ToolStripDropDownMenu;
            new AdminDefaut(menu.OwnerItem.Text).Show();
        }

        // Ouverture de la gui création d'action
        private void ajoutAction(object sender, EventArgs e)
        {
            new ManipAction(new TLaction()).
                Show();
        }

        #endregion

        #region Dynamique IHM

        private void hideCollapse(object sender, EventArgs e)
        {
            String state = this.reduceButton.Tag as String;

            if (state == "expanded" && sender.GetType() == typeof(ToolStripMenuItem)) // Bandeau déployé
            { // Réduction
                this.tabControl1.Appearance = TabAppearance.FlatButtons;
                RowStyle small = new RowStyle(SizeType.Absolute, 30);
                this.mainTableLayout.RowStyles[0] = small;
                this.reduceButton.Tag = "collapsed";
                this.reduceButton.Image = TaskLeader.Properties.Resources.arrow_out;
            }
            else // Bandeau replié
            {
                this.tabControl1.Appearance = TabAppearance.Normal;
                RowStyle big = new RowStyle(SizeType.Absolute, 155);
                this.mainTableLayout.RowStyles[0] = big;
                this.reduceButton.Tag = "expanded";
                this.reduceButton.Image = TaskLeader.Properties.Resources.arrow_in;
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

                        // Affichage du filtre
                        this.tagsPanel.Controls.Add(new Etiquette(filtre));
                    }
                }
            }
            else
                this.tagsPanel.Controls.Add(new Etiquette(filtre));
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

        #region Onglet Filtres enregistrés

        /// <summary>
        /// Méthode appelée après sélection de filtres enregistrés
        /// </summary>
        private void storedFilterBout_Click(object sender, EventArgs e)
        {
            foreach (FiltreSelect widget in this.filtersPanel.Controls)
            {
                foreach (Filtre filtre in widget.getSelected())
                    this.tagsPanel.Controls.Add(new Etiquette(filtre));
                widget.clearChecked();
            }

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

        #region TagsPanel

        /// <summary>
        /// Quand une étiquette est ajoutée au panel, on ajoute le filtre à la Grille
        /// </summary>
        private void tagsPanel_ControlAdded(object sender, ControlEventArgs e)
        {
            // Ajout des actions correspondant à ce filtre au tableau
            int nombre = data.add(((Etiquette)e.Control).filtre);
            this.afficheNombre(nombre);
        }

        /// <summary>
        /// Quant une étiquette est supprimée du panel, on supprimer le filtre de la Grille
        /// </summary>
        private void tagsPanel_ControlRemoved(object sender, ControlEventArgs e)
        {
            int nombre = data.remove(((Etiquette)e.Control).filtre);

            if (tagsPanel.Controls.Count > 0) // S'il reste au moins une étiquette                
                this.afficheNombre(nombre);
            else
                this.resultLabel.Visible = false;
        }

        #endregion
    }
}
