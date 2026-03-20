using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using MxPdv.Data.Configuration;
using MxPdv.Entities;

namespace MxPdv.Data
{
    public class MxPdvContext : DbContext
    {
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<GrupoProduto> GruposProdutos { get; set; }
        public DbSet<ItemVenda> ItensVendas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Venda> Vendas { get; set; }


        public MxPdvContext() : base("name=MxPdvContext")
        {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new UsuarioMap());
            modelBuilder.Configurations.Add(new GrupoProdutoMap());
            modelBuilder.Configurations.Add(new ProdutoMap());
            modelBuilder.Configurations.Add(new VendaMap());
            modelBuilder.Configurations.Add(new ItemVendaMap());

            base.OnModelCreating(modelBuilder);
        }
    }
}