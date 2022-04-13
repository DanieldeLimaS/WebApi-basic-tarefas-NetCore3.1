using TarefasBackEnd.Models;
using TarefasBackEnd.Models.ViewModels;

namespace TarefasBackEnd.Repositories
{
    public interface IUsuarioRepository
    {
        Usuario Read(string email, string senha);
        void Create(UsuarioCadastroViewModel usuario);
    }
}
