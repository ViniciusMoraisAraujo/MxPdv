using System.Data.Entity.ModelConfiguration;
using MxPdv.Entities;

namespace MxPdv.Data.Configuration
{
    public class ProdutoMap : EntityTypeConfiguration<Produto>
    {
        public ProdutoMap()
        {
            ToTable("Produtos");
            HasKey(x => x.Id);
            
            Property(x => x.Nome)
                .IsRequired()
                .HasMaxLength(100)
                .HasColumnType("varchar(100)")
                .HasColumnName("Nome");

            Property(x => x.Preco)
                .IsRequired()
                .HasPrecision(18, 2)
                .HasColumnType("decimal(18,2)")
                .HasColumnName("Preco");
            
            Property(x => x.Estoque)
                .IsRequired();
            
            HasRequired(x => x.GrupoProduto)
                .WithMany(x => x.Produtos)
                .HasForeignKey(x => x.GrupoProduto.Id);
        }
    }
}