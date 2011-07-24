namespace TaskLeader.GUI
{
    partial class AdminDefaut
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

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.ctxtListBox = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.sujetListBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.destListBox = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.statutListBox = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.filterCombo = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.saveBut = new System.Windows.Forms.Button();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.ctxtListBox);
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.sujetListBox);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.destListBox);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.statutListBox);
            this.flowLayoutPanel1.Controls.Add(this.label4);
            this.flowLayoutPanel1.Controls.Add(this.filterCombo);
            this.flowLayoutPanel1.Controls.Add(this.label5);
            this.flowLayoutPanel1.Controls.Add(this.saveBut);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(270, 189);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // ctxtListBox
            // 
            this.ctxtListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ctxtListBox.FormattingEnabled = true;
            this.ctxtListBox.Location = new System.Drawing.Point(80, 12);
            this.ctxtListBox.Margin = new System.Windows.Forms.Padding(3, 12, 10, 3);
            this.ctxtListBox.Name = "ctxtListBox";
            this.ctxtListBox.Size = new System.Drawing.Size(180, 21);
            this.ctxtListBox.TabIndex = 1;
            this.ctxtListBox.SelectedIndexChanged += new System.EventHandler(this.updateSujet);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.label1, true);
            this.label1.Location = new System.Drawing.Point(22, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 15, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Contexte:";
            // 
            // sujetListBox
            // 
            this.sujetListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.sujetListBox.FormattingEnabled = true;
            this.sujetListBox.Location = new System.Drawing.Point(80, 39);
            this.sujetListBox.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.sujetListBox.Name = "sujetListBox";
            this.sujetListBox.Size = new System.Drawing.Size(180, 21);
            this.sujetListBox.TabIndex = 3;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.label2, true);
            this.label2.Location = new System.Drawing.Point(40, 43);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Sujet:";
            // 
            // destListBox
            // 
            this.destListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.destListBox.FormattingEnabled = true;
            this.destListBox.Location = new System.Drawing.Point(80, 66);
            this.destListBox.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.destListBox.Name = "destListBox";
            this.destListBox.Size = new System.Drawing.Size(180, 21);
            this.destListBox.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.label3, true);
            this.label3.Location = new System.Drawing.Point(8, 70);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(66, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Destinataire:";
            // 
            // statutListBox
            // 
            this.statutListBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statutListBox.FormattingEnabled = true;
            this.statutListBox.Location = new System.Drawing.Point(80, 93);
            this.statutListBox.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.statutListBox.Name = "statutListBox";
            this.statutListBox.Size = new System.Drawing.Size(180, 21);
            this.statutListBox.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(36, 97);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(38, 13);
            this.label4.TabIndex = 2;
            this.label4.Text = "Statut:";
            // 
            // filterCombo
            // 
            this.filterCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.filterCombo.FormattingEnabled = true;
            this.filterCombo.Location = new System.Drawing.Point(80, 120);
            this.filterCombo.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.filterCombo.Name = "filterCombo";
            this.filterCombo.Size = new System.Drawing.Size(180, 21);
            this.filterCombo.TabIndex = 3;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(42, 124);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(32, 13);
            this.label5.TabIndex = 2;
            this.label5.Text = "Filtre:";
            // 
            // saveBut
            // 
            this.saveBut.Location = new System.Drawing.Point(95, 159);
            this.saveBut.Margin = new System.Windows.Forms.Padding(3, 15, 95, 3);
            this.saveBut.Name = "saveBut";
            this.saveBut.Size = new System.Drawing.Size(80, 23);
            this.saveBut.TabIndex = 4;
            this.saveBut.Text = "Sauvegarder";
            this.saveBut.UseVisualStyleBackColor = true;
            this.saveBut.Click += new System.EventHandler(this.saveBut_Click);
            // 
            // AdminDefaut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(270, 189);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "AdminDefaut";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Choix des valeurs par défaut";
            this.Load += new System.EventHandler(this.AdminDefaut_Load);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox ctxtListBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox sujetListBox;
        private System.Windows.Forms.ComboBox destListBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox statutListBox;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox filterCombo;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button saveBut;
    }
}