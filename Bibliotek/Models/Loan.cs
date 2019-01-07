using Bibliotek.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bibliotek.Models
{
    public class Loan
    {
        [Key]
        public int ID { get; set; }
        public Book Book { get; set; }
        [Display(Name = "Medlem")]
        public Member Member { get; set; }
        [Display(Name = "Utlåningsdatum")]
        public DateTime DateLoan { get; set; }
        [Display(Name = "Returdatum")]
        public DateTime DateReturn { get; set; }
        // Lägga till bokreferens???? (BookCopy??)
    }
}
