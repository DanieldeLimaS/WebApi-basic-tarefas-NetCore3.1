using System.ComponentModel.DataAnnotations;

namespace TarefasBackEnd.Models.ViewModels
{
    public class TarefaUpdateViewModel
    {
        [Required(ErrorMessage ="Informe o nome da tarefa a ser criada.")]
        [MinLength(5,ErrorMessage ="Informe no minimo 5 caracteres par ao nome da tarefa")]
        [Display(Description ="Nome da Tarefa")]
        public string Nome { get; set; }
        public bool Concluida { get; set; }
    }
}
