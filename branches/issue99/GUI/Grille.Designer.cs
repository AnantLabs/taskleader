namespace TaskLeader.GUI
{
    partial class Grille
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.grilleData = new System.Windows.Forms.DataGridView();
            this.listeContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editActionItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statutTSMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.linksContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.grilleData)).BeginInit();
            this.listeContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // grilleData
            // 
            this.grilleData.AllowUserToAddRows = false;
            this.grilleData.AllowUserToDeleteRows = false;
            this.grilleData.AllowUserToResizeRows = false;
            this.grilleData.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.DisplayedCells;
            this.grilleData.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.DisplayedCells;
            this.grilleData.ClipboardCopyMode = System.Windows.Forms.DataGridViewClipboardCopyMode.EnableAlwaysIncludeHeaderText;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle1.BackColor = System.Drawing.SystemColors.ControlDark;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.SystemColors.WindowText;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grilleData.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.grilleData.ColumnHeadersHeight = 30;
            this.grilleData.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grilleData.DefaultCellStyle = dataGridViewCellStyle2;
            this.grilleData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grilleData.Location = new System.Drawing.Point(0, 0);
            this.grilleData.Margin = new System.Windows.Forms.Padding(0);
            this.grilleData.MultiSelect = false;
            this.grilleData.Name = "grilleData";
            this.grilleData.ReadOnly = true;
            this.grilleData.RowHeadersVisible = false;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grilleData.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.grilleData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grilleData.Size = new System.Drawing.Size(84, 61);
            this.grilleData.TabIndex = 5;
            this.grilleData.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.modifAction);
            this.grilleData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grilleData_CellFormatting);
            this.grilleData.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grilleData_CellMouseClick);
            this.grilleData.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.grilleData_CellMouseEnter);
            this.grilleData.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.grilleData_CellMouseLeave);
            // 
            // listeContext
            // 
            this.listeContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editActionItem,
            this.statutTSMenuItem,
            this.exportMenuItem});
            this.listeContext.Name = "listeContext";
            this.listeContext.ShowImageMargin = false;
            this.listeContext.Size = new System.Drawing.Size(134, 92);
            this.listeContext.Opening += new System.ComponentModel.CancelEventHandler(this.listeContext_Opening);
            // 
            // editActionItem
            // 
            this.editActionItem.Name = "editActionItem";
            this.editActionItem.Size = new System.Drawing.Size(133, 22);
            this.editActionItem.Text = "Editer l\'action";
            this.editActionItem.Click += new System.EventHandler(this.modifAction);
            // 
            // statutTSMenuItem
            // 
            this.statutTSMenuItem.Name = "statutTSMenuItem";
            this.statutTSMenuItem.Size = new System.Drawing.Size(133, 22);
            this.statutTSMenuItem.Text = "Passer l\'action à";
            // 
            // exportMenuItem
            // 
            this.exportMenuItem.Name = "exportMenuItem";
            this.exportMenuItem.Size = new System.Drawing.Size(133, 22);
            this.exportMenuItem.Text = "Exporter vers";
            // 
            // linksContext
            // 
            this.linksContext.Name = "linksContext";
            this.linksContext.Size = new System.Drawing.Size(61, 4);
            // 
            // Grille
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.grilleData);
            this.Name = "Grille";
            this.Size = new System.Drawing.Size(84, 61);
            ((System.ComponentModel.ISupportInitialize)(this.grilleData)).EndInit();
            this.listeContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView grilleData;
        private System.Windows.Forms.ContextMenuStrip listeContext;
        private System.Windows.Forms.ToolStripMenuItem editActionItem;
        private System.Windows.Forms.ToolStripMenuItem statutTSMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportMenuItem;
        private System.Windows.Forms.ContextMenuStrip linksContext;

    }
}
