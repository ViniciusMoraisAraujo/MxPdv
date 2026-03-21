using System;
using MxPdv.Data;
using MxPdv.Entities;
using MxPdv.Interfaces;

namespace MxPdv.Services
{
    public class VendaService : IVendaService
    {
        public void FinalizarVenda(Venda venda)
        {
            using (var context = new MxPdvContext())
            {
                using (var transacao = context.Database.BeginTransaction())
                {
                    try
                    {
                        context.Vendas.Add(venda);
                        foreach (var item in venda.Itens)
                        {
                            var produtoDb = context.Produtos.Find(item.ProdutoId);
                            if (produtoDb != null)
                            {
                                if (produtoDb.Estoque < item.Quantidade)
                                {
                                    throw new Exception($"Estoque insuficiente para o produto: {produtoDb.Nome}. Temos apenas {produtoDb.Estoque} unidades.");
                                }
                                produtoDb.Estoque -= item.Quantidade;
                            }
                        }

                        context.SaveChanges();
                        transacao.Commit();
                    }
                    catch (Exception ex)
                    {
                        transacao.Rollback();
                        throw new Exception("Falha ao gravar a venda: " + ex.Message);
                    }
                }
            }
        }
    }
}