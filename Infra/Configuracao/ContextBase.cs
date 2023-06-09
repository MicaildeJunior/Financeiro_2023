 using Entities.Entidades;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.Configuracao;

public class ContextBase : IdentityDbContext<ApplicationUser>
{
    public ContextBase( DbContextOptions options) : base(options)
    {       
    }

    // Tabelas que queremos que o EntityFramework gerencie, assim podemos chamar elas direto
    public DbSet<SistemaFinanceiro> SistemaFinanceiro { get; set; }
    public DbSet<UsuarioSistemaFinanceiro> UsuarioSistemaFinanceiro { get; set; }
    public DbSet<Categoria> Categoria { get; set; }
    public DbSet<Despesa> Despesa { get; set; }

    // Verifica de a URL esta configurada para Banco de Dados
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(ObterStringConexao());
            base.OnConfiguring(optionsBuilder);
        }
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<ApplicationUser>().ToTable("AspNetUsers").HasKey(t => t.Id);

        base.OnModelCreating(builder);
    }

    public string ObterStringConexao()
    {
        return "Data Source=LAPTOP-563RGJKO\\sqlexpress;Initial Catalog=FINANCEIRO_2023;Integrated Security=True;TrustServerCertificate=True;";
    }
}
