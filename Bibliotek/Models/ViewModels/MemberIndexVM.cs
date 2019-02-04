using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bibliotek.Models.ViewModels
{
    public class MemberIndexVM
    {
        public IEnumerable<Member> Members { get; set; }
    }
}
