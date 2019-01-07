using Bibliotek.Models.ViewModels;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bibliotek.Models
{
    public class Loan
    {
        [Key]
        public int ID { get; set; }
        public int BookID { get; set; }
        [Display(Name = "Medlemsnummer")]
        public int MemberID { get; set; }
    }
}
