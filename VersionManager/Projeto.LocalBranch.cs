using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using IniParser;
using LibGit2Sharp;
using Newtonsoft.Json.Linq;
using Version = System.Version;

namespace VersionManager
{
    public partial class Projeto
    {
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

            private CommitFilter _filter => new CommitFilter { IncludeReachableFrom = _branch.CanonicalName };

            private void CarregarDependencias()
            {
                try
                {
                    var lastCommit = _repositorio.Commits.QueryBy(ArquivoManifesto, _filter).First();
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
                try
                {
                    var lastCommit = _repositorio.Commits.QueryBy(ArquivoVersaoIni, _filter).First();
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
                finally
                {
                    _versaoOriginal = Versao;
                }

            }

            public string Branch => _branch.FriendlyName.Replace("origin/", "");

            private string _versaoOriginal;
            [DisplayName("Versão")]
            public string Versao { get; set; }

            [DisplayName("Depende de")]
            public List<Dependencia> Dependencias { get; set; } = new List<Dependencia>();
            public bool Modificado => Versao != _versaoOriginal;

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
            private void AtualizarArquivoManifesto()
            {
                var manifesto = JObject.Parse(File.ReadAllText(_caminho + ArquivoManifesto));
                foreach (var d in Dependencias)
                    manifesto["dependencias"][d.Nome] = d.Versao;

                File.WriteAllText(_caminho + ArquivoManifesto, manifesto.ToString());
            }

            public void ComitarVersaoIni()
            {
                Commands.Checkout(_repositorio, _branch);
                AtualizarArquivoVersaoIni(Versao);
                
                Commands.Stage(_repositorio, ArquivoVersaoIni);
                Signature autor = ObterCommiter();
                _repositorio.Commit($"Versão do {_nome} alterada para {Versao}", autor, autor);
                _versaoOriginal = Versao;
            }

            public void ComitarManifesto()
            {
                Commands.Checkout(_repositorio, _branch);
                AtualizarArquivoManifesto();

                Commands.Stage(_repositorio, ArquivoManifesto);
                Signature autor = ObterCommiter();
                _repositorio.Commit($"Atualização nas dependências do {_nome}", autor, autor);
            }

            private static Signature ObterCommiter()
            {
                try
                {
                    var appConfig = ConfigurationManager.AppSettings;
                    var nomeCommiter = appConfig["nomeCommiter"] ?? throw new Exception("É preciso configurar o nome do usuario antes de comitar");
                    var emailCommiter = appConfig["emailCommiter"] ?? throw new Exception("É preciso configurar o email do usuario antes de comitar");
                    return new Signature(nomeCommiter, emailCommiter, DateTimeOffset.Now);
                }
                catch (Exception)
                {
                    var config = new Configuracoes();
                    config.ShowDialog();
                    return ObterCommiter();
                }
            }

            public void Revert()
            {
                _repositorio.Reset(ResetMode.Hard, _branch.Tip);
            }
        }


    }
}