using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            var tarefa = _context.Tarefas.Find(id);

            if (tarefa == null)
                return NotFound(new { mensagem = "Tarefa não encontrada" });
                
            return Ok(tarefa);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            var tarefas = _context.Tarefas.ToList();
            return Ok(tarefas);
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            if (string.IsNullOrWhiteSpace(titulo))
                return BadRequest(new { mensagem = "O título da tarefa é obrigatório" });
                
            var tarefas = _context.Tarefas
                .Where(x => x.Titulo.Contains(titulo, StringComparison.OrdinalIgnoreCase))
                .ToList();
                
            return Ok(tarefas);
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date);
            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status) 
        {
            var tarefa = _context.Tarefas.Where(x => x.Status == status);
            return Ok(tarefa);
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (string.IsNullOrWhiteSpace(tarefa.Titulo))
                return BadRequest(new { mensagem = "O título da tarefa é obrigatório" });
                
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { mensagem = "A data da tarefa não pode ser vazia" });
                
            // Define o status como Pendente por padrão se não for informado
            if (tarefa.Status == 0) // Se for o valor padrão do enum
                tarefa.Status = EnumStatusTarefa.Pendente;

            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();
            
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            if (string.IsNullOrWhiteSpace(tarefa.Titulo))
                return BadRequest(new { mensagem = "O título da tarefa é obrigatório" });
                
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { mensagem = "A data da tarefa não pode ser vazia" });

            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound(new { mensagem = "Tarefa não encontrada" });

            // Atualiza as propriedades da tarefa existente
            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();
            
            return Ok(tarefaBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound(new { mensagem = "Tarefa não encontrada" });

            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();
            
            return NoContent();
        }
    }
}
