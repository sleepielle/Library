using Authors.DTOS;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;


namespace Authors.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AuthorsControllers : ControllerBase
    {

        [HttpGet("{authorId}")]
        public IActionResult GetAuthorsInformation(int authorId)
        {

            string text = System.IO.File.ReadAllText("C:\\Users\\pggis\\source\\repos\\Concurrencia\\authors.json");
            var json = JsonConvert.DeserializeObject<IEnumerable<AuthorsDataTransferObjects>>(text);

            var authorInformation = json.SingleOrDefault(x => x.id == authorId);
            Console.WriteLine(authorInformation.ToString());
            return Ok(authorInformation);
        }
    }
}
