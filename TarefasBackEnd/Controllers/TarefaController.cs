using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TarefasBackEnd.Models;
using TarefasBackEnd.Repositories;

namespace TarefasBackEnd.Controllers
{
    [Authorize]
    [ApiController]
    [Route("tarefa")]
    public class TarefaController : ControllerBase
    {
       /// <summary>
       /// Retorna usuário autenticado
       /// </summary>
        [HttpGet]
        [AllowAnonymous]
        public IActionResult Get([FromServices] ITarefaRepository repository)
        {
            try
            {
                if (User.Identity.Name is null)
                    return Unauthorized("Usuário não autorizado.");

                Guid usuarioId = new Guid(User.Identity.Name);//recuperando o usuário autenticado

                var tarefas = repository.Read(usuarioId);
                return Ok(tarefas);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
        }
        [Route("GetListaTarefas")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetListaTarefas( [FromServices] ITarefaRepository repository ,string nomeTarefa)
        {
            var listaTarefa = repository.GetListaTarefa(nomeTarefa);
            if (listaTarefa.Count == 0)
                return NoContent();
            return Ok(listaTarefa);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Tarefa model, [FromServices] ITarefaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            model.UsuarioId = new Guid(User.Identity.Name);//recuperando o usuário autenticado
            repository.Create(model);
            return Ok("Cadastro Realizado com sucesso");
        }

        [HttpPut]
        public IActionResult Update(string id, [FromBody] Tarefa model, [FromServices] ITarefaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest("Erro ao atualizar");

            model.Id = new Guid(id);
            repository.Update(model.Id,model);
            return Ok("Atualizado com sucesso.");
        }

        [HttpDelete]
        public IActionResult Delete(Guid id, [FromServices] ITarefaRepository repository)
        {
            if (id== null)
                return BadRequest();

            repository.Delete(id);
            return Ok("Atualizado com sucesso.");
        }
    }
}
