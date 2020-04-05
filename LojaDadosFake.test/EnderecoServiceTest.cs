using LojaDadosFake.Test.Comparacoes;
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
    public class EnderecoServiceTest
    {
        private LojaContexto _contexto;
        private CotextoBase _contextoBase { get; }

        public EnderecoServiceTest()
        {
            //    var options = new DbContextOptionsBuilder<LojaContexto>();
            //    options.UseSqlServer("Server=localhost,1433;Database=Loja3;User Id =sa;Password=Ing@2020;Trusted_Connection=False;");

            //    _contexto = new CotextoBase(options.Options);
            //var contextoBase = 
            _contextoBase = new CotextoBase();

            _contexto = new LojaContexto(_contextoBase.Options);
        }

        [Fact]
        public void Devera_Add_Novo_Endereco()
        {
            //definir entradas
            //var fakeEnd = new Endereco()
            //{
            //    Id = 0,
            //    Logradouro = "Rua Trezentos",
            //    Numero = 300,
            //    Cidade = "São Paulo",
            //    Bairro = "Treze"
            //};

            var fakeEnd = _contextoBase.GetDadosFake<Endereco>().First();
            fakeEnd.Id = 0;

            var atual = new Endereco();

            //metodo de teste
            var service = new EnderecoService(_contexto);
            atual = service.Salvar(fakeEnd);

            //Assert
            Assert.NotEqual(0,fakeEnd.Id);
        }

        [Fact]
        public void Devera_Alterar__Endereco()
        {
            //definir entradas
            //var fakeEnd = new Endereco()
            //{
            //    Id = 1,
            //    Logradouro = "Rua Um",
            //    Numero = 130,
            //    Cidade = "São Paulo",
            //    Bairro = "Teste"
            //};
            var fakeEnd = _contextoBase.GetDadosFake<Endereco>().First();
            fakeEnd.Id = 1;

            var atual = new Endereco();

            //metodo de teste
            var service = new EnderecoService(_contexto);
            atual = service.Salvar(fakeEnd);

            //Assert
            Assert.NotEqual(0, fakeEnd.Id);
        }

        [Fact]
        public void Devera_retornar_Endereco()
        {
            //definir entradas
            //var enderecoEsperado = new Endereco()
            //{
            //    Id = 1,
            //    Logradouro = "Rua Um",
            //    Numero = 130,
            //    Cidade = "São Paulo",
            //    Bairro = "Teste"
            //};

            var enderecoEsperado = _contextoBase.GetDadosFake<Endereco>().First();
            enderecoEsperado.Id = 1;

            var atual = new Endereco();

            //metodo de teste
            var service = new EnderecoService(_contexto);
            atual = service.ProcurarPorId(enderecoEsperado.Id);


            //Assert 
            //comparação de referencia de objetos
            Assert.Equal(enderecoEsperado, atual, new EnderecoIdComparer());
        }


    }
}
