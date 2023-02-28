using RabbitMQ.Client;
using System.Text;

namespace RabbitMQ.Producer
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                channel.QueueDeclare(queue: "minha-fila",
                          durable: false,
                          exclusive: false,
                          autoDelete: false,
                          arguments: null);

                var mensagem = "Olá, RabbitMQ!";
                var body = Encoding.UTF8.GetBytes(mensagem);

                channel.BasicPublish(exchange: "",
                                     routingKey: "minha-fila",
                                     basicProperties: null,
                                     body: body);                
            }
        }
    }
}
