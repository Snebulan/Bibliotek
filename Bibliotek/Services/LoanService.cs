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

        //Listar alla lån
        public IList<Loan> GetAll()
        {
            return _context.Loans
                .ToList();
        }

        //?
        public IEnumerable<SelectListItem> GetMemberLoanListItems()
        {
            return _context.Loans.ToList().OrderBy(x => x.BookID).Select(x =>
               new SelectListItem
               {
                   Value = x.ID.ToString()
               });
        }

        //Hämtar alla lån för vald medlem
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
        //public IEnumerable<Loan> GetAllByMember(Member member)
        //{
        //    return _context.Loans
        //        .Include("Member")
        //        .ToList()
        //        .Where(m => m.MemberID == member.ID);
        //}
        /// <summary>
        /// Hämtar ett lån utifrån dess ID
        /// </summary>
        /// <param name="id">ID på lånet som ska hämtas</param>
        /// <returns></returns>
        public Loan Get(int? id)
        {
            return _context.Loans.Include(x => x.Member).FirstOrDefault(m => m.ID == id);
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
        /// <summary>
        /// Tar bort ett lån eligt ID
        /// </summary>
        /// <param name="id">ID på lånet som ska tas bort</param>
        public void Delete(int id)
        {
            var loan = _context.Loans.Find(id);
            var bookCopy = _context.BookCopies.
                FirstOrDefault(x => x.BookID == loan.BookID && x.IsAvailable == 0);
            bookCopy.IsAvailable = 1;
            _context.Loans.Remove(loan);
            _context.SaveChanges();
        }
        /// <summary>
        /// Uppdaterar ett lån
        /// </summary>
        /// <param name="loan">Lånet som ska uppdateras</param>
        public void Update(Loan loan)
        {
            var bookCopy = _context.BookCopies.AsNoTracking().
                FirstOrDefault(x => x.BookID == loan.BookID && x.IsAvailable == 1);
            if (bookCopy != null)
            {
                var orginBookId = _context.Loans.AsNoTracking().FirstOrDefault(x => x.ID == loan.ID);
                var bookCopyOrgin = _context.BookCopies.FirstOrDefault(x => x.BookID == orginBookId.BookID && x.IsAvailable == 0);
                bookCopyOrgin.IsAvailable = 1;
                _context.BookCopies.Update(bookCopyOrgin);


                bookCopy.IsAvailable = 0;
                _context.BookCopies.Update(bookCopy);
                _context.Loans.Update(loan);
                _context.SaveChanges();
            }
            
            
        }
        /// <summary>
        /// Returnerar ett lån
        /// </summary>
        /// <param name="loan">Lånet som ska returneras</param>
        public void ReturnLoan(int id)
        {
            var returnBook = _context.Loans
                .FirstOrDefault(x => x.ID == id);
            returnBook.DateReturn = DateTime.Now;

            var bookCopy = _context.BookCopies.
                FirstOrDefault(x => x.BookID == returnBook.BookID);
            bookCopy.IsAvailable = 1;

            _context.Loans.Update(returnBook);
            _context.BookCopies.Update(bookCopy);
            _context.SaveChanges();

        }

        /// <summary>
        /// Räknar dagar mellan datum
        /// </summary>
        /// <param name="loaned"></param>
        /// <param name="returned"></param>
        /// <returns></returns>
        public double GetDaysLeft(DateTime loaned, DateTime returned)
        {
            return (returned - loaned.AddDays(14)).TotalDays;
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loaned"></param>
        /// <returns></returns>
        public IEnumerable<string> LoanOverdue(IEnumerable<Loan> loans)
        {
            List<string> h = new List<string>();
            foreach (var loan in loans)
            {
                if ((loan.DateReturn.HasValue && loan.DateReturn.Value.Date < loan.DateLoan.AddDays(14).Date) ||
                    loan.DateReturn == null && loan.DateLoan.AddDays(14) < DateTime.Now)
                {
                    var days = Math.Abs((loan.DateLoan.AddDays(14).Date - DateTime.Now.Date).TotalDays);
                    var debt = Math.Abs(Math.Ceiling((loan.DateLoan.AddDays(14).Date - DateTime.Now).TotalDays * 12));
                    h.Add($"{days} dagar för sen, {debt}:-");
                }
                else
                {
                    h.Add("I tid");
                }
            }
            return h;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="loans"></param>
        /// <returns></returns>
        public double GetTotalDebt(IEnumerable<Loan> loans)
        {
            List<double> debts = new List<double>();
            foreach (var loan in loans)
            {
                if ((loan.DateReturn.HasValue && loan.DateReturn.Value.Date < loan.DateLoan.AddDays(14).Date) ||
                    loan.DateReturn == null && loan.DateLoan.AddDays(14) < DateTime.Now)
                {
                    var days = Math.Abs((loan.DateLoan.AddDays(14).Date - DateTime.Now.Date).TotalDays);
                    var debt = Math.Abs(Math.Ceiling((loan.DateLoan.AddDays(14).Date - DateTime.Now).TotalDays * 12));
                    debts.Add(debt);
                }
            }
            return debts.Sum();
        }

        //Hämtar alla lån som inte är returnerade
        public IList<Loan> GetActiveLoans()
        {
            return _context.Loans.Where(x => x.DateReturn == null).ToList();
        }

        //Hämtar alla lån som inte är returnerade på vald medlem
        public IEnumerable<Loan> GetAllActiveLoansForMember(int? id)
        {
            return _context.Loans.Where(x => x.DateReturn == null)
                .ToList()
                .Where(m => m.MemberID == id);
        }
    }
}
