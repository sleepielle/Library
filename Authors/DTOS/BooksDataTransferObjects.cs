namespace Authors.DTOS
{
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
}
