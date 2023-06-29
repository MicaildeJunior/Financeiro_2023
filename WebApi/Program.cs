using Domain.Interfaces.Generics;
using Domain.Interfaces.ICategoria;
using Domain.Interfaces.IDespesa;
using Domain.Interfaces.InterfaceServicos;
using Domain.Interfaces.ISistemaFinanceiro;
using Domain.Interfaces.IUsuarioSistemaFinanceiro;
using Domain.Servicos;
using Entities.Entidades;
using Infra.Configuracao;
using Infra.Repositorio;
using Infra.Repositorio.Generics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WebApi.Token;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// Adicionado para tentar resolver o erro de Adicionar Categoria na API
builder.Services.AddLogging();

// CONFIGURA��O DO BANCO DE DADOS 
builder.Services.AddDbContext<ContextBase>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ContextBase>();


// INTERFACE E REPOSITORIO
builder.Services.AddSingleton(typeof(InterfaceGeneric<>), typeof(RepositoryGenerics<>));
builder.Services.AddSingleton<InterfaceCategoria, RepositorioCategoria>();
builder.Services.AddSingleton<InterfaceDespesa, RepositorioDespesa>();
builder.Services.AddSingleton<InterfaceSistemaFinanceiro, RepositorioSistemaFinanceiro>();
builder.Services.AddSingleton<InterfaceUsuarioSistemaFinanceiro, RepositorioUsuarioSistemaFinanceiro>();


// SERVICO E DOMINIO
builder.Services.AddSingleton<ICategoriaServico, CategoriaServico>();
builder.Services.AddSingleton<IDespesaServico, DespesaServico>();
builder.Services.AddSingleton<ISistemaFinanceiroServico, SistemaFinanceiroServico>();
builder.Services.AddSingleton<IUsuarioSistemaFinanceiroServico, UsuarioSistemaFinanceiroServico>();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(option =>
                {
                    option.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,

                        ValidIssuer = "Teste.Securiry.Bearer",
                        ValidAudience = "Teste.Securiry.Bearer",
                        IssuerSigningKey = JwtSecurityKey.Create("Secret_Key-12345678")
                    };

                    option.Events = new JwtBearerEvents
                    {
                        OnAuthenticationFailed = context =>
                        {
                            Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
                            return Task.CompletedTask;
                        },
                        OnTokenValidated = context =>
                        {
                            Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
                            return Task.CompletedTask;
                        }
                    };
                });


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // Adicionado para tentar resolver o problema do metodo POST adicionar Categoria
    app.UseHttpLogging();
}

app.UseHttpsRedirection();

// New
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();