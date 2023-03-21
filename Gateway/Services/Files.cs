using Gateway.DTOS;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace Gateway.Services
{
    public class Files : IFiles
    {


        public string path;
        public async Task<string> createFile(string isbn)
        {
            Guid result;


            using (MD5 md5 = MD5.Create())
            {
                byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(isbn));
                result = new Guid(hash);
            }

            path = $"C:\\Users\\pggis\\source\\repos\\Concurrencia\\{result}.txt";

            try
            {
                FileStream fs = File.Create(path);
                fs.Close();
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return await Task.FromResult(path);
        }

        public void writeFile(CompleteBookInformation info, string filepath)
        {
            // Serialize the object to JSON
            string json = JsonSerializer.Serialize(info);

            // Write the JSON to the file
            using (StreamWriter writer = new StreamWriter(filepath))
            {
                writer.Write(json);
            }
        }

        public string GetFilePath()
        {
            return path;
        }
    }

}
