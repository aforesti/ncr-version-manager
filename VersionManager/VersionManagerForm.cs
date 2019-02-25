using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using Ncr.Ui.Dlg;

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
            using (var dlg = new DlgAguarde(this, "Aguarde", "Carregando projetos"))
            {
                try
                {
                    await Task.Run(() => {
                        _projetos.Clear();
                        var projetos = Directory.EnumerateDirectories(@"D:\Projetos");
                        foreach (var projeto in projetos)
                        {
                            var arquivoVersao = Configuracoes.ObterArquivoVersaoDoProjeto(projeto);
                            if (arquivoVersao == null)
                                continue;
                            var arquivoManifesto = Configuracoes.ObterArquivoManifestoDoProjeto(projeto);

                            _projetos.Add(new Projeto(projeto, arquivoVersao, arquivoManifesto));
                        }
                    });
                }
                finally
                {
                    grid.DataSource = _projetos;
                }
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

       
        private async void BtnPush_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var projeto = ((Projeto) gridProjetos.GetFocusedRow());
            if (projeto == null) return;
            using (var aguarde = new DlgAguarde(this, "Push", "Subindo commits para o servidor remoto."))
            {
                try
                {
                    await projeto.Push();
                }
                catch
                {
                    MessageBox.Show(
                        "Falha ao tentar fazer o push dos commits.\n" +
                        "Verifique se o usuário e senha estão configurados corretamente.\n" +
                        "Atualmente conexões ssh não são suportadas.",
                        "Falha",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }

        }

        private GridView DetailView => (GridView) gridProjetos.GetDetailView(gridProjetos.GetFocusedDataSourceRowIndex(), 0);

        private void repItemButtonOk_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Index == 0)
                AdicionarDependencia(); 
            else
                Commit();
        }

        private void AdicionarDependencia()
        {
            var detailView = gridProjetos.GetDetailView(gridProjetos.GetFocusedDataSourceRowIndex(), 0);
            var branch = (Projeto.LocalBranch)DetailView.GetFocusedRow();
            var nome = "";
            if (!Input.PedirNome(this, "Nova dependência", "Projeto", ref nome))
                return;
            var versao = "";
            if (!Input.PedirNome(this, "Nova dependência", "Versão", ref versao))
                return;
            branch.Dependencias.Add(new Projeto.Dependencia(nome, versao, branch));
            detailView.RefreshData();
        }

        private void Commit()
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
                branch?.Comitar();
            }
        }
    }
}

