using System;
using System.Collections.Generic;
using System.Linq;
using MxPdv.Data;
using MxPdv.Entities;
using MxPdv.Helpers;
using MxPdv.Interfaces;

namespace MxPdv.Services
{
    public class UsuarioService : IUsuarioService
    {
        public List<Usuario> ObterTodos()
        {
            using (var context = new MxPdvContext())
            {
                return context.Usuarios.ToList();
            }
        }

        public void Salvar(Usuario usuario)
        {
            using (var context = new MxPdvContext())
            {
                bool loginJaExiste = context.Usuarios
                    .Any(u => u.Login.ToLower() == usuario.Login.ToLower() && u.Id != usuario.Id);

                if (loginJaExiste)
                {
                    throw new Exception("Já existe um utilizador com este Login. Escolha outro.");
                }

                usuario.Senha = SecurityHelper.HashPassword(usuario.Senha);

                if (usuario.Id == 0)
                {
                    context.Usuarios.Add(usuario);
                }
                else
                {
                    var usuarioDb = context.Usuarios.Find(usuario.Id);
                    if (usuarioDb != null)
                    {
                        usuarioDb.Login = usuario.Login;
                        usuarioDb.Senha = usuario.Senha;
                    }
                }
                context.SaveChanges();
            }
        }

        public void Excluir(int id)
        {
            using (var context = new MxPdvContext())
            {
                if (context.Usuarios.Count() <= 1)
                {
                    throw new Exception("Não pode excluir o único utilizador do sistema!");
                }

                var usuario = context.Usuarios.Find(id);
                if (usuario != null)
                {
                    context.Usuarios.Remove(usuario);
                    context.SaveChanges();
                }
            }
        }

        public Usuario Autenticar(string login, string senhaPura)
        {
            using (var context = new MxPdvContext())
            {
                string senhaHash = SecurityHelper.HashPassword(senhaPura);
                return context.Usuarios.FirstOrDefault(u => u.Login == login && u.Senha == senhaHash);
            }
        }

        public void CriarUsuarioPadraoSeNecessario()
        {
            using (var context = new MxPdvContext())
            {
                if (!context.Usuarios.Any())
                {
                    var novoUsuario = new Usuario
                    {
                        Login = "admin",
                        Senha = SecurityHelper.HashPassword("admin")
                    };

                    context.Usuarios.Add(novoUsuario);
                    context.SaveChanges();
                }
                else
                {
                    var adminAntigo = context.Usuarios.FirstOrDefault(u => u.Login == "admin" && u.Senha == "admin");
                    if (adminAntigo != null)
                    {
                        adminAntigo.Senha = SecurityHelper.HashPassword("admin");
                        context.SaveChanges();
                    }
                }
            }
        }
    }
}