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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Toolbox));
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            this.mainTableLayout = new System.Windows.Forms.TableLayoutPanel();
            this.statusPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.searchFlowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.typeLabel = new System.Windows.Forms.Label();
            this.searchedText = new System.Windows.Forms.Label();
            this.exitSearchBut = new System.Windows.Forms.Button();
            this.resultLabel = new System.Windows.Forms.Label();
            this.topMenu = new System.Windows.Forms.MenuStrip();
            this.ajouterItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nouvelleActionToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsEvernoteToolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.adminItem = new System.Windows.Forms.ToolStripMenuItem();
            this.baseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.defaultValuesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.manuelTable = new System.Windows.Forms.TableLayoutPanel();
            this.selectPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.saveFilterCheck = new System.Windows.Forms.CheckBox();
            this.filtreBout = new System.Windows.Forms.Button();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel3 = new System.Windows.Forms.FlowLayoutPanel();
            this.searchButton = new System.Windows.Forms.Button();
            this.searchBox = new System.Windows.Forms.TextBox();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.flowLayoutPanel4 = new System.Windows.Forms.FlowLayoutPanel();
            this.dbsTree = new System.Windows.Forms.TreeView();
            this.button1 = new System.Windows.Forms.Button();
            this.grilleData = new System.Windows.Forms.DataGridView();
            this.ajouterToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.nouvelleActionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsEvernoteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.listeContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.editActionItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statutTSMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ajouterToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.nouvelleActionToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.actionsEvernoteToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.linksContext = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.mainTableLayout.SuspendLayout();
            this.statusPanel.SuspendLayout();
            this.searchFlowLayoutPanel.SuspendLayout();
            this.topMenu.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.manuelTable.SuspendLayout();
            this.flowLayoutPanel1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.flowLayoutPanel3.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.flowLayoutPanel4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grilleData)).BeginInit();
            this.listeContext.SuspendLayout();
            this.SuspendLayout();
            // 
            // mainTableLayout
            // 
            this.mainTableLayout.ColumnCount = 3;
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.mainTableLayout.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.mainTableLayout.Controls.Add(this.statusPanel, 0, 1);
            this.mainTableLayout.Controls.Add(this.topMenu, 1, 1);
            this.mainTableLayout.Controls.Add(this.tabControl1, 0, 0);
            this.mainTableLayout.Controls.Add(this.button1, 2, 0);
            this.mainTableLayout.Controls.Add(this.grilleData, 0, 2);
            this.mainTableLayout.Dock = System.Windows.Forms.DockStyle.Fill;
            this.mainTableLayout.Location = new System.Drawing.Point(0, 0);
            this.mainTableLayout.Margin = new System.Windows.Forms.Padding(0);
            this.mainTableLayout.Name = "mainTableLayout";
            this.mainTableLayout.RowCount = 3;
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 155F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.mainTableLayout.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.mainTableLayout.Size = new System.Drawing.Size(721, 750);
            this.mainTableLayout.TabIndex = 7;
            // 
            // statusPanel
            // 
            this.statusPanel.Controls.Add(this.searchFlowLayoutPanel);
            this.statusPanel.Controls.Add(this.resultLabel);
            this.statusPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.statusPanel.Location = new System.Drawing.Point(0, 155);
            this.statusPanel.Margin = new System.Windows.Forms.Padding(0);
            this.statusPanel.Name = "statusPanel";
            this.statusPanel.Size = new System.Drawing.Size(599, 30);
            this.statusPanel.TabIndex = 5;
            // 
            // searchFlowLayoutPanel
            // 
            this.searchFlowLayoutPanel.AutoSize = true;
            this.searchFlowLayoutPanel.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.searchFlowLayoutPanel.BackColor = System.Drawing.Color.LightSteelBlue;
            this.searchFlowLayoutPanel.Controls.Add(this.typeLabel);
            this.searchFlowLayoutPanel.Controls.Add(this.searchedText);
            this.searchFlowLayoutPanel.Controls.Add(this.exitSearchBut);
            this.searchFlowLayoutPanel.Location = new System.Drawing.Point(5, 4);
            this.searchFlowLayoutPanel.Margin = new System.Windows.Forms.Padding(5, 4, 0, 4);
            this.searchFlowLayoutPanel.Name = "searchFlowLayoutPanel";
            this.searchFlowLayoutPanel.Size = new System.Drawing.Size(98, 22);
            this.searchFlowLayoutPanel.TabIndex = 5;
            this.searchFlowLayoutPanel.Visible = false;
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
            // resultLabel
            // 
            this.resultLabel.AutoSize = true;
            this.resultLabel.Location = new System.Drawing.Point(113, 8);
            this.resultLabel.Margin = new System.Windows.Forms.Padding(10, 8, 3, 0);
            this.resultLabel.Name = "resultLabel";
            this.resultLabel.Size = new System.Drawing.Size(44, 13);
            this.resultLabel.TabIndex = 6;
            this.resultLabel.Text = "Nombre";
            this.resultLabel.Visible = false;
            // 
            // topMenu
            // 
            this.topMenu.BackColor = System.Drawing.SystemColors.Control;
            this.mainTableLayout.SetColumnSpan(this.topMenu, 2);
            this.topMenu.Dock = System.Windows.Forms.DockStyle.Fill;
            this.topMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ajouterItem,
            this.adminItem});
            this.topMenu.Location = new System.Drawing.Point(599, 158);
            this.topMenu.Margin = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.topMenu.Name = "topMenu";
            this.topMenu.Size = new System.Drawing.Size(122, 27);
            this.topMenu.TabIndex = 6;
            this.topMenu.Text = "menuStrip1";
            // 
            // ajouterItem
            // 
            this.ajouterItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nouvelleActionToolStripMenuItem2,
            this.actionsEvernoteToolStripMenuItem2});
            this.ajouterItem.Name = "ajouterItem";
            this.ajouterItem.ShowShortcutKeys = false;
            this.ajouterItem.Size = new System.Drawing.Size(58, 23);
            this.ajouterItem.Text = "Ajouter";
            // 
            // nouvelleActionToolStripMenuItem2
            // 
            this.nouvelleActionToolStripMenuItem2.Image = global::TaskLeader.Properties.Resources.add;
            this.nouvelleActionToolStripMenuItem2.Name = "nouvelleActionToolStripMenuItem2";
            this.nouvelleActionToolStripMenuItem2.ShowShortcutKeys = false;
            this.nouvelleActionToolStripMenuItem2.Size = new System.Drawing.Size(156, 22);
            this.nouvelleActionToolStripMenuItem2.Text = "Nouvelle action";
            this.nouvelleActionToolStripMenuItem2.Click += new System.EventHandler(this.ajoutAction);
            // 
            // actionsEvernoteToolStripMenuItem2
            // 
            this.actionsEvernoteToolStripMenuItem2.Enabled = false;
            this.actionsEvernoteToolStripMenuItem2.Image = global::TaskLeader.Properties.Resources.evernote;
            this.actionsEvernoteToolStripMenuItem2.Name = "actionsEvernoteToolStripMenuItem2";
            this.actionsEvernoteToolStripMenuItem2.ShowShortcutKeys = false;
            this.actionsEvernoteToolStripMenuItem2.Size = new System.Drawing.Size(156, 22);
            this.actionsEvernoteToolStripMenuItem2.Text = "Actions Evernote";
            // 
            // adminItem
            // 
            this.adminItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.baseToolStripMenuItem,
            this.defaultValuesToolStripMenuItem});
            this.adminItem.Name = "adminItem";
            this.adminItem.Size = new System.Drawing.Size(55, 23);
            this.adminItem.Text = "Admin";
            // 
            // baseToolStripMenuItem
            // 
            this.baseToolStripMenuItem.Name = "baseToolStripMenuItem";
            this.baseToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.baseToolStripMenuItem.Text = "Base d\'actions actives";
            // 
            // defaultValuesToolStripMenuItem
            // 
            this.defaultValuesToolStripMenuItem.Name = "defaultValuesToolStripMenuItem";
            this.defaultValuesToolStripMenuItem.Size = new System.Drawing.Size(188, 22);
            this.defaultValuesToolStripMenuItem.Text = "Valeurs par défaut";
            this.defaultValuesToolStripMenuItem.Click += new System.EventHandler(this.defaultValuesToolStripMenuItem_Click);
            // 
            // tabControl1
            // 
            this.tabControl1.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.mainTableLayout.SetColumnSpan(this.tabControl1, 2);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Location = new System.Drawing.Point(3, 3);
            this.tabControl1.Margin = new System.Windows.Forms.Padding(3, 3, 0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(686, 152);
            this.tabControl1.TabIndex = 8;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.manuelTable);
            this.tabPage1.Location = new System.Drawing.Point(4, 25);
            this.tabPage1.Margin = new System.Windows.Forms.Padding(0);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Size = new System.Drawing.Size(678, 123);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "Filtre manuel";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // manuelTable
            // 
            this.manuelTable.ColumnCount = 3;
            this.manuelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 19.56912F));
            this.manuelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80.43088F));
            this.manuelTable.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 117F));
            this.manuelTable.Controls.Add(this.selectPanel, 1, 0);
            this.manuelTable.Controls.Add(this.flowLayoutPanel1, 2, 0);
            this.manuelTable.Dock = System.Windows.Forms.DockStyle.Fill;
            this.manuelTable.Location = new System.Drawing.Point(0, 0);
            this.manuelTable.Margin = new System.Windows.Forms.Padding(0);
            this.manuelTable.Name = "manuelTable";
            this.manuelTable.RowCount = 1;
            this.manuelTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.manuelTable.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.manuelTable.Size = new System.Drawing.Size(678, 123);
            this.manuelTable.TabIndex = 2;
            // 
            // selectPanel
            // 
            this.selectPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.selectPanel.Location = new System.Drawing.Point(109, 0);
            this.selectPanel.Margin = new System.Windows.Forms.Padding(0);
            this.selectPanel.Name = "selectPanel";
            this.selectPanel.Size = new System.Drawing.Size(451, 123);
            this.selectPanel.TabIndex = 1;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Controls.Add(this.saveFilterCheck);
            this.flowLayoutPanel1.Controls.Add(this.filtreBout);
            this.flowLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flowLayoutPanel1.Location = new System.Drawing.Point(560, 0);
            this.flowLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(118, 123);
            this.flowLayoutPanel1.TabIndex = 2;
            // 
            // saveFilterCheck
            // 
            this.saveFilterCheck.AutoSize = true;
            this.saveFilterCheck.Location = new System.Drawing.Point(3, 10);
            this.saveFilterCheck.Margin = new System.Windows.Forms.Padding(3, 10, 10, 3);
            this.saveFilterCheck.Name = "saveFilterCheck";
            this.saveFilterCheck.Size = new System.Drawing.Size(98, 17);
            this.saveFilterCheck.TabIndex = 11;
            this.saveFilterCheck.Text = "Enregistrer filtre";
            this.saveFilterCheck.UseVisualStyleBackColor = true;
            // 
            // filtreBout
            // 
            this.filtreBout.Image = global::TaskLeader.Properties.Resources.filtre;
            this.filtreBout.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.filtreBout.Location = new System.Drawing.Point(3, 33);
            this.filtreBout.Name = "filtreBout";
            this.filtreBout.Size = new System.Drawing.Size(65, 30);
            this.filtreBout.TabIndex = 8;
            this.filtreBout.Text = "Filtrer";
            this.filtreBout.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.filtreBout.UseVisualStyleBackColor = true;
            this.filtreBout.Click += new System.EventHandler(this.filtreAction);
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.flowLayoutPanel3);
            this.tabPage2.Location = new System.Drawing.Point(4, 25);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(678, 123);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "Recherche";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel3
            // 
            this.flowLayoutPanel3.Controls.Add(this.searchButton);
            this.flowLayoutPanel3.Controls.Add(this.searchBox);
            this.flowLayoutPanel3.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowLayoutPanel3.Location = new System.Drawing.Point(4, 12);
            this.flowLayoutPanel3.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel3.Name = "flowLayoutPanel3";
            this.flowLayoutPanel3.Size = new System.Drawing.Size(185, 35);
            this.flowLayoutPanel3.TabIndex = 0;
            // 
            // searchButton
            // 
            this.searchButton.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("searchButton.BackgroundImage")));
            this.searchButton.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.searchButton.Location = new System.Drawing.Point(153, 2);
            this.searchButton.Margin = new System.Windows.Forms.Padding(2);
            this.searchButton.Name = "searchButton";
            this.searchButton.Size = new System.Drawing.Size(30, 30);
            this.searchButton.TabIndex = 2;
            this.searchButton.UseVisualStyleBackColor = true;
            this.searchButton.Click += new System.EventHandler(this.searchButton_Click);
            // 
            // searchBox
            // 
            this.searchBox.Location = new System.Drawing.Point(3, 7);
            this.searchBox.Margin = new System.Windows.Forms.Padding(3, 7, 3, 3);
            this.searchBox.Name = "searchBox";
            this.searchBox.Size = new System.Drawing.Size(145, 20);
            this.searchBox.TabIndex = 1;
            this.searchBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.searchBox_KeyPress);
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.flowLayoutPanel4);
            this.tabPage3.Location = new System.Drawing.Point(4, 25);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Size = new System.Drawing.Size(678, 123);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "Filtres enregistrés";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // flowLayoutPanel4
            // 
            this.flowLayoutPanel4.Controls.Add(this.dbsTree);
            this.flowLayoutPanel4.FlowDirection = System.Windows.Forms.FlowDirection.RightToLeft;
            this.flowLayoutPanel4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flowLayoutPanel4.Location = new System.Drawing.Point(39, 23);
            this.flowLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.flowLayoutPanel4.Name = "flowLayoutPanel4";
            this.flowLayoutPanel4.Size = new System.Drawing.Size(182, 111);
            this.flowLayoutPanel4.TabIndex = 0;
            // 
            // dbsTree
            // 
            this.dbsTree.CheckBoxes = true;
            this.dbsTree.Location = new System.Drawing.Point(3, 3);
            this.dbsTree.Name = "dbsTree";
            this.dbsTree.Size = new System.Drawing.Size(176, 105);
            this.dbsTree.TabIndex = 12;
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(692, 3);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(26, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "^";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
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
            this.mainTableLayout.SetColumnSpan(this.grilleData, 3);
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle2.BackColor = System.Drawing.SystemColors.Window;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.SystemColors.ControlText;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.SystemColors.Highlight;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.SystemColors.HighlightText;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.grilleData.DefaultCellStyle = dataGridViewCellStyle2;
            this.grilleData.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grilleData.Location = new System.Drawing.Point(0, 185);
            this.grilleData.Margin = new System.Windows.Forms.Padding(0);
            this.grilleData.MultiSelect = false;
            this.grilleData.Name = "grilleData";
            this.grilleData.ReadOnly = true;
            this.grilleData.RowHeadersVisible = false;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.grilleData.RowsDefaultCellStyle = dataGridViewCellStyle3;
            this.grilleData.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.grilleData.Size = new System.Drawing.Size(721, 720);
            this.grilleData.TabIndex = 4;
            this.grilleData.CellContentDoubleClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.modifAction);
            this.grilleData.CellFormatting += new System.Windows.Forms.DataGridViewCellFormattingEventHandler(this.grilleData_CellFormatting);
            this.grilleData.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.grilleData_CellMouseClick);
            this.grilleData.CellMouseEnter += new System.Windows.Forms.DataGridViewCellEventHandler(this.grilleData_CellMouseEnter);
            this.grilleData.CellMouseLeave += new System.Windows.Forms.DataGridViewCellEventHandler(this.grilleData_CellMouseLeave);
            // 
            // ajouterToolStripMenuItem
            // 
            this.ajouterToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.nouvelleActionToolStripMenuItem,
            this.actionsEvernoteToolStripMenuItem});
            this.ajouterToolStripMenuItem.Name = "ajouterToolStripMenuItem";
            this.ajouterToolStripMenuItem.ShowShortcutKeys = false;
            this.ajouterToolStripMenuItem.Size = new System.Drawing.Size(51, 19);
            this.ajouterToolStripMenuItem.Text = "Ajouter";
            // 
            // nouvelleActionToolStripMenuItem
            // 
            this.nouvelleActionToolStripMenuItem.Image = global::TaskLeader.Properties.Resources.add;
            this.nouvelleActionToolStripMenuItem.Name = "nouvelleActionToolStripMenuItem";
            this.nouvelleActionToolStripMenuItem.ShowShortcutKeys = false;
            this.nouvelleActionToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.nouvelleActionToolStripMenuItem.Text = "Nouvelle action";
            this.nouvelleActionToolStripMenuItem.Click += new System.EventHandler(this.ajoutAction);
            // 
            // actionsEvernoteToolStripMenuItem
            // 
            this.actionsEvernoteToolStripMenuItem.Image = global::TaskLeader.Properties.Resources.evernote;
            this.actionsEvernoteToolStripMenuItem.Name = "actionsEvernoteToolStripMenuItem";
            this.actionsEvernoteToolStripMenuItem.ShowShortcutKeys = false;
            this.actionsEvernoteToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.actionsEvernoteToolStripMenuItem.Text = "Actions Evernote";
            // 
            // listeContext
            // 
            this.listeContext.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editActionItem,
            this.statutTSMenuItem,
            this.exportMenuItem});
            this.listeContext.Name = "listeContext";
            this.listeContext.ShowImageMargin = false;
            this.listeContext.Size = new System.Drawing.Size(134, 70);
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
            // ajouterToolStripMenuItem1
            // 
            this.ajouterToolStripMenuItem1.Name = "ajouterToolStripMenuItem1";
            this.ajouterToolStripMenuItem1.ShowShortcutKeys = false;
            this.ajouterToolStripMenuItem1.Size = new System.Drawing.Size(51, 19);
            this.ajouterToolStripMenuItem1.Text = "Ajouter";
            // 
            // nouvelleActionToolStripMenuItem1
            // 
            this.nouvelleActionToolStripMenuItem1.Image = global::TaskLeader.Properties.Resources.add;
            this.nouvelleActionToolStripMenuItem1.Name = "nouvelleActionToolStripMenuItem1";
            this.nouvelleActionToolStripMenuItem1.ShowShortcutKeys = false;
            this.nouvelleActionToolStripMenuItem1.Size = new System.Drawing.Size(156, 22);
            this.nouvelleActionToolStripMenuItem1.Text = "Nouvelle action";
            this.nouvelleActionToolStripMenuItem1.Click += new System.EventHandler(this.ajoutAction);
            // 
            // actionsEvernoteToolStripMenuItem1
            // 
            this.actionsEvernoteToolStripMenuItem1.Enabled = false;
            this.actionsEvernoteToolStripMenuItem1.Image = global::TaskLeader.Properties.Resources.evernote;
            this.actionsEvernoteToolStripMenuItem1.Name = "actionsEvernoteToolStripMenuItem1";
            this.actionsEvernoteToolStripMenuItem1.ShowShortcutKeys = false;
            this.actionsEvernoteToolStripMenuItem1.Size = new System.Drawing.Size(156, 22);
            this.actionsEvernoteToolStripMenuItem1.Text = "Actions Evernote";
            // 
            // linksContext
            // 
            this.linksContext.Name = "linksContext";
            this.linksContext.Size = new System.Drawing.Size(61, 4);
            // 
            // Toolbox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.AutoSize = true;
            this.ClientSize = new System.Drawing.Size(721, 750);
            this.Controls.Add(this.mainTableLayout);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(435, 10);
            this.MainMenuStrip = this.topMenu;
            this.Name = "Toolbox";
            this.Text = "Liste des actions - TaskLeader";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Toolbox_Load);
            this.Resize += new System.EventHandler(this.Toolbox_Resize);
            this.mainTableLayout.ResumeLayout(false);
            this.mainTableLayout.PerformLayout();
            this.statusPanel.ResumeLayout(false);
            this.statusPanel.PerformLayout();
            this.searchFlowLayoutPanel.ResumeLayout(false);
            this.searchFlowLayoutPanel.PerformLayout();
            this.topMenu.ResumeLayout(false);
            this.topMenu.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.manuelTable.ResumeLayout(false);
            this.flowLayoutPanel1.ResumeLayout(false);
            this.flowLayoutPanel1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.flowLayoutPanel3.ResumeLayout(false);
            this.flowLayoutPanel3.PerformLayout();
            this.tabPage3.ResumeLayout(false);
            this.flowLayoutPanel4.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grilleData)).EndInit();
            this.listeContext.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button filtreBout;
        private System.Windows.Forms.ContextMenuStrip listeContext;
        private System.Windows.Forms.ToolStripMenuItem statutTSMenuItem;
        private System.Windows.Forms.ToolStripMenuItem editActionItem;
        private System.Windows.Forms.ToolStripMenuItem exportMenuItem;
        private System.Windows.Forms.TableLayoutPanel mainTableLayout;
        private System.Windows.Forms.FlowLayoutPanel statusPanel;
        private System.Windows.Forms.TextBox searchBox;
        private System.Windows.Forms.Button searchButton;
        private System.Windows.Forms.FlowLayoutPanel searchFlowLayoutPanel;
        private System.Windows.Forms.Label searchedText;
        private System.Windows.Forms.Button exitSearchBut;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel4;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel3;
        private System.Windows.Forms.Label resultLabel;
        private System.Windows.Forms.Label typeLabel;
        private System.Windows.Forms.CheckBox saveFilterCheck;
        private System.Windows.Forms.ToolStripMenuItem ajouterToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem nouvelleActionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem actionsEvernoteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ajouterToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem nouvelleActionToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem actionsEvernoteToolStripMenuItem1;
        private System.Windows.Forms.MenuStrip topMenu;
        private System.Windows.Forms.ToolStripMenuItem ajouterItem;
        private System.Windows.Forms.ToolStripMenuItem nouvelleActionToolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem actionsEvernoteToolStripMenuItem2;
        private System.Windows.Forms.ContextMenuStrip linksContext;
        private System.Windows.Forms.ToolStripMenuItem adminItem;
        private System.Windows.Forms.ToolStripMenuItem defaultValuesToolStripMenuItem;
        private System.Windows.Forms.DataGridView grilleData;
        private System.Windows.Forms.ToolStripMenuItem baseToolStripMenuItem;
        private System.Windows.Forms.TreeView dbsTree;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.FlowLayoutPanel selectPanel;
        private System.Windows.Forms.TableLayoutPanel manuelTable;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;

    }
}