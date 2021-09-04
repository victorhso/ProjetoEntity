using CursoEFCore.Domain;
using CursoEFCore.ValueObjects;
using System;
using System.Collections.Generic;

namespace Curso.Domain
{
    public class Pedido
    {
        public int Id { get; set; }
        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public String IniciadoEm { get; set; }
        public String FinalizadoEm { get; set; }
        public TipoFrete TipoFrete { get; set; }
        public StatusPedido Status { get; set; }
        public string Observacao { get; set; }
        public ICollection<PedidoItem> Itens { get; set; }

    }
}