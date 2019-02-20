using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            SalvarConfiguracao("nomeCommiter", TxtNomeCommiter.Text);
            SalvarConfiguracao("emailCommiter", TxtEmailCommiter.Text);
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
                MessageBox.Show("Erro ao gravar as configurações no arquivo.s");  
            }  
        }
    }
}
