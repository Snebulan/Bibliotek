using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bibliotek.Models.ViewModels
{
    public class LoanDetailsVM
    {
        public Loan Loan { get; set; }
        public IEnumerable<Book> Book { get; set; }
    }
}
