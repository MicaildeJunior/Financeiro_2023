using Domain.Interfaces.InterfaceServicos;
using Domain.Interfaces.IUsuarioSistemaFinanceiro;
using Entities.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Servicos;

public class UsuarioSistemaFinanceiroServico : IUsuarioSistemaFinanceiroServico
{
    private readonly InterfaceUsuarioSistemaFinanceiro _interfaceUsusarioFinanceiro;
    public UsuarioSistemaFinanceiroServico(InterfaceUsuarioSistemaFinanceiro interfaceUsusarioFinanceiro)
    {
        _interfaceUsusarioFinanceiro = interfaceUsusarioFinanceiro;
    }

    public async Task CadastrarUsuarioNoSistema(UsuarioSistemaFinanceiro usuarioSistemaFinanceiro)
    {
        await _interfaceUsusarioFinanceiro.Add(usuarioSistemaFinanceiro);
    }
}
