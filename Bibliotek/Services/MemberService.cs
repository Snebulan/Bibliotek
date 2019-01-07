using Bibliotek.Data;
using Bibliotek.Models;
using Bibliotek.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
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

        public List<Book> GetAllMembersLoans(List<Loan> loans)
        {
            //idOfBook =  select BookID FROM Loans Where MemberID == id
            //select Book_Name From Book Where BookId = IdOfBook


            List<Book> books = new List<Book>();

            foreach (var loan in loans )
            {
                books.Add(_context.Books.FirstOrDefault(x => x.ID == loan.Book.ID));
            }

            return books;
        }


        //public object GetDetails(int? id)
        //{
        //    return _context.Members
        //        .Include(x => x.Loans)
        //        .ToList()
        //        .Where(m => m.ID == id);
        //}

        public Member GetDetails(int? id)
        {
            return _context.Members
                .Include(x => x.Loans)
                .FirstOrDefault(m => m.ID == id);
        }
    }
}
