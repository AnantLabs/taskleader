namespace TaskLeader.GUI
{
    partial class ManipAction
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
            this.components = new System.ComponentModel.Container();
            this.saveButton = new System.Windows.Forms.Button();
            this.destBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.actionDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.desField = new System.Windows.Forms.TextBox();
            this.sujetBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.contexteBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.statutBox = new System.Windows.Forms.ComboBox();
            this.noDueDate = new System.Windows.Forms.CheckBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.label3 = new System.Windows.Forms.Label();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.linksLabel = new System.Windows.Forms.Label();
            this.addPJBut = new System.Windows.Forms.Button();
            this.AddMailLabel = new System.Windows.Forms.Label();
            this.linksView = new System.Windows.Forms.ListView();
            this.linksViewMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.deleteEncItem = new System.Windows.Forms.ToolStripMenuItem();
            this.biblio = new System.Windows.Forms.ImageList(this.components);
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.label7 = new System.Windows.Forms.Label();
            this.dbsBox = new System.Windows.Forms.ComboBox();
            this.addLinksMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.ajouterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.mailItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.linksViewMenu.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.addLinksMenu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(203, 30);
            this.saveButton.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.saveButton.Name = "saveButton";
            this.saveButton.Size = new System.Drawing.Size(81, 23);
            this.saveButton.TabIndex = 21;
            this.saveButton.Text = "Sauvegarder";
            this.saveButton.UseVisualStyleBackColor = true;
            this.saveButton.Click += new System.EventHandler(this.sauveAction);
            // 
            // destBox
            // 
            this.destBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.destBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.flowLayoutPanel2.SetFlowBreak(this.destBox, true);
            this.destBox.FormattingEnabled = true;
            this.destBox.Location = new System.Drawing.Point(75, 3);
            this.destBox.Name = "destBox";
            this.destBox.Size = new System.Drawing.Size(240, 21);
            this.destBox.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 7);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Destinataire:";
            // 
            // actionDatePicker
            // 
            this.actionDatePicker.Location = new System.Drawing.Point(75, 30);
            this.actionDatePicker.Name = "actionDatePicker";
            this.actionDatePicker.Size = new System.Drawing.Size(177, 20);
            this.actionDatePicker.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(15, 33);
            this.label4.Margin = new System.Windows.Forms.Padding(15, 6, 3, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Due date:";
            // 
            // desField
            // 
            this.desField.Location = new System.Drawing.Point(3, 21);
            this.desField.Multiline = true;
            this.desField.Name = "desField";
            this.desField.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.desField.Size = new System.Drawing.Size(312, 155);
            this.desField.TabIndex = 16;
            // 
            // sujetBox
            // 
            this.sujetBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.sujetBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.flowLayoutPanel3.SetFlowBreak(this.sujetBox, true);
            this.sujetBox.FormattingEnabled = true;
            this.sujetBox.Location = new System.Drawing.Point(61, 30);
            this.sujetBox.Name = "sujetBox";
            this.sujetBox.Size = new System.Drawing.Size(222, 21);
            this.sujetBox.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(21, 33);
            this.label2.Margin = new System.Windows.Forms.Padding(21, 6, 3, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Sujet:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Contexte:";
            // 
            // contexteBox
            // 
            this.contexteBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.contexteBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.flowLayoutPanel3.SetFlowBreak(this.contexteBox, true);
            this.contexteBox.FormattingEnabled = true;
            this.contexteBox.Location = new System.Drawing.Point(61, 3);
            this.contexteBox.Name = "contexteBox";
            this.contexteBox.Size = new System.Drawing.Size(222, 21);
            this.contexteBox.TabIndex = 11;
            this.contexteBox.SelectedIndexChanged += new System.EventHandler(this.contexteBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.flowLayoutPanel4.SetFlowBreak(this.label6, true);
            this.label6.Location = new System.Drawing.Point(106, 7);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 7, 3, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Statut:";
            // 
            // statutBox
            // 
            this.statutBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statutBox.FormattingEnabled = true;
            this.statutBox.Location = new System.Drawing.Point(150, 3);
            this.statutBox.Margin = new System.Windows.Forms.Padding(3, 3, 10, 3);
            this.statutBox.Name = "statutBox";
            this.statutBox.Size = new System.Drawing.Size(134, 21);
            this.statutBox.TabIndex = 23;
            // 
            // noDueDate
            // 
            this.noDueDate.AutoSize = true;
            this.noDueDate.Location = new System.Drawing.Point(258, 33);
            this.noDueDate.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.noDueDate.Name = "noDueDate";
            this.noDueDate.Size = new System.Drawing.Size(63, 17);
            this.noDueDate.TabIndex = 24;
            this.noDueDate.Text = "Aucune";
            this.noDueDate.UseVisualStyleBackColor = true;
            this.noDueDate.CheckedChanged += new System.EventHandler(this.dateChosen_CheckedChanged);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 330F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 270F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel3, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel4, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel5, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 189F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 8F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(630, 299);
            this.tableLayoutPanel1.TabIndex = 27;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.desField);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 45);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(324, 183);
            this.flowLayoutPanel1.TabIndex = 17;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.flowLayoutPanel1.SetFlowBreak(this.label3, true);
            this.label3.Location = new System.Drawing.Point(3, 5);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 5, 3, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Descriptif:";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.label5);
            this.flowLayoutPanel2.Controls.Add(this.destBox);
            this.flowLayoutPanel2.Controls.Add(this.label4);
            this.flowLayoutPanel2.Controls.Add(this.actionDatePicker);
            this.flowLayoutPanel2.Controls.Add(this.noDueDate);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 234);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(324, 62);
            this.flowLayoutPanel2.TabIndex = 18;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.label1);
            this.flowLayoutPanel3.Controls.Add(this.contexteBox);
            this.flowLayoutPanel3.Controls.Add(this.label2);
            this.flowLayoutPanel3.Controls.Add(this.sujetBox);
            this.flowLayoutPanel3.Controls.Add(this.linksLabel);
            this.flowLayoutPanel3.Controls.Add(this.addPJBut);
            this.flowLayoutPanel3.Controls.Add(this.AddMailLabel);
            this.flowLayoutPanel3.Controls.Add(this.linksView);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(333, 45);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(294, 183);
            this.flowLayoutPanel3.TabIndex = 19;
            // 
            // linksLabel
            // 
            this.linksLabel.AutoSize = true;
            this.linksLabel.Location = new System.Drawing.Point(20, 62);
            this.linksLabel.Margin = new System.Windows.Forms.Padding(20, 8, 3, 0);
            this.linksLabel.Name = "linksLabel";
            this.linksLabel.Size = new System.Drawing.Size(35, 13);
            this.linksLabel.TabIndex = 13;
            this.linksLabel.Text = "Liens:";
            // 
            // addPJBut
            // 
            this.addPJBut.Location = new System.Drawing.Point(61, 57);
            this.addPJBut.Name = "addPJBut";
            this.addPJBut.Size = new System.Drawing.Size(49, 23);
            this.addPJBut.TabIndex = 28;
            this.addPJBut.Text = "Ajouter";
            this.addPJBut.UseVisualStyleBackColor = true;
            this.addPJBut.Click += new System.EventHandler(this.addPJBut_Click);
            // 
            // AddMailLabel
            // 
            this.AddMailLabel.AutoSize = true;
            this.AddMailLabel.Location = new System.Drawing.Point(116, 62);
            this.AddMailLabel.Margin = new System.Windows.Forms.Padding(3, 8, 3, 0);
            this.AddMailLabel.Name = "AddMailLabel";
            this.AddMailLabel.Size = new System.Drawing.Size(75, 13);
            this.AddMailLabel.TabIndex = 29;
            this.AddMailLabel.Text = "Statut addMail";
            this.AddMailLabel.Visible = false;
            // 
            // linksView
            // 
            this.linksView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.linksView.ContextMenuStrip = this.linksViewMenu;
            this.linksView.LabelEdit = true;
            this.linksView.Location = new System.Drawing.Point(60, 86);
            this.linksView.Margin = new System.Windows.Forms.Padding(60, 3, 3, 3);
            this.linksView.MultiSelect = false;
            this.linksView.Name = "linksView";
            this.linksView.Size = new System.Drawing.Size(222, 90);
            this.linksView.SmallImageList = this.biblio;
            this.linksView.TabIndex = 27;
            this.linksView.UseCompatibleStateImageBehavior = false;
            this.linksView.View = System.Windows.Forms.View.SmallIcon;
            this.linksView.Visible = false;
            this.linksView.ItemActivate += new System.EventHandler(this.pj_Click);
            // 
            // linksViewMenu
            // 
            this.linksViewMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.deleteEncItem});
            this.linksViewMenu.Name = "linksViewMenu";
            this.linksViewMenu.ShowImageMargin = false;
            this.linksViewMenu.Size = new System.Drawing.Size(139, 26);
            this.linksViewMenu.Opening += new System.ComponentModel.CancelEventHandler(this.linksViewMenu_Opening);
            // 
            // deleteEncItem
            // 
            this.deleteEncItem.Name = "deleteEncItem";
            this.deleteEncItem.Size = new System.Drawing.Size(138, 22);
            this.deleteEncItem.Text = "Supprimer le lien";
            this.deleteEncItem.Click += new System.EventHandler(this.deleteEncItem_Click);
            // 
            // biblio
            // 
            this.biblio.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.biblio.ImageSize = new System.Drawing.Size(30, 30);
            this.biblio.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.statutBox);
            this.flowLayoutPanel4.Controls.Add(this.label6);
            this.flowLayoutPanel4.Controls.Add(this.saveButton);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(333, 234);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(294, 62);
            this.flowLayoutPanel4.TabIndex = 20;
            // 
            // flowLayoutPanel5
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.flowLayoutPanel5, 2);
            this.flowLayoutPanel5.Controls.Add(this.pictureBox1);
            this.flowLayoutPanel5.Controls.Add(this.label7);
            this.flowLayoutPanel5.Controls.Add(this.dbsBox);
            this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(624, 36);
            this.flowLayoutPanel5.TabIndex = 21;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(43, 10);
            this.label7.Margin = new System.Windows.Forms.Padding(3, 10, 3, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(79, 13);
            this.label7.TabIndex = 0;
            this.label7.Text = "Base d\'actions:";
            // 
            // dbsBox
            // 
            this.dbsBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.dbsBox.FormattingEnabled = true;
            this.dbsBox.Location = new System.Drawing.Point(128, 7);
            this.dbsBox.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.dbsBox.Name = "dbsBox";
            this.dbsBox.Size = new System.Drawing.Size(164, 21);
            this.dbsBox.TabIndex = 1;
            this.dbsBox.SelectedValueChanged += new System.EventHandler(this.changeDB);
            // 
            // addLinksMenu
            // 
            this.addLinksMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajouterToolStripMenuItem,
            this.mailItem});
            this.addLinksMenu.Name = "linksMenu";
            this.addLinksMenu.Size = new System.Drawing.Size(144, 48);
            // 
            // ajouterToolStripMenuItem
            // 
            this.ajouterToolStripMenuItem.Image = global::TaskLeader.Properties.Resources.shortcut;
            this.ajouterToolStripMenuItem.Name = "ajouterToolStripMenuItem";
            this.ajouterToolStripMenuItem.Size = new System.Drawing.Size(143, 22);
            this.ajouterToolStripMenuItem.Text = "Fichier / URL";
            this.ajouterToolStripMenuItem.Click += new System.EventHandler(this.ajouterLink_Click);
            // 
            // mailItem
            // 
            this.mailItem.Image = global::TaskLeader.Properties.Resources.outlook;
            this.mailItem.Name = "mailItem";
            this.mailItem.Size = new System.Drawing.Size(143, 22);
            this.mailItem.Text = "Mail Outlook";
            this.mailItem.Click += new System.EventHandler(this.mailItem_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::TaskLeader.Properties.Resources.database_go32;
            this.pictureBox1.Location = new System.Drawing.Point(5, 2);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(5, 2, 3, 0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(32, 32);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.pictureBox1.TabIndex = 2;
            this.pictureBox1.TabStop = false;
            // 
            // ManipAction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 299);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "ManipAction";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.TopMost = true;
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.ManipAction_FormClosed);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.linksViewMenu.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.addLinksMenu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ComboBox destBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker actionDatePicker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox desField;
        private System.Windows.Forms.ComboBox sujetBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox contexteBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox statutBox;
        private System.Windows.Forms.CheckBox noDueDate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label linksLabel;
        private System.Windows.Forms.ImageList biblio;
        private System.Windows.Forms.ToolStripMenuItem ajouterToolStripMenuItem;
        private System.Windows.Forms.Button addPJBut;
        private System.Windows.Forms.ContextMenuStrip addLinksMenu;
        public System.Windows.Forms.ToolStripMenuItem mailItem;
        private System.Windows.Forms.ListView linksView;
        private System.Windows.Forms.Label AddMailLabel;
        private System.Windows.Forms.ContextMenuStrip linksViewMenu;
        private System.Windows.Forms.ToolStripMenuItem deleteEncItem;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox dbsBox;
        private System.Windows.Forms.PictureBox pictureBox1;


    }
}

