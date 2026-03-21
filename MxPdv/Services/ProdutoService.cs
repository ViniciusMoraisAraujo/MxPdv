using System.Collections.Generic;
using System.Linq;
using MxPdv.Data;
using MxPdv.Entities;
using MxPdv.Interfaces;

namespace MxPdv.Services
{
    public class ProdutoService : IProdutoService
    {
        public List<Produto> ObterTodos()
        {
            using (var context = new MxPdvContext())
            {
                return context.Produtos.Include("GrupoProduto").ToList();
            }
        }

        public Produto ObterPorId(int id)
        {
            using (var context = new MxPdvContext())
            {
                return context.Produtos.AsNoTracking().FirstOrDefault(x => x.Id == id);
            }
        }

        public void Salvar(Produto produto)
        {
            using (var context = new MxPdvContext())
            {
                if (produto.Id == 0) 
                {
                    context.Produtos.Add(produto);
                }
                else 
                {
                    var produtoDb = context.Produtos.Find(produto.Id);
                    if (produtoDb != null)
                    {
                        produtoDb.Nome = produto.Nome;
                        produtoDb.Preco = produto.Preco;
                        produtoDb.Estoque = produto.Estoque;
                        produtoDb.GrupoProdutoId = produto.GrupoProdutoId;
                    }
                }
                context.SaveChanges(); 
            }
        }

        public void Excluir(int id)
        {
            using (var context = new MxPdvContext())
            {
                var produto = context.Produtos.Find(id);
                if (produto != null)
                {
                    context.Produtos.Remove(produto);
                    context.SaveChanges();
                }
            }
        }
    }
}