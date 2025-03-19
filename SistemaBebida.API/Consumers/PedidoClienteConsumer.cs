using Microsoft.Extensions.Hosting;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using SistemaBebida.Application.DTOs;
using SistemaBebida.Application.Clientes;

namespace SistemaBebida.API.Consumers
{
    public class PedidoClienteConsumer : BackgroundService
    {
        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly FornecedorApiClient _fornecedorApiClient;

        public PedidoClienteConsumer(FornecedorApiClient fornecedorApiClient)
        {
            _fornecedorApiClient = fornecedorApiClient;

            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            // Declara a fila para garantir que ela exista
            _channel.QueueDeclare(queue: "pedidos_queue",
                                 durable: true,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += async (model, eventArgs) =>
            {
                var body = eventArgs.Body.ToArray();
                var mensagem = Encoding.UTF8.GetString(body);

                Console.WriteLine($"Pedido recebido da fila: {mensagem}");

                var pedido = JsonSerializer.Deserialize<PedidoClienteDTO>(mensagem);

                if (pedido != null)
                {
                    await _fornecedorApiClient.CriarPedidoAsync(pedido);
                }

                _channel.BasicAck(eventArgs.DeliveryTag, false);
            };

            _channel.BasicConsume(queue: "pedidos_queue", autoAck: false, consumer: consumer);
            Console.WriteLine("Consumidor de pedidos iniciado...");

            return Task.CompletedTask;
        }

        public override void Dispose()
        {
            _channel.Close();
            _connection.Close();
            base.Dispose();
        }
    }
}
