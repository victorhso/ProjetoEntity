using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using Curso.Domain;
using CursoEFCore.ValueObjects;
using CursoEFCore.Domain;
using System.Collections.Generic;

namespace CursoEFCore
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello World!");

            //Verificando se há alterações pendentes para subir.
            //using var db = new Data.ApplicationContext();
            //var existeMigracao = db.Database.GetPendingMigrations().Any();

            //if (existeMigracao)   
            //{
            //Mostrar notificação.
            //}

            //InsereRegistro();

            //InsereDadosMassa();

            //consultaDados();

            //CadastraPedido();

            //ConsultaPedidoCarregamentoAdiantado();

            //AtualizaDados();

            RemoveDados();

        }

        private static void RemoveDados()
        {
            using var db = new Data.ApplicationContext();

            //var cliente = db.Clientes.Find(2);

            //db.Clientes.Remove(cliente); opção 1
            // db.Remove(cliente); opção 2
            //db.Entry(cliente).State = EntityState.Deleted;

            //forma desconectada
            var cliente = new Cliente
            {
                Id = 3
            };

            db.Clientes.Remove(cliente);
            db.SaveChanges();
        }

        private static void AtualizaDados()
        {
            using var db = new Data.ApplicationContext();

            //var cliente = db.Clientes.FirstOrDefault(p => p.Id == 1);

            var clienteDesconectado1 = new Cliente
            {
                Id = 1
            };

            var clienteDesconectado = new
            {
                Nome = "Vinicius Moreira Oliveira",
                Telefone = "3199999999"
            };

            //cliente.Nome = "Alterado";
            //db.Clientes.Update(cliente); Ao comentar esta linha, será atualizado somente o campo que houve alteração.

            db.Attach(clienteDesconectado1);
            db.Entry(clienteDesconectado1).CurrentValues.SetValues(clienteDesconectado);

            db.SaveChanges();

            }

        //Carregando dados de entidades relacionadas.
        private static void ConsultaPedidoCarregamentoAdiantado()
        {
            using var db = new Data.ApplicationContext();

            var pedido = db.Pedidos.Include(p => p.Itens).ThenInclude(p => p.Produto).ToList();
            Console.WriteLine(pedido.Count);
        }

        private static void CadastraPedido()
        {
            using var db = new Data.ApplicationContext();

            var cliente = db.Clientes.FirstOrDefault();
            var produto = db.Produtos.FirstOrDefault();

            var Pedido = new Pedido
            {
                ClienteId = cliente.Id,
                IniciadoEm = "2021-09-07 14:45:00",
                FinalizadoEm = "2021-09-07 14:50:00",
                Observacao = "Pedido Teste",
                TipoFrete = TipoFrete.FOB,
                Itens = new List<PedidoItem>
                {
                    new PedidoItem
                    {
                        ProdutoId = produto.Id,
                        Desconto = 0,
                        Quantidade = 1,
                        Valor = 10,
                    }
                }
            };

            db.Pedidos.Add(Pedido);
            db.SaveChanges();
        }

        private static void InsereRegistro()
        {
            // Criando instância
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "12345678910",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaVenda,
                Ativo = true
            };

            using var db = new Data.ApplicationContext();
            db.Produtos.Add(produto); //Opção 1 de inserção de registros na base de dados.
            //db.Set<Produto>().Add(produto); Opção 2: Inserção genérica.
            //db.Entry(produto).State = EntityState.Added; Opção 3: rastreando o registro e falando o que deve ser feito
            //db.Add(produto); Opção 4

            //Salvando os dados no banco
            var registros = db.SaveChanges();
            Console.WriteLine($"Total registros: {registros}");
        }

        private static void InsereDadosMassa()
        {
            // Criando instância
            var produto = new Produto
            {
                Descricao = "Produto Teste",
                CodigoBarras = "12345678910",
                Valor = 10m,
                TipoProduto = TipoProduto.MercadoriaParaVenda,
                Ativo = true
            };

            // Criando instância
            var cliente = new Cliente
            {
                Nome = "Victor Henrique",
                Cep = "99999020",
                Cidade = "Belo Horizonte",
                Estado = "MG",
                Telefone = "3133333333"
            };

            //Criando instância do nosso contexto.
            using var db = new Data.ApplicationContext();

            db.AddRange(produto, cliente);

            //Salvando os dados no banco
            var registros = db.SaveChanges();
            Console.WriteLine($"Total registros: {registros}");
        }

        private static void consultaDados()
        {
            //Criando instância do nosso contexto.
            using var db = new Data.ApplicationContext();

            //var consultaSintaxe = (from c in db.Clientes where c.Id > 0 select c).ToList();
            var consultaMetedo = db.Clientes
                                    .Where(p => p.Id > 0)
                                    .OrderBy(p => p.Id)
                                    .ToList();

            foreach(var cliente in consultaMetedo)
            {
                Console.WriteLine($"Consultado cliente: {cliente.Id} - {cliente.Nome}");
                db.Clientes.Find(cliente.Id);
            }
        }
    }
}
