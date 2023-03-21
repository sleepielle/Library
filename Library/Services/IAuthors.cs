using Books.DTOS;

namespace Books.Services
{
    public interface IAuthors
    {
        public Task<AuthorsDataTransferObjects> getAuthorsInformation(int id);

    }
}
