using Layer.Domain.Entities;
using Layer.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace Layer.Workers
{
    public class ConsoleApp
    {
        private readonly IUsuarioPosicaoService _usuarioPosicaoService;
        private readonly IHistoricoTransacoesService _historicoTransacoesService;
        private readonly IConfigRabbit _configRabbit;
        private readonly IFilaService _filaService;
        public ConsoleApp(IUsuarioPosicaoService usuarioPosicaoService, IConfigRabbit configrabbit, IFilaService filaService, IHistoricoTransacoesService historicoTransacoesService)
        {
            _usuarioPosicaoService = usuarioPosicaoService;
            _configRabbit = configrabbit;
            _filaService = filaService;
            _historicoTransacoesService = historicoTransacoesService;
        }

        public void Run()
        {
            var queueName = _configRabbit.QueueName;

            IniciarFila(queueName);

            using var channel = _filaService.Canal();

            var consumer = new EventingBasicConsumer(channel);
            consumer.Received += (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                try
                {
                    _usuarioPosicaoService.Processar(message);
                }
                catch (Exception ex)
                {
                    var historicotransacoes = new HistoricoTransacoes();

                    historicotransacoes.DataTransacao = DateTime.Now;
                    historicotransacoes.Payload = message;
                    historicotransacoes.Obs = $"Erro ao processar o payload. erro: {ex.Message}";
                    historicotransacoes.Status = 3;

                    _historicoTransacoesService.Inserir(historicotransacoes);
                }


                Console.WriteLine($"Message received: {message}");
            };

            channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

            Console.ReadKey();

        }

        private void IniciarFila(string queuename)
        {
            var durable = _configRabbit.Durable;
            var exclusive = _configRabbit.Exclusive;
            var autoDelete = _configRabbit.AutoDelete;
            var uri = _configRabbit.Uri;

            Dictionary<string, object> args = new Dictionary<string, object>()
            {
                { "x-queue-mode", _configRabbit.XQueueMode }
            };

            _filaService.ConfigFila(uri, queuename, durable, exclusive, autoDelete, args);
        }
    }
}
