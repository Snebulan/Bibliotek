using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bibliotek.Models.ViewModels
{
    public class LoanIndexVM
    {
        public IEnumerable<Member> Members { get; set; }
        public IEnumerable<Loan> Loans { get; set; }
        public IEnumerable<Book> Books { get; set; }
    }
}
