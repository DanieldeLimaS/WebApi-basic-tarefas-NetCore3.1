using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using TarefasBackEnd.Models;
using TarefasBackEnd.Models.ViewModels;
using TarefasBackEnd.Repositories;

namespace TarefasBackEnd.Controllers
{
    [Authorize]
    [ApiController]
    [Route("tarefa")]
    public class TarefaController : ControllerBase
    {
        /// <summary>
        /// Retorna a lista de tarefas do usuário autenticado
        /// </summary>
        [HttpGet]
        [AllowAnonymous]
        [Route("GetListaTarefasDoUsuarioLogado")]
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

        /// <summary>
        /// Realiza consulta de acordo com o nome da tarefa passado e retorna uma lista de tarefas
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="nomeTarefa">Retorna a lista com o nome da tarefa</param>
        /// <returns></returns>
        [Route("GetListaTarefasPorNome")]
        [HttpGet]
        [AllowAnonymous]
        public IActionResult GetListaTarefas([FromServices] ITarefaRepository repository, string nomeTarefa)
        {
            var listaTarefa = repository.GetListaTarefa(nomeTarefa);
            if (listaTarefa.Count == 0)
                return NoContent();
            return Ok(listaTarefa);
        }
        /// <summary>
        /// Realiza o cadastro da tarefa no banco de dados
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("InserirNovaTarefa")]
        public IActionResult Create([FromBody] TarefaCadastroViewModel tarefaViewModel, [FromServices] ITarefaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            repository.Create(tarefaViewModel, new Guid(User.Identity.Name));
            return Ok("Cadastro Realizado com sucesso");
        }
        /// <summary>
        /// Realiza a alteração dos dados da tarefa no banco de dados
        /// </summary>
        /// <returns></returns>
        [HttpPut]
        [Route("AtualizarTarefa")]
        public IActionResult Update(string id, [FromBody] Tarefa model, [FromServices] ITarefaRepository repository)
        {
            if (!ModelState.IsValid)
                return BadRequest("Erro ao atualizar");

            model.Id = new Guid(id);
            repository.Update(model.Id, model);
            return Ok("Atualizado com sucesso.");
        }
        /// <summary>
        /// Deleta a tarefa
        /// </summary>
        /// <param name="id"> Id da tarefa</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("DeletarTarefaPorId")]
        public IActionResult Delete(Guid id, [FromServices] ITarefaRepository repository)
        {
            if (id == null)
                return BadRequest();

            repository.Delete(id);
            return Ok("Atualizado com sucesso.");
        }
    }
}
