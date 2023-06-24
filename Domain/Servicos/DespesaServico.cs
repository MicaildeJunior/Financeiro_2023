using Domain.Interfaces.IDespesa;
using Domain.Interfaces.InterfaceServicos;
using Entities.Entidades;

namespace Domain.Servicos;

public class DespesaServico : IDespesaServico
{
    private readonly InterfaceDespesa _InterfaceDespesa;
    public DespesaServico(InterfaceDespesa interfaceDespesa)
    {
        _InterfaceDespesa = interfaceDespesa;
    }

    public async Task AdicionarDespesa(Despesa despesa)
    {
        var data = DateTime.UtcNow;
        despesa.DataCadastro = data;
        despesa.Ano = data.Year;
        despesa.Mes = data.Month;

        var valido = despesa.ValidarPropriedadeString(despesa.Nome, "Nome");
        if (valido)
            await _InterfaceDespesa.Add(despesa);
    }

    public async Task AtualizarDespesa(Despesa despesa)
    {
        var data = DateTime.UtcNow;
        despesa.DataAlteracao = data;

        if (despesa.Pago)
        {
            despesa.DataPagamento = data;
        }

        var valido = despesa.ValidarPropriedadeString(despesa.Nome, "Nome");
        if (valido)
            await _InterfaceDespesa.Update(despesa);
    }

    public async Task<object> CarregaGraficos(string emailUsuario)
    {
        // Listar despesas do Usuario
        var despesasUsuario = await _InterfaceDespesa.ListarDespesasUsuario(emailUsuario);

        // Listar despesas atrasadas
        var despesasAnterior = await _InterfaceDespesa.ListarDespesasUsuarioNaoPagasMesesAnterior(emailUsuario);

        // Soma de despesas nao pagas
        // despesasAnterior.Alguma ?, despesasAnterior.Listar.Soma(x => x.Valor)"Valor é propriedade de Despesa" senao retorna 0;
        var despesas_naoPagasMesesAnteriores = despesasAnterior.Any() ?
            despesasAnterior.ToList().Sum(x => x.Valor) : 0;

        // Listar as despesas pagas do usuario onde é pago e tipo despesa é igual a entidade do enum da conta e ja soma o valor
        var despesas_pagas = despesasUsuario.Where(d => d.Pago && d.TipoDespesa == Entities.Enums.EnumTipodespesa.Contas)
            .Sum(x => x.Valor);

        // Listar as despesas que nao estao pagas. Onde nao "!" estao pagas
        var despesas_pendentes = despesasUsuario.Where(d => !d.Pago && d.TipoDespesa == Entities.Enums.EnumTipodespesa.Contas)
            .Sum(x => x.Valor);

        // Listar todas as despesas onde forem investimentos
        var investimentos = despesasUsuario.Where(d => d.TipoDespesa == Entities.Enums.EnumTipodespesa.Investimento)
            .Sum(x => x.Valor);

        return new
        {
            sucesso = "OK",
            despesas_pagas = despesas_pagas,
            despesas_pendentes = despesas_pendentes,
            investimentos = investimentos
        };
    }
}
