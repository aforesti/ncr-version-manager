using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
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
            foreach (var nomeProjeto in projetos)
            {
                _projetos.Add(new Projeto(nomeProjeto));
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            CarregarProjetos();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            ((Projeto) gridView1.GetFocusedRow()).Revert();
        }

        private void simpleButton3_Click(object sender, EventArgs e)
        {
            ((Projeto)gridView1.GetFocusedRow()).Comitar();
        }
    }
}

