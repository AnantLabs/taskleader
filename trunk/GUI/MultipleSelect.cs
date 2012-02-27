using System;
using System.Windows.Forms;
using TaskLeader.BO;
using TaskLeader.DAL;

namespace TaskLeader.GUI
{
    public partial class MultipleSelect : UserControl
    {
        private DBentity type;
        private MultipleSelect child;
        private bool hasChildren { get { return (child != null); } }

        public void addChild(MultipleSelect widget)
        {
            // Objectif = possibilité d'avoir plusieurs children
            this.child = widget;
        }

        // TODO: Sans doute plus simple à gérer avec des évènements
        private void liste_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.hasChildren)
                this.child.updateFromParent(liste.CheckedItems.Count, this.liste.CheckedItems[0].ToString());
        }

        public void updateFromParent(int countSelected, String key)
        {
            // Dans tous les cas de changement de séléction on vide la liste
            liste.Items.Clear();

            // On n'affiche la liste des sujets que si un seul contexte est tické
            if (countSelected == 1)
                this.maj(null,key); //TODO: comment récupérer la base ?
            else
                box.Enabled = false;

        }

        public MultipleSelect()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Constructeur de widget MultipleSelect
        /// </summary>
        /// <param name="title">Titre du critère (et aussi nom du contrôle)</param>
        public MultipleSelect(String title, DBentity entity)
        {
            InitializeComponent();
            this.Name = title; // Le nom du contrôle sera son titre
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
        /// Remise à zéro du MultipleSelect
        /// </summary>
        public void raz()
        {
            // On sélectionne tout
            box.Checked = true;
            for (int i = 0; i < this.liste.Items.Count; i++)
                this.liste.SetItemChecked(i, true);

        }

        /// <summary>
        /// Applique un critère au MultipleSelect
        /// </summary>
        /// <param name="critere">Criterium à appliquer</param>
        public void apply(Criterium critere)
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
        /// <param name="entity">DBentity (DB.contexte...)</param>
        public void maj(DB db,String key=null)
        {
            this.liste.Items.Clear(); // Vidage de la liste
            foreach (object item in db.getTitres(this.type,key))
                this.liste.Items.Add(item, true); // Sélection de toutes les valeurs
        }
    }
}
