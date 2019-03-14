using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Windows.Forms;
using LibGit2Sharp;
using LibGit2Sharp.Handlers;

namespace VersionManager
{
    using JetBrains.Annotations;

    internal sealed partial class Configuracoes : Form
    {
        internal Configuracoes()
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

        private void BtnFechar(object sender, EventArgs e)
        {
            Close();
        }

        private void BtnSalvar_Click(object sender, EventArgs e)
        {
            SalvarConfiguracao("nomeCommiter", TxtNomeCommiter.Text);
            SalvarConfiguracao("emailCommiter", TxtEmailCommiter.Text);
            SalvarConfiguracao("senha", TxtSenha.Text);
            Close();
        }

        private static void SalvarConfiguracao(string key, string value)
        {
            try  
            {  
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);  
                var settings = configFile.AppSettings.Settings;  
                if (settings[key] == null)  
                    settings.Add(key, value);  
                else  
                    settings[key].Value = value;  
                
                configFile.Save(ConfigurationSaveMode.Modified); 
                
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);  
            }  
            catch (ConfigurationErrorsException)  
            {  
                MessageBox.Show(@"Erro ao gravar as configurações no arquivo.");  
            }  
        }

        [NotNull]
        internal static CredentialsHandler ObterCredenciais()
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

        internal static string ObterArquivoVersaoDoProjeto(string projeto)
        {
            var locaisPossiveis = new List<string>
            {
                @"_build\versao.ini",
                @"_build\bin\versao.ini",
                @"_build\pos\versao.ini",
                @"_build\server.ini",
                @"server.ini",
            };

            foreach (var local in locaisPossiveis)
            {
                if (File.Exists(Path.Combine(projeto, local)))
                    return local.Replace("\\", "/");
            }
            
            var config = ConfigurationManager.AppSettings;
            return config["Versao." + projeto];
        }

        internal static string ObterArquivoManifestoDoProjeto(string projeto)
        {
            var locaisPossiveis = new List<string>
            {
                @"_build\pacote\manifesto.server",
                @"_build\bin\pacote\manifesto.server",
                @"_build\pos\pacote\manifesto.server",
                @"_build\manifesto.server"
            };

            foreach (var local in locaisPossiveis)
            {
                if (File.Exists(Path.Combine(projeto, local)))
                    return local.Replace("\\", "/");
            }
            
            var config = ConfigurationManager.AppSettings;
            return config["Manifesto." + projeto]; 
        }
    }
}
