using System;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace SistemaBebida.Application.Clientes
{
    public class FornecedorApiClient
    {
        private readonly HttpClient _httpClient;

        public FornecedorApiClient(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> CriarPedidoAsync(object pedido)
        {
            var jsonContent = JsonSerializer.Serialize(pedido);
            var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

            // Simulação: Em vez de fazer a requisição real, retornamos um JSON fixo de sucesso.
            await Task.Delay(500); // Simula um pequeno tempo de espera

            return JsonSerializer.Serialize(new
            {
                mensagem = "Pedido criado com sucesso!",
                pedidoId = new Random().Next(1000, 9999) // Gera um ID aleatório para o pedido
            });
        }
    }
}
