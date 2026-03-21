using System.Collections.Generic;
using MxPdv.Entities;

namespace MxPdv.Interfaces
{
    public interface IGrupoProdutoService
    {
        List<GrupoProduto> ObterTodos();
        void Salvar(GrupoProduto grupo);
        void Excluir(int id);
    }
}