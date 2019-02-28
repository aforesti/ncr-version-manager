using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using LibGit2Sharp;
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
                    _projetos.Clear();
                    var projetos = Directory.EnumerateDirectories(@"D:\Projetos");
                    var tasks = (
                        from projeto in projetos 
                        let arquivoVersao = Configuracoes.ObterArquivoVersaoDoProjeto(projeto) 
                        where arquivoVersao != null 
                        let arquivoManifesto = Configuracoes.ObterArquivoManifestoDoProjeto(projeto) 
                        select Task.Run(() => _projetos.Add(new Projeto(projeto, arquivoVersao, arquivoManifesto)))
                    ).ToList();
                    await Task.WhenAll(tasks);
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

        private void GridProjetos_MasterRowGetLevelDefaultView(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetLevelDefaultViewEventArgs e)
        {
            e.DefaultView = gridBranches;
        }

        private void BtnConfig_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var config = new Configuracoes();
            config.Show();
        }

        private async void BtnRefresh_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            await CarregarProjetos();
        }
               
        private async void BtnPush_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            var projeto = ((Projeto)gridProjetos.GetFocusedRow());
            if (projeto == null) return;
            await Push(projeto);
        }

        private async Task Push(Projeto projeto)
        {
            using (new DlgAguarde(this, "Push", "Subindo commits para o servidor remoto."))
            {                
                try
                {
                    await projeto.Push();
                }
                catch (LibGit2SharpException x)
                {
                    if (x.Message.Contains("unsupported URL protocol") &&
                        MessageBox.Show
                        (
                            "Não foi possivel realizar a operação pois o repositório usa um protocolo não suportado.\n " +
                            "Deseja converter o repositório para https e tentar novamente?",
                            "Atenção",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
                        ) == DialogResult.No)
                        return;

                    projeto.ConverterParaHttps();
                    await projeto.Push();
                }
                catch (Exception x)
                {
                    DlgErro.Erro(this, x,
                        "Falha ao tentar fazer o push dos commits.\n" +
                        "Verifique se o usuário e senha estão configurados corretamente.\n" +
                        "Atualmente conexões ssh não são suportadas."
                    );
                }
            }
        }

        private GridView DetailView => (GridView) gridProjetos.GetDetailView(gridProjetos.GetFocusedDataSourceRowIndex(), 0);

        private async void RepItemButtonOk_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            switch (e.Button.Index)
            {
                case 0:
                    await Pull();
                    break;
                case 1:
                    AdicionarDependencia();
                    break;
                default:
                    Commit();
                    break;
            }
        }

        private async Task Pull()
        {
            var branch = (Projeto.LocalBranch)DetailView.GetFocusedRow();
            if (branch == null) return;
            using (new DlgAguarde(this, "Aguarde", "Fazendo pull do branch seelcionado."))
            {
                try
                {
                    await Task.Run(() => branch.Pull());
                }
                catch (LibGit2SharpException x)
                {
                    if (x.Message.Contains("unsupported URL protocol") &&
                        MessageBox.Show
                        (
                            "Não foi possivel realizar a operação pois o repositório usa um protocolo não suportado.\n " +
                            "Deseja converter o repositório para https e tentar novamente?",
                            "Atenção",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
                        ) == DialogResult.Yes)
                    {
                        var projeto = ((Projeto)gridProjetos.GetFocusedRow());
                        projeto.ConverterParaHttps();
                        await Pull();
                    }
                    else
                    {
                        DlgErro.Erro(this, x,
                            "Falha ao tentar fazer o pull.\n"
                        );
                    }
                }
            }
        }

        private void AdicionarDependencia()
        {
            var detailView = gridProjetos.GetDetailView(gridProjetos.GetFocusedDataSourceRowIndex(), 0);
            var branch = (Projeto.LocalBranch)DetailView.GetFocusedRow();
            if (branch == null) return;
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
                    MessageBoxIcon.Question) != DialogResult.Yes) return;
            using (new DlgAguarde(this, "Aguarde", "Commitando arquivos alterados."))
            {
                branch?.Comitar();
            }
        }

        private void BtnExpandAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            for (var i=0; i<gridProjetos.RowCount; i++)
            {
                gridProjetos.ExpandMasterRow(i);
            }
        }

        private void BtnCollapseAll_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            gridProjetos.CollapseAllDetails();
        }

        public async Task Fetch()
        {
            using (new DlgAguarde(this, "Fetch", "Baixando atualizações dos repositórios"))
            {
                var projeto = ((Projeto) gridProjetos.GetFocusedRow());
                if (projeto == null) return;
                try
                {
                    await projeto.Fetch();
                }
                catch (LibGit2SharpException x)
                {
                    if (x.Message.Contains("unsupported URL protocol") &&
                        MessageBox.Show
                        (
                            "Não foi possivel realizar a operação pois o repositório usa um protocolo não suportado.\n " +
                            "Deseja converter o repositório para https e tentar novamente?",
                            "Atenção",
                            MessageBoxButtons.YesNo,
                            MessageBoxIcon.Warning
                        ) == DialogResult.Yes)
                    {
                        projeto.ConverterParaHttps();
                        await Fetch();
                    }
                    else
                    {
                        DlgErro.Erro(this, x,
                            "Falha ao tentar fazer o fetch.\n"
                        );
                    }
                }
            }
        }


        private async void BtnFetch_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            await Fetch();
        }
    }
}

