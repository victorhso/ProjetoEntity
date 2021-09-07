using Curso.Domain;
using CursoEFCore.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CursoEFCore.Data
{
    class ApplicationContext: DbContext
    {
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        //Criando Logging para ser escrito e mostrado no console de nossa aplicaçao.
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(p => p.AddConsole());

        //Onde se configura a string de conexão com o BD.
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //Informar qual provider se está utilizando (SQL Server/ MySQL, NoSQL ...)
            optionsBuilder
                .UseLoggerFactory(_logger)
                .EnableSensitiveDataLogging()
                .UseSqlServer("Data source=DEV-VICTOR;Initial Catalog=CursoEFCore;Integrated Security=true", p => p.EnableRetryOnFailure(maxRetryCount: 2, maxRetryDelay: TimeSpan.FromSeconds(5), errorNumbersToAdd:null).MigrationsHistoryTable("Curso_EF_Core"));    
        }
        //Configurando modelo de Dados.
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Aplica a configuração de todas as IEntityTypeConfiguration<TEntity> instâncias definidas no assembly fornecido.
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
            MapearPropriedadesEquecidas(modelBuilder);
        }

        private void MapearPropriedadesEquecidas(ModelBuilder modelBuilder)
        {
            foreach(var entity in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entity.GetProperties().Where(p => p.ClrType == typeof(string));

                foreach(var property in properties)
                {
                    if(string.IsNullOrEmpty(property.GetColumnType()) && !property.GetMaxLength().HasValue)
                    {
                        //property.SetMaxLength(100); Ou
                        property.SetColumnType("VARCHAR(100)");
                    }
                }
            }
        }
    }
}
