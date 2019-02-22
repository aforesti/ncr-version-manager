using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using IniParser.Model;

namespace VersionManager
{
    public partial class VersionManagerForm : Form
    {
        public VersionManagerForm()
        {
            InitializeComponent();
            grid.DataSource = _projetos;
        }

        private readonly List<Projeto> _projetos = new List<Projeto>();
        private void CarregarProjetos()
        {
            gridProjetos.LoadingPanelVisible = true;
            grid.BeginUpdate();
            try
            {
                _projetos.Clear();
                var projetos = Directory.EnumerateDirectories(@"D:\Projetos")
                    .Where(d => File.Exists(d + @"\_build\versao.ini") || File.Exists(d + @"\_build\bin\versao.ini"));
                foreach (var projeto in projetos)
                {
                    _projetos.Add(new Projeto(projeto));
                }
            }
            finally
            {
                grid.EndUpdate();
                gridProjetos.LoadingPanelVisible = false;
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            CarregarProjetos();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            
        }

        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var config = new Configuracoes();
            config.Show();
        }

        private void VersionManagerForm_Shown(object sender, EventArgs e)
        {
            CarregarProjetos();
        }

        private void repItemVersao_Modified(object sender, EventArgs e)
        {
            
        }

        private void gridProjetos_MasterRowExpanded(object sender, DevExpress.XtraGrid.Views.Grid.CustomMasterRowEventArgs e)
        {
            
        }

        private void gridProjetos_MasterRowExpanding(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowCanExpandEventArgs e)
        {
            
        }

        private void gridProjetos_MasterRowGetLevelDefaultView(object sender, DevExpress.XtraGrid.Views.Grid.MasterRowGetLevelDefaultViewEventArgs e)
        {
            e.DefaultView = gridBranches;
        }

        private void repItemVersao_Leave(object sender, EventArgs e)
        {
            var detailView = gridProjetos.GetDetailView(gridProjetos.GetFocusedDataSourceRowIndex(), 0);
            var branch = (Projeto.LocalBranch)((GridView)detailView).GetFocusedRow();

            var comitar = branch.Modificado && (MessageBox.Show(
                "Deseja comitar a alteração?",
                "Pergunta",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question) == DialogResult.Yes);
            if (comitar)
                branch?.ComitarVersaoIni();
        }
    }
}

