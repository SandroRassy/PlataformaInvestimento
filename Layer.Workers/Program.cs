using Layer.Domain.Settings;
using Layer.Repository;
using Layer.Repository.Context;
using Layer.Repository.Interfaces;
using Layer.Services;
using Layer.Services.Interfaces;
using Layer.Workers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


var configuration = new ConfigurationBuilder()
.AddJsonFile("appsettings.json")
.Build();

var queueName = configuration["UsuarioPosicaoFila:queueName"];
var durable = bool.Parse(configuration["UsuarioPosicaoFila:durable"]);
var exclusive = bool.Parse(configuration["UsuarioPosicaoFila:exclusive"]);
var autoDelete = bool.Parse(configuration["UsuarioPosicaoFila:autoDelete"]);
var uri = configuration["RabbitMq:uri"];
var xqueuemode = configuration["UsuarioPosicaoFila:x-queue-mode"];

var mongoDbSettings = configuration.GetSection("MongoDatabase").Get<MongoDBSetting>();
var connectionFactory = new ConnectionFactory(mongoDbSettings.ConnectionString);

var services = new ServiceCollection();

//services.AddSingleton(configuration);
services.AddSingleton<IUsuarioRepository>(p => new UsuarioRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNUsuarios.ToString()));
services.AddSingleton<IUsuarioPosicaoRepository>(p => new UsuarioPosicaoRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNUsuariosPosicao.ToString()));
services.AddSingleton<ITendenciaRepository>(p => new TendenciaRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNTendencias.ToString()));
services.AddSingleton<IHistoricoTransacoesRepository>(p => new HistoricoTransacoesRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNHistoricoTransacoes.ToString()));
services.AddSingleton<IConfigRabbit>(p => new ConfigRabbitUsuarioPosicao(queueName, durable, exclusive, autoDelete, uri, xqueuemode));

services.AddTransient<IUsuarioService, UsuarioServices>();
services.AddTransient<IUsuarioPosicaoService, UsuarioPosicaoServices>();
services.AddTransient<ITendenciaService, TendenciaServices>();
services.AddTransient<IHistoricoTransacoesService, HistoricoTransacoesServices>();
services.AddTransient<IFilaService, FilaService>();

services.AddTransient<ConsoleApp>();

services.BuildServiceProvider()
    .GetService<ConsoleApp>()!.Run();
