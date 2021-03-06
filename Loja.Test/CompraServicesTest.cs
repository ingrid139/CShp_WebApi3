﻿using Loja.Test.Comparacoes;
using LojaServices2.Api.Models;
using LojaServices2.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace Loja.Test
{
    public class CompraServicesTest
    {

        private LojaContexto _contexto;
        private ClienteService _clienteService;
        private CompraService _comprasService;
        private ProdutoService _produtoService;

        public CompraServicesTest()
        {
            var options = new DbContextOptionsBuilder<LojaContexto>();
            options.UseSqlServer("Server=localhost,1433;Database=Loja3;User Id =sa;Password=Ing@2020;Trusted_Connection=False;");

            _contexto = new LojaContexto(options.Options);
            _clienteService = new ClienteService(_contexto);
            _comprasService = new CompraService(_contexto);
            _produtoService = new ProdutoService(_contexto);
        }

        [Fact]
        public void Devera_Add_Nova_Compra()
        {
            //definir entradas
            var cliente = _clienteService.ProcurarPorId(1);
            var produto = _produtoService.ProcurarPorId(1);

            var compra = new Compra()
            {
                Id = 0,
                ClienteId = cliente.Id,
                ProdutoId = produto.Id,
                Preco = 3600,
                Quantidade = 2
            };

            //metodo de teste
            var atual = _comprasService.Salvar(compra);

            //Assert
            Assert.NotEqual(0, compra.Id);
        }

        [Fact]
        public void Devera_retornar_Compra()
        {
            //definir entradas
            //definir entradas
            var cliente = _clienteService.ProcurarPorId(1);
            var produto = _produtoService.ProcurarPorId(1);

            var compraEsperada = new Compra()
            {
                Id = 1,
                ClienteId = cliente.Id,
                ProdutoId = produto.Id,
                Preco = 3600,
                Quantidade = 2
            };

            //metodo de teste
            var compraAtual = _comprasService.ProcurarPorId(compraEsperada.Id);


            //Assert 
            //comparação de referencia de objetos
            Assert.Equal(compraEsperada, compraAtual, new CompraIdComparer());
        }

        [Fact]
        public void Devera_retornar_Compra_Por_Cliente()
        {
            //definir entradas
            var cliente = _clienteService.ProcurarPorId(1);

            //metodo de teste
            var compra = _comprasService.ProcurarPorClienteId(cliente.Id).ToList();


            //Assert 
            Assert.Equal(cliente.Id, compra.FirstOrDefault().Id);
        }

        [Fact]
        public void Devera_retornar_Compra_Por_Produto()
        {
            //definir entradas
            var produto = _produtoService.ProcurarPorId(1);

            //metodo de teste
            var compra = _comprasService.ProcurarPorProduto(produto.Id).ToList();


            //Assert 
            Assert.Equal(produto.Id, compra.FirstOrDefault().Id);
        }
    }
}
