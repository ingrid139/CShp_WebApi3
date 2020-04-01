using LojaServices2.Api.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Loja.Test.Comparacoes
{
    public class CompraIdComparer : IEqualityComparer<Compra>
    {
        public bool Equals(Compra x, Compra y)
        {
            return x.Id == y.Id &&
                    x.ClienteId == y.ClienteId &&
                    x.ProdutoId == y.ProdutoId;
        }

        public int GetHashCode(Compra obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
