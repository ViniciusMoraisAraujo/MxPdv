namespace MxPdv.Entities
{
    public class ItemVenda
    {
        public int Id {get; set;}
        public int VendaId {get; set;}
        public virtual Venda Venda {get; set;}
        public int ProdutoId {get; set;}
        public virtual Produto Produto {get; set;}
        public int Quantidade {get; set;}
        public decimal ValorUnitario {get; set;}
    }
}