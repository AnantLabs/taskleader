using System;
using System.Drawing;
using System.Collections.Generic;
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

            // Remplissage de la 1ère ligne
            this.dbTitreLabel.Font = new Font(dbTitreLabel.Font, FontStyle.Bold);
            this.dbLabel.Text = filtre.dbName;

            int rowsCount = this.tablePanel.RowCount;
            foreach (KeyValuePair<String,String> kvp in filtre.getDescription())
            {
                this.tablePanel.Controls.Add(new SimpleLabel(kvp.Key, FontStyle.Bold), 0, rowsCount);
                
                SimpleLabel value;
                if(String.IsNullOrEmpty(kvp.Value))
                    value = new SimpleLabel("Tous",FontStyle.Italic);
                else
                    value = new SimpleLabel(kvp.Value);
                this.tablePanel.Controls.Add(value, 1, rowsCount);
                this.tablePanel.SetColumnSpan(value, 2);

                rowsCount++;
            }
        }

        /// <summary>
        /// Mise en forme du tableau 
        /// </summary>
        private void tablePanel_CellPaint(object sender, TableLayoutCellPaintEventArgs e)
        {
            // Coloriage de la première colonne en 'jaune info'
            if (e.Column == 0)
                e.Graphics.FillRectangle(new SolidBrush(SystemColors.Info), e.CellBounds);

            // Ajout d'une ligne double en bas de la première ligne
            if (e.Row == 0)
                e.Graphics.DrawLine(
                    new Pen(Color.Black, 2),
                    new Point(e.CellBounds.Left,e.CellBounds.Bottom-1),    // -1 pour que l'épaisseur de 2px tienne
                    new Point(e.CellBounds.Right, e.CellBounds.Bottom - 1) // dans la ligne
               );
        }
    }
}
