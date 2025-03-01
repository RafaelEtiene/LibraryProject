using System;
using System.Collections.Generic;

namespace Library.Infrastructure.Models;

public partial class User
{
    public uint IdUser { get; set; }

    public string User1 { get; set; } = null!;

    public string Password { get; set; } = null!;
}
