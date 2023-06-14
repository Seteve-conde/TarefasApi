using Microsoft.AspNetCore.Mvc;
using TarefasApi.Enums;
using TarefasApi.Models;
using TarefasApi.Repositorio;
using TarefasApi.Repositorio.Interfaces;

namespace TarefasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefasController : ControllerBase
    {
        private readonly ITarefasRepositorio _tarefasRepositorio;
        private readonly IUsuariosRepositorio _usuariosRepositorio;

        public TarefasController(ITarefasRepositorio tarefasRepositorio, IUsuariosRepositorio usuariosRepositorio)
        {
            _tarefasRepositorio = tarefasRepositorio;
            _usuariosRepositorio = usuariosRepositorio;
        }

        [HttpGet("Buscar todas as tarefas")]
        public async Task<ActionResult<List<TarefasModel>>> ListarTodasTarefas()
        {
            List<TarefasModel> tarefas = await _tarefasRepositorio.BuscarTodasTarefas();

            return Ok(tarefas);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<TarefasModel>> GetId(int id)
        {
            TarefasModel tarefas = await _tarefasRepositorio.BuscarPorId(id);

            return Ok(tarefas);
        }

        [HttpPost("Criar tarefa para João Silva")]
        public async Task<ActionResult<TarefasModel>> CriarTarefaJoaoSilva()
        {
            UsuariosModel joaoSilva = await _usuariosRepositorio.BuscarPorNome("João Silva");

            if (joaoSilva == null)
            {
                joaoSilva = await _usuariosRepositorio.Adicionar(new UsuariosModel { Nome = "João Silva" });
            }

            TarefasModel tarefa = new TarefasModel
            {
                Nome = "Tarefa para João Silva",
                Descricao = "Organização de estoque",
                DataDeInicio = new DateTime(2021, 12, 31),
                DataDeFinalizacao = DateTime.MinValue, 
                EstimativaEmDias = 1, 
                Status = StatusTarefa.Agendada,
                UsuarioId = joaoSilva.Id
            };

            tarefa = await _tarefasRepositorio.Adicionar(tarefa);

            return Ok(tarefa);
        }

        [HttpPost("Criar tarefa para Ana Silva")]
        public async Task<ActionResult<TarefasModel>> CriarTarefaAnaSilva()
        {
            UsuariosModel anaSilva = await _usuariosRepositorio.BuscarPorNome("Ana Silva");

            if (anaSilva == null)
            {
                anaSilva = await _usuariosRepositorio.Adicionar(new UsuariosModel { Nome = "Ana Silva" });
            }

            TarefasModel tarefa = new TarefasModel
            {
                Nome = "Tarefa para Ana Silva",
                Descricao = "Tarefa para Ana Silva com data atual",
                DataDeInicio = DateTime.Now,
                DataDeFinalizacao = DateTime.MinValue, 
                EstimativaEmDias = 0, 
                Status = StatusTarefa.Agendada,
                UsuarioId = anaSilva.Id
            };

            tarefa = await _tarefasRepositorio.Adicionar(tarefa);

            return Ok(tarefa);
        }

        [HttpPost("Criar tarefas")]
        public async Task<ActionResult<TarefasModel>> Cadastrar([FromBody] TarefasModel tarefaModel)
        {
            TarefasModel tarefas = await _tarefasRepositorio.Adicionar(tarefaModel);

            return Ok(tarefas);
        }

        // este método é capaz de atender a solicitação 3 e 6 dos metodos a serem implementados pelo teste.
        [HttpPut("Atualizar tarefa Agendada para Em Andamento")]
        public async Task<ActionResult<TarefasModel>> AtualizarTarefaParaEmAndamento([FromBody] TarefasModel tarefaModel, int id)
        {
            UsuariosModel usuario = await _usuariosRepositorio.BuscarPorNome("Ana Silva");

            if (usuario == null)
            {                
                return NotFound($"Usuário {usuario} não encontrado.");
            }

            List<TarefasModel> tarefaAgendada = await _tarefasRepositorio.BuscarTarefaAgendada();

            if (tarefaAgendada != null)
            {
                foreach (var tarefa in tarefaAgendada)
                {
                    if (tarefa.UsuarioId == usuario.Id)
                    {
                        tarefa.Status = StatusTarefa.EmAndamento;

                        await _tarefasRepositorio.Atualizar(tarefa, tarefa.Id);
                    }
                    
                }                
                
                return Ok(tarefaAgendada);
            }
            else
            {                
                return NotFound("Não há tarefas em agendadas no momento.");
            }
        }

        [HttpPut("{id}/Finalizar tarefa")]
        public async Task<ActionResult<bool>> AtualizarTarefaParaFinalizada(int id)
        {
            bool atualizacaoSucesso = await _tarefasRepositorio.AtualizarTarefaParaFinalizada(id);

            if (!atualizacaoSucesso)
            {
                return NotFound("Tarefa não encontrada.");
            }

            return Ok(true);
        }


        [HttpGet("{id}/duracao-em-andamento")]
        public async Task<ActionResult<TimeSpan>> ObterDuracaoEmAndamento(int id)
        {
            try
            {
                TimeSpan duracao = await _tarefasRepositorio.CalcularDuracaoEmAndamento(id);
                return Ok(duracao);
            }           
            catch (Exception)
            {
                return StatusCode(500, "Ocorreu um erro ao calcular a duração em andamento.");
            }
        }

        [HttpPost("Anexar arquivo PDF")]
        public async Task<ActionResult<string>> AnexarArquivoPDF(int tarefaId, IFormFile arquivo)
        {
            TarefasModel tarefa = await _tarefasRepositorio.BuscarPorId(tarefaId);

            if (tarefa == null)
            {
                return NotFound("Tarefa não encontrada.");
            }

            string caminhoArquivo = await _tarefasRepositorio.AnexarArquivoPDF(tarefaId, arquivo);

            if (caminhoArquivo == null)
            {
                return BadRequest("Falha ao anexar o arquivo PDF.");
            }

            return Ok(caminhoArquivo);
        }


        [HttpPut("id")]
        public async Task<ActionResult<TarefasModel>> Atualizar([FromBody] TarefasModel tarefaModel, int id)
        {
            tarefaModel.Id = id;
            TarefasModel tarefas = await _tarefasRepositorio.Atualizar(tarefaModel, id);

            return Ok(tarefas);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<TarefasModel>> Delete(int id)
        {
            bool deletada = await _tarefasRepositorio.Finalizar(id);

            return Ok(deletada);
        }
    }
}
