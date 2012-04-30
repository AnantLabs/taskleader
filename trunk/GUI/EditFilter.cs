using System;
using System.Windows.Forms;
using TaskLeader.BO;

namespace TaskLeader.GUI
{
    public partial class EditFilter : UserControl, IComplexToolTipContent
    {
        #region Implémentation de l'interface IComplexToolTipContent

        public event EventHandler ClosureRequested
        {
            add { this.closeBox.Click += value; }
            remove { this.closeBox.Click -= value; }
        }

        public Control control { get { return this; } }

        #endregion

        public EditFilter(Filtre filtre)
        {
            InitializeComponent();

        }

    }
}
