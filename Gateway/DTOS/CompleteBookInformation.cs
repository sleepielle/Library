namespace Gateway.DTOS
{
    public class CompleteBookInformation
    {
        public BooksDataTransferObjects book { get; set; }
        public AuthorsDataTransferObjects author { get; set; }
    }

    public class BooksDataTransferObjects
    {
        public string isbn { get; set; }
        public string title { get; set; }
        public string subtitle { get; set; }
        public DateTime published { get; set; }
        public string publisher { get; set; }
        public int pages { get; set; }
        public string description { get; set; }
        public string website { get; set; }
        public int authorId { get; set; }
    }

    public class AuthorsDataTransferObjects
    {
        public int id { get; set; }
        public string first_name { get; set; }
        public string last_name { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
    }

}
