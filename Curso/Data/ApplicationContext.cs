using Curso.Domain;
using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace CursoEFCore.Data
{
    class ApplicationContext: DbContext
    {
        //Onde se configura a string de conexão com o BD.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Informar qual provider se está utilizando (SQL Server/ MySQL, NoSQL ...)
            optionsBuilder.UseSqlServer("Data source=DEV-VICTOR;Initial Catalog=CursoEFCore;Integrated Security=true");    
        }
        //Configurando modelo de Dados.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Aplica a configuração de todas as IEntityTypeConfiguration<TEntity> instâncias definidas no assembly fornecido.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
        }
    }
}
