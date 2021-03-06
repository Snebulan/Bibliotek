﻿using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bibliotek.Models.ViewModels
{
    public class LoanReturnVM
    {
        public IEnumerable<Member> Member { get; set; }
        public IEnumerable<Loan> Loans { get; set; }
        public IEnumerable<Book> Book { get; set; }
        public IEnumerable<SelectListItem> Members { get; set; }
        public Member SelectMember { get; set; } = new Member();
    }
}
