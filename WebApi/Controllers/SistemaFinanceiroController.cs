using Domain.Interfaces.InterfaceServicos;
using Domain.Interfaces.ISistemaFinanceiro;
using Domain.Servicos;
using Entities.Entidades;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class SistemaFinanceiroController : ControllerBase
{
    private readonly InterfaceSistemaFinanceiro _interfaceSistemaFinanceiro;
    private readonly ISistemaFinanceiroServico _ISistemaFinanceiroServico;
    private readonly ILogger<SistemaFinanceiroController> _logger;

    public SistemaFinanceiroController(InterfaceSistemaFinanceiro interfaceSistemaFinanceiro, 
        ISistemaFinanceiroServico iSistemaFinanceiroServico,
        ILogger<SistemaFinanceiroController> logger)
    {
        _interfaceSistemaFinanceiro = interfaceSistemaFinanceiro;
        _ISistemaFinanceiroServico = iSistemaFinanceiroServico;
        _logger = logger;

    }

    // Método que ListaSistemaUsuario
    [HttpGet("/api/ListaSistemaUsuario")]
    [Produces("application/json")]   
    public async Task<object> ListaSistemaUsuario(string emailUsuario)
    {
        return await _interfaceSistemaFinanceiro.ListaSistemasUsuario(emailUsuario);
    }

    // Método de Adicionar Sistema Financeiro
    [HttpPost("/api/AdicionarSistemaFinanceiro")]
    [Produces("application/json")]
    public async Task<object> AdicionarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro)
    {
        await _ISistemaFinanceiroServico.AdicionarSistemaFinanceiro(sistemaFinanceiro);

        // Retornar o mesmo objeto que criamos, pra saber se criou ou não
        return Task.FromResult(sistemaFinanceiro);
    }

    // Método de Atualizar
    [HttpPut("/api/AtualizarSistemaFinanceiro")]
    [Produces("application/json")]
    public async Task<object> AtualizarSistemaFinanceiro(SistemaFinanceiro sistemaFinanceiro)
    {
        await _ISistemaFinanceiroServico.AtualizarSistemaFinanceiro(sistemaFinanceiro);

        // Retornar o objeto que atualizamos
        return Task.FromResult(sistemaFinanceiro);
    }

    // Método de Obter SistemaFinanceiro
    [HttpGet("/api/ObterSistemaFinanceiro")]
    [Produces("application/json")]
    public async Task<object> ObterSistemaFinanceiro(int id)
    {
        return await _interfaceSistemaFinanceiro.GetEntityById(id);
    }

    // Método de Deletar
    [HttpDelete("/api/DeleteSistemaFinanceiro")]
    [Produces("application/json")]
    public async Task<object> DeleteSistemaFinanceiro(int id)
    {
        try
        {
            var sistemaFinanceiro = await _interfaceSistemaFinanceiro.GetEntityById(id);

            await _interfaceSistemaFinanceiro.Delete(sistemaFinanceiro);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Falha ao remover sistema financeiro {id}", id);

            return BadRequest("Não foi possível remover o sistema financeiro");
        }
        
        return Ok("Sistema Financeiro deletado");
    }    
}
