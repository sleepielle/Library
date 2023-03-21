using Gateway.DTOS;
using Gateway.Services;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
namespace Gateway.Controllers
{
    public class receiveMessages : BackgroundService
    {

        private readonly IFiles files;
        private readonly IConnection conn;
        private readonly IModel channel;
        private readonly EventingBasicConsumer consumer;

        public receiveMessages(IFiles file)
        {
            files = file;
            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
            };

            conn = factory.CreateConnection();
            channel = conn.CreateModel();
            channel.QueueDeclare("sendCompleteInfo", false, false, false, null);
            consumer = new EventingBasicConsumer(channel);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {

            consumer.Received += async (model, content) =>
            {
                var body = content.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                var message = JsonConvert.DeserializeObject<CompleteBookInformation>(json);
                Console.WriteLine("Mensaje recibido\n First name" + message.author.first_name
                    + "Title : " + message.book.title);

                // Get the file path from the Files class
                //  await Task.Delay(2000);
                string filepath = files.GetFilePath();

                // Write the book information to the file
                files.writeFile(message, filepath);
            };

            channel.BasicConsume("sendCompleteInfo", true, consumer);
            return Task.CompletedTask;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                Console.WriteLine($"Recibiendo mensajes : {DateTimeOffset.Now}");
                await Task.Delay(1000, stoppingToken);
            }
        }
    }
}
