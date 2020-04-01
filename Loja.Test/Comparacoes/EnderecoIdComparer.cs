using LojaServices2.Api.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace Loja.Test.Comparacoes
{
    public class EnderecoIdComparer : IEqualityComparer<Endereco>
    {
        public bool Equals(Endereco x, Endereco y)
        {
            return x.Id == y.Id;
        }

        public int GetHashCode(Endereco obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
