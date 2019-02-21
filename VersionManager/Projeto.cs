using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using DevExpress.XtraEditors.Controls;
using IniParser;
using LibGit2Sharp;
using Newtonsoft.Json.Linq;
using Version = System.Version;

namespace VersionManager
{
    public class Projeto
    {
        public class Dependencia
        {
            private string _versao;
            private readonly LocalBranch _branch;
            public string Nome { get; }

            [DisplayName("Versão")]
            public string Versao
            {
                get => _versao;
                set => _versao = value;
            }

            public Dependencia(string nome, string versao, LocalBranch branch)
            {
                Nome = nome;
                _versao = versao;
                _branch = branch;
            }
        }

        public class LocalBranch
        {
            private readonly Repository _repositorio;
            private readonly Branch _branch;
            private readonly string _caminho;
            private readonly string _nome;
            private const string ArquivoVersaoIni = "_build/versao.ini";
            private const string ArquivoManifesto = "_build/pacote/manifesto.server";

            public LocalBranch(string nome, Repository repositorio, Branch branch)
            {
                _nome = nome;
                _repositorio = repositorio;
                _caminho = repositorio.Info.WorkingDirectory;
                _branch = branch;

                AtualizarVersao();
                CarregarDependencias();
            }
            
            private void CarregarDependencias()
            {
                var filter = new CommitFilter { FirstParentOnly = true, IncludeReachableFrom = _branch.CanonicalName };
                try
                {
                    var lastCommit = _repositorio.Commits.QueryBy(ArquivoManifesto, filter).First();
                    var blob = lastCommit.Commit[ArquivoManifesto].Target as Blob;
                    if (blob == null)
                        return;
                    using (var content = new StreamReader(blob.GetContentStream(), Encoding.UTF8))
                    {
                        var manifestoJson = JObject.Parse(content.ReadToEnd());
                        var d = manifestoJson["dependencias"];
                        if (d == null) return;
                        foreach (var dependencia in d.Children())
                        {
                            var projeto = dependencia.Path.Substring(dependencia.Path.LastIndexOf('.') + 1);
                            var versao = dependencia.Values<string>().First();
                            Dependencias.Add(new Dependencia(projeto, versao, this));
                        }
                    }
                }
                catch (Exception)
                {
                    
                }
            }

            public void AtualizarVersao()
            {
                var filter = new CommitFilter {IncludeReachableFrom = _branch.CanonicalName};
                try
                {
                    var lastCommit = _repositorio.Commits.QueryBy(ArquivoVersaoIni, filter).First();
                    var blob = lastCommit.Commit[ArquivoVersaoIni].Target as Blob;
                    if (blob == null)
                        return;
                    using (var content = new StreamReader(blob.GetContentStream(), Encoding.UTF8))
                    {
                        var parser = new FileIniDataParser();
                        var data = parser.ReadData(content);
                        Versao = $"{data["versaoinfo"]["MajorVersion"]}.{data["versaoinfo"]["MinorVersion"]}.{data["versaoinfo"]["Release"]}";
                    }
                }
                catch (Exception)
                {
                    Versao = "";
                }
                
            }

            public string Branch => _branch.FriendlyName.Replace("origin/", "");

            [DisplayName("Versão")]
            public string Versao { get; set; }

            [DisplayName("Depende de")]
            public List<Dependencia> Dependencias { get; set; } = new List<Dependencia>();

            public void AtualizarArquivoVersaoIni(string value)
            {
                var parser = new FileIniDataParser();
                var data = parser.ReadFile(_caminho + ArquivoVersaoIni);
                var versao = new Version(value);
                data["versaoinfo"]["MajorVersion"] = versao.Major.ToString();
                data["versaoinfo"]["MinorVersion"] = versao.Minor.ToString();
                data["versaoinfo"]["Release"] = versao.Build.ToString();
                parser.WriteFile(_caminho + ArquivoVersaoIni, data);

            }
            public void Comitar()
            {
                try
                {
                    Commands.Checkout(_repositorio, _branch);
                    AtualizarArquivoVersaoIni(Versao);
                }
                catch (Exception)
                {
                    return;
                }
                
                Commands.Stage(_repositorio, ArquivoVersaoIni);
                Commands.Stage(_repositorio, ArquivoManifesto);

                var appConfig = ConfigurationManager.AppSettings;
                var nomeCommiter = appConfig["nomeCommiter"] ?? throw new Exception("É preciso configurar o nome do usuario antes de comitar");
                var emailCommiter = appConfig["emailCommiter"] ?? throw new Exception("É preciso configurar o email do usuario antes de comitar");
                var autor = new Signature(nomeCommiter, emailCommiter, DateTimeOffset.Now);
                _repositorio.Commit($"Versão do {_nome} alterada para {Versao}", autor, autor);
            }

            public void Revert()
            {
                _repositorio.Reset(ResetMode.Hard, _branch.Tip);
            }
        }

        private readonly string _caminho;

        public string Nome
        {
            get
            {
                JObject manifesto = null;
                if (File.Exists(ArquivoManifesto))
                    manifesto = JObject.Parse(File.ReadAllText(ArquivoManifesto));

                return manifesto != null 
                    ? manifesto["nome"].ToString() 
                    : _caminho.Substring(_caminho.LastIndexOf('\\') + 1);
            }
        }
        
        private string ArquivoManifesto =>
            File.Exists(_caminho + @"\_build\pacote\manifesto.server") ?
                _caminho + @"\_build\pacote\manifesto.server" :
                _caminho + @"\_build\bin\pacote\manifesto.server";

        public List<LocalBranch> Branches { get; set; } = new List<LocalBranch>();
        
        public Projeto(string caminho)
        {
            _caminho = caminho;
            var repositorio = new Repository(_caminho);

            var branches = from b in repositorio.Branches
                where b.IsRemote
                select b;
            
            foreach (var branch in branches)
            {
                Branches.Add(new LocalBranch(Nome, repositorio, branch));
            }
        }
        
        
    }
}