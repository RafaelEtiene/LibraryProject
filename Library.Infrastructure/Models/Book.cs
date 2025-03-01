using System;
using System.Collections.Generic;

namespace Library.Infrastructure.Models;

public partial class Book
{
    public int IdBook { get; set; }

    public string NameBook { get; set; } = null!;

    public string Author { get; set; } = null!;

    public DateOnly PublicationDate { get; set; }

    public int IdBookGenre { get; set; }

    public virtual Bookgenre IdBookGenreNavigation { get; set; } = null!;

    public virtual ICollection<Loan> Loans { get; set; } = new List<Loan>();
}
