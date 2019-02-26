using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibGit2Sharp;
using LibGit2Sharp.Handlers;

namespace VersionManager
{
    public partial class Configuracoes : Form
    {
        public Configuracoes()
        {
            InitializeComponent();
            CarregarConfiguracoes();
        }

        private void CarregarConfiguracoes()
        {
            var config = ConfigurationManager.AppSettings;
            TxtNomeCommiter.Text = config["nomeCommiter"];
            TxtEmailCommiter.Text = config["emailCommiter"];
            TxtSenha.Text = config["senha"];
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            SalvarConfiguracao("nomeCommiter", TxtNomeCommiter.Text);
            SalvarConfiguracao("emailCommiter", TxtEmailCommiter.Text);
            SalvarConfiguracao("senha", TxtSenha.Text);
            Close();
        }

        private void SalvarConfiguracao(string key, string value)
        {
            try  
            {  
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);  
                var settings = configFile.AppSettings.Settings;  
                if (settings[key] == null)  
                {  
                    settings.Add(key, value);  
                }  
                else  
                {  
                    settings[key].Value = value;  
                }  
                configFile.Save(ConfigurationSaveMode.Modified);  
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);  
            }  
            catch (ConfigurationErrorsException)  
            {  
                MessageBox.Show("Erro ao gravar as configurações no arquivo.");  
            }  
        }

        public static CredentialsHandler ObterCredenciais()
        {

            var config = ConfigurationManager.AppSettings;
            var username = config["emailCommiter"];
            var password = config["senha"];
            return (url, usernameFromUrl, types) => new UsernamePasswordCredentials
            {
                Username = username,
                Password = password
            };
        }

        public static string ObterArquivoVersaoDoProjeto(string projeto)
        {
            if (File.Exists(projeto + @"\_build\versao.ini"))
                return "_build/versao.ini";
            if (File.Exists(projeto + @"\_build\bin\versao.ini"))
                return "_build/bin/versao.ini";
            if (File.Exists(projeto + @"\_build\pos\versao.ini"))
                return "_build/pos/versao.ini";
            if (File.Exists(projeto + @"\_build\server.ini"))
                return "_build/server.ini";

            var config = ConfigurationManager.AppSettings;
            return config["Versao."+projeto];
        }

        public static string ObterArquivoManifestoDoProjeto(string projeto)
        {
            if (File.Exists(projeto + @"\_build\pacote\manifesto.server"))
                return "_build/pacote/manifesto.server";
            if (File.Exists(projeto + @"\_build\bin\pacote\manifesto.server"))
                return "_build/bin/pacote/manifesto.server";
            if (File.Exists(projeto + @"\_build\pos\pacote\manifesto.server"))
                return "_build/pos/pacote/manifesto.server";

            var config = ConfigurationManager.AppSettings;
            return config["Manifesto."+projeto]; 
        }
    }
}
