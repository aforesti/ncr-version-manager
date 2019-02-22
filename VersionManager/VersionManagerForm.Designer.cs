namespace VersionManager
{
    partial class VersionManagerForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VersionManagerForm));
            this.gridBranches = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBranchName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVersao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repItemVersao = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.grid = new DevExpress.XtraGrid.GridControl();
            this.gridProjetos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNome = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repItemLookUp = new DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.BtnConfig = new DevExpress.XtraEditors.SimpleButton();
            this.simpleButton1 = new DevExpress.XtraEditors.SimpleButton();
            ((System.ComponentModel.ISupportInitialize)(this.gridBranches)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemVersao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProjetos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemLookUp)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // gridBranches
            // 
            this.gridBranches.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBranchName,
            this.colVersao});
            this.gridBranches.GridControl = this.grid;
            this.gridBranches.Name = "gridBranches";
            this.gridBranches.OptionsView.ShowGroupPanel = false;
            // 
            // colBranchName
            // 
            this.colBranchName.Caption = "Branch";
            this.colBranchName.FieldName = "Branch";
            this.colBranchName.Name = "colBranchName";
            this.colBranchName.Visible = true;
            this.colBranchName.VisibleIndex = 0;
            // 
            // colVersao
            // 
            this.colVersao.Caption = "versão";
            this.colVersao.ColumnEdit = this.repItemVersao;
            this.colVersao.FieldName = "Versao";
            this.colVersao.Name = "colVersao";
            this.colVersao.Visible = true;
            this.colVersao.VisibleIndex = 1;
            // 
            // repItemVersao
            // 
            this.repItemVersao.AutoHeight = false;
            this.repItemVersao.Mask.BeepOnError = true;
            this.repItemVersao.Mask.EditMask = "\\d+\\.\\d+\\.\\d+";
            this.repItemVersao.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.repItemVersao.Name = "repItemVersao";
            this.repItemVersao.Leave += new System.EventHandler(this.repItemVersao_Leave);
            // 
            // grid
            // 
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            gridLevelNode1.LevelTemplate = this.gridBranches;
            gridLevelNode1.RelationName = "Level1";
            this.grid.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.grid.Location = new System.Drawing.Point(0, 0);
            this.grid.MainView = this.gridProjetos;
            this.grid.Name = "grid";
            this.grid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repItemVersao,
            this.repItemLookUp});
            this.grid.Size = new System.Drawing.Size(610, 418);
            this.grid.TabIndex = 1;
            this.grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridProjetos,
            this.gridBranches});
            // 
            // gridProjetos
            // 
            this.gridProjetos.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNome});
            this.gridProjetos.GridControl = this.grid;
            this.gridProjetos.Name = "gridProjetos";
            this.gridProjetos.OptionsDetail.AllowExpandEmptyDetails = true;
            this.gridProjetos.OptionsDetail.DetailMode = DevExpress.XtraGrid.Views.Grid.DetailMode.Embedded;
            this.gridProjetos.OptionsDetail.ShowEmbeddedDetailIndent = DevExpress.Utils.DefaultBoolean.True;
            this.gridProjetos.OptionsFind.AlwaysVisible = true;
            this.gridProjetos.OptionsImageLoad.AnimationType = DevExpress.Utils.ImageContentAnimationType.Expand;
            this.gridProjetos.OptionsImageLoad.AsyncLoad = true;
            this.gridProjetos.OptionsView.ShowGroupPanel = false;
            this.gridProjetos.RowStyle += new DevExpress.XtraGrid.Views.Grid.RowStyleEventHandler(this.gridView1_RowStyle);
            this.gridProjetos.MasterRowExpanding += new DevExpress.XtraGrid.Views.Grid.MasterRowCanExpandEventHandler(this.gridProjetos_MasterRowExpanding);
            this.gridProjetos.MasterRowExpanded += new DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventHandler(this.gridProjetos_MasterRowExpanded);
            this.gridProjetos.MasterRowGetLevelDefaultView += new DevExpress.XtraGrid.Views.Grid.MasterRowGetLevelDefaultViewEventHandler(this.gridProjetos_MasterRowGetLevelDefaultView);
            this.gridProjetos.SelectionChanged += new DevExpress.Data.SelectionChangedEventHandler(this.gridView1_SelectionChanged);
            this.gridProjetos.FocusedRowObjectChanged += new DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventHandler(this.gridView1_FocusedRowObjectChanged);
            // 
            // colNome
            // 
            this.colNome.Caption = "Nome";
            this.colNome.FieldName = "Nome";
            this.colNome.Name = "colNome";
            this.colNome.OptionsColumn.AllowEdit = false;
            this.colNome.Visible = true;
            this.colNome.VisibleIndex = 0;
            // 
            // repItemLookUp
            // 
            this.repItemLookUp.AcceptEditorTextAsNewValue = DevExpress.Utils.DefaultBoolean.True;
            this.repItemLookUp.AutoHeight = false;
            this.repItemLookUp.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.repItemLookUp.EditValueChangedFiringMode = DevExpress.XtraEditors.Controls.EditValueChangedFiringMode.Buffered;
            this.repItemLookUp.Name = "repItemLookUp";
            this.repItemLookUp.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.BtnConfig);
            this.panelControl1.Controls.Add(this.simpleButton1);
            this.panelControl1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panelControl1.Location = new System.Drawing.Point(0, 418);
            this.panelControl1.Name = "panelControl1";
            this.panelControl1.Size = new System.Drawing.Size(610, 63);
            this.panelControl1.TabIndex = 0;
            // 
            // BtnConfig
            // 
            this.BtnConfig.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnConfig.ImageOptions.SvgImage")));
            this.BtnConfig.Location = new System.Drawing.Point(12, 12);
            this.BtnConfig.Name = "BtnConfig";
            this.BtnConfig.Size = new System.Drawing.Size(40, 39);
            this.BtnConfig.TabIndex = 3;
            this.BtnConfig.Click += new System.EventHandler(this.simpleButton4_Click);
            // 
            // simpleButton1
            // 
            this.simpleButton1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.simpleButton1.ButtonStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.simpleButton1.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("simpleButton1.ImageOptions.SvgImage")));
            this.simpleButton1.Location = new System.Drawing.Point(474, 12);
            this.simpleButton1.Name = "simpleButton1";
            this.simpleButton1.Size = new System.Drawing.Size(124, 39);
            this.simpleButton1.TabIndex = 0;
            this.simpleButton1.Text = "Atualizar";
            this.simpleButton1.Click += new System.EventHandler(this.simpleButton1_Click);
            // 
            // VersionManagerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(610, 481);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.panelControl1);
            this.Name = "VersionManagerForm";
            this.Text = "NCR Version Manager";
            this.Shown += new System.EventHandler(this.VersionManagerForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.gridBranches)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemVersao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProjetos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemLookUp)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraGrid.GridControl grid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridProjetos;
        private DevExpress.XtraGrid.Views.Grid.GridView gridBranches;
        private DevExpress.XtraEditors.SimpleButton simpleButton1;
        private DevExpress.XtraGrid.Columns.GridColumn colNome;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repItemVersao;
        private DevExpress.XtraEditors.Repository.RepositoryItemLookUpEdit repItemLookUp;
        private DevExpress.XtraEditors.SimpleButton BtnConfig;
        private DevExpress.XtraGrid.Columns.GridColumn colBranchName;
        private DevExpress.XtraGrid.Columns.GridColumn colVersao;
    }
}

