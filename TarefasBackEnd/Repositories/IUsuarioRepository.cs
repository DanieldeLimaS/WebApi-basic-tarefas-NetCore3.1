using TarefasBackEnd.Models;

namespace TarefasBackEnd.Repositories
{
    public interface IUsuarioRepository
    {
        Usuario Read(string email, string senha);
        void Create(Usuario usuario);
    }
}
