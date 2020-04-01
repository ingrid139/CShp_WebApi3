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
    public class CompraServicesTest
    {

        private LojaContexto _contexto;
        private CotextoBase _contextoBase { get; }

        private CompraService _comprasService;
        //private ClienteService _clienteService;
        //private ProdutoService _produtoService;

        public CompraServicesTest()
        {
            _contextoBase = new CotextoBase("ComprasTestes");

            _contexto = new LojaContexto(_contextoBase.Options);
            _comprasService = new CompraService(_contexto);
            //_clienteService = new ClienteService(_contexto);
            //_produtoService = new ProdutoService(_contexto);
        }

        [Fact]
        public void Devera_Add_Nova_Compra()
        {
            var baseContext = new CotextoBase("AddCompra");
            var context = new LojaContexto(baseContext.Options);

            var fakeCompra = _contextoBase.GetDadosFake<Compra>()
                                           .Where(x => x.Id == 4)
                                           .Join(_contextoBase.GetDadosFake<Compra>(),
                                           compra => compra.ClienteId,
                                           cliente => cliente.Id,
                                           (cliente, compra) => compra)
                                           .Join(_contextoBase.GetDadosFake<Produto>(),
                                           compra => compra.ProdutoId,
                                           produto => produto.Id,
                                           (compra, produto) => compra)
                                           .Distinct()
                                           .FirstOrDefault();

            fakeCompra.Id = 0;


            //metodo de teste
            var compraservice = new CompraService(context);
            var atual = compraservice.Salvar(fakeCompra);

            //Assert
            Assert.NotEqual(0, fakeCompra.Id);
        }

        [Fact]
        public void Devera_retornar_Compra()
        {
            //carregar a base
            _contextoBase.AdicionarTodosDados();

            //defiir entradas
            var compraEsperada = _contextoBase.GetDadosFake<Compra>()
                                          .Where(x => x.Id == 4)
                                          .Join(_contextoBase.GetDadosFake<Compra>(),
                                          compra => compra.ClienteId,
                                          cliente => cliente.Id,
                                          (cliente, compra) => compra)
                                          .Join(_contextoBase.GetDadosFake<Produto>(),
                                          compra => compra.ProdutoId,
                                          produto => produto.Id,
                                          (compra, produto) => compra)
                                          .Distinct()
                                          .FirstOrDefault();

            //metodo de teste
            var compraAtual = _comprasService.ProcurarPorId(compraEsperada.Id);

            //Assert 
            //comparação de referencia de objetos
            Assert.Equal(compraEsperada, compraAtual, new CompraIdComparer());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void Devera_retornar_Compra_Por_Cliente(int id)
        {
            _contextoBase.AdicionarTodosDados();

            //definir entradas
            var cliente = _contextoBase.GetDadosFake<Cliente>().Find(x => x.Id == id);

            //metodo de teste
            var compra = _comprasService.ProcurarPorClienteId(cliente.Id).ToList();

            //Assert 
            Assert.Equal(cliente.Id, compra.FirstOrDefault().ClienteId);
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void Devera_retornar_Compra_Por_Produto(int id)
        {
            _contextoBase.AdicionarTodosDados();

            //definir entradas
            var produto = _contextoBase.GetDadosFake<Produto>().Find(x => x.Id == id);

            //metodo de teste
            var compra = _comprasService.ProcurarPorProduto(produto.Id).ToList();

            //Assert 
            Assert.Equal(produto.Id, compra.FirstOrDefault().ProdutoId);
        }
    }
}
