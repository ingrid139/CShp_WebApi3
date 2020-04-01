using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LojaServices2.Api.Models.Configurations
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.HasKey(x => x.Id);

            //builder.HasOne(x => x.EnderecoDeEntrega)
            //   .WithOne(p => p.Cliente)
            //   .HasForeignKey<Cliente>(d => d.EnderecoId)
            //   .HasConstraintName("FK_Cliente_Endereco");
        }
    }
}
