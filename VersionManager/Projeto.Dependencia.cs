using System.ComponentModel;

namespace VersionManager
{
    public sealed partial class Projeto
    {
        public class Dependencia
        {
            public string Nome { get; set; }
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