namespace TaskLeader.GUI
{
    partial class Toolbox
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Toolbox));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.filterCombo = new System.Windows.Forms.ComboBox();
            this.openFilterBut = new System.Windows.Forms.Button();
            this.saveFilterBut = new System.Windows.Forms.Button();
            this.groupBox7 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel10 = new System.Windows.Forms.FlowLayoutPanel();
            this.allCtxt = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.ctxtListBox = new System.Windows.Forms.CheckedListBox();
            this.allSujt = new System.Windows.Forms.CheckBox();
            this.label3 = new System.Windows.Forms.Label();
            this.sujetListBox = new System.Windows.Forms.CheckedListBox();
            this.allDest = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.destListBox = new System.Windows.Forms.CheckedListBox();
            this.allStat = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.statutListBox = new System.Windows.Forms.CheckedListBox();
            this.filtreBout = new System.Windows.Forms.Button();
            this.flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.addBout = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.flowLayoutPanel9 = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.searchButton = new System.Windows.Forms.Button();
            this.grilleData = new System.Windows.Forms.DataGridView();
            this.listeContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editActionItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statutTSMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.groupBox7.SuspendLayout();
            this.flowLayoutPanel10.SuspendLayout();
            this.flowLayoutPanel8.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flowLayoutPanel9.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grilleData)).BeginInit();
            this.listeContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel8, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(614, 662);
            this.tableLayoutPanel1.TabIndex = 4;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Controls.Add(this.groupBox7);
            this.flowLayoutPanel1.Controls.Add(this.filtreBout);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(164, 656);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flowLayoutPanel4);
            this.groupBox2.Location = new System.Drawing.Point(4, 3);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(157, 90);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Filtres enregistrés";
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.filterCombo);
            this.flowLayoutPanel4.Controls.Add(this.openFilterBut);
            this.flowLayoutPanel4.Controls.Add(this.saveFilterBut);
            this.flowLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel4.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(151, 71);
            this.flowLayoutPanel4.TabIndex = 0;
            // 
            // filterCombo
            // 
            this.filterCombo.FormattingEnabled = true;
            this.filterCombo.Location = new System.Drawing.Point(0, 3);
            this.filterCombo.Name = "filterCombo";
            this.filterCombo.Size = new System.Drawing.Size(148, 21);
            this.filterCombo.TabIndex = 8;
            // 
            // openFilterBut
            // 
            this.openFilterBut.Image = global::TaskLeader.Properties.Resources.open;
            this.openFilterBut.Location = new System.Drawing.Point(108, 30);
            this.openFilterBut.Name = "openFilterBut";
            this.openFilterBut.Size = new System.Drawing.Size(40, 40);
            this.openFilterBut.TabIndex = 11;
            this.openFilterBut.UseVisualStyleBackColor = true;
            this.openFilterBut.Click += new System.EventHandler(this.openFilterBut_Click);
            // 
            // saveFilterBut
            // 
            this.saveFilterBut.Image = global::TaskLeader.Properties.Resources.sauve;
            this.saveFilterBut.Location = new System.Drawing.Point(62, 30);
            this.saveFilterBut.Name = "saveFilterBut";
            this.saveFilterBut.Size = new System.Drawing.Size(40, 40);
            this.saveFilterBut.TabIndex = 10;
            this.saveFilterBut.UseVisualStyleBackColor = true;
            this.saveFilterBut.Click += new System.EventHandler(this.saveFilterBut_Click);
            // 
            // groupBox7
            // 
            this.groupBox7.Controls.Add(this.flowLayoutPanel10);
            this.groupBox7.Location = new System.Drawing.Point(4, 99);
            this.groupBox7.Name = "groupBox7";
            this.groupBox7.Size = new System.Drawing.Size(157, 500);
            this.groupBox7.TabIndex = 11;
            this.groupBox7.TabStop = false;
            this.groupBox7.Text = "Filtres manuels";
            // 
            // flowLayoutPanel10
            // 
            this.flowLayoutPanel10.Controls.Add(this.allCtxt);
            this.flowLayoutPanel10.Controls.Add(this.label2);
            this.flowLayoutPanel10.Controls.Add(this.ctxtListBox);
            this.flowLayoutPanel10.Controls.Add(this.allSujt);
            this.flowLayoutPanel10.Controls.Add(this.label3);
            this.flowLayoutPanel10.Controls.Add(this.sujetListBox);
            this.flowLayoutPanel10.Controls.Add(this.allDest);
            this.flowLayoutPanel10.Controls.Add(this.label4);
            this.flowLayoutPanel10.Controls.Add(this.destListBox);
            this.flowLayoutPanel10.Controls.Add(this.allStat);
            this.flowLayoutPanel10.Controls.Add(this.label5);
            this.flowLayoutPanel10.Controls.Add(this.statutListBox);
            this.flowLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel10.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel10.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel10.Name = "flowLayoutPanel10";
            this.flowLayoutPanel10.Size = new System.Drawing.Size(151, 481);
            this.flowLayoutPanel10.TabIndex = 0;
            // 
            // allCtxt
            // 
            this.allCtxt.AutoSize = true;
            this.allCtxt.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.allCtxt.Checked = true;
            this.allCtxt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.allCtxt.Location = new System.Drawing.Point(98, 3);
            this.allCtxt.Name = "allCtxt";
            this.allCtxt.Size = new System.Drawing.Size(50, 17);
            this.allCtxt.TabIndex = 10;
            this.allCtxt.Text = "Tous";
            this.allCtxt.UseVisualStyleBackColor = true;
            this.allCtxt.Click += new System.EventHandler(this.allBox_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(3, 4);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 4, 38, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(54, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Contextes";
            // 
            // ctxtListBox
            // 
            this.ctxtListBox.CheckOnClick = true;
            this.ctxtListBox.FormattingEnabled = true;
            this.ctxtListBox.Location = new System.Drawing.Point(0, 26);
            this.ctxtListBox.Name = "ctxtListBox";
            this.ctxtListBox.Size = new System.Drawing.Size(148, 94);
            this.ctxtListBox.TabIndex = 1;
            this.ctxtListBox.Click += new System.EventHandler(this.listBox_Click);
            this.ctxtListBox.SelectedIndexChanged += new System.EventHandler(this.updateSujet);
            // 
            // allSujt
            // 
            this.allSujt.AutoSize = true;
            this.allSujt.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.allSujt.Checked = true;
            this.allSujt.CheckState = System.Windows.Forms.CheckState.Checked;
            this.allSujt.Enabled = false;
            this.allSujt.Location = new System.Drawing.Point(98, 126);
            this.allSujt.Name = "allSujt";
            this.allSujt.Size = new System.Drawing.Size(50, 17);
            this.allSujt.TabIndex = 10;
            this.allSujt.Text = "Tous";
            this.allSujt.UseVisualStyleBackColor = true;
            this.allSujt.Click += new System.EventHandler(this.allBox_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(3, 127);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 4, 56, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(36, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Sujets";
            // 
            // sujetListBox
            // 
            this.sujetListBox.CheckOnClick = true;
            this.sujetListBox.Enabled = false;
            this.sujetListBox.FormattingEnabled = true;
            this.sujetListBox.Location = new System.Drawing.Point(0, 149);
            this.sujetListBox.Name = "sujetListBox";
            this.sujetListBox.Size = new System.Drawing.Size(148, 94);
            this.sujetListBox.TabIndex = 3;
            this.sujetListBox.Click += new System.EventHandler(this.listBox_Click);
            // 
            // allDest
            // 
            this.allDest.AutoSize = true;
            this.allDest.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.allDest.Checked = true;
            this.allDest.CheckState = System.Windows.Forms.CheckState.Checked;
            this.allDest.Location = new System.Drawing.Point(98, 249);
            this.allDest.Name = "allDest";
            this.allDest.Size = new System.Drawing.Size(50, 17);
            this.allDest.TabIndex = 10;
            this.allDest.Text = "Tous";
            this.allDest.UseVisualStyleBackColor = true;
            this.allDest.Click += new System.EventHandler(this.allBox_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(3, 250);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 4, 24, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(68, 13);
            this.label4.TabIndex = 9;
            this.label4.Text = "Destinataires";
            // 
            // destListBox
            // 
            this.destListBox.CheckOnClick = true;
            this.destListBox.FormattingEnabled = true;
            this.destListBox.Location = new System.Drawing.Point(0, 272);
            this.destListBox.Name = "destListBox";
            this.destListBox.Size = new System.Drawing.Size(148, 94);
            this.destListBox.TabIndex = 5;
            this.destListBox.Click += new System.EventHandler(this.listBox_Click);
            // 
            // allStat
            // 
            this.allStat.AutoSize = true;
            this.allStat.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.allStat.Checked = true;
            this.allStat.CheckState = System.Windows.Forms.CheckState.Checked;
            this.allStat.Location = new System.Drawing.Point(98, 372);
            this.allStat.Name = "allStat";
            this.allStat.Size = new System.Drawing.Size(50, 17);
            this.allStat.TabIndex = 10;
            this.allStat.Text = "Tous";
            this.allStat.UseVisualStyleBackColor = true;
            this.allStat.Click += new System.EventHandler(this.allBox_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(3, 373);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 4, 52, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(40, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Statuts";
            // 
            // statutListBox
            // 
            this.statutListBox.CheckOnClick = true;
            this.statutListBox.FormattingEnabled = true;
            this.statutListBox.Location = new System.Drawing.Point(0, 395);
            this.statutListBox.Name = "statutListBox";
            this.statutListBox.Size = new System.Drawing.Size(148, 79);
            this.statutListBox.TabIndex = 7;
            this.statutListBox.Click += new System.EventHandler(this.listBox_Click);
            // 
            // filtreBout
            // 
            this.filtreBout.Image = global::TaskLeader.Properties.Resources.liste;
            this.filtreBout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.filtreBout.Location = new System.Drawing.Point(57, 605);
            this.filtreBout.Name = "filtreBout";
            this.filtreBout.Size = new System.Drawing.Size(104, 35);
            this.filtreBout.TabIndex = 8;
            this.filtreBout.Text = "Afficher liste";
            this.filtreBout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.filtreBout.UseVisualStyleBackColor = true;
            this.filtreBout.Click += new System.EventHandler(this.filtreAction);
            // 
            // flowLayoutPanel8
            // 
            this.flowLayoutPanel8.Controls.Add(this.groupBox1);
            this.flowLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel8.Location = new System.Drawing.Point(547, 3);
            this.flowLayoutPanel8.Name = "flowLayoutPanel8";
            this.flowLayoutPanel8.Size = new System.Drawing.Size(64, 656);
            this.flowLayoutPanel8.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel2);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(56, 101);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ajouter action";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.addBout);
            this.flowLayoutPanel2.Controls.Add(this.button1);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(50, 82);
            this.flowLayoutPanel2.TabIndex = 0;
            // 
            // addBout
            // 
            this.addBout.Image = global::TaskLeader.Properties.Resources.add;
            this.addBout.Location = new System.Drawing.Point(3, 3);
            this.addBout.Name = "addBout";
            this.addBout.Size = new System.Drawing.Size(35, 35);
            this.addBout.TabIndex = 0;
            this.addBout.UseVisualStyleBackColor = true;
            this.addBout.Click += new System.EventHandler(this.ajoutAction);
            // 
            // button1
            // 
            this.button1.Image = global::TaskLeader.Properties.Resources.evernote;
            this.button1.Location = new System.Drawing.Point(3, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 35);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.flowLayoutPanel9, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.grilleData, 0, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(170, 0);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel2.Size = new System.Drawing.Size(374, 662);
            this.tableLayoutPanel2.TabIndex = 7;
            // 
            // flowLayoutPanel9
            // 
            this.flowLayoutPanel9.Controls.Add(this.label1);
            this.flowLayoutPanel9.Controls.Add(this.searchBox);
            this.flowLayoutPanel9.Controls.Add(this.searchButton);
            this.flowLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel9.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel9.Name = "flowLayoutPanel9";
            this.flowLayoutPanel9.Size = new System.Drawing.Size(368, 24);
            this.flowLayoutPanel9.TabIndex = 5;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 6);
            this.label1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 3);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Rechercher:";
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(75, 3);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(200, 20);
            this.searchBox.TabIndex = 1;
            // 
            // searchButton
            // 
            this.searchButton.Location = new System.Drawing.Point(281, 3);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(30, 20);
            this.searchButton.TabIndex = 2;
            this.searchButton.Text = "OK";
            this.searchButton.UseVisualStyleBackColor = true;
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
            this.grilleData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grilleData.Location = new System.Drawing.Point(0, 30);
            this.grilleData.Margin = new System.Windows.Forms.Padding(0);
            this.grilleData.MultiSelect = false;
            this.grilleData.Name = "grilleData";
            this.grilleData.ReadOnly = true;
            this.grilleData.RowHeadersVisible = false;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grilleData.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.grilleData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grilleData.Size = new System.Drawing.Size(374, 632);
            this.grilleData.TabIndex = 4;
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
            this.listeContext.Size = new System.Drawing.Size(159, 70);
            // 
            // editActionItem
            // 
            this.editActionItem.Name = "editActionItem";
            this.editActionItem.Size = new System.Drawing.Size(158, 22);
            this.editActionItem.Text = "Editer l\'action";
            this.editActionItem.Click += new System.EventHandler(this.modifAction);
            // 
            // statutTSMenuItem
            // 
            this.statutTSMenuItem.Name = "statutTSMenuItem";
            this.statutTSMenuItem.Size = new System.Drawing.Size(158, 22);
            this.statutTSMenuItem.Text = "Passer l\'action à";
            // 
            // exportMenuItem
            // 
            this.exportMenuItem.Name = "exportMenuItem";
            this.exportMenuItem.Size = new System.Drawing.Size(158, 22);
            this.exportMenuItem.Text = "Exporter vers";
            // 
            // Toolbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(614, 662);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(435, 10);
            this.Name = "Toolbox";
            this.Text = "TaskLeader";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Toolbox_Load);
            this.Resize += new System.EventHandler(this.Toolbox_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.groupBox7.ResumeLayout(false);
            this.flowLayoutPanel10.ResumeLayout(false);
            this.flowLayoutPanel10.PerformLayout();
            this.flowLayoutPanel8.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flowLayoutPanel9.ResumeLayout(false);
            this.flowLayoutPanel9.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grilleData)).EndInit();
            this.listeContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.DataGridView grilleData;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.CheckedListBox ctxtListBox;
        private System.Windows.Forms.CheckedListBox sujetListBox;
        private System.Windows.Forms.CheckedListBox destListBox;
        private System.Windows.Forms.CheckedListBox statutListBox;
        private System.Windows.Forms.Button filtreBout;
        private System.Windows.Forms.ContextMenuStrip listeContext;
        private System.Windows.Forms.ToolStripMenuItem statutTSMenuItem;
        private System.Windows.Forms.ComboBox filterCombo;
        private System.Windows.Forms.Button saveFilterBut;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button addBout;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button openFilterBut;
        private System.Windows.Forms.ToolStripMenuItem editActionItem;
        private System.Windows.Forms.ToolStripMenuItem exportMenuItem;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel9;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckBox allCtxt;
        private System.Windows.Forms.GroupBox groupBox7;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel10;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.CheckBox allSujt;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox allDest;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.CheckBox allStat;

    }
}