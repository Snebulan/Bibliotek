using Bibliotek.Data;
using Bibliotek.Models;
using Bibliotek.Services.Interfaces;
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
                //.Include("Member")
                //.Include(x => x.BookCopeis)
                .ToList();
        }

        public IEnumerable<Loan> GetAllLoansForMember(int? id)
        {
            return _context.Loans
                .ToList()
                .Where(m => m.MemberID== id);
        }

        /// <summary>
        /// Lägger till ett lån
        /// </summary>
        /// <param name="loan">Lånet som ska läggas till</param>
        public void Add(Loan loan)
        {
            loan.Member = _context.Members.Find(loan.Member.ID);
            loan.Book = _context.Books.Find(loan.Book.ID);
            loan.DateLoan = DateTime.Now;
            
            _context.Add(loan);
            _context.SaveChanges();
        }
    }
}
