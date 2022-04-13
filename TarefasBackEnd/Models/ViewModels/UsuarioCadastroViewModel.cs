using System.ComponentModel.DataAnnotations;

namespace TarefasBackEnd.Models.ViewModels
{
    public class UsuarioCadastroViewModel
    {
        [Required(ErrorMessage ="Campo obrigatório")]
        [MinLength(5,ErrorMessage ="Informe no minimo 5 caracteres")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Informe um endereço de email válido.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Password)]
        public string Senha { get; set; }
    }
}
