using RabbitMQ.Client.Events;
using RabbitMQ.Client;
using System.Text;
using System.Text.Json;

namespace SistemaBebida.Infrastructure.Messaging
{
    //public class PedidoClienteConsumer
 
    //{
    //    private readonly IConnection _connection;
    //    private readonly IModel _channel;
    //    private readonly HttpClient _httpClient;

    //    public PedidoClienteConsumer(HttpClient httpClient)
    //    {
    //        _httpClient = httpClient;

    //        var factory = new ConnectionFactory()
    //        {
    //            HostName = "localhost",
    //            UserName = "guest",
    //            Password = "guest"
    //        };

    //        _connection = factory.CreateConnection();
    //        _channel = _connection.CreateModel();

    //        // Declara a fila para garantir que ela exista
    //        _channel.QueueDeclare(queue: "pedidos_cliente",
    //                             durable: false,
    //                             exclusive: false,
    //                             autoDelete: false,
    //                             arguments: null);
    //    }

    //    public void StartConsuming()
    //    {
    //        var consumer = new EventingBasicConsumer(_channel);
    //        consumer.Received += async (model, eventArgs) =>
    //        {
    //            var body = eventArgs.Body.ToArray();
    //            var mensagem = Encoding.UTF8.GetString(body);

    //            Console.WriteLine($"Pedido recebido: {mensagem}");

    //            // Desserializa o pedido
    //            var pedido = JsonSerializer.Deserialize<PedidoClienteDTO>(mensagem);

    //            // 🔥 Chama a API do fornecedor para processar o pedido
    //            await EnviarPedidoParaFornecedor(pedido);

    //            _channel.BasicAck(eventArgs.DeliveryTag, false);
    //        };

    //        _channel.BasicConsume(queue: "pedidos_cliente",
    //                              autoAck: false,
    //                              consumer: consumer);

    //        Console.WriteLine("📡 Consumidor de pedidos iniciado...");
    //    }

    //    private async Task EnviarPedidoParaFornecedor(PedidoClienteDTO pedido)
    //    {
    //        var fornecedorApiUrl = "https://fornecedor.com/api/pedidos"; // 🔧 URL do fornecedor

    //        var jsonPedido = JsonSerializer.Serialize(pedido);
    //        var content = new StringContent(jsonPedido, Encoding.UTF8, "application/json");

    //        try
    //        {
    //            var response = await _httpClient.PostAsync(fornecedorApiUrl, content);

    //            if (response.IsSuccessStatusCode)
    //            {
    //                Console.WriteLine($"Pedido enviado ao fornecedor com sucesso!");
    //            }
    //            else
    //            {
    //                Console.WriteLine($"Erro ao enviar pedido ao fornecedor: {response.StatusCode}");
    //            }
    //        }
    //        catch (Exception ex)
    //        {
    //            Console.WriteLine($"Falha ao conectar na API do fornecedor: {ex.Message}");
    //        }
    //    }
    //}
}
