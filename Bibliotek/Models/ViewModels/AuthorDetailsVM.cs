using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bibliotek.Models.ViewModels
{
    public class AuthorDetailsVM
    {

        public Author Author { get; set; }
        public List<Book> Books { get; set; }
    }
}
