using Microsoft.EntityFrameworkCore;
using TarefasApi.Data;
using TarefasApi.Enums;
using TarefasApi.Models;
using TarefasApi.Repositorio.Interfaces;

namespace TarefasApi.Repositorio
{
    public class TarefasRepositorio : ITarefasRepositorio
    {
        private readonly TarefasDbContext _dbContext;
        private readonly IHostEnvironment _environment;
        public TarefasRepositorio(TarefasDbContext tarefasDbContext, IHostEnvironment environment)
        {
            _dbContext = tarefasDbContext;
            _environment = environment;
        }
        public async Task<TarefasModel> Adicionar(TarefasModel tarefa)
        {
            await _dbContext.Tarefas.AddAsync(tarefa);
            await _dbContext.SaveChangesAsync();

            return tarefa;
        }

        public async Task<TarefasModel> Atualizar(TarefasModel tarefa, int id)
        {
            TarefasModel tarefaId = await BuscarPorId(id);

            if (tarefaId == null)
            {
                throw new Exception($"Tarefa para o ID: {id} não foi encontrado no banco de dados.");
            }

            tarefaId.Nome = tarefa.Nome;
            tarefaId.Descricao = tarefa.Descricao;
            tarefaId.DataDeInicio = tarefa.DataDeInicio;
            tarefaId.DataDeFinalizacao = tarefa.DataDeFinalizacao;
            tarefaId.EstimativaEmDias = tarefa.EstimativaEmDias;
            tarefaId.Status = tarefa.Status;
            tarefaId.UsuarioId = tarefa.UsuarioId;

            _dbContext.Tarefas.Update(tarefaId);
            await _dbContext.SaveChangesAsync();

            return tarefaId;
        }

        public async Task<TarefasModel> BuscarPorId(int id)
        {
            return await _dbContext.Tarefas
                .Include(x => x.Usuario)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<List<TarefasModel>> BuscarTodasTarefas()
        {
            return await _dbContext.Tarefas
                .Include (x => x.Usuario)
                .ToListAsync();
        }

        public async Task<List<TarefasModel>> BuscarTarefaAgendada()
        {
            return await _dbContext.Tarefas
                .Where(x => x.Status == StatusTarefa.Agendada)
                .ToListAsync();
        }

        public async Task<bool> AtualizarTarefaParaFinalizada(int tarefaId)
        {
            TarefasModel tarefa = await _dbContext.Tarefas.FindAsync(tarefaId);

            if (tarefa == null)
            {
                return false;
            }

            tarefa.Status = StatusTarefa.Finalizada;
            _dbContext.Entry(tarefa).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();

            return true;
        }


        public async Task<TimeSpan> CalcularDuracaoEmAndamento(int tarefaId)
        {
            TarefasModel tarefa = await _dbContext.Tarefas.FindAsync(tarefaId);

            if (tarefa == null)
            {
                throw new ArgumentException("Tarefa não encontrada.");
            }

            if (tarefa.Status != StatusTarefa.EmAndamento)
            {
                throw new InvalidOperationException("A tarefa não está em andamento.");
            }

            TimeSpan duracaoEmAndamento = tarefa.DataDeFinalizacao - tarefa.DataDeInicio;

            return duracaoEmAndamento;
        }

        public async Task<string> AnexarArquivoPDF(int tarefaId, IFormFile arquivo)
        {           
            TarefasModel tarefa = await _dbContext.Tarefas.FindAsync(tarefaId);
            if (tarefa == null)
            {
                throw new Exception($"Tarefa para o ID: {tarefaId} não foi encontrado no banco de dados.");
            }
           
            if (arquivo == null || arquivo.Length == 0)
            {
                throw new Exception($"{arquivo} não foi encontrado");
            }

            try
            {                
                string pastaDocumentos = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                
                string nomeArquivo = Guid.NewGuid().ToString() + Path.GetExtension(arquivo.FileName);
                
                string caminhoArquivo = Path.Combine(pastaDocumentos, nomeArquivo);
                
                using (var stream = new FileStream(caminhoArquivo, FileMode.Create))
                {
                    await arquivo.CopyToAsync(stream);
                }
                
                tarefa.ArquivoPDF = caminhoArquivo;
                
                await _dbContext.SaveChangesAsync();

                return caminhoArquivo;
            }
            catch (FileNotFoundException ex)
            {
                throw new Exception($"Arquivo para tarefa com o ID: {tarefa.Id} não foi encontrado no banco de dados.", ex);
            }

        }

        public async Task<bool> Finalizar(int id)
        {
            TarefasModel tarefaId = await BuscarPorId(id);

            if (tarefaId == null)
            {
                throw new Exception($"Tarefa para o ID: {id} não foi encontrado no banco de dados.");
            }

            _dbContext.Tarefas.Remove(tarefaId);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
