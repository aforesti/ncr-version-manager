using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;

namespace VersionManager
{
    public partial class VersionManagerForm : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public VersionManagerForm()
        {
            InitializeComponent();            
        }

        private readonly List<Projeto> _projetos = new List<Projeto>();
        private async Task CarregarProjetos()
        {
            gridProjetos.ShowLoadingPanel();
            try
            {
                await Task.Run(() => { 
                    _projetos.Clear();
                    var projetos = Directory.EnumerateDirectories(@"D:\Projetos")
                        .Where(d => File.Exists(d + @"\_build\versao.ini") || File.Exists(d + @"\_build\bin\versao.ini"));
                
                    foreach (var projeto in projetos)
                    {
                        _projetos.Add(new Projeto(projeto));
                    }
                });
            }
            finally
            {
                gridProjetos.HideLoadingPanel();
                grid.DataSource = _projetos;
            }
        }

        private async void VersionManagerForm_Shown(object sender, EventArgs e)
        {
            await CarregarProjetos();
        }

        private void gridProjetos_MasterRowGetLevelDefaultView(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetLevelDefaultViewEventArgs e)
        {
            e.DefaultView = gridBranches;
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var config = new Configuracoes();
            config.Show();
        }

        private async void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            await CarregarProjetos();
        }

        private void barButtonItem2_ItemClick_1(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var detailView = gridProjetos.GetDetailView(gridProjetos.GetFocusedDataSourceRowIndex(), 0);
            var branch = (Projeto.LocalBranch)((GridView)detailView).GetFocusedRow();
            if (branch == null) return;
            if (!branch.Modificado)
            {
                MessageBox.Show(
                    "Branch sem modificações para comitar.",
                    "Info",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information
                );
                return;
            }                

            if (MessageBox.Show(
                $"Confirma o commit no branch {branch.Branch} ?",
                "Pergunta",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes)
            {
                branch?.ComitarVersaoIni();
            }                
        }

        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var detailView = gridProjetos.GetDetailView(gridProjetos.GetFocusedDataSourceRowIndex(), 0);
            var branch = (Projeto.LocalBranch)((GridView)detailView).GetFocusedRow();
            if (branch == null) return;

            branch.ComitarManifesto();
        }

        private void BtnPush_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void gridProjetos_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            
        }
    }
}

