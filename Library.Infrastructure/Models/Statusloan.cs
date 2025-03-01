using System;
using System.Collections.Generic;

namespace Library.Infrastructure.Models;

public partial class Statusloan
{
    public int IdStatusLoan { get; set; }

    public string Status { get; set; } = null!;

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
