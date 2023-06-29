using Domain.Interfaces.ICategoria;
using Domain.Interfaces.InterfaceServicos;
using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class CategoriaController : ControllerBase
    {
        private readonly InterfaceCategoria _InterfaceCategoria;
        private readonly ICategoriaServico _ICategoriaServico;
        private readonly ILogger<CategoriaController> _Logger;
        public CategoriaController(InterfaceCategoria InterfaceCategoria, ICategoriaServico ICategoriaServico, ILogger<CategoriaController> Logger)
        {
            _InterfaceCategoria = InterfaceCategoria;
            _ICategoriaServico = ICategoriaServico;
            _Logger = Logger;
        }

        // Método de Listar a Categoria do Usuario
        [HttpGet("/api/ListarCategoriasUsuario")]
        [Produces("application/json")]
        public async Task<object> ListarCategoriasUsuario(string emailUsuario)
        {
            return await _InterfaceCategoria.ListarCategoriasUsuario(emailUsuario);
        }

        // Método de Adicionar Categoria
        [HttpPost("/api/AdicionarCategoria")]
        [Produces("application/json")]
        public async Task<object> AdicionarCategoria(Categoria categoria)
        {
            try
            {
                await _ICategoriaServico.AdicionarCategoria(categoria);
            }
            catch (Exception ex)
            {
                // Era .LogError, mudei para testar a Controller AdicionarCategoria metodo POST
                _Logger.LogInformation(ex, "Falha ao Adicionar Categoria");

                return BadRequest("Não foi possivel adicionar a categoria");
            }
          
            // Retornar o mesmo objeto que criamos, pra saber se criou ou não
            return categoria;
        }

        // Método de Atualizar a Categoria do Usuario
        [HttpPut("/api/AtualizarCategoria")]
        [Produces("application/json")]
        public async Task<object> AtualizarCategoria(Categoria categoria)
        {
            await _ICategoriaServico.AtualizarCategoria(categoria);

            // Retornar o objeto que atualizamos
            return categoria;
        }

        // Método que ira Obter a Categoria
        [HttpGet("/api/ObterCategoria")]
        [Produces("application/json")]
        public async Task<object> ObterCategoria(int id)
        {
            return await _InterfaceCategoria.GetEntityById(id);
        }


        // Método que ira Deletar a Categoria
        [HttpDelete("/api/DeleteCategoria")]
        [Produces("application/json")]
        public async Task<object> DeleteCategoria(int id)
        {
            try
            {
                var categoria = await _InterfaceCategoria.GetEntityById(id);

                await _InterfaceCategoria.Delete(categoria);
            }
            catch (Exception ex)
            {                
                _Logger.LogInformation(ex, "Falha ao remover a categoria", id);

                return BadRequest("Não foi possivel deletar a Categoria"); ;
            }

            return Ok("Categoria Deletada!");
        }
    }
}
