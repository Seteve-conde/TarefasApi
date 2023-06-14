using Microsoft.EntityFrameworkCore;
using TarefasApi.Data;
using TarefasApi.Models;
using TarefasApi.Repositorio.Interfaces;

namespace TarefasApi.Repositorio
{
    public class UsuariosRepositorio : IUsuariosRepositorio
    {
        private readonly TarefasDbContext _dbContext;
        public UsuariosRepositorio(TarefasDbContext tarefasDbContext)
        {
            _dbContext = tarefasDbContext;
        }
        public async Task<List<UsuariosModel>> BuscarTodos()
        {
            return await _dbContext.Usuarios.ToListAsync();
        }

        public async Task<UsuariosModel> BuscarPorId(int id)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UsuariosModel> BuscarPorNome(string Nome)
        {
            return await _dbContext.Usuarios.FirstOrDefaultAsync(x => x.Nome == Nome);
        }

        public async Task<UsuariosModel> Adicionar(UsuariosModel usuario)
        {
            await _dbContext.Usuarios.AddAsync(usuario);
            await _dbContext.SaveChangesAsync();

            return usuario;
        }

        public async Task<UsuariosModel> Atualizar(UsuariosModel usuario, int id)
        {
            UsuariosModel usuarioId = await BuscarPorId(id);

            if (usuarioId == null) 
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
            }

            usuarioId.Nome = usuario.Nome;
            
            _dbContext.Usuarios.Update(usuarioId);
            await _dbContext.SaveChangesAsync();

            return usuarioId;
        }

        public async Task<bool> Apagar(int id)
        {
            UsuariosModel usuarioId = await BuscarPorId(id);

            if (usuarioId == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
            }
            
            _dbContext.Usuarios.Remove(usuarioId);
            await _dbContext.SaveChangesAsync();

            return true;
        }    
    }
}
