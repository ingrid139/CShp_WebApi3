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
    public class ProdutoServiceTest
    {
        private LojaContexto _contexto;
        private CotextoBase _contextoBase { get; }

        private ProdutoService _produtoService;

        public ProdutoServiceTest()
        {
            _contextoBase = new CotextoBase("ProdutoTestes");

            _contexto = new LojaContexto(_contextoBase.Options);
            _produtoService = new ProdutoService(_contexto);
        }

        [Fact]
        public void Devera_Add_Novo_Produto()
        {
            var baseContext = new CotextoBase("AddProd");
            var context = new LojaContexto(baseContext.Options);

            var fakeProduto = baseContext.GetDadosFake<Produto>().Find(x => x.Id == 3);
            fakeProduto.Id = 0;

            //metodo de teste
            var proservices = new ProdutoService(context);
            var atual = proservices.Salvar(fakeProduto);

            //Assert
            Assert.NotEqual(0, fakeProduto.Id);
        }

        [Fact]
        public void Devera_Alterar_Produto()
        {
            _contextoBase.AdicionarTodosDados();

            //definir entradas
            var fakeProd = _contextoBase.GetDadosFake<Produto>().Last();
            fakeProd.Nome = "Copo";
            fakeProd.Categoria = "Utensilios";
            fakeProd.PrecoUnitario = 15;

            //metodo de teste
            var atual = _produtoService.Salvar(fakeProd);

            //Assert
            Assert.Equal(fakeProd.Id, atual.Id);
            Assert.Equal(fakeProd.Nome, atual.Nome);
            Assert.Equal(fakeProd.Categoria, atual.Categoria);
            Assert.Equal(fakeProd.PrecoUnitario, atual.PrecoUnitario);
        }

        [Fact]
        public void Devera_retornar_Produto()
        {
            _contextoBase.AdicionarTodosDados();
            var produtoEsperado = _contextoBase.GetDadosFake<Produto>().Find(x => x.Id == 3);

            //metodo de teste
            var produtoAtual = _produtoService.ProcurarPorId(produtoEsperado.Id);

            //Assert 
            //comparação de referencia de objetos
            Assert.Equal(produtoEsperado, produtoAtual, new ProdutoIdComparer());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void Devera_retornar_Produto_Por_Id(int id)
        {
            _contextoBase.AdicionarTodosDados();

            //procurar pelo id nos dados mocados
            var produtoEsperado = _contextoBase.GetDadosFake<Produto>().Find(x => x.Id == id);

            //metodo de teste
            var atual = _produtoService.ProcurarPorId(produtoEsperado.Id);

            //Assert 
            //comparação de referencia de objetos
            Assert.Equal(produtoEsperado, atual, new ProdutoIdComparer());
        }
    }
}
