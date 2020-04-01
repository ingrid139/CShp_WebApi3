using LojaServices2.Api.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LojaInMemoryDatabase.Test.Comparacoes
{
    public class PromocaoProdutoIdComparer : IEqualityComparer<PromocaoProduto>
    {
        public bool Equals(PromocaoProduto x, PromocaoProduto y)
        {
            return x.ProdutoId == y.ProdutoId && x.PromocaoId == y.PromocaoId;
        }

        public int GetHashCode(PromocaoProduto obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
