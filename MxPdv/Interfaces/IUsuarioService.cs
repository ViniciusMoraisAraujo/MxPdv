using System.Collections.Generic;
using MxPdv.Entities;

namespace MxPdv.Interfaces
{
    public interface IUsuarioService
    {
        List<Usuario> ObterTodos();
        void Salvar(Usuario usuario);
        void Excluir(int id);
        Usuario Autenticar(string login, string senhaPura);
        void CriarUsuarioPadraoSeNecessario();
    }
}