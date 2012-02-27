using System;
using System.Windows.Forms;
using TaskLeader.BO;
using TaskLeader.DAL;

namespace TaskLeader.GUI
{
    public partial class CritereSelect : UserControl
    {
        private DBentity type;
        private bool hasParent = false;

        /// <summary>
        /// Evènement déclenché lors du changement de sélection dans la liste du MultipleSelect
        /// </summary>
        public event EventHandler SelectedIndexChanged
        {
            add { this.liste.SelectedIndexChanged += value; }
            remove { this.liste.SelectedIndexChanged -= value; }
        }

        /// <summary>
        /// Rend dépendant ce widget d'un autre
        /// </summary>
        /// <param name="widget">MultipleSelect parent</param>
        public void addParent(CritereSelect widget)
        {
            this.hasParent = true;
            widget.SelectedIndexChanged += new EventHandler(this.liste_SelectedIndexChanged);
        }

        private void liste_SelectedIndexChanged(object sender, EventArgs e)
        {
            CheckedListBox criteres = ((CheckedListBox)sender);
            CheckedListBox.CheckedItemCollection items = criteres.CheckedItems;
            DB db = (DB)criteres.Tag;

            // On n'affiche la liste des sujets que si un seul contexte est tické
            if (items.Count == 1)
            {
                this.maj(db, items[0].ToString());
                this.box.Enabled = true;
            }
            else
            {
                this.liste.Items.Clear();
                this.box.Checked = true;
                this.box.Enabled = false;
            }
        }

        public CritereSelect()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructeur de widget MultipleSelect
        /// </summary>
        /// <param name="title">Titre du critère (et aussi nom du contrôle)</param>
        public CritereSelect(String title, DBentity entity)
        {
            InitializeComponent();
            this.titre.Text = title;
            this.type = entity;
        }

        /// <summary>
        /// Méthode appelée si checkbox 'Tous' sélectionnée
        /// </summary>
        private void box_Click(object sender, EventArgs e)
        {
            for (int i = 0; i < this.liste.Items.Count; i++)
                this.liste.SetItemChecked(i, this.box.Checked);
        }

        /// <summary>
        /// Méthode appelée si click sur la liste
        /// </summary>
        private void liste_Click(object sender, EventArgs e)
        {
            if (box.Checked)
                box.Checked = false;
        }

        /// <summary>
        /// Applique un critère au MultipleSelect.
        /// Pas utilisé pour le moment mais le sera si édition d'un filtre
        /// </summary>
        /// <param name="critere">Criterium à appliquer</param>
        private void apply(Criterium critere)
        {
            box.Checked = false; // La checkbox "Tous" n'est pas sélectionnée
            for (int i = 0; i < liste.Items.Count; i++) // Parcours de la ListBox
            {
                int index = critere.selected.IndexOf(liste.Items[i]); // Recherche de l'item dans le filtre
                liste.SetItemChecked(i, !(index == -1));
            }
        }

        /// <summary>
        /// Renvoie le Criterium correspondant ou null
        /// </summary>
        /// <returns></returns>
        public Criterium getCriterium()
        {
            if (!box.Checked)
                return new Criterium(type, liste.CheckedItems);
            else
                return null;
        }

        /// <summary>
        /// Mise à jour de la liste
        /// </summary>
        /// <param name="db">DB de référence</param>
        public void maj(DB db)
        {
            // Les contrôles enfants ne doivent pas être mis à jour depuis l'extérieur
            if (!this.hasParent)
                this.maj(db, null);
        }

        private void maj(DB db, String key)
        {
            this.liste.Tag = db;
            this.liste.ClearSelected(); // Permet de déclencher l'évènement SelectedIndexChanged
            this.liste.Items.Clear(); // Vidage de la liste
            foreach (object item in db.getTitres(this.type, key))
                this.liste.Items.Add(item, true); // Sélection de toutes les valeurs
            this.box.Checked = true;
        }
    }
}
