using LojaServices2.Api.Models.Configurations;
using Microsoft.EntityFrameworkCore;

namespace LojaServices2.Api.Models
{
    public class LojaContexto : DbContext
    {
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Compra> Compras { get; set; }
        public DbSet<Promocao> Promocoes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco> Enderecos { get; set; }
        public DbSet<PromocaoProduto> PromocoesProduto { get; set; }

        public LojaContexto(DbContextOptions<LojaContexto> options) : base(options)
        {

        }
        

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //confirmação de configuraão para utilizar com In Memory Database
            if (!optionsBuilder.IsConfigured)
                optionsBuilder.UseSqlServer("Server=localhost,1433;Database=Loja3;User Id =sa;Password=Ing@2020;Trusted_Connection=False;");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CompraConfiguration());
            modelBuilder.ApplyConfiguration(new ClienteConfiguration());
            modelBuilder.ApplyConfiguration(new PromocaoProdutoConfiguration());
        }
    }
}