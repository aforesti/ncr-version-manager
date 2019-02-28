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
        private readonly string _caminho;

        private readonly string _arquivoVersaoIni;

        private readonly string _arquivoManifesto;

        public string Nome
        {
            get
            {
                if (_arquivoManifesto == null || 
                   !File.Exists(_caminho +"\\"+ _arquivoManifesto.Replace("/","\\")))
                    return _caminho.Substring(_caminho.LastIndexOf('\\') + 1);

                var manifesto = JObject.Parse(File.ReadAllText(Path.Combine(_caminho, _arquivoManifesto)));
                return manifesto["nome"].ToString(); 
            }
        }
        
        public List<LocalBranch> Branches { get; set; } = new List<LocalBranch>();

        public async void Fetch(Repository repositorio)
        {
            var remote = repositorio.Network.Remotes["origin"];
            var refSpecs = remote.FetchRefSpecs.Select(x => x.Specification);
            try
            {
                FetchOptions options = new FetchOptions
                {
                    CredentialsProvider = Configuracoes.ObterCredenciais()
                };
                await Task.Run(() => Commands.Fetch(repositorio,remote.Name, refSpecs, options, ""));
            }
            catch 
            {

            }
        }

        public async Task Push()
        {
            using (var repo = new Repository(_caminho))
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
            var config = File.ReadAllText(_caminho + @"\.git\config");
            config = config.Replace("git@bitbucket.org:", "https://bitbucket.org/");            
            config = config.Replace(".git", "");
            File.WriteAllText(_caminho + @"\.git\config", config);
        }

        public Projeto(string caminho, string arquivoVersaoIni, string arquivoManifesto)
        {
            _caminho = caminho;
            _arquivoVersaoIni = arquivoVersaoIni;
            _arquivoManifesto = arquivoManifesto;
            var repositorio = new Repository(_caminho);
            //Fetch(repositorio);

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