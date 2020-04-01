using LojaDadosFake.Test.Comparacoes;
using LojaServices2.Api.Models;
using LojaServices2.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace LojaDadosFake.Test
{
    public class ProdutoServiceTest
    {
        private LojaContexto _contexto;
        private CotextoBase _contextoBase { get; }

        private ProdutoService _produtoService;

        public ProdutoServiceTest()
        {
            //var options = new DbContextOptionsBuilder<LojaContexto>();
            //options.UseSqlServer("Server=localhost,1433;Database=Loja3;User Id =sa;Password=Ing@2020;Trusted_Connection=False;");

            //_contexto = new LojaContexto(options.Options);

            //var options = new CotextoBase();

            _contextoBase = new CotextoBase();
            _contexto = new LojaContexto(_contextoBase.Options);
            _produtoService = new ProdutoService(_contexto);
        }

        [Fact]
        public void Devera_Add_Novo_Produto()
        {
            //definir entradas
            //var fakeProduto = new Produto()
            //{
            //    Id = 0,
            //    Nome = "NoteBook",
            //    Categoria = "Computadores",
            //    PrecoUnitario = (decimal)1800.0
            //};

            var fakeProduto = _contextoBase.GetDadosFake<Produto>().Find(x => x.Id == 3);
            fakeProduto.Id = 0;
            //metodo de teste
            var atual = _produtoService.Salvar(fakeProduto);

            //Assert
            Assert.NotEqual(0, fakeProduto.Id);
        }

        [Fact]
        public void Devera_retornar_Produto()
        {
            //definir entradas
            //var produtoEsperado = new Produto()
            //{
            //    Id = 1,
            //    Nome = "NoteBook",
            //    Categoria = "Computadores",
            //    PrecoUnitario = (decimal)1800.0
            //};

            var produtoEsperado = _contextoBase.GetDadosFake<Produto>().Find(x => x.Id == 3);
            produtoEsperado.Id = 4;
            //metodo de teste
            var produtoAtual = _produtoService.ProcurarPorId(produtoEsperado.Id);


            //Assert 
            //comparação de referencia de objetos
            Assert.Equal(produtoEsperado, produtoAtual, new ProdutoIdComparer());
        }
    }
}
