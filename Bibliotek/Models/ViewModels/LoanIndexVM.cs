using Bibliotek.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Models.ViewModels
{
    public class LoanIndexVM
    {
        public IEnumerable<SelectListItem> Members { get; set; }

        public Member Member { get; set; } = new Member();
    }
}
