using System;
using System.Collections.Generic;

namespace Library.Infrastructure.Models;

public partial class Client
{
    public int IdClient { get; set; }

    public string Name { get; set; } = null!;

    public int Age { get; set; }

    public string Gender { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public string Address { get; set; } = null!;

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
