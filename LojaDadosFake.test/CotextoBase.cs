using LojaServices2.Api.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace LojaDadosFake.Test
{
    public class CotextoBase
    {
        //campo para acesso às opções nas classes de teste
        public DbContextOptions<LojaContexto> Options { get; }

        // propriedade para armanezar o tipo e  caminho (path string) dos arquivos de dados mocados
        private Dictionary<Type, string> NomesArquivosDados { get; } = new Dictionary<Type, string>();

        public CotextoBase()
        {
            //instancia de testes
            Options = new DbContextOptionsBuilder<LojaContexto>()
            .UseSqlServer("Server=localhost,1433;Database=Loja3;User Id =sa;Password=Ing@2020;Trusted_Connection=False;")
            .Options;

            // preenchimento dos paths para acesso aos arquivos de testes 
            // chave = tipo do Modelo, valor = path string da localização do arquivo no projeto
            NomesArquivosDados.Add(typeof(Endereco), $"DadosFake{Path.DirectorySeparatorChar}enderecos.json");

            NomesArquivosDados.Add(typeof(Cliente), $"DadosFake{Path.DirectorySeparatorChar}clientes.json");

            NomesArquivosDados.Add(typeof(Produto), $"DadosFake{Path.DirectorySeparatorChar}produtos.json");

            NomesArquivosDados.Add(typeof(Compra), $"DadosFake{Path.DirectorySeparatorChar}compras.json");

            NomesArquivosDados.Add(typeof(Promocao), $"DadosFake{Path.DirectorySeparatorChar}promocoes.json");

            NomesArquivosDados.Add(typeof(PromocaoProduto), $"DadosFake{Path.DirectorySeparatorChar}promocaoproduto.json");
        }

        //método que retorna o arquivo de dados fake através do método tipado
        private string NomeArquivo<T>() 
        { 
            // retorno do valor de acordo com a chave - dictionary
            // variável T (tê maiúsculo) representa um tipo genérico, podemos recuperar através de todos modelos 
            return NomesArquivosDados[typeof(T)]; 
        }

        //método que retorna os dados fake deserializado de acordo com o método tipado
        public List<T> GetDadosFake<T>()
        {
            // retorna lista tipada JSON (serealizada)
            string conteudo = File.ReadAllText(NomeArquivo<T>());

            // variável T (tê maiúsculo) representa um tipo genérico, podemos deserializar através de todos modelos 
            return JsonConvert.DeserializeObject<List<T>>(conteudo);
        }

    }
}
