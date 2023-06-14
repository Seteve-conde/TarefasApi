using TarefasApi.Models;

namespace TarefasApi.Repositorio.Interfaces
{
    public interface IUsuariosRepositorio
    {
        Task<List<UsuariosModel>> BuscarTodos();
        Task<UsuariosModel> BuscarPorId(int id);
        Task<UsuariosModel> BuscarPorNome(string Nome);
        Task<UsuariosModel> Adicionar(UsuariosModel usuario);
        Task<UsuariosModel> Atualizar(UsuariosModel usuario, int id);
        Task<bool> Apagar(int id);
    }
}
