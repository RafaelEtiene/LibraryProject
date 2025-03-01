using Library.Core.Enum;

namespace Library.Core.Entities
{
    public class BookInfo
    {
        public int IdBook { get; set; }
        public required string NameBook { get; set; }
        public required string Author { get; set; }
        public DateTime PublicationDate { get; set; }
        public BookGenre Genre { get; set; }
    }
}
