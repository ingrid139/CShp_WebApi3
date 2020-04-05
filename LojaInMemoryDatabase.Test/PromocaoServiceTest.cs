using LojaInMemoryDatabase.Test.Comparacoes;
using LojaServices2.Api.Models;
using LojaServices2.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace LojaInMemoryDatabase.Test
{
    public class PromocaoServiceTest
    {
        private LojaContexto _contexto;
        private CotextoBase _contextoBase { get; }

        //private ProdutoService _produtoService;
        private PromocoesService _promocaoService;

        public PromocaoServiceTest()
        {
            _contextoBase = new CotextoBase("PromocaoTestes");
            _contexto = new LojaContexto(_contextoBase.Options);
            //_produtoService = new ProdutoService(_contexto);
            _promocaoService = new PromocoesService(_contexto);
        }


        [Fact]
        public void Devera_Add_Nova_Promocao()
        {
            var baseContext = new CotextoBase("AddPromocao");
            var context = new LojaContexto(baseContext.Options);

            var promocao = baseContext.GetDadosFake<Promocao>().FirstOrDefault();
            promocao.Id = 0;

            promocao.IncluiProduto(baseContext.GetDadosFake<Produto>().FirstOrDefault());

            //metodo de teste
            var promoservices = new PromocoesService(context);
            var promocaoAtual = promoservices.Salvar(promocao);

            //Assert
            Assert.NotEqual(0, promocaoAtual.Id);
        }

        [Fact]
        public void Devera_Alterar_Promocao()
        {
            _contextoBase.AdicionarTodosDados();

            var promocaoEsperada = _contextoBase.GetDadosFake<Promocao>().Last();
            promocaoEsperada.DataInicio = DateTime.Parse("2020-10-01");
            promocaoEsperada.DataTermino = DateTime.Parse("2020-10-22");

            //metodo de teste
            var promocaoAtual = _promocaoService.Salvar(promocaoEsperada);

            //Assert
            Assert.Equal(promocaoEsperada.Id, promocaoAtual.Id);
            Assert.Equal(promocaoEsperada.DataInicio, promocaoAtual.DataInicio);
            Assert.Equal(promocaoEsperada.DataTermino, promocaoAtual.DataTermino);
        }



        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void Devera_retornar_PromocaoProdutos_por_PromocaoId(int id)
        {
            _contextoBase.AdicionarTodosDados();

            var promocaoEsperada = _contextoBase.GetDadosFake<Promocao>().Find(x => x.Id == id);

            var pp = _contextoBase.GetDadosFake<Promocao>()
                                            .Where(x => x.Id == promocaoEsperada.Id)
                                            .Join(_contextoBase.GetDadosFake<PromocaoProduto>(),
                                            promocao => promocao.Id,
                                            pp => pp.PromocaoId,
                                            (promocao, pp) => pp)
                                            .Distinct()
                                            .FirstOrDefault();

            //metodo de teste
            var promocaoAtual = _promocaoService.ProdutosPorPromocaoId(promocaoEsperada.Id);

            //Assert
            Assert.Equal(pp, promocaoAtual.Produtos.FirstOrDefault(), new PromocaoProdutoIdComparer()); ;
        }

        [Fact]
        public void Exibir_Produtos_Da_Promocao()
        {
            _contextoBase.AdicionarTodosDados();

            //metodo de teste
            var promocaoAtual = _promocaoService.ProdutosPromocoes();

            //Assert
            Assert.NotNull(promocaoAtual);
        }

        [Fact]
        public void Exibir_Lista_de_Promocao()
        {
            _contextoBase.AdicionarTodosDados();

            //metodo de teste
            var promocaoAtual = _promocaoService.ProdutosPromocoesLista();

            //Assert
            Assert.NotNull(promocaoAtual);
        }
    }
}
