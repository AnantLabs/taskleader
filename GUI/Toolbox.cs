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
            // Remplissage de la liste des bases d'action disponibles
            foreach (DB db in TrayIcon.dbs.Values)
            {
                // Filtre manuel
                manuelDBcombo.Items.Add(db);

                // Filtres enregistrés
                this.filtersPanel.Controls.Add(new FiltreSelect(db));

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
            this.criteresSelect.Add("contextes", new CritereSelect("Contextes", DB.contexte));
            this.criteresSelect.Add("sujets", new CritereSelect("Sujets", DB.sujet));
            this.criteresSelect["sujets"].addParent(this.criteresSelect["contextes"]);
            this.criteresSelect.Add("destinataires", new CritereSelect("Destinataires", DB.destinataire));
            this.criteresSelect.Add("statuts", new CritereSelect("Statuts", DB.statut));

            foreach(Control control in this.criteresSelect.Values)
                this.selectPanel.Controls.Add(control);

            this.manuelDBcombo.Text = TrayIcon.defaultDB.name;
            // ATTENTION: déclenche la mise à jour de toutes les CritereSelect!!

            // Remplissage de la combo des filtres
            this.loadFilters();

            // -------
            // Grille
            // -------
            this.data.Dock = DockStyle.Fill;
            this.mainTableLayout.Controls.Add(this.data, 0, 2);
            this.mainTableLayout.SetColumnSpan(this.data, 4);

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
            //this.razTableau(); //TODO: Pourquoi? 
            this.loadFilters();
            this.miseAjour(true); //TODO: Pourquoi?
        }

        /// <summary>
        /// Met à jour la liste des filtres pour la DB courante
        /// </summary>
        private void loadFilters()
        {

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
                //TODO: this.criteresSelect["contextes"].maj(db);
                // Mise à jour des destinataires
                //TODO: this.criteresSelect["destinataires"].maj(db);
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

        // Affichage des actions sur filtre manuel
        private void filtreManuel(object sender, EventArgs e)
        {
            Filtre filtre = new Filtre(manuelDBcombo.Text);
            foreach (CritereSelect widget in this.criteresSelect.Values)
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
                        this.tagsPanel.Controls.Add(new Etiquette(filtre));
                    }
                }
            }
            else
                this.tagsPanel.Controls.Add(new Etiquette(filtre));           
        }

        /// <summary>
        /// Application d'un filtre: affichage des actions correspondantes
        /// </summary>
        private void showFilter(Filtre filtre)
        {
            tagsPanel.Controls.Add(new Etiquette(filtre));

            // Application du filtre
            //afficheActions(filtre);
        }

        // Validation de la recherche après click sur OK
        private void searchButton_Click(object sender, EventArgs e)
        {
        //    if (searchBox.Text != "")
        //        this.showFilter(new Filtre(searchBox.Text, this.db.name));
        //    else
        //        MessageBox.Show("Veuillez entrer un mot clé pour la recherche", "Recherche", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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

        /// <summary>
        /// Changement de DB dans le tab 'Filtre manuel'
        /// </summary>
        private void manuelDBcombo_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(CritereSelect widget in this.criteresSelect.Values)
                widget.maj((DB)manuelDBcombo.Items[manuelDBcombo.SelectedIndex]);
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

        // ----------- TagsPanel -----------

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
            //TODO: si tagsPanel ne contient plus d'étiquette, il faut tout désactiver
            int nombre = data.remove(((Etiquette)e.Control).filtre);
            this.afficheNombre(nombre);
        }

        // ----------- resultLabel -----------

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

        
    }
}
