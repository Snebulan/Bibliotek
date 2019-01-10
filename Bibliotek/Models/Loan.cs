    using Bibliotek.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace Bibliotek.Models
{
    public class Loan
    {
        [Key]
        public int ID { get; set; }
        public int BookID { get; set; }
        //public Book Book { get; set; }
        [Display(Name = "Medlem")]
        public int MemberID { get; set; }
        public Member Member { get; set; }
        [Display(Name = "Utlånad")]
        public DateTime DateLoan { get; set; }
        [Display(Name = "Returnerad")]
        public DateTime DateReturn { get; set; }
    }
}
