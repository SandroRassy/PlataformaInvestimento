using Layer.API.Models.Enum;
using Layer.Domain.Settings;
using Layer.Repository;
using Layer.Repository.Context;
using Layer.Repository.Interfaces;
using Layer.Services;
using Layer.Services.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mongoDbSettings = builder.Configuration.GetSection("MongoDatabase").Get<MongoDBSetting>();
var connectionFactory = new ConnectionFactory(mongoDbSettings.ConnectionString);

builder.Services.AddSingleton<IUsuarioRepository>(p => new UsuarioRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNUsuarios.ToString()));
builder.Services.AddSingleton<IUsuarioPosicaoRepository>(p => new UsuarioPosicaoRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNUsuariosPosicao.ToString()));
builder.Services.AddSingleton<ITendenciaRepository>(p => new TendenciaRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNTendencias.ToString()));

builder.Services.AddTransient<IUsuarioService, UsuarioServices>();
builder.Services.AddTransient<IUsuarioPosicaoService, UsuarioPosicaoServices>();
builder.Services.AddTransient<ITendenciaService, TendenciaServices>();
builder.Services.AddTransient<IFilaService, FilaService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
