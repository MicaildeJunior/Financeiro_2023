using Domain.Interfaces.InterfaceServicos;
using Domain.Interfaces.IUsuarioSistemaFinanceiro;
using Entities.Entidades;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UsuarioSistemaFinanceiroController : ControllerBase
{
    private readonly InterfaceUsuarioSistemaFinanceiro _InterfaceUsuarioSistemaFinanceiro;
    private readonly IUsuarioSistemaFinanceiroServico _IUsuarioSistemaFinanceiroServico;
    private readonly ILogger<SistemaFinanceiroController> _logger;
    public UsuarioSistemaFinanceiroController(InterfaceUsuarioSistemaFinanceiro InterfaceUsuarioSistemaFinanceiro, 
           IUsuarioSistemaFinanceiroServico IUsuarioSistemaFinanceiroServico,
           ILogger<SistemaFinanceiroController> logger)
    {
        _InterfaceUsuarioSistemaFinanceiro = InterfaceUsuarioSistemaFinanceiro;
        _IUsuarioSistemaFinanceiroServico = IUsuarioSistemaFinanceiroServico;
        _logger = logger;

    }


    // Metodo que ira listar os Ususarios do sistema
    [HttpGet("/api/ListarUsuariosSistema")]
    [Produces("application/json")]
    public async Task<object> ListaSistemaFinanceiro(int idSistema)
    {
        return await _InterfaceUsuarioSistemaFinanceiro.ListarUsuarioSistema(idSistema);
    }

    // Metodo que ira Cadastrar um Usuario
    [HttpPost("/api/CadastrarUsuarioNoSistema")]
    [Produces("application/json")]
    public async Task<object> CadastrarUsuarioNoSistema(int idSistema, string emailUsuario)
    {
        try
        {
            await _IUsuarioSistemaFinanceiroServico.CadastrarUsuarioNoSistema(
            new UsuarioSistemaFinanceiro
            {
                IdSistema = idSistema,
                EmailUsuario = emailUsuario,
                Administrador = false,
                SistemaAtual = true
            });
        }
        catch (Exception)
        {
            return Task.FromResult(false);
        }   

        return Task.FromResult(true);
    }

    // Metodo que ira Deletar o Usuario do sistema
    [HttpDelete("/api/DeleteUsuarioNoSistemaFinanceiro{id}")]
    [Produces("application/json")]
    public async Task<object> DeleteUsuarioNoSistemaFinanceiro(int id)
    {
        try
        {
            var usuarioSistemaFinanceiro = await _InterfaceUsuarioSistemaFinanceiro.GetEntityById(id);

            await _InterfaceUsuarioSistemaFinanceiro.Delete(usuarioSistemaFinanceiro);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Falha ao remover o usuario do sistema {id}", id);

            return BadRequest("Não foi possível remover o sistema financeiro");
        }

        return Ok("Usuário do sistema deletado");

    }
}