﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bibliotek.Models.ViewModels
{
    public class MemberDetailsVM
    {
        public Member Member { get; set; }
        public List<Loan> Loans { get; set; }

    }
}