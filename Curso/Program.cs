using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            //Verificando se há alterações pendentes para subir.
            using var db = new Data.ApplicationContext();
            var existeMigracao = db.Database.GetPendingMigrations().Any();

            if (existeMigracao)
            {
                //Mostrar notificação.
            }
        }
    }
}
