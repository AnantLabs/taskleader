using System.Data;
using System;
using System.Windows.Forms;
using TaskLeader.DAL;
using TaskLeader.GUI;

namespace TaskLeader.GUI
{
    public partial class FiltreSelect : UserControl
    {
        private DB db;

        public FiltreSelect()
        {
            InitializeComponent();
        }

        public FiltreSelect(DB database)
        {
            InitializeComponent();
            this.db = database;

            // On attribue un nom au contrôle pour pouvoir le récupérer ensuite
            this.Name = this.db.name;

            this.titre.Text = this.db.name;

            this.liste.Items.AddRange(this.db.getFilters());
        }

        /// <summary>
        /// Met à jour la liste des filtres de cette base
        /// </summary>
        public void maj()
        {
            this.liste.Items.Clear();
            this.liste.Items.AddRange(this.db.getFilters());
        }
    }
}
