using Layer.API.Models.Enum;
using Layer.Domain.Settings;
using Layer.Repository;
using Layer.Repository.Context;
using Layer.Services;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Layer.UnitTests.System.Base
{
    public class TestBase
    {
        public static IConfiguration Configuration { get; }
        public readonly MongoDBSetting mongoDbSettings;
        public readonly ConnectionFactory connectionFactory;
        public readonly UsuarioRepository _usuarioRepository;
        public readonly UsuarioPosicaoRepository _usuarioPosicaoRepository;
        public readonly TendenciaRepository _tendenciaRepository;
        public readonly UsuarioServices _usuarioServices;        
        public readonly TendenciaServices _tendenciaServices;
               

        static TestBase()
        {
            Configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile($"appsettings.json")
            .AddEnvironmentVariables().Build();
        }

        public TestBase()
        {
            mongoDbSettings = Configuration.GetSection("MongoDatabase").Get<MongoDBSetting>();
            connectionFactory = new ConnectionFactory(mongoDbSettings.ConnectionString);

            _usuarioRepository = new UsuarioRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNUsuarios.ToString());
            _usuarioPosicaoRepository = new UsuarioPosicaoRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNUsuariosPosicao.ToString());
            _tendenciaRepository = new TendenciaRepository(connectionFactory, mongoDbSettings.DatabaseName, MongoDBCollections.CNTendencias.ToString());
            
            _usuarioServices = new UsuarioServices(_usuarioRepository, _usuarioPosicaoRepository);            
            _tendenciaServices = new TendenciaServices(_tendenciaRepository);
        }
    }
}
