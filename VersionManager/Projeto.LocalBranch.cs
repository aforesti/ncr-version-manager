﻿using System;
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
            private readonly string _arquivoVersao;
            private readonly string _arquivoManifesto;

            private string _versaoOriginal;
            private Dependencia[] _dependenciasOriginais;
            public string Branch => _branch.FriendlyName.Replace("origin/", "");

            [DisplayName("Versão")]
            public string Versao { get; set; }
            public string VersaoRemota { get; }
            [DisplayName("Depende de")]
            public List<Dependencia> Dependencias { get; set; } = new List<Dependencia>();
            public bool Modificado => (Versao != _versaoOriginal) || (_dependenciasOriginais != Dependencias.ToArray());
            public LocalBranch(string nome, Repository repositorio, Branch branch, string arquivoVersao, string arquivoManifesto)
            {
                _nome = nome;
                _repositorio = repositorio;
                _caminho = repositorio.Info.WorkingDirectory;
                _branch = branch;
                _arquivoVersao = arquivoVersao;
                _arquivoManifesto = arquivoManifesto;

                var filter = new CommitFilter { IncludeReachableFrom = _branch.CanonicalName };
                Versao = ObterVersao(filter);
                _versaoOriginal = Versao;
                if (_branch.TrackedBranch != null)
                {
                    filter.IncludeReachableFrom = _branch.TrackedBranch.CanonicalName;
                    VersaoRemota = ObterVersao(filter);
                }
                CarregarDependencias();
                _dependenciasOriginais = Dependencias.ToArray();
            }
                        
            private void CarregarDependencias()
            {
                try
                {
                    var filter = new CommitFilter { IncludeReachableFrom = _branch.CanonicalName };
                    var lastCommit = _repositorio.Commits.QueryBy(_arquivoManifesto, filter).First();
                    var blob = lastCommit.Commit[_arquivoManifesto].Target as Blob;
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

            public string ObterVersao(CommitFilter filter)
            {
                try
                {
                    var lastCommit = _repositorio.Commits.QueryBy(_arquivoVersao, filter).First();
                    var blob = lastCommit.Commit[_arquivoVersao].Target as Blob;
                    if (blob == null)
                        return "";
                    using (var content = new StreamReader(blob.GetContentStream(), Encoding.UTF8))
                    {
                        var parser = new FileIniDataParser();
                        var data = parser.ReadData(content);
                        return $"{data["versaoinfo"]["MajorVersion"]}.{data["versaoinfo"]["MinorVersion"]}.{data["versaoinfo"]["Release"]}";
                    }
                }
                catch (Exception)
                {
                    return "";
                }
            }

            public void AtualizarArquivoVersaoIni(string value)
            {
                var parser = new FileIniDataParser();
                var data = parser.ReadFile(_caminho + _arquivoVersao);
                var versao = new Version(value);
                data["versaoinfo"]["MajorVersion"] = versao.Major.ToString();
                data["versaoinfo"]["MinorVersion"] = versao.Minor.ToString();
                data["versaoinfo"]["Release"] = versao.Build.ToString();
                parser.WriteFile(_caminho + _arquivoVersao, data);

            }
            private void AtualizarArquivoManifesto()
            {
                var manifesto = JObject.Parse(File.ReadAllText(_caminho + _arquivoManifesto));
                foreach (var d in Dependencias)
                    manifesto["dependencias"][d.Nome] = d.Versao;

                File.WriteAllText(_caminho + _arquivoManifesto, manifesto.ToString());
            }

            private void ComitarVersaoIni()
            {
                Commands.Checkout(_repositorio, _branch);
                AtualizarArquivoVersaoIni(Versao);
                
                Commands.Stage(_repositorio, _arquivoVersao);
                Signature autor = ObterCommiter();
                _repositorio.Commit($"Versão do {_nome} alterada para {Versao}", autor, autor);
                _versaoOriginal = Versao;
            }

            private void ComitarManifesto()
            {
                Commands.Checkout(_repositorio, _branch);
                AtualizarArquivoManifesto();

                Commands.Stage(_repositorio, _arquivoManifesto);
                Signature autor = ObterCommiter();
                
                _repositorio.Commit($"Atualização nas dependências do {_nome}", autor, autor);
                _dependenciasOriginais = Dependencias.ToArray();
            }

            public void Comitar()
            {
                if (_versaoOriginal != Versao)
                    ComitarVersaoIni();
                if (_dependenciasOriginais != Dependencias.ToArray())
                    ComitarManifesto();
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
        }


    }
}