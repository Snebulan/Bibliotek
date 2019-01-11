using Bibliotek.Data;
using Bibliotek.Models;
using Bibliotek.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bibliotek.Services
{
    public class LoanService : ILoanService
    {
        private readonly LibraryContext _context;

        public LoanService(LibraryContext context)
        {
            this._context = context;
        }

        public IList<Loan> GetAll()
        {
            return _context.Loans

                .ToList();
        }

        public IEnumerable<SelectListItem> GetMemberLoanListItems()
        {
            return _context.Loans.ToList().OrderBy(x => x.BookID).Select(x =>
               new SelectListItem
               {
                   //Text = $"{x.Author}  {x.Title}",
                   Value = x.ID.ToString()
               });
        }

        public IEnumerable<Loan> GetAllLoansForMember(int? id)
        {
            return _context.Loans
                .ToList()
                .Where(m => m.MemberID== id);
        }

        /// <summary>
        /// Hämtar alla lån från angiven medlem
        /// </summary>
        /// <param name="author">Medlem vars lån ska hämtas</param>
        /// <returns></returns>
        public IEnumerable<Loan> GetAllByMember(Member member)
        {
            return _context.Loans
                .Include("Member")
                .ToList()
                .Where(m => m.MemberID == member.ID);
        }
        /// <summary>
        /// Lägger till ett lån
        /// </summary>
        /// <param name="loan">Lånet som ska läggas till</param>
        public void Add(Loan loan)
        {
            DateTime dateTime = DateTime.Now;
            var date = dateTime.Date;
            loan.DateLoan = date;
           
            
            _context.Add(loan);
            _context.SaveChanges();
        }

        public void ReturnLoan(int id)
        {
            
            var returnBook = _context.Loans
                .FirstOrDefault(x => x.ID == id);
                returnBook.DateReturn = DateTime.Now;
                _context.Loans.Update(returnBook);
                _context.SaveChanges();
            //loan.DateReturn = DateTime.Now;

            //_context.Add(loan);
        }
    }
}
