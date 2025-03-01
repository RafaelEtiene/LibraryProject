using System;
using System.Collections.Generic;

namespace Library.Infrastructure.Models;

public partial class Bookgenre
{
    public int IdBookGenre { get; set; }

    public string NameGenre { get; set; } = null!;

    public virtual ICollection<Book> Books { get; set; } = new List<Book>();
}
