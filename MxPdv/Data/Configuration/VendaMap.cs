using System.Data.Entity.ModelConfiguration;
using MxPdv.Entities;

namespace MxPdv.Data.Configuration
{
    public class VendaMap : EntityTypeConfiguration<Venda>
    {
        public VendaMap()
        {
            ToTable("Vendas");
            HasKey(v => v.Id);

            Property(v => v.DataVenda)
                .IsRequired()
                .HasColumnType("datetime")
                .HasColumnName("DataVenda");

            Property(v => v.ValorTotal)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasColumnName("ValorTotal");;
        }
    }
}