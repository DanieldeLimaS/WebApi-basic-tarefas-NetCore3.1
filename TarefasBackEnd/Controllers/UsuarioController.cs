using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TarefasBackEnd.Models;
using TarefasBackEnd.Repositories;
using TarefasBackEnd.ViewModels;

namespace TarefasBackEnd.Controllers
{
    [ApiController]
    [Route("usuario")]
    public class UsuarioController : ControllerBase
    {
        [HttpPost]
        [Route("")]
        public IActionResult Create([FromBody]Usuario model, [FromServices]IUsuarioRepository repository)
        {
            if (!ModelState.IsValid) return BadRequest();

            repository.Create(model);
            return Ok();
        }
        [HttpPost]
        [Route("login")]
        public IActionResult Login([FromBody]UsuarioLoginViewModel model, [FromServices] IUsuarioRepository repository)
        {
            if (!ModelState.IsValid) return BadRequest();

            Usuario usuario = repository.Read(model.Email, model.Senha);
            if (usuario is null)
                return Unauthorized();
            return Ok(new
            {
                usuario = usuario
            });
            
        }
    }
}
