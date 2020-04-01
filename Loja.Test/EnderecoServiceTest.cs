using Loja.Test.Comparacoes;
using LojaServices2.Api.Models;
using LojaServices2.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Loja.Test
{
    public class EnderecoServiceTest
    {
        private LojaContexto _contexto;

        public EnderecoServiceTest()
        {
            var options = new DbContextOptionsBuilder<LojaContexto>();
            options.UseSqlServer("Server=localhost,1433;Database=Loja3;User Id =sa;Password=Ing@2020;Trusted_Connection=False;");

            _contexto = new LojaContexto(options.Options);
        }

        [Fact]
        public void Devera_Add_Novo_Endereco()
        {
            //definir entradas
            var fakeEnd = new Endereco()
            {
                Id = 0,
                Logradouro = "Rua Duzentos",
                Numero = 500,
                Cidade = "São Paulo",
                Bairro = "Treze"
            };

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
            var fakeEnd = new Endereco()
            {
                Id = 1,
                Logradouro = "Rua Um",
                Numero = 130,
                Cidade = "São Paulo",
                Bairro = "Teste"
            };

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
            var enderecoEsperado = new Endereco()
            {
                Id = 1,
                Logradouro = "Rua Um",
                Numero = 130,
                Cidade = "São Paulo",
                Bairro = "Teste"
            };

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
