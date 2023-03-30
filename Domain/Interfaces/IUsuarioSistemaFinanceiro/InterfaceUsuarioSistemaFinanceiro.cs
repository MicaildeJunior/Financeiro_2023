
using Domain.Interfaces.Generics;
using Entities.Entidades;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.IUsuarioSistemaFinanceiro;

public interface InterfaceUsuarioSistemaFinanceiro : InterfaceGeneric<UsuarioSistemaFinanceiro>
{
    Task<IList<UsuarioSistemaFinanceiro>> ListarUsuarioSistema(int IdSistema);

    Task RemoveUsuarios(List<UsuarioSistemaFinanceiro> usuario);

    Task<UsuarioSistemaFinanceiro> ObterUsuarioPorEmail(string emailUsuario);
}
