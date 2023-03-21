using Books.Services;

using Microsoft.AspNetCore.Mvc;

namespace Books.Controllers
{
    [ApiController]
    [Route("books")]
    public class BooksController : ControllerBase
    {

        private readonly IAuthors authors;
        private readonly Messages send;
        public BooksController(IAuthors _authors, Messages _send)
        {
            authors = _authors;
            this.send = _send;
        }

        [HttpGet]
        public async Task<string> Get()
        {
            string hola = "hola";
            return await Task.FromResult(hola);
        }

        //[HttpGet("{authorId}")]
        //public async Task<IActionResult> GetAuthorInformation(int authorId)
        //{
        //    var text = await this.authors.getAuthorsInformation(authorId);

        //    //GetBookInformation(authorId);

        //    string book = System.IO.File.ReadAllText("C:\\Users\\pggis\\source\\repos\\Concurrencia\\books.json");
        //    var json = JsonConvert.DeserializeObject<IEnumerable<BooksDataTransferObjects>>(book);

        //    var bookInformation = json.SingleOrDefault(x => x.authorId == authorId);

        //    await send.send(text, bookInformation);
        //    return Ok(new { BooksDataTransferObjects = bookInformation, AuthorsDataTransferObjects = text });
        //    //mandando ambos elementos de respuesta.
        //}



    }
}

