using System;
using System.Collections.Generic;
using System.Linq;
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

            var response = await _httpClient.PostAsync("/api/pedidos", content);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Erro ao criar pedido na API do fornecedor: {response.StatusCode}");
            }

            return await response.Content.ReadAsStringAsync();
        }
    }
}
