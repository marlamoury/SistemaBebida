
using System.Text;
using System.Text.Json;
using SistemaBebida.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata;
using RabbitMQ.Client;

namespace SistemaBebida.Infrastructure.Messaging
{
    public class PedidoClientePublisher
    {
        private readonly IConnection _connection;
        private readonly RabbitMQ.Client.IModel _channel;
        private const string QueueName = "pedidos_queue";

        public PedidoClientePublisher()
        {
            var factory = new ConnectionFactory()
            {
                HostName = "localhost",
                UserName = "guest",
                Password = "guest"
            };

            // Criando conexão e canal
            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();

            // Declarando a fila corretamente
            _channel.QueueDeclare(queue: QueueName,
                                  durable: true,
                                  exclusive: false,
                                  autoDelete: false,
                                  arguments: null);
        }

        public void PublicarPedido(PedidoCliente pedido)
        {
            var mensagem = JsonSerializer.Serialize(pedido);
            var body = Encoding.UTF8.GetBytes(mensagem);

            var properties = _channel.CreateBasicProperties();
            properties.Persistent = true; // Garante que as mensagens persistam na fila

            _channel.BasicPublish(exchange: "",
                                  routingKey: QueueName,
                                  basicProperties: properties,
                                  body: body);
        }
    }
}
