using System;
using System.Collections.Generic;

namespace Library.Infrastructure.Models;

public partial class Loan
{
    public int IdLoan { get; set; }

    public int IdBook { get; set; }

    public int IdClient { get; set; }

    public DateOnly DateInitialLoan { get; set; }

    public int IdStatusLoan { get; set; }

    public decimal? LateFine { get; set; }

    public string? Note { get; set; }

    public DateOnly? LastStatusDate { get; set; }

    public virtual Book IdBookNavigation { get; set; } = null!;

    public virtual Client IdClientNavigation { get; set; } = null!;

    public virtual Statusloan IdStatusLoanNavigation { get; set; } = null!;
}
