using Bibliotek.Data;
using Bibliotek.Models;
using Bibliotek.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bibliotek.Services
{
    public class MemberService : IMembersService
    {
        private readonly LibraryContext _context;

        public MemberService(LibraryContext context)
        {
            this._context = context;
        }

        //Hämtar alla böcker för vald medlem
        public List<Book> GetAllMembersLoans(List<Loan> loans)
        {
            List<Book> books = new List<Book>();

            foreach (var loan in loans )
            {

                books.Add(_context.Books.FirstOrDefault(x => x.ID == loan.BookID));
            }

            return books;
        }
        /// <summary>
        /// Hämtar en SelectList av alla medlemmar.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetSelectListItems()
        {
            return _context.Members.ToList().OrderBy(x => x.FirstName).Select(x =>
               new SelectListItem
               {
                   Text = $"{x.FirstName}  {x.LastName}",
                   Value = x.ID.ToString()
               });
        }

        //Visar detaljer för vald medlem
        public Member GetDetails(int? id)
        {
            return _context.Members
                .Include(x => x.Loans)
                .FirstOrDefault(m => m.ID == id);
        }

        //Kollar om en medlems personnummer är ändrat
        public bool CheckPersonNumber(int id, Member member)
        {
            bool personNumberChanged = false;
            var originalMember = _context.Members.AsNoTracking().FirstOrDefault(x => x.ID == id);
            if (originalMember.PersonNumber != member.PersonNumber)
            {
                personNumberChanged = true;

            }
            return personNumberChanged;
        }
    }
}
