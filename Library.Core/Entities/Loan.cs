namespace Library.Core.Entities
{
    public class Loan
    {
        public int IdLoan { get; set; }
        public int IdBook { get; set; }
        public int IdClient { get; set; }
        public DateTime DateInitialLoan { get; set; }
        public int IdStatusLoan { get; set; }
        public decimal LateFine { get; set; }
        public string? Note { get; set; }
    }
}
