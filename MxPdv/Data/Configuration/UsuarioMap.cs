using System.Data.Entity.ModelConfiguration;
using MxPdv.Entities;

namespace MxPdv.Data.Configuration
{
    public class UsuarioMap : EntityTypeConfiguration<Usuario>
    {
        public UsuarioMap()
        {
            ToTable("Usuarios");
            HasKey(u => u.Id);

            Property(u => u.Login)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar")
                .HasColumnName("Login");

            Property(u => u.Senha)
                .IsRequired()
                .HasMaxLength(50)
                .HasColumnType("nvarchar")
                .HasColumnName("Senha");
        }
    }
}