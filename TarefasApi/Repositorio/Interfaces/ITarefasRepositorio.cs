using TarefasApi.Models;

namespace TarefasApi.Repositorio.Interfaces
{
    public interface ITarefasRepositorio
    {
        Task<List<TarefasModel>> BuscarTodasTarefas();
        Task<TarefasModel> BuscarPorId(int id);
        Task<List<TarefasModel>> BuscarTarefaAgendada();
        Task<bool> AtualizarTarefaParaFinalizada(int tarefaId);
        Task<TimeSpan> CalcularDuracaoEmAndamento(int tarefaId);
        Task<string> AnexarArquivoPDF(int tarefaId, IFormFile arquivo);
        Task<TarefasModel> Adicionar(TarefasModel tarefa);
        Task<TarefasModel> Atualizar(TarefasModel tarefa, int id);
        Task<bool> Finalizar(int id);
    }
}
