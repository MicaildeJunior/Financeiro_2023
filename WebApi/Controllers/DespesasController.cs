using Domain.Interfaces.IDespesa;
using Domain.Interfaces.InterfaceServicos;
using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class DespesasController : ControllerBase
{
    private readonly InterfaceDespesa _InterfaceDespesa;
    private readonly IDespesaServico _IDespesaServico;
    private readonly ILogger _Logger;
    public DespesasController(InterfaceDespesa InterfaceDespesa, IDespesaServico IDespesaServico, ILogger<DespesasController> Logger)
    {
        _InterfaceDespesa = InterfaceDespesa;
        _IDespesaServico = IDespesaServico;
        _Logger = Logger;
    }

    // Método Listar Despesas do Usuario
    [HttpGet("/api/ListarDespesaUsuario")]
    [Produces("application/json")]
    public async Task<object> ListarDespesasUsuario(string emailUsuario)
    {
        return await _InterfaceDespesa.ListarDespesasUsuario(emailUsuario);
    }

    // Método Adicionar Despesas
    [HttpPost("/api/AdicionarDespesa")]
    [Produces("application/json")]
    public async Task<object> AdicionarDespesa(Despesa despesa)
    {
        await _IDespesaServico.AdicionarDespesa(despesa);

        // Retornar o mesmo objeto que criamos, pra saber se criou ou não
        return despesa;
    }

    // Método Atualizar Despesas
    [HttpPut("/api/AtualizarDespesa")]
    [Produces("application/json")]
    public async Task<object> AtualizarDespesa(Despesa despesa)
    {
        await _IDespesaServico.AtualizarDespesa(despesa);

        // Retornar o objeto que atualizamos
        return despesa;
    }

    // Método que ira Obter as Despesas
    [HttpGet("/api/ObterDespesa")]
    [Produces("application/json")]
    public async Task<object> ObterDespesa(int id)
    {
        return await _InterfaceDespesa.GetEntityById(id);
    }

    // Método que ira Deletar Despesa
    [HttpDelete("/api/DeleteDespesa")]
    [Produces("application/json")]
    public async Task<object> DeleteDespesa(int id)
    {
        try
        {
            var despesa = await _InterfaceDespesa.GetEntityById(id);

            await _InterfaceDespesa.Delete(despesa);
        }
        catch (Exception ex)
        {
            _Logger.LogError(ex, "Falha ao remover Despesa");

            return BadRequest("Não foi possivel Deletar Despesa!");
        }

        return Ok("Despesa deletada com sucesso"); 
    }

    //
    [HttpGet("/api/CarregaGraficos")]
    [Produces("application/json")]
    public async Task<object> CarregaGraficos(string emailUsuario)
    {
        return await _IDespesaServico.CarregaGraficos(emailUsuario);
    }
}
