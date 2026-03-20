using System.Data.Entity.ModelConfiguration;
using MxPdv.Entities;

namespace MxPdv.Data.Configuration
{
    public class GrupoProdutoMap : EntityTypeConfiguration<GrupoProduto>
    {
        public GrupoProdutoMap()
        {
            ToTable("GrupoProdutos");
            HasKey(x => x.Id);
            
            Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnName("Nome");
            
        }
    }
}