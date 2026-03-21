using System.Collections.Generic;
using MxPdv.Entities;

namespace MxPdv.Interfaces
{
    public interface IProdutoService
    {
        List<Produto> ObterTodos();
        Produto ObterPorId(int id);
        void Salvar(Produto produto);
        void Excluir(int id);
    }
}