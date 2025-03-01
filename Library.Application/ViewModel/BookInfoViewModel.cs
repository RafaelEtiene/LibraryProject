using Library.Core.Entities;
using Library.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Application.ViewModel
{
    public class BookInfoViewModel
    {
        public required string NameBook { get; set; }
        public required string Author { get; set; }
        public DateOnly PublicationDate { get; set; }
        public BookGenre Genre { get; set; }
    }
}
