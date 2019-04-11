using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Atlassian.Jira;
using LibGit2Sharp;
using Newtonsoft.Json.Linq;

namespace VersionManager
{
    public sealed partial class Projeto
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

        public string VersaoNoJira { get; set; }

        public List<LocalBranch> Branches { get; } = new List<LocalBranch>();

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

        private Jira _jira;
        private readonly Dictionary<string, string> _projetosJira = new Dictionary<string, string>()
        {
            ["Colibri"] = "DSK",
            ["Config"] = "CFG",
            ["CBO"] = "CBO",
            ["ColibriAgent"] = "AG",
            ["PluginFiscal"] = "FI",
            ["Launcher"] = "LA",
            ["Reports"] = "REP",
            ["Microterm"] = "MT",
            ["Guard"] = "GU",
            ["Sync"] = "SY",
            ["Master"] = "MA",
            ["Monitor"] = "PM",
            ["Transact"] = "TR"
        };

        private string JiraKey => _projetosJira.ContainsKey(Nome) ? _projetosJira[Nome] : null;

        public Projeto(string caminho, string arquivoVersaoIni, string arquivoManifesto)
        {
            Caminho = caminho;
            _arquivoVersaoIni = arquivoVersaoIni;
            _arquivoManifesto = arquivoManifesto;
            var repositorio = new Repository(Caminho);

            var branches = repositorio.Branches.Where(b => !b.IsRemote);
            
            foreach (var branch in branches)
            {
                Branches.Add(new LocalBranch(Nome, repositorio, branch, arquivoVersaoIni, arquivoManifesto));
            }

            _jira = Jira.CreateRestClient(@"https://jira.ncrcolibri.com.br", "dev", "dev@1234");
            if (JiraKey != null)
                VersaoNoJira = _jira.Versions.GetVersionsAsync(JiraKey).Result.LastOrDefault(v => !v.Name.Contains("?"))?.Name;
        }
        
        
    }
}