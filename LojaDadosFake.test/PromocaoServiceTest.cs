using LojaServices2.Api.Models;
using LojaServices2.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace LojaDadosFake.Test
{
    public class PromocaoServiceTest
    {
        private LojaContexto _contexto;
        private CotextoBase _contextoBase { get; }

        private ProdutoService _produtoService;
        private PromocoesService _promocaoService;

        public PromocaoServiceTest()
        {
            //var options = new DbContextOptionsBuilder<LojaContexto>();
            //options.UseSqlServer("Server=localhost,1433;Database=Loja3;User Id =sa;Password=Ing@2020;Trusted_Connection=False;");

            //_contexto = new LojaContexto(options.Options);

            //var options = new CotextoBase();

            _contextoBase = new CotextoBase();
            _contexto = new LojaContexto(_contextoBase.Options);
            _produtoService = new ProdutoService(_contexto);
            _promocaoService = new PromocoesService(_contexto);
        }


        [Fact]
        public void Devera_Add_Nova_Promocao()
        {
            //definir entradas
            //var produto = _produtoService.ProcurarPorId(1);

            //var promocao = new Promocao()
            //{
            //    Id = 0,
            //    Descricao = "Black Friday 2020",
            //    DataInicio = new DateTime(2020, 11, 20),
            //    DataTermino = new DateTime(2020, 12, 05),
            //};

            var promocao = _contextoBase.GetDadosFake<Promocao>().FirstOrDefault();
            promocao.Id = 0;

            promocao.IncluiProduto(_contextoBase.GetDadosFake<Produto>().FirstOrDefault());

            //metodo de teste
            var promocaoAtual = _promocaoService.Salvar(promocao);


            //Assert
            Assert.NotEqual(0, promocaoAtual.Id);
        }

        [Fact]
        public void Exibir_Produtos_Da_Promocao()
        {
            //metodo de teste
            var promocaoAtual = _promocaoService.ProdutosPromocoes();

            //Assert
            Assert.NotNull(promocaoAtual);
        }

        [Fact]
        public void Exibir_Lista_de_Promocao()
        {
            //metodo de teste
            var promocaoAtual = _promocaoService.ProdutosPromocoesLista();

            //Assert
            Assert.NotNull(promocaoAtual);
        }
    }
}
