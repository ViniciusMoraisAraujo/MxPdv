using System;
using System.Collections.Generic;

namespace MxPdv.Entities
{
    public class Venda
    {
        public int Id {get; set;}
        public DateTime DataVenda {get; set;} = DateTime.Now;
        public decimal ValorTotal {get; set;}
        public virtual ICollection<ItemVenda> Itens { get; set; } = new List<ItemVenda>();
    }
}