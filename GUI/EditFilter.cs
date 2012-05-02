using System;
using System.Windows.Forms;
using TaskLeader.BO;
using TaskLeader.DAL;

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

            this.dbLabel.Text += filtre.dbName;

            Label label;
            foreach(Criterium critere in filtre.criteria){
                label = new Label();
                label.Text = critere.ToString();
                label.AutoSize = true;
                this.criteriaPanel.Controls.Add(label);
            }

        }
    }
}
