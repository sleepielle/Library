﻿using Books.DTOS;
using Newtonsoft.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
namespace Books.Services
{
    public class Messages : BackgroundService
    {

        private readonly IConnection conn;
        private readonly IModel channel;
        private readonly EventingBasicConsumer consumer;

        public Messages()
        {

            var factory = new ConnectionFactory
            {
                HostName = "localhost",
                Port = 5672,
            };

            conn = factory.CreateConnection();
            channel = conn.CreateModel();
            channel.QueueDeclare("libro", false, false, false, null);
            consumer = new EventingBasicConsumer(channel);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            consumer.Received += async (model, content) =>
            {
                var body = content.Body.ToArray();
                var json = Encoding.UTF8.GetString(body);

                var message = JsonConvert.DeserializeObject<BooksIsbnDataTransferObject>(json);

                string book = System.IO.File.ReadAllText("C:\\Users\\pggis\\source\\repos\\Concurrencia\\books.json");
                var bookInfo = JsonConvert.DeserializeObject<IEnumerable<BooksDataTransferObjects>>(book);

                var bookInformation = bookInfo.SingleOrDefault(x => x.isbn == message.isbn);

                await send(bookInformation);
            };

            channel.BasicConsume("libro", true, consumer);
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
        public Task send(BooksDataTransferObjects book)
        {
            var queueName = "sendingBookInfo";

            //var parentObject = new JObject();
            //parentObject.Add("author", JObject.FromObject(author));
            //parentObject.Add("book", JObject.FromObject(book));
            //var json = parentObject.ToString();


            var json = JsonConvert.SerializeObject(book);


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
            Console.WriteLine("Mensaje enviado: " + json.ToString());

            return Task.CompletedTask;



        }

    }
}
