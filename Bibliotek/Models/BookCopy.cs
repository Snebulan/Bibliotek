using System;
using System.ComponentModel.DataAnnotations;

namespace Bibliotek.Models
{
    public class BookCopy
    {
        [Key]
        public int ID { get; set; }
        //public Book Book { get; set; }
        public int BookID { get; set; }
        public int IsAvailable { get; set; }
    }
}