using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace DlqFunction
{
    public class ConsomeDLQ
    {
        private readonly ILogger _logger;
        private readonly IConnection _connection;
        private readonly IModel _channel;

        public ConsomeDLQ(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<ConsomeDLQ>();

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
                UserName = "guest",
                Password = "guest",
                VirtualHost = "/"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            _logger.LogInformation($" Conectado ao RabbitMQ");

        }

        [Function("ConsomeDLQ")]
        public void Run([TimerTrigger("0 * * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation("Verificando mensagens nas DLQ's...");

            var dlqs = new List<string>()
            { "filaCadastroContato_error",
              "filaCadastroRegiao_error",
              "filaAtualizacaoContato_error",
              "filaAtualizacaoRegiao_error",
              "filaExclusaoContato_error",
              "filaExclusaoRegiao_error"
              };

            foreach (var dlq in dlqs)
            {
                if (!QueueExists(dlq))
                {
                    _logger.LogWarning($"[DLQ] Fila {dlq} não encontrada. Pulando...");
                    continue;
                }

                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body.ToArray();
                    var message = Encoding.UTF8.GetString(body);

                    _logger.LogInformation($"Mensagem recebida: {message}");

                    try
                    {
                        _logger.LogInformation("Processando mensagem...");

                        _channel.BasicAck(ea.DeliveryTag, false);

                        _logger.LogInformation("Mensagem processada e removida da fila.");
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError($"Erro ao processar mensagem: {ex.Message}");
                        _channel.BasicReject(ea.DeliveryTag, true);
                    }
                };

                _channel.BasicConsume(queue: dlq, autoAck: false, consumer: consumer);

            }

        }

        private static bool QueueExists(string queueName)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"http://localhost:15672/api/queues/%2F/{queueName}");
            request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.ASCII.GetBytes("guest:guest")));

            try
            {
                var response = new HttpClient().Send(request);
                return response.IsSuccessStatusCode;
            }
            catch
            {
                return false;
            }
        }

    }
}
