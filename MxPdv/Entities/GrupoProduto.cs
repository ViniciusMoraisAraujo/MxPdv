using System.Collections.Generic;

namespace MxPdv.Entities
{
    public class GrupoProduto
    {
        public int Id {get; set;}
        public string Nome {get; set;}
        public virtual ICollection<Produto> Produtos {get; set;}
    }
}