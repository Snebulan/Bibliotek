using Bibliotek.Data;
using Bibliotek.Models;
using Bibliotek.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
using System.Linq;

namespace Bibliotek.Services
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

        /// <summary>
        /// Uppdaterar en författare
        /// </summary>
        /// <param name="author">Författaren som ska uppdateras</param>
        public void Update(Author author)
        {
            _context.Update(author);
            _context.SaveChanges();
        }

        /// <summary>
        /// Hämtar författare på angivet ID
        /// </summary>
        /// <param name="id">ID på författaren som ska hämtas</param>
        public Author GetAuthor(int? id)
        {
            return _context.Authors.FirstOrDefault(x => x.ID == id);
        }

        //Raderar Författare och alla object tillhörande författare
        public void DeleteAuthorAndConnectedItems (int id)
        {
            var books = _context.Books.Where(y => y.AuthorID == id );
            List<Loan> deletedLoans = new List<Loan>();
            foreach (var book in books)
            {
                var loans = _context.Loans.Where(x => x.BookID == book.ID);
                if (loans != null)
                {
                    deletedLoans.AddRange(loans);
                }
            }
            if (deletedLoans.Any())
            {
                _context.RemoveRange(deletedLoans);
            }
            var author = _context.Authors.FirstOrDefault(x=> x.ID == id);
            _context.Authors.Remove(author);
            _context.SaveChanges();
        }

        public IList<Author> GetAll()
        {
            return _context.Authors.ToList();
                
        }

        public bool Any(int id)
        {
            return _context.Authors.Any(e => e.ID == id);
        }

    }
}
