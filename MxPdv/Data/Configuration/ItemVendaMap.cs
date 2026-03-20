using System.Data.Entity.ModelConfiguration;
using MxPdv.Entities;

namespace MxPdv.Data.Configuration
{
    public class ItemVendaMap : EntityTypeConfiguration<ItemVenda>
    {
        public ItemVendaMap()
        {
            ToTable("ItensVendas");
            HasKey(i => i.Id);

            Property(i => i.Quantidade)
                .IsRequired()
                .HasColumnName("Quantidade");

            Property(i => i.ValorUnitario)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasColumnName("ValorUnitario");

            HasRequired(i => i.Venda)
                .WithMany(v => v.Itens)
                .HasForeignKey(i => i.VendaId);

            HasRequired(i => i.Produto)
                .WithMany() 
                .HasForeignKey(i => i.ProdutoId);
        }
    }
}