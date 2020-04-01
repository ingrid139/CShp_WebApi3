using LojaServices2.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaServices2.Services
{
    public class ProdutoService
    {
        private LojaContexto _context;

        public ProdutoService(LojaContexto contexto)
        {
            _context = contexto;
        }

        public Produto ProcurarPorId(int produtoId)
        {
            //utilzar metodo Find
            return _context.Produtos.Find(produtoId);
        }

        public IList<Produto> ProcurarPorCategoria(string nome)
        {
            //utilizar método Where
            return _context.Produtos.Where(x => x.Categoria == nome).ToList();
        }

        public Produto Salvar(Produto produto)
        {
            //verificar se é adicionar ou alterar
            var estado = produto.Id == 0 ? EntityState.Added : EntityState.Modified;

            //setar estado do entity
            _context.Entry(produto).State = estado;

            //persistir os dados
            _context.SaveChanges();

            //retornar o objeto
            return produto;
        }
    }
}
