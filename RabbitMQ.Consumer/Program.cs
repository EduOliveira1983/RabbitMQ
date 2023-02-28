using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQ.Consumer
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

                Console.WriteLine("Aguardando mensagens. Pressione Enter para sair.");

                while (!Console.KeyAvailable)
                {
                    var mensagem = channel.BasicGet(queue: "minha-fila", autoAck: true);
                    if (mensagem != null)
                    {
                        var mensagemTexto = Encoding.UTF8.GetString(mensagem.Body.ToArray());
                        Console.WriteLine("Mensagem recebida: {0}", mensagemTexto);
                    }
                }
            }
        }
    }
}
