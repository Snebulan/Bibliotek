using Library.Data;
using Library.Models;
using Library.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public class AuthorService : IAuthorService
    {
        private readonly LibraryContext _context;

        public AuthorService(LibraryContext libraryContext )
        {
            this._context = libraryContext;
        }
        /// <summary>
        /// Hämtar en SelectList av alla författare
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetSelectListItems()
        {
            return _context.Authors.ToList().OrderBy(x => x.FirstName).Select(x =>
               new SelectListItem
               {
                   Text = $"{x.FirstName}  {x.LastName}",
                   Value = x.ID.ToString()
               });
        }
        
        /// <summary>
        /// Lägger till en författare
        /// </summary>
        /// <param name="book">Författaren som ska läggas till</param>
        public void Add(Author author)
        {
            _context.Add(author);
            _context.SaveChanges();
        }
    }
}
