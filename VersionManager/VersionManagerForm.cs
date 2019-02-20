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
using IniParser.Model;

namespace VersionManager
{
    public partial class VersionManagerForm : Form
    {
        public VersionManagerForm()
        {
            InitializeComponent();
            CarregarProjetos();
            gridControl1.DataSource = _projetos;
        }

        private readonly List<Projeto> _projetos = new List<Projeto>();
        private void CarregarProjetos()
        {
            _projetos.Clear();
            var projetos = Directory.EnumerateDirectories(@"D:\Projetos")
                .Where(d => File.Exists(d + @"\_build\versao.ini") || File.Exists(d + @"\_build\bin\versao.ini"));
            foreach (var projeto in projetos)
            {
                _projetos.Add(new Projeto(projeto));
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            CarregarProjetos();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            for (var i = 0; i < gridView1.DataRowCount; i++)
            {
                var p = (Projeto) gridView1.GetRow(i); 
                if (p.Modificado)
                    p.Revert();
            }
            ((Projeto) gridView1.GetFocusedRow()).Revert();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            ((Projeto)gridView1.GetFocusedRow()).Comitar();
        }

        private void gridView1_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            if (e.RowHandle < 0)
                return;
            var p = (Projeto) gridView1.GetRow(e.RowHandle);
            if (p.Modificado)
                e.Appearance.BackColor = Color.Beige;
        }

        private void gridView1_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
        {
            
        }

        private void gridView1_FocusedRowObjectChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowObjectChangedEventArgs e)
        {
            repItemLookUp.DataSource = ((Projeto)gridView1.GetFocusedRow()).Branches.ToList();
        }

        private void simpleButton4_Click(object sender, EventArgs e)
        {
            var config = new Configuracoes();
            config.Show();
        }
    }
}

