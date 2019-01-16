using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Bibliotek.Models
{
    public class Member
    {
        [Key]
        public int ID { get; set; }
        [Required(ErrorMessage ="Du måste ange personnummer")]
        //[Editable(false)]
        [RegularExpression(@"^[0-9]{6}[-][0-9]{4}$",
                   ErrorMessage = "Ange personnummer i följande format: ååmmdd-xxxx")]
        [Display(Name = "Personnummer")]
        public string PersonNumber { get; set; }
        [Required(ErrorMessage ="Du måste ange förnamn")]
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Du måste ange efternamn")]
        [Display(Name = "Efternamn")]
        public string LastName { get; set; }
        public List<Loan> Loans { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }
    }
}
