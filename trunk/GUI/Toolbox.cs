using System;
using System.Data;
using System.Collections.Generic;
using System.Windows.Forms;
using TaskLeader.DAL;
using TaskLeader.BO;

namespace TaskLeader.GUI
{
    public partial class Toolbox : Form
    {
        /// <summary>
        /// Dictionnaire des widgets de sélection des critères
        /// </summary>
        private Dictionary<string, CritereSelect> criteresSelect = new Dictionary<string, CritereSelect>();

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

            // -------------------
            // Tab 'filtre manuel'
            // -------------------

            // Création des MultipleSelect

            criteresSelect.Add("contextes", new CritereSelect("Contextes", DB.contexte));
            criteresSelect.Add("sujets", new CritereSelect("Sujets", DB.sujet));
            criteresSelect["sujets"].addParent(criteresSelect["contextes"]);
            criteresSelect.Add("destinataires", new CritereSelect("Destinataires", DB.destinataire));
            criteresSelect.Add("statuts", new CritereSelect("Statuts", DB.statut));

            foreach (Control control in criteresSelect.Values)
                this.selectPanel.Controls.Add(control);

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
            // TODO: il faut sélectionner la DB par défaut dans la combo => c'est à la combo de le gérer elle même
            this.dbSelect.addDB(db); // Recherche
            this.filtersPanel.Controls.Add(new FiltreSelect(db)); // Filtres enregistrés
        }

        #region Menu admin

        /// <summary>
        /// Modifie la liste des bases actives
        /// </summary>
        private void changeActiveDBs(object sender, EventArgs e)
        {
            if (((ToolStripMenuItem)sender).Checked) // La base vient d'être activée
            {
                // Ajout à la liste globale des bases actives
                TrayIcon.activeDBs.Add(sender.ToString());
                // Ajout aux widgets concernés
                this.addDB(TrayIcon.dbs[sender.ToString()]);
            }
            else // La base vient d'être désactivée
            {
                // Suppression de la liste globale des bases actives
                TrayIcon.activeDBs.Remove(sender.ToString());
                // Mise à jour de la 
                this.manuelDBcombo.Items.Remove(TrayIcon.dbs[sender.ToString()]);
                this.manuelDBcombo.Text = TrayIcon.defaultDB.name; // C'est à la combo de le gérer elle même
                //TODO
            }

            // Mise à jour de la toolbox
            // TODO: il faut supprimer les bases qui ne sont plus actives
            //this.razTableau(); //TODO: Pourquoi? 
            //this.loadFilters();
            //this.miseAjour(true); //TODO: Pourquoi?
        }

        #endregion

        /// <summary>
        /// Rafraîchissment de la toolbox
        /// </summary>
        /// <param name="fullUpdate">true si mise à jour des contextes et destinataires</param>
        public void miseAjour(bool fullUpdate)
        {
            if (fullUpdate)
            {
                // Mise à jour des contextes
                //TODO: criteresSelect["contextes"].maj(db);
                // Mise à jour des destinataires
                //TODO: criteresSelect["destinataires"].maj(db);
            }

            // Si un filtre est actif on l'affiche
            //TODO: if (this.db.CurrentFilter != null)
            //    this.showFilter(this.db.CurrentFilter);
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

        private void defaultValuesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //TODO:new AdminDefaut(this.db.name).Show();
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

        #region Onglet Filtre manuel

        /// <summary>
        /// Changement de DB dans le tab 'Filtre manuel'
        /// </summary>
        private void manuelDBcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (CritereSelect widget in criteresSelect.Values)
                widget.majCritere((DB)manuelDBcombo.Items[manuelDBcombo.SelectedIndex]);
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
            foreach (CritereSelect widget in criteresSelect.Values)
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
