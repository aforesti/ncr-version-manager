using System.ComponentModel;

namespace VersionManager
{
    public partial class Projeto
    {
        public class Dependencia
        {
            public string Nome { get; }
            [DisplayName("Versão")]
            public string Versao { get; set; }

            public Dependencia(string nome, string versao, LocalBranch branch)
            {
                Nome = nome;
                Versao = versao;
            }
        }
        
        
    }
}