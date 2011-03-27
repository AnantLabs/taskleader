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
            this.grilleData = new System.Windows.Forms.DataGridView();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.filterCombo = new System.Windows.Forms.ComboBox();
            this.openFilterBut = new System.Windows.Forms.Button();
            this.saveFilterBut = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.CtxtSelRadio = new System.Windows.Forms.RadioButton();
            this.CtxtAllRadio = new System.Windows.Forms.RadioButton();
            this.ctxtListBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel5 = new System.Windows.Forms.FlowLayoutPanel();
            this.SujSelRadio = new System.Windows.Forms.RadioButton();
            this.SujAllRadio = new System.Windows.Forms.RadioButton();
            this.sujetListBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox5 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel6 = new System.Windows.Forms.FlowLayoutPanel();
            this.destSelRadio = new System.Windows.Forms.RadioButton();
            this.destAllRadio = new System.Windows.Forms.RadioButton();
            this.destListBox = new System.Windows.Forms.CheckedListBox();
            this.groupBox6 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel7 = new System.Windows.Forms.FlowLayoutPanel();
            this.statSelRadio = new System.Windows.Forms.RadioButton();
            this.statAllRadio = new System.Windows.Forms.RadioButton();
            this.statutListBox = new System.Windows.Forms.CheckedListBox();
            this.filtreBout = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.flowLayoutPanel8 = new System.Windows.Forms.FlowLayoutPanel();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.flowLayoutPanel2 = new System.Windows.Forms.FlowLayoutPanel();
            this.addBout = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.outlookBout = new System.Windows.Forms.Button();
            this.listeContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editActionItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statutTSMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grilleData)).BeginInit();
            this.flowLayoutPanel1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.flowLayoutPanel5.SuspendLayout();
            this.groupBox5.SuspendLayout();
            this.flowLayoutPanel6.SuspendLayout();
            this.groupBox6.SuspendLayout();
            this.flowLayoutPanel7.SuspendLayout();
            this.flowLayoutPanel8.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.flowLayoutPanel2.SuspendLayout();
            this.listeContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 170F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.Controls.Add(this.grilleData, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.flowLayoutPanel8, 2, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(484, 742);
            this.tableLayoutPanel1.TabIndex = 4;
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
            this.grilleData.Location = new System.Drawing.Point(173, 3);
            this.grilleData.MultiSelect = false;
            this.grilleData.Name = "grilleData";
            this.grilleData.ReadOnly = true;
            this.grilleData.RowHeadersVisible = false;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grilleData.RowsDefaultCellStyle = dataGridViewCellStyle2;
            this.grilleData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grilleData.Size = new System.Drawing.Size(238, 736);
            this.grilleData.TabIndex = 4;
            this.grilleData.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.modifAction);
            this.grilleData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grilleData_CellFormatting);
            this.grilleData.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grilleData_CellMouseClick);
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.groupBox2);
            this.flowLayoutPanel1.Controls.Add(this.groupBox3);
            this.flowLayoutPanel1.Controls.Add(this.groupBox4);
            this.flowLayoutPanel1.Controls.Add(this.groupBox5);
            this.flowLayoutPanel1.Controls.Add(this.groupBox6);
            this.flowLayoutPanel1.Controls.Add(this.filtreBout);
            this.flowLayoutPanel1.Controls.Add(this.button2);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(3, 3);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(164, 736);
            this.flowLayoutPanel1.TabIndex = 5;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.flowLayoutPanel4);
            this.groupBox2.Location = new System.Drawing.Point(3, 3);
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
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.flowLayoutPanel3);
            this.groupBox3.Location = new System.Drawing.Point(3, 99);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(157, 146);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Contextes";
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.CtxtSelRadio);
            this.flowLayoutPanel3.Controls.Add(this.CtxtAllRadio);
            this.flowLayoutPanel3.Controls.Add(this.ctxtListBox);
            this.flowLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel3.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(151, 127);
            this.flowLayoutPanel3.TabIndex = 15;
            // 
            // CtxtSelRadio
            // 
            this.CtxtSelRadio.AutoSize = true;
            this.CtxtSelRadio.Location = new System.Drawing.Point(3, 3);
            this.CtxtSelRadio.Name = "CtxtSelRadio";
            this.CtxtSelRadio.Size = new System.Drawing.Size(69, 17);
            this.CtxtSelRadio.TabIndex = 13;
            this.CtxtSelRadio.Text = "Sélection";
            this.CtxtSelRadio.UseVisualStyleBackColor = true;
            // 
            // CtxtAllRadio
            // 
            this.CtxtAllRadio.AutoSize = true;
            this.CtxtAllRadio.Checked = true;
            this.CtxtAllRadio.Location = new System.Drawing.Point(78, 3);
            this.CtxtAllRadio.Name = "CtxtAllRadio";
            this.CtxtAllRadio.Size = new System.Drawing.Size(49, 17);
            this.CtxtAllRadio.TabIndex = 14;
            this.CtxtAllRadio.TabStop = true;
            this.CtxtAllRadio.Text = "Tous";
            this.CtxtAllRadio.UseVisualStyleBackColor = true;
            this.CtxtAllRadio.CheckedChanged += new System.EventHandler(this.CtxtAllRadio_CheckedChanged);
            // 
            // ctxtListBox
            // 
            this.ctxtListBox.CheckOnClick = true;
            this.ctxtListBox.Enabled = false;
            this.ctxtListBox.FormattingEnabled = true;
            this.ctxtListBox.Location = new System.Drawing.Point(3, 26);
            this.ctxtListBox.Name = "ctxtListBox";
            this.ctxtListBox.Size = new System.Drawing.Size(148, 94);
            this.ctxtListBox.TabIndex = 1;
            this.ctxtListBox.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.updateSujet);
            this.ctxtListBox.SelectedIndexChanged += new System.EventHandler(this.updateSujet);
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.flowLayoutPanel5);
            this.groupBox4.Location = new System.Drawing.Point(3, 251);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(157, 146);
            this.groupBox4.TabIndex = 4;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Sujets";
            // 
            // flowLayoutPanel5
            // 
            this.flowLayoutPanel5.Controls.Add(this.SujSelRadio);
            this.flowLayoutPanel5.Controls.Add(this.SujAllRadio);
            this.flowLayoutPanel5.Controls.Add(this.sujetListBox);
            this.flowLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel5.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel5.Name = "flowLayoutPanel5";
            this.flowLayoutPanel5.Size = new System.Drawing.Size(151, 127);
            this.flowLayoutPanel5.TabIndex = 0;
            // 
            // SujSelRadio
            // 
            this.SujSelRadio.AutoSize = true;
            this.SujSelRadio.Enabled = false;
            this.SujSelRadio.Location = new System.Drawing.Point(3, 3);
            this.SujSelRadio.Name = "SujSelRadio";
            this.SujSelRadio.Size = new System.Drawing.Size(69, 17);
            this.SujSelRadio.TabIndex = 13;
            this.SujSelRadio.Text = "Sélection";
            this.SujSelRadio.UseVisualStyleBackColor = true;
            // 
            // SujAllRadio
            // 
            this.SujAllRadio.AutoSize = true;
            this.SujAllRadio.Checked = true;
            this.SujAllRadio.Enabled = false;
            this.SujAllRadio.Location = new System.Drawing.Point(78, 3);
            this.SujAllRadio.Name = "SujAllRadio";
            this.SujAllRadio.Size = new System.Drawing.Size(49, 17);
            this.SujAllRadio.TabIndex = 14;
            this.SujAllRadio.TabStop = true;
            this.SujAllRadio.Text = "Tous";
            this.SujAllRadio.UseVisualStyleBackColor = true;
            this.SujAllRadio.CheckedChanged += new System.EventHandler(this.SujAllRadio_CheckedChanged);
            // 
            // sujetListBox
            // 
            this.sujetListBox.CheckOnClick = true;
            this.sujetListBox.Enabled = false;
            this.sujetListBox.FormattingEnabled = true;
            this.sujetListBox.Location = new System.Drawing.Point(3, 26);
            this.sujetListBox.Name = "sujetListBox";
            this.sujetListBox.Size = new System.Drawing.Size(148, 94);
            this.sujetListBox.TabIndex = 3;
            // 
            // groupBox5
            // 
            this.groupBox5.Controls.Add(this.flowLayoutPanel6);
            this.groupBox5.Location = new System.Drawing.Point(3, 403);
            this.groupBox5.Name = "groupBox5";
            this.groupBox5.Size = new System.Drawing.Size(157, 146);
            this.groupBox5.TabIndex = 5;
            this.groupBox5.TabStop = false;
            this.groupBox5.Text = "Destinataires";
            // 
            // flowLayoutPanel6
            // 
            this.flowLayoutPanel6.Controls.Add(this.destSelRadio);
            this.flowLayoutPanel6.Controls.Add(this.destAllRadio);
            this.flowLayoutPanel6.Controls.Add(this.destListBox);
            this.flowLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel6.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel6.Name = "flowLayoutPanel6";
            this.flowLayoutPanel6.Size = new System.Drawing.Size(151, 127);
            this.flowLayoutPanel6.TabIndex = 0;
            // 
            // destSelRadio
            // 
            this.destSelRadio.AutoSize = true;
            this.destSelRadio.Location = new System.Drawing.Point(3, 3);
            this.destSelRadio.Name = "destSelRadio";
            this.destSelRadio.Size = new System.Drawing.Size(69, 17);
            this.destSelRadio.TabIndex = 13;
            this.destSelRadio.Text = "Sélection";
            this.destSelRadio.UseVisualStyleBackColor = true;
            // 
            // destAllRadio
            // 
            this.destAllRadio.AutoSize = true;
            this.destAllRadio.Checked = true;
            this.destAllRadio.Location = new System.Drawing.Point(78, 3);
            this.destAllRadio.Name = "destAllRadio";
            this.destAllRadio.Size = new System.Drawing.Size(49, 17);
            this.destAllRadio.TabIndex = 14;
            this.destAllRadio.TabStop = true;
            this.destAllRadio.Text = "Tous";
            this.destAllRadio.UseVisualStyleBackColor = true;
            this.destAllRadio.CheckedChanged += new System.EventHandler(this.destAllRadio_CheckedChanged);
            // 
            // destListBox
            // 
            this.destListBox.CheckOnClick = true;
            this.destListBox.Enabled = false;
            this.destListBox.FormattingEnabled = true;
            this.destListBox.Location = new System.Drawing.Point(3, 26);
            this.destListBox.Name = "destListBox";
            this.destListBox.Size = new System.Drawing.Size(148, 94);
            this.destListBox.TabIndex = 5;
            // 
            // groupBox6
            // 
            this.groupBox6.Controls.Add(this.flowLayoutPanel7);
            this.groupBox6.Location = new System.Drawing.Point(3, 555);
            this.groupBox6.Name = "groupBox6";
            this.groupBox6.Size = new System.Drawing.Size(157, 130);
            this.groupBox6.TabIndex = 6;
            this.groupBox6.TabStop = false;
            this.groupBox6.Text = "Statuts";
            // 
            // flowLayoutPanel7
            // 
            this.flowLayoutPanel7.Controls.Add(this.statSelRadio);
            this.flowLayoutPanel7.Controls.Add(this.statAllRadio);
            this.flowLayoutPanel7.Controls.Add(this.statutListBox);
            this.flowLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel7.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel7.Name = "flowLayoutPanel7";
            this.flowLayoutPanel7.Size = new System.Drawing.Size(151, 111);
            this.flowLayoutPanel7.TabIndex = 0;
            // 
            // statSelRadio
            // 
            this.statSelRadio.AutoSize = true;
            this.statSelRadio.Location = new System.Drawing.Point(3, 3);
            this.statSelRadio.Name = "statSelRadio";
            this.statSelRadio.Size = new System.Drawing.Size(69, 17);
            this.statSelRadio.TabIndex = 13;
            this.statSelRadio.Text = "Sélection";
            this.statSelRadio.UseVisualStyleBackColor = true;
            // 
            // statAllRadio
            // 
            this.statAllRadio.AutoSize = true;
            this.statAllRadio.Checked = true;
            this.statAllRadio.Location = new System.Drawing.Point(78, 3);
            this.statAllRadio.Name = "statAllRadio";
            this.statAllRadio.Size = new System.Drawing.Size(49, 17);
            this.statAllRadio.TabIndex = 14;
            this.statAllRadio.TabStop = true;
            this.statAllRadio.Text = "Tous";
            this.statAllRadio.UseVisualStyleBackColor = true;
            this.statAllRadio.CheckedChanged += new System.EventHandler(this.statAllRadio_CheckedChanged);
            // 
            // statutListBox
            // 
            this.statutListBox.CheckOnClick = true;
            this.statutListBox.Enabled = false;
            this.statutListBox.FormattingEnabled = true;
            this.statutListBox.Location = new System.Drawing.Point(3, 26);
            this.statutListBox.Name = "statutListBox";
            this.statutListBox.Size = new System.Drawing.Size(148, 79);
            this.statutListBox.TabIndex = 7;
            // 
            // filtreBout
            // 
            this.filtreBout.Image = global::TaskLeader.Properties.Resources.liste;
            this.filtreBout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.filtreBout.Location = new System.Drawing.Point(3, 691);
            this.filtreBout.Name = "filtreBout";
            this.filtreBout.Size = new System.Drawing.Size(104, 35);
            this.filtreBout.TabIndex = 8;
            this.filtreBout.Text = "Afficher liste";
            this.filtreBout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.filtreBout.UseVisualStyleBackColor = true;
            this.filtreBout.Click += new System.EventHandler(this.filtreAction);
            // 
            // button2
            // 
            this.button2.Enabled = false;
            this.button2.Image = global::TaskLeader.Properties.Resources.alerte;
            this.button2.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.button2.Location = new System.Drawing.Point(113, 691);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(40, 37);
            this.button2.TabIndex = 2;
            this.button2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.button2.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel8
            // 
            this.flowLayoutPanel8.Controls.Add(this.groupBox1);
            this.flowLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel8.Location = new System.Drawing.Point(417, 3);
            this.flowLayoutPanel8.Name = "flowLayoutPanel8";
            this.flowLayoutPanel8.Size = new System.Drawing.Size(64, 736);
            this.flowLayoutPanel8.TabIndex = 6;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.flowLayoutPanel2);
            this.groupBox1.Location = new System.Drawing.Point(3, 3);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(56, 148);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Ajouter action";
            // 
            // flowLayoutPanel2
            // 
            this.flowLayoutPanel2.Controls.Add(this.addBout);
            this.flowLayoutPanel2.Controls.Add(this.button1);
            this.flowLayoutPanel2.Controls.Add(this.outlookBout);
            this.flowLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel2.Location = new System.Drawing.Point(3, 16);
            this.flowLayoutPanel2.Name = "flowLayoutPanel2";
            this.flowLayoutPanel2.Size = new System.Drawing.Size(50, 129);
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
            this.button1.Enabled = false;
            this.button1.Image = global::TaskLeader.Properties.Resources.evernote;
            this.button1.Location = new System.Drawing.Point(3, 44);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(35, 35);
            this.button1.TabIndex = 0;
            this.button1.UseVisualStyleBackColor = true;
            // 
            // outlookBout
            // 
            this.outlookBout.Enabled = false;
            this.outlookBout.Image = global::TaskLeader.Properties.Resources.outlook;
            this.outlookBout.Location = new System.Drawing.Point(3, 85);
            this.outlookBout.Name = "outlookBout";
            this.outlookBout.Size = new System.Drawing.Size(35, 35);
            this.outlookBout.TabIndex = 1;
            this.outlookBout.UseVisualStyleBackColor = true;
            // 
            // listeContext
            // 
            this.listeContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editActionItem,
            this.statutTSMenuItem,
            this.exportMenuItem});
            this.listeContext.Name = "listeContext";
            this.listeContext.Size = new System.Drawing.Size(159, 92);
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
            this.ClientSize = new System.Drawing.Size(484, 742);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(435, 10);
            this.Name = "Toolbox";
            this.Text = "TaskLeader";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Toolbox_Load);
            this.Resize += new System.EventHandler(this.Toolbox_Resize);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grilleData)).EndInit();
            this.flowLayoutPanel1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.flowLayoutPanel5.ResumeLayout(false);
            this.flowLayoutPanel5.PerformLayout();
            this.groupBox5.ResumeLayout(false);
            this.flowLayoutPanel6.ResumeLayout(false);
            this.flowLayoutPanel6.PerformLayout();
            this.groupBox6.ResumeLayout(false);
            this.flowLayoutPanel7.ResumeLayout(false);
            this.flowLayoutPanel7.PerformLayout();
            this.flowLayoutPanel8.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.flowLayoutPanel2.ResumeLayout(false);
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
        private System.Windows.Forms.Button button2;
        private System.Windows.Forms.ContextMenuStrip listeContext;
        private System.Windows.Forms.ToolStripMenuItem statutTSMenuItem;
        private System.Windows.Forms.ComboBox filterCombo;
        private System.Windows.Forms.Button saveFilterBut;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.RadioButton CtxtSelRadio;
        private System.Windows.Forms.RadioButton CtxtAllRadio;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel5;
        private System.Windows.Forms.RadioButton SujSelRadio;
        private System.Windows.Forms.RadioButton SujAllRadio;
        private System.Windows.Forms.GroupBox groupBox5;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel6;
        private System.Windows.Forms.RadioButton destSelRadio;
        private System.Windows.Forms.RadioButton destAllRadio;
        private System.Windows.Forms.GroupBox groupBox6;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel7;
        private System.Windows.Forms.RadioButton statSelRadio;
        private System.Windows.Forms.RadioButton statAllRadio;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel8;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel2;
        private System.Windows.Forms.Button addBout;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button outlookBout;
        private System.Windows.Forms.Button openFilterBut;
        private System.Windows.Forms.ToolStripMenuItem editActionItem;
        private System.Windows.Forms.ToolStripMenuItem exportMenuItem;

    }
}