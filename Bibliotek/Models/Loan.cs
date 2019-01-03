using Library.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Bibliotek.Models
{
    public class Loan
    {
        [Key]
        public int ID { get; set; }
        public BookCopy Book { get; set; }
        [Display(Name = "Medlemsnummer")]
        public int MemberID { get; set; }
    }
}
