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
                .Where(m => m.MemberID == id);
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
            var bookCopy = _context.BookCopies.
                FirstOrDefault(x => x.BookID == loan.BookID && x.IsAvailable == 1);
            bookCopy.IsAvailable = 0;
            loan.DateLoan = DateTime.Now;



            _context.BookCopies.Update(bookCopy);
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

        /// <summary>
        /// Räknar dagar mellan datum
        /// </summary>
        /// <param name="loaned"></param>
        /// <param name="returned"></param>
        /// <returns></returns>
        public double GetDaysLeft(DateTime loaned, DateTime returned)
        {
            return (returned - loaned).TotalDays;
        }

        /// <summary>
        /// Returnerar datumet när boken senast ska lämnas in
        /// </summary>
        /// <param name="loaned"></param>
        /// <returns></returns>
        public DateTime ReturnDate(DateTime loaned)
        {
            return loaned.AddDays(14).Date;
        }

        public IList<Loan> GetActiveLoans()
        {
            return _context.Loans.Where(x => x.DateReturn == null).ToList();
        }

        public IEnumerable<Loan> GetAllActiveLoansForMember(int? id)
        {
            return _context.Loans.Where(x => x.DateReturn == null)
                .ToList()
                .Where(m => m.MemberID == id);
        }
    }
}
