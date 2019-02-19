using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
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
            private readonly Projeto _projeto;
            public string Nome { get; }

            [DisplayName("Versão")]
            public string Versao
            {
                get => _versao;
                set
                {
                    _versao = value;
                    _projeto.ManifestoJson["dependencias"][Nome] = value;
                    File.WriteAllText(_projeto.ArquivoManifesto, _projeto.ManifestoJson.ToString());
                } 
            }

            public Dependencia(string nome, string versao, Projeto projeto)
            {
                Nome = nome;
                _versao = versao;
                _projeto = projeto;
            }
        }

        private readonly string _caminho;
        private readonly string _hash;

        private string ArquivoVersaoIni => 
            File.Exists(_caminho + @"\_build\versao.ini") ?
                _caminho + @"\_build\versao.ini":
                _caminho + @"\_build\bin\versao.ini";

        private string ArquivoManifesto => 
            File.Exists(_caminho + @"\_build\pacote\manifesto.server") ?
                _caminho + @"\_build\pacote\manifesto.server" :
                _caminho + @"\_build\bin\pacote\manifesto.server";
        private JObject ManifestoJson { get; set; } 

        public string Nome =>
            File.Exists(ArquivoManifesto) ?
                (string) ManifestoJson["nome"] :
                _caminho.Substring(_caminho.LastIndexOf('\\') + 1);
        [DisplayName("Versão")]
        public string Versao
        {
            get
            {
                var parser = new FileIniDataParser();
                var data = parser.ReadFile(ArquivoVersaoIni);
                return $"{data["versaoinfo"]["MajorVersion"]}.{data["versaoinfo"]["MinorVersion"]}.{data["versaoinfo"]["Release"]}";
            }
            set
            {
                var parser = new FileIniDataParser();
                var data = parser.ReadFile(ArquivoVersaoIni);
                var versao = new Version(value); 
                data["versaoinfo"]["MajorVersion"] = versao.Major.ToString();
                data["versaoinfo"]["MinorVersion"] = versao.Minor.ToString();
                data["versaoinfo"]["Release"] = versao.Build.ToString();
                parser.WriteFile(ArquivoVersaoIni, data);
            }
        }
        public bool Modificado => _hash != GetHash();

        private readonly Repository _repositorio;

        [DisplayName("Depende de")]
        public List<Dependencia> Dependencias { get; set; } = new List<Dependencia>();
        
        public Projeto(string caminho)
        {
            _caminho = caminho;
            _repositorio = new Repository(_caminho);

            if (!File.Exists(ArquivoManifesto)) return;
            ManifestoJson = JObject.Parse(File.ReadAllText(ArquivoManifesto));
            var d = ManifestoJson["dependencias"];
            foreach (var dependencia in d.Children())
            {
                var nome = dependencia.Path.Substring(dependencia.Path.LastIndexOf('.') + 1);
                var versao = dependencia.Values<string>().First();
                Dependencias.Add(new Dependencia(nome,versao, this));
            }

            _hash = GetHash();
        }

        public string GetHash()
        {
            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.ASCII.GetBytes(Versao);
                var hashBytes = md5.ComputeHash(inputBytes);

                var sb = new StringBuilder();
                foreach (var t in hashBytes)
                {
                    sb.Append(t.ToString("X2"));
                }
                return sb.ToString();
            }
        }

        public void Comitar()
        {
            var autor = new Signature("andre", "aforesti@gmail.com", DateTimeOffset.Now);
            Commands.Stage(_repositorio, @"_build\versao.ini");
            _repositorio.Commit($"Versão do {Nome} alterada para {Versao}", autor, autor);
        }

        public void Revert()
        {
            _repositorio.Reset(ResetMode.Hard, _repositorio.Head.Tip);
        }
    }
}