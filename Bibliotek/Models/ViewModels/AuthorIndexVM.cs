﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bibliotek.Models.ViewModels
{
    public class AuthorIndexVM
    {
        public IEnumerable<Author> Authors { get; set; }
    }
}
