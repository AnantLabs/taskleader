namespace TaskLeader.GUI
{
    partial class EditFilter
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.tablePanel = new System.Windows.Forms.TableLayoutPanel();
            this.dbLabel = new System.Windows.Forms.Label();
            this.dbTitreLabel = new System.Windows.Forms.Label();
            this.closeBox = new System.Windows.Forms.PictureBox();
            this.tablePanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closeBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tablePanel
            // 
            this.tablePanel.AutoSize = true;
            this.tablePanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tablePanel.ColumnCount = 3;
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tablePanel.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tablePanel.Controls.Add(this.dbLabel, 1, 0);
            this.tablePanel.Controls.Add(this.dbTitreLabel, 0, 0);
            this.tablePanel.Controls.Add(this.closeBox, 2, 0);
            this.tablePanel.Location = new System.Drawing.Point(0, 0);
            this.tablePanel.Name = "tablePanel";
            this.tablePanel.RowCount = 1;
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablePanel.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tablePanel.Size = new System.Drawing.Size(142, 23);
            this.tablePanel.TabIndex = 1;
            this.tablePanel.CellPaint += new System.Windows.Forms.TableLayoutCellPaintEventHandler(this.tablePanel_CellPaint);
            // 
            // dbLabel
            // 
            this.dbLabel.AutoSize = true;
            this.dbLabel.BackColor = System.Drawing.Color.Transparent;
            this.dbLabel.Location = new System.Drawing.Point(85, 5);
            this.dbLabel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.dbLabel.Name = "dbLabel";
            this.dbLabel.Size = new System.Drawing.Size(35, 13);
            this.dbLabel.TabIndex = 1;
            this.dbLabel.Text = "label1";
            // 
            // dbTitreLabel
            // 
            this.dbTitreLabel.AutoSize = true;
            this.dbTitreLabel.BackColor = System.Drawing.Color.Transparent;
            this.dbTitreLabel.Location = new System.Drawing.Point(3, 5);
            this.dbTitreLabel.Margin = new System.Windows.Forms.Padding(3, 5, 3, 3);
            this.dbTitreLabel.Name = "dbTitreLabel";
            this.dbTitreLabel.Size = new System.Drawing.Size(76, 13);
            this.dbTitreLabel.TabIndex = 0;
            this.dbTitreLabel.Text = "Base d\'actions";
            // 
            // closeBox
            // 
            this.closeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeBox.BackColor = System.Drawing.Color.Transparent;
            this.closeBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeBox.Image = global::TaskLeader.Properties.Resources.cross;
            this.closeBox.Location = new System.Drawing.Point(123, 3);
            this.closeBox.Margin = new System.Windows.Forms.Padding(0, 3, 3, 4);
            this.closeBox.Name = "closeBox";
            this.closeBox.Size = new System.Drawing.Size(16, 16);
            this.closeBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.closeBox.TabIndex = 1;
            this.closeBox.TabStop = false;
            // 
            // EditFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tablePanel);
            this.Name = "EditFilter";
            this.Size = new System.Drawing.Size(145, 26);
            this.tablePanel.ResumeLayout(false);
            this.tablePanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closeBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tablePanel;
        private System.Windows.Forms.Label dbTitreLabel;
        private System.Windows.Forms.PictureBox closeBox;
        private System.Windows.Forms.Label dbLabel;
    }
}
