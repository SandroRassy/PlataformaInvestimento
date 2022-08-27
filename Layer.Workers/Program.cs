using Layer.Domain.Settings;
using Layer.Repository;
using Layer.Repository.Context;
using Layer.Repository.Interfaces;
using Layer.Services;
using Layer.Services.Base;
using Layer.Services.Interfaces;
using Layer.Workers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


var configuration = new ConfigurationBuilder()
.AddJsonFile("appsettings.json")
.Build();

var mongoDbSettings = configuration.GetSection("MongoDatabase").Get<MongoDBSetting>();
var connectionFactory = new ConnectionFactory(mongoDbSettings.ConnectionString);

var services = new ServiceCollection();

services.AddSingleton(configuration);
services.AddSingleton<IUsuarioRepository>(p => new UsuarioRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNUsuarios.ToString()));
services.AddSingleton<IUsuarioPosicaoRepository>(p => new UsuarioPosicaoRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNUsuariosPosicao.ToString()));
services.AddSingleton<ITendenciaRepository>(p => new TendenciaRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNTendencias.ToString()));

services.AddTransient<IUsuarioService, UsuarioServices>();
services.AddTransient<IUsuarioPosicaoService, UsuarioPosicaoServices>();
services.AddTransient<ITendenciaService, TendenciaServices>();
services.AddTransient<IFilaService, FilaService>();

services.AddTransient<ConsoleApp>();

services.BuildServiceProvider()
    .GetService<ConsoleApp>()!.Run();
