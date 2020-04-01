using LojaServices2.Api.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace LojaInMemoryDatabase.Test
{
    public class CotextoBase
    {
        //campo para acesso às opções nas classes de teste
        public DbContextOptions<LojaContexto> Options { get; }

        // propriedade para armanezar o tipo e  caminho (path string) dos arquivos de dados mocados
        private Dictionary<Type, string> NomesArquivosDados { get; } = new Dictionary<Type, string>();

        public CotextoBase(string NomeTeste)
        {
            //instancia de testes
            //Options = new DbContextOptionsBuilder<LojaContexto>()
            //.UseSqlServer("Server=localhost,1433;Database=Loja3;User Id =sa;Password=Ing@2020;Trusted_Connection=False;")
            //.Options;

            //InMemoryDatabase utiliza apenas um nome mocado de databse, não necessita de connection string
            Options = new DbContextOptionsBuilder<LojaContexto>()
            .UseInMemoryDatabase(databaseName: $"LojaContexto_{NomeTeste}")
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

        
        //Alimenar a base com todos os dados mocados
        public void AdicionarTodosDados()
        {
            AdicionarDados<Endereco>();
            AdicionarDados<Cliente>();
            AdicionarDados<Produto>();
            AdicionarDados<Compra>();
            AdicionarDados<Promocao>();
            AdicionarDados<PromocaoProduto>();
        }


        //Adiconar Dados na base de acordo com tipo enviado onde  esse tipo deverá ser obrigatoriamente uma classe e não tipo primitivo
        public void AdicionarDados<T>() where T : class
        {
            using (var context = new LojaContexto(this.Options))
            {
                // Se o contexto estiver sem dados
                if(context.Set<T>().Count() == 0)
                {
                    // percorrer a lista de dados mocados tipados
                    foreach (T item in GetDadosFake<T>())
                    {
                        // add cada item da lista no contexto
                        context.Set<T>().Add(item);
                    }
                    
                    //Salvar itens adicionados na lista
                    context.SaveChanges();
                }
            }
        }
    }
}
