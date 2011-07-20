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
            this.linksView = new System.Windows.Forms.ListView();
            this.images = new System.Windows.Forms.ImageList(this.components);
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
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
            this.desField.Size = new System.Drawing.Size(312, 130);
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
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel3, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel4, 1, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 72.56637F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 27.43363F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(630, 226);
            this.tableLayoutPanel1.TabIndex = 27;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.label3);
            this.flowLayoutPanel1.Controls.Add(this.desField);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(324, 157);
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
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 166);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(324, 57);
            this.flowLayoutPanel2.TabIndex = 18;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.label1);
            this.flowLayoutPanel3.Controls.Add(this.contexteBox);
            this.flowLayoutPanel3.Controls.Add(this.label2);
            this.flowLayoutPanel3.Controls.Add(this.sujetBox);
            this.flowLayoutPanel3.Controls.Add(this.linksLabel);
            this.flowLayoutPanel3.Controls.Add(this.linksView);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(333, 3);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(294, 157);
            this.flowLayoutPanel3.TabIndex = 19;
            // 
            // linksLabel
            // 
            this.linksLabel.AutoSize = true;
            this.linksLabel.Location = new System.Drawing.Point(20, 60);
            this.linksLabel.Margin = new System.Windows.Forms.Padding(20, 6, 3, 0);
            this.linksLabel.Name = "linksLabel";
            this.linksLabel.Size = new System.Drawing.Size(35, 13);
            this.linksLabel.TabIndex = 13;
            this.linksLabel.Text = "Liens:";
            this.linksLabel.Visible = false;
            // 
            // linksView
            // 
            this.linksView.Activation = System.Windows.Forms.ItemActivation.OneClick;
            this.linksView.LabelEdit = true;
            this.linksView.Location = new System.Drawing.Point(61, 57);
            this.linksView.MultiSelect = false;
            this.linksView.Name = "linksView";
            this.linksView.Size = new System.Drawing.Size(222, 97);
            this.linksView.SmallImageList = this.images;
            this.linksView.TabIndex = 27;
            this.linksView.UseCompatibleStateImageBehavior = false;
            this.linksView.View = System.Windows.Forms.View.List;
            this.linksView.Visible = false;
            this.linksView.ItemActivate += new System.EventHandler(this.link_Click);
            // 
            // images
            // 
            this.images.ColorDepth = System.Windows.Forms.ColorDepth.Depth32Bit;
            this.images.ImageSize = new System.Drawing.Size(30, 30);
            this.images.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.statutBox);
            this.flowLayoutPanel4.Controls.Add(this.label6);
            this.flowLayoutPanel4.Controls.Add(this.saveButton);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(333, 166);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(294, 57);
            this.flowLayoutPanel4.TabIndex = 20;
            // 
            // ManipAction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(630, 226);
            this.Controls.Add(this.tableLayoutPanel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "ManipAction";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TaskLeader - ";
            this.TopMost = true;
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.flowLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel2.PerformLayout();
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.flowLayoutPanel4.ResumeLayout(false);
            this.flowLayoutPanel4.PerformLayout();
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
        private System.Windows.Forms.ListView linksView;
        private System.Windows.Forms.ImageList images;


    }
}

