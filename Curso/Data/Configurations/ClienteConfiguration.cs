using Curso.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoEFCore.Data.Configurations
{
    class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.ToTable("Clientes");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Nome).HasColumnType("VARCHAR(80)").IsRequired();
            builder.Property(p => p.Telefone).HasColumnType("CHAR(11)");
            builder.Property(p => p.Cep).HasColumnType("VARCHAR(80)").IsRequired();
            builder.Property(p => p.Estado).HasColumnType("CHAR(8)").IsRequired();
            builder.Property(p => p.Cidade).HasMaxLength(60).IsRequired();

            //Criando campo de índice.
            builder.HasIndex(i => i.Telefone).HasName("idx_cliente_telefone");
        }
    }
}
