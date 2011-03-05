namespace ActionsList.GUI
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
            this.saveButton = new System.Windows.Forms.Button();
            this.destBox = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.actionDatePicker = new System.Windows.Forms.DateTimePicker();
            this.label4 = new System.Windows.Forms.Label();
            this.desField = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.sujetBox = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.contexteBox = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.statutBox = new System.Windows.Forms.ComboBox();
            this.dateChosen = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // saveButton
            // 
            this.saveButton.Location = new System.Drawing.Point(311, 273);
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
            this.destBox.FormattingEnabled = true;
            this.destBox.Location = new System.Drawing.Point(77, 234);
            this.destBox.Name = "destBox";
            this.destBox.Size = new System.Drawing.Size(312, 21);
            this.destBox.TabIndex = 20;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(5, 237);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(66, 13);
            this.label5.TabIndex = 19;
            this.label5.Text = "Destinataire:";
            // 
            // actionDatePicker
            // 
            this.actionDatePicker.Location = new System.Drawing.Point(77, 205);
            this.actionDatePicker.Name = "actionDatePicker";
            this.actionDatePicker.Size = new System.Drawing.Size(177, 20);
            this.actionDatePicker.TabIndex = 18;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(17, 205);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 13);
            this.label4.TabIndex = 17;
            this.label4.Text = "Due date:";
            // 
            // desField
            // 
            this.desField.Location = new System.Drawing.Point(77, 69);
            this.desField.Multiline = true;
            this.desField.Name = "desField";
            this.desField.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.desField.Size = new System.Drawing.Size(312, 130);
            this.desField.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(17, 69);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(54, 13);
            this.label3.TabIndex = 15;
            this.label3.Text = "Descriptif:";
            // 
            // sujetBox
            // 
            this.sujetBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.sujetBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.sujetBox.FormattingEnabled = true;
            this.sujetBox.Location = new System.Drawing.Point(77, 40);
            this.sujetBox.Name = "sujetBox";
            this.sujetBox.Size = new System.Drawing.Size(312, 21);
            this.sujetBox.TabIndex = 14;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(37, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(34, 13);
            this.label2.TabIndex = 13;
            this.label2.Text = "Sujet:";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(19, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(52, 13);
            this.label1.TabIndex = 12;
            this.label1.Text = "Contexte:";
            // 
            // contexteBox
            // 
            this.contexteBox.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend;
            this.contexteBox.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems;
            this.contexteBox.FormattingEnabled = true;
            this.contexteBox.Location = new System.Drawing.Point(77, 12);
            this.contexteBox.Name = "contexteBox";
            this.contexteBox.Size = new System.Drawing.Size(312, 21);
            this.contexteBox.TabIndex = 11;
            this.contexteBox.SelectedIndexChanged += new System.EventHandler(this.contexteBox_SelectedIndexChanged);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(33, 278);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(38, 13);
            this.label6.TabIndex = 22;
            this.label6.Text = "Statut:";
            // 
            // statutBox
            // 
            this.statutBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.statutBox.FormattingEnabled = true;
            this.statutBox.Location = new System.Drawing.Point(77, 275);
            this.statutBox.Name = "statutBox";
            this.statutBox.Size = new System.Drawing.Size(134, 21);
            this.statutBox.TabIndex = 23;
            // 
            // dateChosen
            // 
            this.dateChosen.AutoSize = true;
            this.dateChosen.Location = new System.Drawing.Point(261, 207);
            this.dateChosen.Name = "dateChosen";
            this.dateChosen.Size = new System.Drawing.Size(63, 17);
            this.dateChosen.TabIndex = 24;
            this.dateChosen.Text = "Aucune";
            this.dateChosen.UseVisualStyleBackColor = true;
            this.dateChosen.CheckedChanged += new System.EventHandler(this.dateChosen_CheckedChanged);
            // 
            // ManipAction
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(404, 305);
            this.Controls.Add(this.dateChosen);
            this.Controls.Add(this.statutBox);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.saveButton);
            this.Controls.Add(this.destBox);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.actionDatePicker);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.desField);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.sujetBox);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.contexteBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "ManipAction";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Titre de la fenetre dynamique";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button saveButton;
        private System.Windows.Forms.ComboBox destBox;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.DateTimePicker actionDatePicker;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox desField;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox sujetBox;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox contexteBox;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.ComboBox statutBox;
        private System.Windows.Forms.CheckBox dateChosen;


    }
}

