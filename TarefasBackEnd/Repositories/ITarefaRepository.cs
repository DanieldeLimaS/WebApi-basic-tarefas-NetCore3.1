using System;
using System.Collections.Generic;
using TarefasBackEnd.Models;

namespace TarefasBackEnd.Repositories{
   
    public interface ITarefaRepository
    {
        List<Tarefa> Read(Guid id);
        List<Tarefa> GetListaTarefa(string nomeTarefa);
        void Create(Tarefa tarefa);
        void Delete(Guid Id);
        void Update(Guid id,Tarefa tarefa);
    }
}