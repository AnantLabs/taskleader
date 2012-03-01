namespace TaskLeader.GUI
{
    partial class Etiquette
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
            this.exitSearchBut = new System.Windows.Forms.Button();
            this.searchedText = new System.Windows.Forms.Label();
            this.typeLabel = new System.Windows.Forms.Label();
            this.searchFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.searchFlowLayoutPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // exitSearchBut
            // 
            this.exitSearchBut.Location = new System.Drawing.Point(77, 1);
            this.exitSearchBut.Margin = new System.Windows.Forms.Padding(3, 1, 1, 1);
            this.exitSearchBut.Name = "exitSearchBut";
            this.exitSearchBut.Size = new System.Drawing.Size(20, 20);
            this.exitSearchBut.TabIndex = 3;
            this.exitSearchBut.Text = "X";
            this.exitSearchBut.UseVisualStyleBackColor = true;
            this.exitSearchBut.Click += new System.EventHandler(this.exitSearchBut_Click);
            // 
            // searchedText
            // 
            this.searchedText.AutoSize = true;
            this.searchedText.Location = new System.Drawing.Point(38, 4);
            this.searchedText.Margin = new System.Windows.Forms.Padding(0, 4, 0, 0);
            this.searchedText.Name = "searchedText";
            this.searchedText.Size = new System.Drawing.Size(36, 13);
            this.searchedText.TabIndex = 4;
            this.searchedText.Text = "valeur";
            // 
            // typeLabel
            // 
            this.typeLabel.AutoSize = true;
            this.typeLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.typeLabel.Location = new System.Drawing.Point(3, 4);
            this.typeLabel.Margin = new System.Windows.Forms.Padding(3, 4, 0, 0);
            this.typeLabel.Name = "typeLabel";
            this.typeLabel.Size = new System.Drawing.Size(35, 13);
            this.typeLabel.TabIndex = 5;
            this.typeLabel.Text = "Type";
            // 
            // searchFlowLayoutPanel
            // 
            this.searchFlowLayoutPanel.AutoSize = true;
            this.searchFlowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.searchFlowLayoutPanel.BackColor = System.Drawing.Color.LightSteelBlue;
            this.searchFlowLayoutPanel.Controls.Add(this.typeLabel);
            this.searchFlowLayoutPanel.Controls.Add(this.searchedText);
            this.searchFlowLayoutPanel.Controls.Add(this.exitSearchBut);
            this.searchFlowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.searchFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(5, 4, 0, 4);
            this.searchFlowLayoutPanel.Name = "searchFlowLayoutPanel";
            this.searchFlowLayoutPanel.Size = new System.Drawing.Size(98, 22);
            this.searchFlowLayoutPanel.TabIndex = 6;
            // 
            // Etiquette
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.searchFlowLayoutPanel);
            this.Margin = new System.Windows.Forms.Padding(5, 4, 5, 4);
            this.Name = "Etiquette";
            this.Size = new System.Drawing.Size(98, 26);
            this.searchFlowLayoutPanel.ResumeLayout(false);
            this.searchFlowLayoutPanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button exitSearchBut;
        private System.Windows.Forms.Label searchedText;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.FlowLayoutPanel searchFlowLayoutPanel;

    }
}
