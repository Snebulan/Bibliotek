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

        public IList<Member> GetAll()
        {
            return _context.Members.ToList();

        }

        /// <summary>
        /// Hämtar medlem på angivet ID
        /// </summary>
        /// <param name="id">ID på medlem som ska hämtas</param>
        public Member GetMember(int? id)
        {
            return _context.Members.FirstOrDefault(x => x.ID == id);
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

        /// <summary>
        /// Lägger till en medlem
        /// </summary>
        /// <param name="member">Medlemen som ska läggas till</param>
        public void Add(Member member)
        {
            _context.Add(member);
            _context.SaveChanges();
        }

        /// <summary>
        /// Uppdaterar en medlem
        /// </summary>
        /// <param name="member">Medlemmen som ska uppdateras</param>
        public void Update(Member member)
        {
            _context.Update(member);
            _context.SaveChanges();
        }

        //Raderar Medlem och alla tillhörande lån
        public void DeleteMemberAndConnectedItems(int id)
        {
            var loans = _context.Loans.Where(y => y.MemberID == id);
            //List<Loan> deletedLoans = new List<Loan>();
            foreach (var loan in loans)
            {
                _context.RemoveRange(loan);
            }
            var member = _context.Members.FirstOrDefault(x => x.ID == id);
            _context.Members.Remove(member);
            _context.SaveChanges();
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

        public bool Any(int id)
        {
            return _context.Members.Any(e => e.ID == id);
        }

        public bool ComparePersonNumber(Member member)
        {
            bool Pnumber = _context.Members.Any(x => x.PersonNumber.Equals(member.PersonNumber));

            return Pnumber;
        }
    }
}
