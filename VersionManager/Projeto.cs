using System.Collections.Generic;
using System.Configuration;
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
        
        private readonly string _arquivoVersaoIni;

        private readonly string _arquivoManifesto;

        public string Caminho { get; }
        public string Nome
        {
            get
            {
                if (_arquivoManifesto == null || 
                   !File.Exists(Caminho +"\\"+ _arquivoManifesto.Replace("/","\\")))
                    return Caminho.Substring(Caminho.LastIndexOf('\\') + 1);

                var manifesto = JObject.Parse(File.ReadAllText(Path.Combine(Caminho, _arquivoManifesto)));
                return manifesto["nome"].ToString(); 
            }
        }
        
        public List<LocalBranch> Branches { get; set; } = new List<LocalBranch>();

        public async Task Fetch()
        {
            using (var repositorio = new Repository(Caminho))
            {
                {
                    var remote = repositorio.Network.Remotes["origin"];
                    var refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);
                    var options = new FetchOptions
                    {
                        CredentialsProvider = Configuracoes.ObterCredenciais()
                    };
                    await Task.Run(() => Commands.Fetch(repositorio, remote.Name, refSpecs, options, ""));
                }
            }
        }

        public async Task Push()
        {
            using (var repo = new Repository(Caminho))
            {
                var options = new PushOptions
                {
                    CredentialsProvider = Configuracoes.ObterCredenciais()
                };

                var branches = from b in repo.Branches
                    where !b.IsRemote && b.TrackedBranch != null
                    select b;

                await Task.Run(() =>
                {
                    foreach (var branch in branches)
                    {
                        repo.Network.Push(branch, options);
                    }
                });
            }
        }

        public void ConverterParaHttps()
        {            
            var config = File.ReadAllText(Caminho + @"\.git\config");
            config = config.Replace("ssh://git@bitbucket.org/", "https://bitbucket.org/");
            config = config.Replace("git@bitbucket.org:", "https://bitbucket.org/");            
            config = config.Replace(".git", "");
            File.WriteAllText(Caminho + @"\.git\config", config);
        }

        public Projeto(string caminho, string arquivoVersaoIni, string arquivoManifesto)
        {
            Caminho = caminho;
            _arquivoVersaoIni = arquivoVersaoIni;
            _arquivoManifesto = arquivoManifesto;
            var repositorio = new Repository(Caminho);

            var branches = from b in repositorio.Branches
                where !b.IsRemote
                select b;           
            
            foreach (var branch in branches)
            {
                Branches.Add(new LocalBranch(Nome, repositorio, branch, arquivoVersaoIni, arquivoManifesto));
            }
        }
        
        
    }
}