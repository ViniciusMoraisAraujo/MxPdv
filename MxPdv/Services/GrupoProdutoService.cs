using System;
using System.Collections.Generic;
using System.Linq;
using MxPdv.Data;
using MxPdv.Entities;
using MxPdv.Interfaces; 

namespace MxPdv.Services
{
    public class GrupoProdutoService : IGrupoProdutoService
    {
        public List<GrupoProduto> ObterTodos()
        {
            using (var context = new MxPdvContext())
            {
                return context.GruposProdutos.ToList();
            }
        }
        public void Salvar(GrupoProduto grupo)
        {
            using (var context = new MxPdvContext())
            {
                if (grupo.Id == 0)
                {
                    context.GruposProdutos.Add(grupo);
                }
                else 
                {
                    var grupoDb = context.GruposProdutos.Find(grupo.Id);
                    if (grupoDb != null)
                    {
                        grupoDb.Nome = grupo.Nome;
                    }
                }
                context.SaveChanges();
            }
        }

        public void Excluir(int id)
        {
            using (var context = new MxPdvContext())
            {
                bool temProdutosVinculados = context.Produtos.Any(p => p.GrupoProdutoId == id);
                if (temProdutosVinculados)
                {
                    throw new Exception("Não é possível excluir este grupo, pois existem produtos vinculados a ele.");
                }

                var grupo = context.GruposProdutos.Find(id);
                if (grupo != null)
                {
                    context.GruposProdutos.Remove(grupo);
                    context.SaveChanges();
                }
            }
        }
    }
}