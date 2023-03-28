using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Entidades;

[Table("Categoria")]
public class Categoria : Base
{
    [ForeignKey("SistemaFinanceiro")]
    [Column(Order = 1)]
    public int IdSistema { get; set; }
    // Foreign Key com Tabela SistemaFinanceiro
    public virtual SistemaFinanceiro SistemaFinanceiro { get; set; }
}
