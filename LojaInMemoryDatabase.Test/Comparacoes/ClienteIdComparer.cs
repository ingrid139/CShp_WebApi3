using LojaServices2.Api.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Text;

namespace LojaInMemoryDatabase.Test.Comparacoes
{
    public class ClienteIdComparer : IEqualityComparer<Cliente>
    {
        public bool Equals(Cliente x, Cliente y)
        {
            return x.Id == y.Id && x.EnderecoId == y.EnderecoId;
        }

        public int GetHashCode(Cliente obj)
        {
            return obj.Id.GetHashCode();
        }
    }
}
