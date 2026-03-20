using System.Data.Entity;
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
    }
}