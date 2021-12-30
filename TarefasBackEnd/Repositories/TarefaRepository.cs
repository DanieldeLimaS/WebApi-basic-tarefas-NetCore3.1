using System.Xml.Serialization;
using System.Collections.Generic;
using TarefasBackEnd.Models;
using System;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace TarefasBackEnd.Repositories
{
    public class TarefaRepository : ITarefaRepository
    {
        private readonly DataContext context;
        /// <summary>
        /// Construtor da classe com injeção de dependência
        /// </summary>
        /// <param name="context"></param>
        public TarefaRepository(DataContext context)
        {
            this.context = context;
        }
        /// <summary>
        /// Método responsável pela criação da tarefa
        /// </summary>
        /// <param name="tarefa"> objeto tarefa</param>
        public void Create(Tarefa tarefa)
        {
            tarefa.Id = Guid.NewGuid();
            context.Add(tarefa);//Adiciona o objeto tarefa no contexto
            context.SaveChanges();//salva as alterações
        }
        /// <summary>
        /// Método responsável por deletar uma terafa por ID
        /// </summary>
        /// <param name="Id">Id da tarefa</param>
        public void Delete(Guid Id)
        {
            //valida se o objeto está nulo e retorna uma excessão com a mensagem de erro
            if (Id == null)
                throw new Exception("Selecione uma tarefa para ser atualizada!");
            var tarefa = context.Tarefas.Find(Id);//consultando a tarefa pelo id
            context.Entry(tarefa).State = EntityState.Deleted;//muda o estado da tarefa para deletado
            context.SaveChanges();//salva as alterações
        }
        /// <summary>
        /// Método responsável por retornar todas as tarefas
        /// </summary>
        public List<Tarefa> Read(Guid id)
        {
            //apenas retorna todos os dados usando  a expressao linq
            return context.Tarefas.Where(x=>x.UsuarioId==id).ToList();
        }
        /// <summary>
        /// Método responsável por realizar a alteração dos dados
        /// Esse método já espera que o objeto tarefa já vem carregado para realizar a operação
        /// </summary>
        /// <param name="tarefa"> Objeto Tarefa</param>
        public void Update(Guid id,Tarefa tarefa)
        {
            Tarefa _tarefa = new Tarefa();
             _tarefa = context.Tarefas.Find(id);
            _tarefa.Nome = tarefa.Nome;
            _tarefa.Concluida = tarefa.Concluida;

            //modifica o estado para modified e salva as alterações
            context.Entry(_tarefa).State = EntityState.Modified;
            context.SaveChanges();
        }
    }
}