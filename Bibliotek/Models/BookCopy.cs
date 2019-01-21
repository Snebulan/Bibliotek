using System;
using System.ComponentModel.DataAnnotations;

namespace Bibliotek.Models
{
    public class BookCopy
    {
        [Key]
        public int ID { get; set; }
        public int BookID { get; set; }
        /// <summary>
        /// 0 for false, 1 for true
        /// </summary>
        public int IsAvailable { get; set; }
    }
}