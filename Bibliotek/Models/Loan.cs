using Bibliotek.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bibliotek.Models
{
    public class Loan
    {
        [Key]
        public int ID { get; set; }
        public BookCopy Book { get; set; }
        [Display(Name = "Medlemsnummer")]
        public int MemberID { get; set; }
        public Member Member { get; set; }
    }
}
