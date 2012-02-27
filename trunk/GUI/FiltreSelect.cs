using System.Data;
using System;
using System.Windows.Forms;
using TaskLeader.DAL;
using TaskLeader.GUI;

namespace TaskLeader.GUI
{
    public partial class FiltreSelect : UserControl
    {
        public FiltreSelect()
        {
            InitializeComponent();
        }

        public FiltreSelect(DB db)
        {
            InitializeComponent();

            this.titre.Text = db.name;
            this.liste.Items.AddRange(db.getFilters());
        }
    }
}
