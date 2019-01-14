using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Bibliotek.Models
{
    public class Author
    {
        [Key]
        public int ID { get; set; }

        [Display(Name = "Förnamn")]
        [Required(ErrorMessage = "Förnamn måste anges")]
        public string FirstName { get; set; }
        [Required(ErrorMessage ="Efternamn måste anges")]
        [Display(Name = "Efternamn")]
        public string LastName { get; set; }
        public virtual ICollection<Book> AuthorBooks { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

    }
}