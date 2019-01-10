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

                .ToList();
        }

        public IEnumerable<Loan> GetAllLoansForMember(int? id)
        {
            return _context.Loans
                .ToList()
                .Where(m => m.MemberID== id);
        }
        /// <summary>
        /// Hämtar alla böcker från angiven författare
        /// </summary>
        /// <param name="author">Författare vars böcker ska hämtas</param>
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

            loan.DateLoan = DateTime.Now;
            
            _context.Add(loan);
            _context.SaveChanges();
        }
    }
}
