namespace TaskLeader.GUI
{
    partial class DatePickerPopup
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
            this.calendar = new System.Windows.Forms.MonthCalendar();
            this.noDueDate = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.validBut = new System.Windows.Forms.Button();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.closeBox = new System.Windows.Forms.PictureBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closeBox)).BeginInit();
            this.SuspendLayout();
            // 
            // calendar
            // 
            this.tableLayoutPanel1.SetColumnSpan(this.calendar, 2);
            this.calendar.Location = new System.Drawing.Point(5, 26);
            this.calendar.Margin = new System.Windows.Forms.Padding(5, 3, 5, 5);
            this.calendar.MaxSelectionCount = 1;
            this.calendar.Name = "calendar";
            this.calendar.ShowWeekNumbers = true;
            this.calendar.TabIndex = 0;
            // 
            // noDueDate
            // 
            this.noDueDate.AutoSize = true;
            this.noDueDate.Location = new System.Drawing.Point(3, 199);
            this.noDueDate.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.noDueDate.Name = "noDueDate";
            this.noDueDate.Size = new System.Drawing.Size(63, 17);
            this.noDueDate.TabIndex = 1;
            this.noDueDate.Text = "Aucune";
            this.noDueDate.UseVisualStyleBackColor = true;
            this.noDueDate.CheckStateChanged += new System.EventHandler(this.noDueDate_CheckStateChanged);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 5);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 5, 3, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(117, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Sélectionner une date :";
            // 
            // validBut
            // 
            this.validBut.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.validBut.Location = new System.Drawing.Point(181, 196);
            this.validBut.Name = "validBut";
            this.validBut.Size = new System.Drawing.Size(75, 23);
            this.validBut.TabIndex = 2;
            this.validBut.Text = "Valider";
            this.validBut.UseVisualStyleBackColor = true;
            this.validBut.Click += new System.EventHandler(this.validBut_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.validBut, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.calendar, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.noDueDate, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.closeBox, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(259, 222);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // closeBox
            // 
            this.closeBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.closeBox.BackColor = System.Drawing.Color.Transparent;
            this.closeBox.Cursor = System.Windows.Forms.Cursors.Hand;
            this.closeBox.Image = global::TaskLeader.Properties.Resources.cross;
            this.closeBox.Location = new System.Drawing.Point(240, 3);
            this.closeBox.Name = "closeBox";
            this.closeBox.Size = new System.Drawing.Size(16, 16);
            this.closeBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.AutoSize;
            this.closeBox.TabIndex = 5;
            this.closeBox.TabStop = false;
            this.closeBox.Click += new System.EventHandler(this.closeBut_Click);
            // 
            // DatePickerPopup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "DatePickerPopup";
            this.Size = new System.Drawing.Size(259, 222);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.closeBox)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MonthCalendar calendar;
        private System.Windows.Forms.CheckBox noDueDate;
        private System.Windows.Forms.Button validBut;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.PictureBox closeBox;
    }
}
