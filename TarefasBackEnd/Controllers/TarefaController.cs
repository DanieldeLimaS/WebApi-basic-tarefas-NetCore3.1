using Microsoft.AspNetCore.Mvc;
using System;
using TarefasBackEnd.Models;
using TarefasBackEnd.Repositories;

namespace TarefasBackEnd.Controllers
{
    [ApiController]
    [Route("tarefa")]
    public class TarefaController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get([FromServices] ITarefaRepository repository)
        {
            var tarefas = repository.Read();
            return Ok(tarefas);
        }

        [HttpPost]
        public IActionResult Create([FromBody] Tarefa model, [FromServices] ITarefaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            repository.Create(model);
            return Ok("Cadastro Realizado com sucesso");
        }

        [HttpPut]
        public IActionResult Update(string id, [FromBody] Tarefa model, [FromServices] ITarefaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();

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
