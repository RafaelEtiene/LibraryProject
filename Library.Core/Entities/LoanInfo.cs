namespace Library.Core.Entities
{
    public class LoanInfo
    {
        public int IdLoan { get; set; }
        public string NameBook { get; set; }
        public string NameClient { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime DateInitialLoan { get; set; }
        public DateTime LastStatusDate { get; set; }
        public int IdStatusLoan { get; set; }
        public decimal LateFine { get; set; }
        public string? Note { get; set; }
    }
}
