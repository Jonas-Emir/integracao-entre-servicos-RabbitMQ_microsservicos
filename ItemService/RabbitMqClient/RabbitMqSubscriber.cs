using ItemService.EventProcessor;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

namespace ItemService.RabbitMqClient
{
    public class RabbitMqSubscriber : BackgroundService
    {
        private readonly IConfiguration _configuration;
        private readonly string _nomeDaFila;
        private readonly IConnection _connection;
        private IModel _channel;
        private IProcessaEvento _processaEvento;

        public RabbitMqSubscriber(IConfiguration configuration, IProcessaEvento processaEvento)
        {
            try
            {
                _configuration = configuration;
                _connection = new ConnectionFactory() { HostName = _configuration["RabbitMQHost"], Port = Convert.ToInt32(_configuration["RabbitMQPort"]) }.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.ExchangeDeclare(exchange: "trigger", type: ExchangeType.Fanout);
                _nomeDaFila = _channel.QueueDeclare().QueueName;
                _channel.QueueBind(queue: _nomeDaFila, exchange: "trigger", routingKey: "");
                _processaEvento = processaEvento;

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error connecting to RabbitMQ. HostName: {_configuration["RabbitMQHost"]}, Port: {_configuration["RabbitMQPort"]}");
                Console.WriteLine($"Exception message: {ex.Message}");
            }
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            EventingBasicConsumer? consumidor = new EventingBasicConsumer(_channel);

            consumidor.Received += (ModuleHandle, ea) =>
            {
                ReadOnlyMemory<byte> body = ea.Body;
                string? mensagem = Encoding.UTF8.GetString(body.ToArray());
                _processaEvento.Processa(mensagem);
            };

            _channel.BasicConsume(queue: _nomeDaFila, autoAck: true, consumer: consumidor);

            return Task.CompletedTask;

        }
    }
}