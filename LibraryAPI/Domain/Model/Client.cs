namespace LibraryAPI.Domain.Model
{
    public class Client
    {
        public int IdClient { get; set; }
        public required string Name { get; set; }
        public int Age { get; set; }
        public required string Gender { get; set; }
        public required string Email { get; set; }
        public required string PhoneNumber { get; set; }
        public required string Address { get; set; }
    }
}
