using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using DevExpress.XtraEditors.Controls;
using LibGit2Sharp;
using LibGit2Sharp.Handlers;
using Newtonsoft.Json.Linq;

namespace VersionManager
{
    public partial class Projeto
    {
        private readonly string _caminho;

        public string Nome
        {
            get
            {
                var arquivoManifesto = File.Exists(_caminho + @"\_build\pacote\manifesto.server") 
                    ? _caminho + @"\_build\pacote\manifesto.server" 
                    : _caminho + @"\_build\bin\pacote\manifesto.server";
                
                if (!File.Exists(arquivoManifesto))
                    return _caminho.Substring(_caminho.LastIndexOf('\\') + 1);

                var manifesto = JObject.Parse(File.ReadAllText(arquivoManifesto));
                return manifesto["nome"].ToString(); 
            }
        }
        
        public List<LocalBranch> Branches { get; set; } = new List<LocalBranch>();
        
        private async void Fetch(Repository repositorio)
        {
            var remote = repositorio.Network.Remotes["origin"];
            var refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);
            try
            {
                FetchOptions options = new FetchOptions
                {
                    CredentialsProvider = new CredentialsHandler((url, usernameFromUrl, types) =>
                    new UsernamePasswordCredentials()
                    {
                        Username = "andre.foresti@ncr.com",
                        Password = "Jg6!82%q1Z"
                    })
                };
                await Task.Run(() => Commands.Fetch(repositorio,remote.Name, refSpecs, options, ""));
            }
            catch 
            {

            }
        }

        public Projeto(string caminho)
        {
            _caminho = caminho;
            var repositorio = new Repository(_caminho);
            var branches = from b in repositorio.Branches
                where !b.IsRemote
                select b;
            
            Fetch(repositorio);

            foreach (var branch in branches)
            {
                Branches.Add(new LocalBranch(Nome, repositorio, branch));
            }
        }
        
        
    }
}