using Microsoft.AspNetCore.Mvc;
using TarefasApi.Models;
using TarefasApi.Repositorio.Interfaces;

namespace TarefasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuariosRepositorio _usuarioRepositorio;

        public UsuariosController(IUsuariosRepositorio usuariosRepositorio)
        {
            _usuarioRepositorio = usuariosRepositorio;
        }

        [HttpGet("Buscar todos usuarios")]
        public async Task<ActionResult<List<UsuariosModel>>> BuscarTodos()
        {
            List<UsuariosModel> usuario = await _usuarioRepositorio.BuscarTodos();
            
            return Ok(usuario);
        }
       
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuariosModel>> Get(int id)
        {
            UsuariosModel usuario = await _usuarioRepositorio.BuscarPorId(id);

            return Ok(usuario);
        }
        
        [HttpPost]
        public async Task<ActionResult<UsuariosModel>> Cadastrar([FromBody] UsuariosModel usuarioModel)
        {
            UsuariosModel usuario = await _usuarioRepositorio.Adicionar(usuarioModel);

            return Ok(usuario);
        }

        [HttpPut("id")]
        public async Task<ActionResult<UsuariosModel>> Atualizar([FromBody] UsuariosModel usuarioModel, int id)
        {
            usuarioModel.Id = id;
            UsuariosModel usuario = await _usuarioRepositorio.Atualizar(usuarioModel, id);

            return Ok(usuario);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<UsuariosModel>> Delete(int id)
        {
            bool deletado = await _usuarioRepositorio.Apagar(id);

            return Ok(deletado);
        }
    }
}
