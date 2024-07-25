using LibraryAPI.Domain.Enum;

namespace LibraryAPI.Domain.Model
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
