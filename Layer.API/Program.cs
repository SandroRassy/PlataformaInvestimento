using Layer.API.Models.Enum;
using Layer.Domain.Settings;
using Layer.Repository;
using Layer.Repository.Context;
using Layer.Repository.Interfaces;
using Layer.Services;
using Layer.Services.Interfaces;
using Layer.Services.Models.Shared;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mongoDbSettings = builder.Configuration.GetSection("MongoDatabase").Get<MongoDBSetting>();
var connectionFactory = new ConnectionFactory(mongoDbSettings.ConnectionString);

var queueName = builder.Configuration["UsuarioPosicaoFila:queueName"];
var durable = bool.Parse(builder.Configuration["UsuarioPosicaoFila:durable"]);
var exclusive = bool.Parse(builder.Configuration["UsuarioPosicaoFila:exclusive"]);
var autoDelete = bool.Parse(builder.Configuration["UsuarioPosicaoFila:autoDelete"]);
var uri = builder.Configuration["RabbitMq:uri"];
var xqueuemode = builder.Configuration["UsuarioPosicaoFila:x-queue-mode"];

builder.Services.AddSingleton<IUsuarioRepository>(p => new UsuarioRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNUsuarios.ToString()));
builder.Services.AddSingleton<IUsuarioPosicaoRepository>(p => new UsuarioPosicaoRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNUsuariosPosicao.ToString()));
builder.Services.AddSingleton<ITendenciaRepository>(p => new TendenciaRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNTendencias.ToString()));
builder.Services.AddSingleton<IHistoricoTransacoesRepository>(p => new HistoricoTransacoesRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNHistoricoTransacoes.ToString()));
builder.Services.AddSingleton<IConfigRabbit>(p => new ConfigRabbitUsuarioPosicao(queueName, durable, exclusive, autoDelete, uri, xqueuemode));

builder.Services.AddTransient<IUsuarioService, UsuarioServices>();
builder.Services.AddTransient<IUsuarioPosicaoService, UsuarioPosicaoServices>();
builder.Services.AddTransient<ITendenciaService, TendenciaServices>();
builder.Services.AddTransient<IHistoricoTransacoesService, HistoricoTransacoesServices>();
builder.Services.AddTransient<IFilaService, FilaService>();

#region [Cors]
builder.Services.AddCors();
#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

#region [Cors]
app.UseCors(c =>
{
    c.AllowAnyHeader();
    c.AllowAnyMethod();
    c.AllowAnyOrigin();

});
#endregion

app.UseAuthorization();

app.MapControllers();

app.Run();
