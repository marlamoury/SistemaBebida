# SistemaBebida - API BackEnd

Esta API permite gerenciar **revendas, clientes e pedidos**, além de integrar com um **fornecedor** para envio de pedidos.

## 📚 Índice
1. 📦 Tecnologias Utilizadas
2. ⚙️ Configuração do Projeto
3. 🚀 Como Rodar o Projeto
4. 📌 Estrutura do Projeto
5. 📰 API - Endpoints
6. 🐰 Integração com RabbitMQ


---

## 📦 Tecnologias Utilizadas
- **.NET 8.0** - Backend principal
- **Entity Framework Core** - ORM para MySQL
- **RabbitMQ** - Sistema de mensageria
- **Polly** - Resiliência para chamadas externas
- **Swagger** - Documentação da API
- **xUnit** - Testes automatizados

---

## ⚙️ Configuração do Projeto

Antes de rodar o projeto, configure os seguintes serviços:

### **Banco de Dados**
O banco utilizado é o **MySQL**, e a string de conexão está no `appsettings.json`:

```json
"ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=SistemaBebida;User=root;Password=root;"
}
```

### **RabbitMQ**
O projeto usa o **RabbitMQ** para processar pedidos de forma assíncrona. Se ainda não tiver instalado, use Docker:

```sh
docker run -d --hostname my-rabbit --name rabbitmq -p 5672:5672 -p 15672:15672 rabbitmq:3-management
```

Acesse a interface do RabbitMQ em:  
🔗 **http://localhost:15672** (Usuário: `guest`, Senha: `guest`)

---

## 🚀 Como Rodar o Projeto

### **Passo 1: Configurar o banco**
Execute as migrações para criar as tabelas:

```sh
dotnet ef database update
```

### **Passo 2: Rodar o projeto**
Execute a API:

```sh
dotnet run --project SistemaBebida.API
```

Acesse o Swagger em:  
🔗 **http://localhost:7104/swagger**

---

## 📌 Estrutura do Projeto

```
📂 SistemaBebida
 ├── 📁 SistemaBebida.API                 # Projeto principal (API)
 │    ├── 📁 Controllers                   # Controllers (Pontos de entrada da API)
 │    ├── 📁 Consumers                     # Consumidores do RabbitMQ
 │    ├── 📁 DTOs                          # Objetos de Transferência de Dados
 │    ├── 📁 appsettings.json              # Configurações globais do projeto
 │    ├── 📄 Program.cs                    # Configuração inicial do projeto
 │
 ├── 📁 SistemaBebida.Application         # Camada de aplicação (Regras de negócio)
 │    ├── 📁 Services                      # Serviços principais
 │
 ├── 📁 SistemaBebida.Domain              # Camada de domínio (Entidades e Repositórios)
 │    ├── 📁 Entities                      # Modelos de dados
 │    ├── 📁 Repositories                  # Interfaces dos repositórios
 │
 ├── 📁 SistemaBebida.Infrastructure      # Infraestrutura (Banco, Mensageria)
 │    ├── 📁 Persistence                   # Configuração do banco (EF Core)
 │    ├── 📁 Messaging                     # Configuração do RabbitMQ
 │
 ├── 📁 SistemaBebida.Tests               # Testes automatizados
      ├── 📁 Services                      # Testes dos serviços
      ├── 📁 Controllers                   # Testes dos controllers
```

---

## 🐰 Integração com RabbitMQ

O projeto utiliza filas para processar pedidos assíncronos. A API **publica mensagens** na fila `pedidos_cliente`, e o **consumer processa os pedidos**.

### **📄 Publicação**
O publisher (`PedidoClientePublisher.cs`) envia pedidos para a fila:

```csharp
public void PublicarPedido(PedidoCliente pedido)
{
    var message = JsonSerializer.Serialize(pedido);
    var body = Encoding.UTF8.GetBytes(message);
    
    _channel.BasicPublish(
        exchange: "",
        routingKey: "pedidos_cliente",
        basicProperties: null,
        body: body
    );
}
```

### **📄 Consumo**
O consumer (`PedidoClienteConsumer.cs`) escuta a fila e processa os pedidos:

```csharp
public void StartConsuming()
{
    var consumer = new EventingBasicConsumer(_channel);
    consumer.Received += async (model, eventArgs) =>
    {
        var body = eventArgs.Body.ToArray();
        var message = Encoding.UTF8.GetString(body);
        var pedido = JsonSerializer.Deserialize<PedidoCliente>(message);

        // Processa o pedido e envia para o fornecedor
        await _fornecedorApiClient.CriarPedidoAsync(pedido);

        _channel.BasicAck(eventArgs.DeliveryTag, false);
    };

    _channel.BasicConsume(queue: "pedidos_cliente", autoAck: false, consumer: consumer);
}
```



