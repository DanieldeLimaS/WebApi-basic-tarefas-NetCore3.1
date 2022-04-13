using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarefasBackEnd.Models;
using TarefasBackEnd.Models.ViewModels;

namespace TarefasBackEnd.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly DataContext context;
        public UsuarioRepository(DataContext context)
        {
            this.context = context;
        }
        public void Create(UsuarioCadastroViewModel usuarioViewmodel)
        {
            Usuario usuario = new Usuario()
            {
                Id = Guid.NewGuid(),
                Email = usuarioViewmodel.Email,
                Nome = usuarioViewmodel.Nome,
                Senha = usuarioViewmodel.Senha,
            };
            context.Add(usuario);
            context.SaveChanges();
        }

        public Usuario Read(string email, string senha)
        {
            return context.Usuarios
                 .Where(x => x.Email == email && x.Senha == senha)
                 .Select(x => new Usuario
                 {
                     Id = x.Id,
                     Nome = x.Nome,
                     Email = x.Email,
                     Senha = string.Empty
                 })
                 .SingleOrDefault();
        }
    }
}
