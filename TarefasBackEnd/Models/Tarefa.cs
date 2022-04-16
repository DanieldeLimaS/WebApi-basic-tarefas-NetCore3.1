using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TarefasBackEnd.Models
{
    public class Tarefa
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("usuarioId")]
        public Guid UsuarioId { get; set; }
        [Required]
        [Column("nome")]
        public string Nome { get; set; }
        [Column("concluida")]
        public bool Concluida { get; set; }
    }
}
