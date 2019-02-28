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
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions1 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions2 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject8 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraEditors.Controls.EditorButtonImageOptions editorButtonImageOptions3 = new DevExpress.XtraEditors.Controls.EditorButtonImageOptions();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject9 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject10 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject11 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject12 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.XtraGrid.GridLevelNode gridLevelNode1 = new DevExpress.XtraGrid.GridLevelNode();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VersionManagerForm));
            this.gridBranches = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colBranchName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVersao = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repItemVersao = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colVersaoRemota = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBtn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.repItemButtonOk = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.grid = new DevExpress.XtraGrid.GridControl();
            this.gridProjetos = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colNome = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCaminho = new DevExpress.XtraGrid.Columns.GridColumn();
            this.fluentDesignFormContainer1 = new DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer();
            this.ribbonControl1 = new DevExpress.XtraBars.Ribbon.RibbonControl();
            this.BtnConfig = new DevExpress.XtraBars.BarButtonItem();
            this.BtnRefresh = new DevExpress.XtraBars.BarButtonItem();
            this.skinDropDownButtonItem1 = new DevExpress.XtraBars.SkinDropDownButtonItem();
            this.BtnCommitVersao = new DevExpress.XtraBars.BarButtonItem();
            this.BtnPush = new DevExpress.XtraBars.BarButtonItem();
            this.BtnAddDependency = new DevExpress.XtraBars.BarButtonItem();
            this.BtnExpandAll = new DevExpress.XtraBars.BarButtonItem();
            this.BtnCollapseAll = new DevExpress.XtraBars.BarButtonItem();
            this.ribbonPage1 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup2 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPageGroup3 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            this.ribbonPage2 = new DevExpress.XtraBars.Ribbon.RibbonPage();
            this.ribbonPageGroup1 = new DevExpress.XtraBars.Ribbon.RibbonPageGroup();
            ((System.ComponentModel.ISupportInitialize)(this.gridBranches)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemVersao)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemButtonOk)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProjetos)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridBranches
            // 
            this.gridBranches.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colBranchName,
            this.colVersao,
            this.colVersaoRemota,
            this.colBtn});
            this.gridBranches.DetailHeight = 512;
            this.gridBranches.GridControl = this.grid;
            this.gridBranches.Name = "gridBranches";
            this.gridBranches.OptionsView.ShowGroupPanel = false;
            // 
            // colBranchName
            // 
            this.colBranchName.Caption = "Branch";
            this.colBranchName.FieldName = "Branch";
            this.colBranchName.MinWidth = 30;
            this.colBranchName.Name = "colBranchName";
            this.colBranchName.OptionsColumn.AllowEdit = false;
            this.colBranchName.OptionsColumn.AllowFocus = false;
            this.colBranchName.Visible = true;
            this.colBranchName.VisibleIndex = 0;
            this.colBranchName.Width = 168;
            // 
            // colVersao
            // 
            this.colVersao.Caption = "Versão local";
            this.colVersao.ColumnEdit = this.repItemVersao;
            this.colVersao.FieldName = "Versao";
            this.colVersao.MinWidth = 30;
            this.colVersao.Name = "colVersao";
            this.colVersao.Visible = true;
            this.colVersao.VisibleIndex = 2;
            this.colVersao.Width = 199;
            // 
            // repItemVersao
            // 
            this.repItemVersao.AutoHeight = false;
            this.repItemVersao.Mask.BeepOnError = true;
            this.repItemVersao.Mask.EditMask = "\\d+\\.\\d+\\.\\d+";
            this.repItemVersao.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.repItemVersao.Name = "repItemVersao";
            // 
            // colVersaoRemota
            // 
            this.colVersaoRemota.Caption = "Versão remota";
            this.colVersaoRemota.FieldName = "VersaoRemota";
            this.colVersaoRemota.Name = "colVersaoRemota";
            this.colVersaoRemota.OptionsColumn.AllowEdit = false;
            this.colVersaoRemota.OptionsColumn.AllowFocus = false;
            this.colVersaoRemota.Visible = true;
            this.colVersaoRemota.VisibleIndex = 1;
            this.colVersaoRemota.Width = 168;
            // 
            // colBtn
            // 
            this.colBtn.AppearanceHeader.Options.UseTextOptions = true;
            this.colBtn.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.colBtn.Caption = "Ações";
            this.colBtn.ColumnEdit = this.repItemButtonOk;
            this.colBtn.Name = "colBtn";
            this.colBtn.Visible = true;
            this.colBtn.VisibleIndex = 3;
            this.colBtn.Width = 60;
            // 
            // repItemButtonOk
            // 
            this.repItemButtonOk.AutoHeight = false;
            this.repItemButtonOk.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Down, "Pull", -1, true, true, false, editorButtonImageOptions1, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, serializableAppearanceObject2, serializableAppearanceObject3, serializableAppearanceObject4, "Pull", null, null, DevExpress.Utils.ToolTipAnchor.Default),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Plus, "Adicionar Dependência", -1, true, true, false, editorButtonImageOptions2, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, serializableAppearanceObject6, serializableAppearanceObject7, serializableAppearanceObject8, "Adicionar Dependência", null, null, DevExpress.Utils.ToolTipAnchor.Cursor),
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.OK, "Comitar", -1, true, true, false, editorButtonImageOptions3, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject9, serializableAppearanceObject10, serializableAppearanceObject11, serializableAppearanceObject12, "Commit", null, null, DevExpress.Utils.ToolTipAnchor.Default)});
            this.repItemButtonOk.Name = "repItemButtonOk";
            this.repItemButtonOk.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.HideTextEditor;
            this.repItemButtonOk.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.RepItemButtonOk_ButtonClick);
            // 
            // grid
            // 
            this.grid.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grid.EmbeddedNavigator.Margin = new System.Windows.Forms.Padding(4);
            this.grid.Font = new System.Drawing.Font("Ubuntu", 11F);
            gridLevelNode1.LevelTemplate = this.gridBranches;
            gridLevelNode1.RelationName = "Level1";
            this.grid.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode1});
            this.grid.Location = new System.Drawing.Point(0, 122);
            this.grid.MainView = this.gridProjetos;
            this.grid.Margin = new System.Windows.Forms.Padding(4);
            this.grid.Name = "grid";
            this.grid.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.repItemVersao,
            this.repItemButtonOk});
            this.grid.Size = new System.Drawing.Size(638, 451);
            this.grid.TabIndex = 1;
            this.grid.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridProjetos,
            this.gridBranches});
            // 
            // gridProjetos
            // 
            this.gridProjetos.Appearance.EvenRow.BackColor = System.Drawing.Color.WhiteSmoke;
            this.gridProjetos.Appearance.EvenRow.BackColor2 = System.Drawing.Color.Gainsboro;
            this.gridProjetos.Appearance.EvenRow.Font = new System.Drawing.Font("Ubuntu", 10F);
            this.gridProjetos.Appearance.EvenRow.Options.UseBackColor = true;
            this.gridProjetos.Appearance.EvenRow.Options.UseFont = true;
            this.gridProjetos.Appearance.Row.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.gridProjetos.Appearance.Row.Options.UseFont = true;
            this.gridProjetos.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colNome,
            this.colCaminho});
            this.gridProjetos.DetailHeight = 747;
            this.gridProjetos.GridControl = this.grid;
            this.gridProjetos.Name = "gridProjetos";
            this.gridProjetos.OptionsDetail.AllowExpandEmptyDetails = true;
            this.gridProjetos.OptionsDetail.DetailMode = DevExpress.XtraGrid.Views.Grid.DetailMode.Embedded;
            this.gridProjetos.OptionsDetail.ShowDetailTabs = false;
            this.gridProjetos.OptionsDetail.ShowEmbeddedDetailIndent = DevExpress.Utils.DefaultBoolean.True;
            this.gridProjetos.OptionsFind.AlwaysVisible = true;
            this.gridProjetos.OptionsImageLoad.AnimationType = DevExpress.Utils.ImageContentAnimationType.Push;
            this.gridProjetos.OptionsView.ShowGroupPanel = false;
            this.gridProjetos.OptionsView.ShowHorizontalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridProjetos.OptionsView.ShowVerticalLines = DevExpress.Utils.DefaultBoolean.False;
            this.gridProjetos.MasterRowGetLevelDefaultView += new DevExpress.XtraGrid.Views.Grid.MasterRowGetLevelDefaultViewEventHandler(this.GridProjetos_MasterRowGetLevelDefaultView);
            // 
            // colNome
            // 
            this.colNome.Caption = "Nome";
            this.colNome.FieldName = "Nome";
            this.colNome.MinWidth = 45;
            this.colNome.Name = "colNome";
            this.colNome.OptionsColumn.AllowEdit = false;
            this.colNome.Visible = true;
            this.colNome.VisibleIndex = 0;
            this.colNome.Width = 302;
            // 
            // colCaminho
            // 
            this.colCaminho.AppearanceCell.Options.UseTextOptions = true;
            this.colCaminho.AppearanceCell.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colCaminho.AppearanceHeader.Options.UseTextOptions = true;
            this.colCaminho.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.colCaminho.Caption = "Caminho";
            this.colCaminho.FieldName = "Caminho";
            this.colCaminho.Name = "colCaminho";
            this.colCaminho.OptionsColumn.AllowEdit = false;
            this.colCaminho.Visible = true;
            this.colCaminho.VisibleIndex = 1;
            this.colCaminho.Width = 318;
            // 
            // fluentDesignFormContainer1
            // 
            this.fluentDesignFormContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.fluentDesignFormContainer1.Location = new System.Drawing.Point(0, 122);
            this.fluentDesignFormContainer1.Margin = new System.Windows.Forms.Padding(4);
            this.fluentDesignFormContainer1.Name = "fluentDesignFormContainer1";
            this.fluentDesignFormContainer1.Size = new System.Drawing.Size(638, 451);
            this.fluentDesignFormContainer1.TabIndex = 2;
            // 
            // ribbonControl1
            // 
            this.ribbonControl1.ExpandCollapseItem.Id = 0;
            this.ribbonControl1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.ribbonControl1.ExpandCollapseItem,
            this.BtnConfig,
            this.BtnRefresh,
            this.skinDropDownButtonItem1,
            this.BtnCommitVersao,
            this.BtnPush,
            this.BtnAddDependency,
            this.BtnExpandAll,
            this.BtnCollapseAll});
            this.ribbonControl1.Location = new System.Drawing.Point(0, 0);
            this.ribbonControl1.Margin = new System.Windows.Forms.Padding(4);
            this.ribbonControl1.MaxItemId = 9;
            this.ribbonControl1.Name = "ribbonControl1";
            this.ribbonControl1.Pages.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPage[] {
            this.ribbonPage1});
            this.ribbonControl1.QuickToolbarItemLinks.Add(this.skinDropDownButtonItem1);
            this.ribbonControl1.ShowApplicationButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowDisplayOptionsMenuButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowExpandCollapseButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowMoreCommandsButton = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowPageHeadersInFormCaption = DevExpress.Utils.DefaultBoolean.False;
            this.ribbonControl1.ShowPageHeadersMode = DevExpress.XtraBars.Ribbon.ShowPageHeadersMode.ShowOnMultiplePages;
            this.ribbonControl1.ShowQatLocationSelector = false;
            this.ribbonControl1.ShowToolbarCustomizeItem = false;
            this.ribbonControl1.Size = new System.Drawing.Size(638, 122);
            this.ribbonControl1.Toolbar.ShowCustomizeItem = false;
            this.ribbonControl1.TransparentEditorsMode = DevExpress.Utils.DefaultBoolean.True;
            // 
            // BtnConfig
            // 
            this.BtnConfig.Caption = "Configurar";
            this.BtnConfig.Id = 1;
            this.BtnConfig.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnConfig.ImageOptions.SvgImage")));
            this.BtnConfig.Name = "BtnConfig";
            this.BtnConfig.RibbonStyle = DevExpress.XtraBars.Ribbon.RibbonItemStyles.Large;
            this.BtnConfig.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnConfig_ItemClick);
            // 
            // BtnRefresh
            // 
            this.BtnRefresh.Caption = "Atualizar";
            this.BtnRefresh.Id = 4;
            this.BtnRefresh.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnRefresh.ImageOptions.SvgImage")));
            this.BtnRefresh.Name = "BtnRefresh";
            this.BtnRefresh.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnRefresh_ItemClick);
            // 
            // skinDropDownButtonItem1
            // 
            this.skinDropDownButtonItem1.Id = 1;
            this.skinDropDownButtonItem1.Name = "skinDropDownButtonItem1";
            // 
            // BtnCommitVersao
            // 
            this.BtnCommitVersao.Caption = "Comitar versao.ini ";
            this.BtnCommitVersao.Enabled = false;
            this.BtnCommitVersao.Hint = "Comita as alterações no branch selecionado";
            this.BtnCommitVersao.Id = 2;
            this.BtnCommitVersao.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnCommitVersao.ImageOptions.SvgImage")));
            this.BtnCommitVersao.Name = "BtnCommitVersao";
            // 
            // BtnPush
            // 
            this.BtnPush.Caption = "Push";
            this.BtnPush.Hint = "Envia os commits do projeto selecionado para o servidor remoto";
            this.BtnPush.Id = 3;
            this.BtnPush.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnPush.ImageOptions.SvgImage")));
            this.BtnPush.Name = "BtnPush";
            this.BtnPush.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnPush_ItemClick);
            // 
            // BtnAddDependency
            // 
            this.BtnAddDependency.Caption = "Adicionar dependência";
            this.BtnAddDependency.Enabled = false;
            this.BtnAddDependency.Id = 5;
            this.BtnAddDependency.ImageOptions.SvgImage = ((DevExpress.Utils.Svg.SvgImage)(resources.GetObject("BtnAddDependency.ImageOptions.SvgImage")));
            this.BtnAddDependency.Name = "BtnAddDependency";
            // 
            // BtnExpandAll
            // 
            this.BtnExpandAll.Caption = "Expandir tudo";
            this.BtnExpandAll.Id = 7;
            this.BtnExpandAll.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnExpandAll.ImageOptions.Image")));
            this.BtnExpandAll.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BtnExpandAll.ImageOptions.LargeImage")));
            this.BtnExpandAll.Name = "BtnExpandAll";
            this.BtnExpandAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnExpandAll_ItemClick);
            // 
            // BtnCollapseAll
            // 
            this.BtnCollapseAll.Caption = "Colapsar tudo";
            this.BtnCollapseAll.Id = 8;
            this.BtnCollapseAll.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BtnCollapseAll.ImageOptions.Image")));
            this.BtnCollapseAll.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BtnCollapseAll.ImageOptions.LargeImage")));
            this.BtnCollapseAll.Name = "BtnCollapseAll";
            this.BtnCollapseAll.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BtnCollapseAll_ItemClick);
            // 
            // ribbonPage1
            // 
            this.ribbonPage1.Groups.AddRange(new DevExpress.XtraBars.Ribbon.RibbonPageGroup[] {
            this.ribbonPageGroup2,
            this.ribbonPageGroup3});
            this.ribbonPage1.Name = "ribbonPage1";
            // 
            // ribbonPageGroup2
            // 
            this.ribbonPageGroup2.AllowTextClipping = false;
            this.ribbonPageGroup2.ItemLinks.Add(this.BtnConfig);
            this.ribbonPageGroup2.ItemLinks.Add(this.BtnRefresh);
            this.ribbonPageGroup2.ItemLinks.Add(this.BtnPush);
            this.ribbonPageGroup2.Name = "ribbonPageGroup2";
            this.ribbonPageGroup2.ShowCaptionButton = false;
            this.ribbonPageGroup2.Text = "Repositórios";
            // 
            // ribbonPageGroup3
            // 
            this.ribbonPageGroup3.ItemLinks.Add(this.BtnExpandAll);
            this.ribbonPageGroup3.ItemLinks.Add(this.BtnCollapseAll);
            this.ribbonPageGroup3.Name = "ribbonPageGroup3";
            this.ribbonPageGroup3.Text = "Grid";
            // 
            // ribbonPage2
            // 
            this.ribbonPage2.Name = "ribbonPage2";
            this.ribbonPage2.Text = "ribbonPage2";
            // 
            // ribbonPageGroup1
            // 
            this.ribbonPageGroup1.ItemLinks.Add(this.BtnConfig);
            this.ribbonPageGroup1.Name = "ribbonPageGroup1";
            this.ribbonPageGroup1.Text = "ribbonPageGroup1";
            // 
            // VersionManagerForm
            // 
            this.Appearance.Options.UseFont = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 19F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(638, 573);
            this.Controls.Add(this.grid);
            this.Controls.Add(this.fluentDesignFormContainer1);
            this.Controls.Add(this.ribbonControl1);
            this.Font = new System.Drawing.Font("Ubuntu", 11F);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "VersionManagerForm";
            this.Ribbon = this.ribbonControl1;
            this.Text = "NCR Version Manager";
            this.Shown += new System.EventHandler(this.VersionManagerForm_Shown);
            ((System.ComponentModel.ISupportInitialize)(this.gridBranches)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemVersao)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repItemButtonOk)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grid)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridProjetos)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ribbonControl1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private DevExpress.XtraGrid.GridControl grid;
        private DevExpress.XtraGrid.Views.Grid.GridView gridProjetos;
        private DevExpress.XtraGrid.Views.Grid.GridView gridBranches;
        private DevExpress.XtraGrid.Columns.GridColumn colNome;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit repItemVersao;
        private DevExpress.XtraGrid.Columns.GridColumn colBranchName;
        private DevExpress.XtraGrid.Columns.GridColumn colVersao;
        private DevExpress.XtraBars.FluentDesignSystem.FluentDesignFormContainer fluentDesignFormContainer1;
        private DevExpress.XtraBars.Ribbon.RibbonControl ribbonControl1;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage1;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup2;
        private DevExpress.XtraBars.Ribbon.RibbonPage ribbonPage2;
        private DevExpress.XtraBars.BarButtonItem BtnConfig;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup1;
        private DevExpress.XtraBars.BarButtonItem BtnRefresh;
        private DevExpress.XtraBars.SkinDropDownButtonItem skinDropDownButtonItem1;
        private DevExpress.XtraBars.BarButtonItem BtnCommitVersao;
        private DevExpress.XtraBars.BarButtonItem BtnPush;
        private DevExpress.XtraGrid.Columns.GridColumn colVersaoRemota;
        private DevExpress.XtraBars.BarButtonItem BtnAddDependency;
        private DevExpress.XtraGrid.Columns.GridColumn colBtn;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit repItemButtonOk;
        private DevExpress.XtraBars.BarButtonItem BtnExpandAll;
        private DevExpress.XtraBars.Ribbon.RibbonPageGroup ribbonPageGroup3;
        private DevExpress.XtraBars.BarButtonItem BtnCollapseAll;
        private DevExpress.XtraGrid.Columns.GridColumn colCaminho;
    }
}

