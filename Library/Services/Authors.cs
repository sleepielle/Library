using Books.DTOS;
using Newtonsoft.Json;

namespace Books.Services
{
    public class Authors : IAuthors
    {
        //no se usa el private readonly para iauthors!!!!!!
        private readonly HttpClient client;
        public Authors(HttpClient _client)
        {
            client = _client;
        }

        public async Task<AuthorsDataTransferObjects> getAuthorsInformation(int id)
        {

            var baseUrl = $"https://localhost:7276/authorscontrollers";
            var json = await client.GetStringAsync($"{baseUrl}/{id}");


            return JsonConvert.DeserializeObject<AuthorsDataTransferObjects>(json);

        }
    }
}
