using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bibliotek.Models.ViewModels
{
    public class BookIndexVM
    {
        public IEnumerable<Book> Books { get; set; }
        public IEnumerable<SelectListItem> Authors { get; set; }

        public Author Author { get; set; } = new Author();
    }
}
