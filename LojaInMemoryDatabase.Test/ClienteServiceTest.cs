using LojaInMemoryDatabase.Test.Comparacoes;
using LojaServices2.Api.Models;
using LojaServices2.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Xunit;

namespace LojaInMemoryDatabase.Test
{
    public class ClienteServiceTest
    {

        private LojaContexto _contexto;
        private CotextoBase _contextoBase { get; }

        //não é mais necessário utilizar para retornar dados da base, estamos utilizando os dados mocados
        //private EnderecoService _enderecoService;
        private ClienteService _clienteService;

        public ClienteServiceTest()
        {
            _contextoBase = new CotextoBase("ClienteTestes");
            _contextoBase.AdicionarTodosDados();

            _contexto = new LojaContexto(_contextoBase.Options);
            //_enderecoService = new EnderecoService(_contexto);
            _clienteService = new ClienteService(_contexto);
        }

        [Fact]
        public void Devera_Add_Novo_Cliente()
        {
            var baseContext = new CotextoBase("AddCliente");
            var context = new LojaContexto(baseContext.Options);
            // erro endereco Unique
            var fakeCliente = baseContext.GetDadosFake<Endereco>()
                                            .Where(x => x.Id == 1)
                                            .Join(_contextoBase.GetDadosFake<Cliente>(),
                                            endereco => endereco.Id,
                                            cliente => cliente.EnderecoId,
                                            (endereco, cliente) => cliente)
                                            .Distinct()
                                            .FirstOrDefault();

            fakeCliente.Id = 0;

            //Assert
            var clieneservices = new ClienteService(context);
            Assert.Throws<ArgumentException>(() => _clienteService.Salvar(fakeCliente));
        }

        [Fact]
        public void Devera_Alterar_Cliente()
        {
            //definir entradas
            var fakeCliente = _contextoBase.GetDadosFake<Cliente>().Last();
            fakeCliente.Nome = "Amanda";

            //metodo de teste
            var atual = _clienteService.Salvar(fakeCliente);

            //Assert
            Assert.Equal(fakeCliente.Id, atual.Id);
            Assert.Equal(fakeCliente.Nome, atual.Nome);
        }

        [Fact]
        public void Devera_retornar_Cliente()
        {
            var clienteEsperado = _contextoBase.GetDadosFake<Endereco>()
                                            .Where(x => x.Id == 1)
                                            .Join(_contextoBase.GetDadosFake<Cliente>(),
                                            endereco => endereco.Id,
                                            cliente => cliente.EnderecoId,
                                            (endereco, cliente) => cliente)
                                            .Distinct()
                                            .FirstOrDefault();


            //metodo de teste
            var atual = _clienteService.ProcurarPorId(clienteEsperado.Id);

            //Assert 
            //comparação de referencia de objetos
            Assert.Equal(clienteEsperado, atual, new ClienteIdComparer());
        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        public void Devera_retornar_Cliente_Por_Id(int id)
        {
            //procurar pelo id nos dados mocados
            var clienteEsperado = _contextoBase.GetDadosFake<Cliente>().Find(x => x.Id == id);

            //metodo de teste
            var atual = _clienteService.ProcurarPorId(clienteEsperado.Id);

            //Assert 
            //comparação de referencia de objetos
            Assert.Equal(clienteEsperado, atual, new ClienteIdComparer());
        }

    }
}
