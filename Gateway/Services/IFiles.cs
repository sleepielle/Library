using Gateway.DTOS;
namespace Gateway.Services
{
    public interface IFiles
    {
        public void writeFile(CompleteBookInformation info, string filepath);
        public Task<string> createFile(string isbn);
        public string GetFilePath();
    }
}
