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
    public class EnderecoServiceTest
    {
        private LojaContexto _contexto;
        private CotextoBase _contextoBase { get; }

        public EnderecoServiceTest()
        {
            _contextoBase = new CotextoBase("EnderecoTestes");
            _contextoBase.AdicionarTodosDados();

            _contexto = new LojaContexto(_contextoBase.Options);
        }

        [Fact]
        public void Devera_Add_Novo_Endereco()
        {
            var fakeEnd = _contextoBase.GetDadosFake<Endereco>().First();

            var atual = new Endereco();

            //metodo de teste
            var service = new EnderecoService(_contexto);
            atual = service.Salvar(fakeEnd);

            //Assert
            Assert.NotEqual(0, fakeEnd.Id);
        }

        [Fact]
        public void Devera_Alterar__Endereco()
        {
            var fakeEnd = _contextoBase.GetDadosFake<Endereco>().First();
            fakeEnd.Logradouro = "novo endereco";
            fakeEnd.Complemento = "novo complemento"; 
            fakeEnd.Bairro = "novo bairro";
            fakeEnd.Cidade = "nova cidade";
            fakeEnd.Numero = 123;


            //metodo de teste
            var service = new EnderecoService(_contexto);
            var atual = service.Salvar(fakeEnd);

            //Assert
            Assert.Equal(fakeEnd.Id, fakeEnd.Id);
            Assert.Equal(fakeEnd.Logradouro, atual.Logradouro);
            Assert.Equal(fakeEnd.Complemento, atual.Complemento);
            Assert.Equal(fakeEnd.Bairro, atual.Bairro);
            Assert.Equal(fakeEnd.Cidade, atual.Cidade);
            Assert.Equal(fakeEnd.Numero, atual.Numero);
        }

        [Fact]
        public void Devera_retornar_Endereco()
        {
            var enderecoEsperado = _contextoBase.GetDadosFake<Endereco>().First();

            var atual = new Endereco();

            //metodo de teste
            var service = new EnderecoService(_contexto);
            atual = service.ProcurarPorId(enderecoEsperado.Id);


            //Assert 
            //comparação de referencia de objetos
            Assert.Equal(enderecoEsperado, atual, new EnderecoIdComparer());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void Devera_retornar_Endereco_Por_Id(int id)
        {
            //procurar pelo id nos dados mocados
            var enderecoEsperado = _contextoBase.GetDadosFake<Endereco>().Find(x => x.Id == id);

            //metodo de teste
            var service = new EnderecoService(_contexto);
            var atual = service.ProcurarPorId(enderecoEsperado.Id);

            //Assert 
            //comparação de referencia de objetos
            Assert.Equal(enderecoEsperado, atual, new EnderecoIdComparer());
        }


    }
}
