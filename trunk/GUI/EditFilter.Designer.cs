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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.criteriaPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.dbLabel = new System.Windows.Forms.Label();
            this.closeBox = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closeBox)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.closeBox, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.criteriaPanel, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 23F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(122, 28);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // criteriaPanel
            // 
            this.criteriaPanel.AutoSize = true;
            this.criteriaPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.SetColumnSpan(this.criteriaPanel, 2);
            this.criteriaPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.criteriaPanel.Location = new System.Drawing.Point(3, 25);
            this.criteriaPanel.Name = "criteriaPanel";
            this.criteriaPanel.Size = new System.Drawing.Size(0, 0);
            this.criteriaPanel.TabIndex = 2;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.AutoSize = true;
            this.flowLayoutPanel1.Controls.Add(this.dbLabel);
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(94, 15);
            this.flowLayoutPanel1.TabIndex = 3;
            // 
            // dbLabel
            // 
            this.dbLabel.AutoSize = true;
            this.dbLabel.Location = new System.Drawing.Point(3, 0);
            this.dbLabel.Name = "dbLabel";
            this.dbLabel.Size = new System.Drawing.Size(88, 15);
            this.dbLabel.TabIndex = 0;
            this.dbLabel.Text = "Base d\'actions: ";
            // 
            // closeBox
            // 
            this.closeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeBox.Image = global::TaskLeader.Properties.Resources.cross;
            this.closeBox.Location = new System.Drawing.Point(103, 3);
            this.closeBox.Name = "closeBox";
            this.closeBox.Size = new System.Drawing.Size(16, 16);
            this.closeBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.closeBox.TabIndex = 1;
            this.closeBox.TabStop = false;
            // 
            // EditFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "EditFilter";
            this.Size = new System.Drawing.Size(125, 31);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closeBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel criteriaPanel;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label dbLabel;
        private System.Windows.Forms.PictureBox closeBox;
    }
}
