//using Authors.DTOS;
//using Newtonsoft.Json;
//using Newtonsoft.Json.Linq;
//using RabbitMQ.Client;
//using RabbitMQ.Client.Events;
//using System.Text;
//namespace Authors.Services
//{
//    public class Messages : BackgroundService
//    {


//        private readonly IConnection conn;
//        private readonly IModel channel;
//        private readonly EventingBasicConsumer consumer;

//        public Messages()
//        {

//            var factory = new ConnectionFactory
//            {
//                HostName = "localhost",
//                Port = 5672,
//            };

//            conn = factory.CreateConnection();
//            channel = conn.CreateModel();
//            channel.QueueDeclare("sendingBookInfo", false, false, false, null);
//            consumer = new EventingBasicConsumer(channel);
//        }

//        public override Task StartAsync(CancellationToken cancellationToken)
//        {
//            consumer.Received += async (model, content) =>
//            {
//                var body = content.Body.ToArray();
//                var json = Encoding.UTF8.GetString(body);

//                var bookInfo = JsonConvert.DeserializeObject<BooksDataTransferObjects>(json);
//                Console.WriteLine("Mensaje recibido\n Id de autor:" + bookInfo.isbn.ToString());

//                string author = System.IO.File.ReadAllText("C:\\Users\\pggis\\source\\repos\\Concurrencia\\authors.json");
//                var authors = JsonConvert.DeserializeObject<IEnumerable<AuthorsDataTransferObjects>>(author);

//                var authorInfo = authors.SingleOrDefault(x => x.id == bookInfo.authorId);
//                Console.WriteLine("Mensaje RECIBIDO: " + authorInfo.ToString());

//                await send(authorInfo, bookInfo);
//            };

//            channel.BasicConsume("sendingBookInfo", true, consumer);
//            return Task.CompletedTask;
//        }

//        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
//        {
//            while (!stoppingToken.IsCancellationRequested)
//            {
//                Console.WriteLine($"Recibiendo mensajes : {DateTimeOffset.Now}");
//                await Task.Delay(1000, stoppingToken);
//            }
//        }

//        public Task send(AuthorsDataTransferObjects author, BooksDataTransferObjects book)
//        {
//            var queueName = "sendCompleteInfo";

//            var parentObject = new JObject();
//            parentObject.Add("author", JObject.FromObject(author));
//            parentObject.Add("book", JObject.FromObject(book));
//            var json = parentObject.ToString();


//            var bodyJson = parentObject.ToString();



//            var factory = new ConnectionFactory
//            {
//                HostName = "localhost",
//                Port = 5672
//            };

//            using var conn = factory.CreateConnection();
//            using var channel = conn.CreateModel();
//            channel.QueueDeclare(queueName, false, false, false, null);
//            var body = Encoding.UTF8.GetBytes(bodyJson);
//            channel.BasicPublish(string.Empty, queueName, null, body);
//            Console.WriteLine("Mensaje enviado: " + bodyJson.ToString());

//            return Task.CompletedTask;



//        }


//    }
//}
