using LojaServices2.Api.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaServices2.Services
{
    public class PromocoesService
    {
        private LojaContexto _context;

        public PromocoesService(LojaContexto contexto)
        {
            _context = contexto;
        }

        public Promocao ProcurarPorId(int promocaoId)
        {
            //utilzar metodo Find
            return _context.Promocoes.Find(promocaoId);
        }

        public Promocao ProdutosPorPromocaoId(int promocaoId)
        {
            return _context.Promocoes
                            .Where(x => x.Id == promocaoId)
                            .Include(p => p.Produtos)
                            .ThenInclude(pp => pp.Produto)
                            .FirstOrDefault();
        }

        public Promocao ProdutosPromocoes()
        {
            return _context.Promocoes
                            .Include(p => p.Produtos)
                            .ThenInclude(pp => pp.Produto)
                            .FirstOrDefault();
        }

        public IList<Promocao> ProdutosPromocoesLista()
        {
            return _context.Promocoes
                            .Include(p => p.Produtos)
                            .ThenInclude(pp => pp.Produto)
                            .ToList();
        }

        public Promocao Salvar(Promocao produto)
        {
            //detached
            var state = _context.Entry(produto).State;
            //var existe = _context.Promocoes.Find(produto.Id);

            // ignorar change tracker
            var existe = _context.Promocoes.AsNoTracking().Where(x => x.Id == produto.Id);

            if (existe == null)
                _context.Add(produto);
            else
                _context.Update(produto);

            //persistir os dados
            _context.SaveChanges();

            //retornar o objeto
            return produto;
        }
    }
}
