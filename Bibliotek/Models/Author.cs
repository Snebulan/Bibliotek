using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
    public class Author
    {
        [Key]
        public int ID { get; set; }
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }
        [Display(Name = "Efternamn")]
        public string LastName { get; set; }
        public virtual ICollection<Book> AuthorBooks { get; set; }

        public string FullName
        {
            get { return FirstName + " " + LastName; }
        }

    }
}