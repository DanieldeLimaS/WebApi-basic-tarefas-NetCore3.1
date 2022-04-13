using System;
using System.Collections.Generic;
using TarefasBackEnd.Models;
using TarefasBackEnd.Models.ViewModels;

namespace TarefasBackEnd.Repositories{
   
    public interface ITarefaRepository
    {
        List<Tarefa> Read(Guid id);
        List<Tarefa> GetListaTarefa(string nomeTarefa);
        void Create(TarefaCadastroViewModel tarefaViewModel,Guid UsuarioId);
        void Delete(Guid Id);
        void Update(Guid id,Tarefa tarefa);
    }
}