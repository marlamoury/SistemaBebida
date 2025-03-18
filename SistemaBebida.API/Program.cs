using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;
using RabbitMQ.Client;
using SistemaBebida.Infrastructure.Persistence;
using SistemaBebida.Domain.Repositories;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Net.Http;
using SistemaBebida.Application.Clientes;
using SistemaBebida.Application.Services;
using SistemaBebida.Infrastructure.Messaging;

var builder = WebApplication.CreateBuilder(args);

// Configuração do banco de dados
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
    new MySqlServerVersion(new Version(8, 0, 32))));

// Adicionando repositórios e serviços
builder.Services.AddScoped<IRevendaRepository, RevendaRepository>();
builder.Services.AddScoped<IPedidoClienteRepository, PedidoClienteRepository>();
builder.Services.AddScoped<PedidoClienteService>();
builder.Services.AddSingleton<PedidoClientePublisher>();

// Configuração da integração com a API do fornecedor com resiliência usando Polly
builder.Services.AddHttpClient<FornecedorApiClient>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["FornecedorApi:BaseUrl"]);
})
.AddPolicyHandler(GetRetryPolicy()); // 🔹 Certifique-se que Polly está instalado

// Adicionando serviços necessários
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do RabbitMQ
builder.Services.AddSingleton<IConnectionFactory>(sp => new ConnectionFactory
{
    HostName = "localhost",
    UserName = "guest",
    Password = "guest"
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

// Definição da política de retry com Polly
static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
{
    return HttpPolicyExtensions
        .HandleTransientHttpError()
        .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.InternalServerError)
        .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));
}