using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace TarefasBackEnd.Models
{
    [Table("usuario")]
    public class Usuario
    {
        [Column("id")]
        public Guid Id { get; set; }
        [Column("nome")]
        public string Nome { get; set; }
        [Column("email")]
        public string Email { get; set; }
        [Column("senha")]
        public string Senha { get; set; }
    }
}
