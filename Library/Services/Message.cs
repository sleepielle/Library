using Books.DTOS;
using Newtonsoft.Json.Linq;
using RabbitMQ.Client;
using System.Text;

namespace Books.Services
{
    public class sendMessage
    {

        public Task send(AuthorsDataTransferObjects author, BooksDataTransferObjects book)
        {
            var queueName = "information";

            var parentObject = new JObject();
            parentObject.Add("author", JObject.FromObject(author));
            parentObject.Add("book", JObject.FromObject(book));
            var json = parentObject.ToString();

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672
            };

            using var conn = factory.CreateConnection();
            using var channel = conn.CreateModel();
            channel.QueueDeclare(queueName, false, false, false, null);
            var body = Encoding.UTF8.GetBytes(json);
            channel.BasicPublish(string.Empty, queueName, null, body);
            Console.WriteLine("Mensaje : enviado.");
            return Task.CompletedTask;



        }

    }
}
