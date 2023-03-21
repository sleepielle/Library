using Gateway.DTOS;
using Gateway.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System.Text;
namespace Gateway.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GatewayController : ControllerBase
    {

        private readonly IFiles receive;

        public GatewayController(IFiles _receive)
        {
            receive = _receive;
        }

        [HttpGet("{isbn}")]
        public async Task<IActionResult> GetBooks(string isbn)
        {

            var response = await receive.createFile(isbn);

            var queueName = "libro";
            var bookIsbn = new BookIsbnDataTransferObject
            {
                isbn = isbn
            };


            var json = JsonConvert.SerializeObject(bookIsbn);

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
            Console.WriteLine("Mensaje : " + bookIsbn.isbn + " enviado.");


            return Ok(response);

        }
    }
}
