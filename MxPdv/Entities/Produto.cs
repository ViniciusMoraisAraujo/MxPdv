namespace MxPdv.Entities
{
    public class Produto
    {
        public int Id {get; set;}
        public string Nome {get; set;}
        public decimal Preco {get; set;}
        public int Estoque {get; set;}
        public int GrupoProdutoId {get; set;}
        public virtual GrupoProduto GrupoProduto {get; set;}
    }
}