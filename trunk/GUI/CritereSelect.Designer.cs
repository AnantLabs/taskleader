namespace TaskLeader.GUI
{
    partial class CritereSelect
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
            this.box = new System.Windows.Forms.CheckBox();
            this.titre = new System.Windows.Forms.Label();
            this.liste = new System.Windows.Forms.CheckedListBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // box
            // 
            this.box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.box.AutoSize = true;
            this.box.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.box.Checked = true;
            this.box.CheckState = System.Windows.Forms.CheckState.Checked;
            this.box.Location = new System.Drawing.Point(122, 3);
            this.box.Name = "box";
            this.box.Size = new System.Drawing.Size(50, 16);
            this.box.TabIndex = 10;
            this.box.Text = "Tous";
            this.box.UseVisualStyleBackColor = true;
            this.box.Click += new System.EventHandler(this.box_Click);
            // 
            // titre
            // 
            this.titre.AutoSize = true;
            this.titre.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titre.Location = new System.Drawing.Point(3, 4);
            this.titre.Margin = new System.Windows.Forms.Padding(3, 4, 0, 0);
            this.titre.Name = "titre";
            this.titre.Size = new System.Drawing.Size(33, 13);
            this.titre.TabIndex = 9;
            this.titre.Text = "Titre";
            // 
            // liste
            // 
            this.liste.CheckOnClick = true;
            this.tableLayoutPanel1.SetColumnSpan(this.liste, 2);
            this.liste.FormattingEnabled = true;
            this.liste.Location = new System.Drawing.Point(3, 25);
            this.liste.Name = "liste";
            this.liste.Size = new System.Drawing.Size(169, 94);
            this.liste.TabIndex = 1;
            this.liste.Click += new System.EventHandler(this.liste_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.box, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.titre, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.liste, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 17.55725F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 82.44275F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(175, 126);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // MultipleSelect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MultipleSelect";
            this.Size = new System.Drawing.Size(175, 126);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.CheckBox box;
        private System.Windows.Forms.Label titre;
        private System.Windows.Forms.CheckedListBox liste;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
    }
}
