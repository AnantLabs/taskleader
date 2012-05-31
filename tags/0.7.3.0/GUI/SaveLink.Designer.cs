namespace TaskLeader.GUI
{
    partial class SaveLink
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
            this.label1 = new System.Windows.Forms.Label();
            this.fileBut = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.pathBox = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.titreBox = new System.Windows.Forms.TextBox();
            this.saveBut = new System.Windows.Forms.Button();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.flowLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label1);
            this.flowLayoutPanel1.Controls.Add(this.fileBut);
            this.flowLayoutPanel1.Controls.Add(this.label2);
            this.flowLayoutPanel1.Controls.Add(this.pathBox);
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.titreBox);
            this.flowLayoutPanel1.Controls.Add(this.saveBut);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(259, 127);
            this.flowLayoutPanel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(25, 15);
            this.label1.Margin = new System.Windows.Forms.Padding(25, 15, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Fichier:";
            // 
            // fileBut
            // 
            this.flowLayoutPanel1.SetFlowBreak(this.fileBut, true);
            this.fileBut.Location = new System.Drawing.Point(72, 10);
            this.fileBut.Margin = new System.Windows.Forms.Padding(3, 10, 3, 3);
            this.fileBut.Name = "fileBut";
            this.fileBut.Size = new System.Drawing.Size(75, 23);
            this.fileBut.TabIndex = 1;
            this.fileBut.Text = "Parcourir";
            this.fileBut.UseVisualStyleBackColor = true;
            this.fileBut.Click += new System.EventHandler(this.fileBut_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 42);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 2;
            this.label2.Text = "Chemin/Url:";
            // 
            // pathBox
            // 
            this.pathBox.Location = new System.Drawing.Point(72, 39);
            this.pathBox.Name = "pathBox";
            this.pathBox.Size = new System.Drawing.Size(180, 20);
            this.pathBox.TabIndex = 3;
            this.pathBox.TextChanged += new System.EventHandler(this.pathBox_TextChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(35, 68);
            this.label3.Margin = new System.Windows.Forms.Padding(35, 6, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(31, 13);
            this.label3.TabIndex = 2;
            this.label3.Text = "Titre:";
            // 
            // titreBox
            // 
            this.titreBox.Location = new System.Drawing.Point(72, 65);
            this.titreBox.Name = "titreBox";
            this.titreBox.Size = new System.Drawing.Size(180, 20);
            this.titreBox.TabIndex = 3;
            // 
            // saveBut
            // 
            this.saveBut.Location = new System.Drawing.Point(105, 98);
            this.saveBut.Margin = new System.Windows.Forms.Padding(105, 10, 3, 3);
            this.saveBut.Name = "saveBut";
            this.saveBut.Size = new System.Drawing.Size(50, 23);
            this.saveBut.TabIndex = 4;
            this.saveBut.Text = "Ajouter";
            this.saveBut.UseVisualStyleBackColor = true;
            this.saveBut.Click += new System.EventHandler(this.saveBut_Click);
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "linkPath";
            this.openFileDialog.Title = "Sélectionner le fichier";
            // 
            // SaveLink
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(259, 127);
            this.Controls.Add(this.flowLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "SaveLink";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Ajouter un raccourci";
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button fileBut;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox pathBox;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox titreBox;
        private System.Windows.Forms.Button saveBut;
    }
}