﻿using Bibliotek.Data;
using Bibliotek.Models;
using Bibliotek.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bibliotek.Services
{
    public class BookService : IBookService
    {
        private readonly LibraryContext _context;

        public BookService(LibraryContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Hämtar alla böcker
        /// </summary>
        /// <returns>en lista av alla böcker</returns>
        public IList<Book> GetAll()
        {
            return _context.Books
                .Include("Author")
                .Include(x => x.BookCopeis)
                .ToList();
        }

        /// <summary>
        /// Hämtar alla böcker som är tillgängliga
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Book> GetAvailable()
        {
            return _context.Books
                .Include("Author")
                .Include(x => x.BookCopeis)
                .ToList()
                .Where(x => IsAvailable(x));
        }
        /// <summary>
        /// Hämtar en SelectList av alla tillgängliga böcker.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SelectListItem> GetAvailableListItems()
        {
            return _context.Books.ToList().OrderBy(x => x.Author).Select(x =>
               new SelectListItem
               {
                   Text = $"{x.Author}  {x.Title}",
                   Value = x.ID.ToString()
               });

            //return _context.Books
            //    .Include("Author")
            //    .Include(x => x.BookCopeis)
            //    .ToList()
            //    .Where(x => IsAvailable(x));
        }

        /// <summary>
        /// Hämtar alla böcker från angiven författare
        /// </summary>
        /// <param name="author">Författare vars böcker ska hämtas</param>
        /// <returns></returns>
        public IEnumerable<Book> GetAllByAuthor(Author author)
        {
            return _context.Books
                .Include("Author")
                .Include(x => x.BookCopeis)
                .ToList()
                .Where(m => m.AuthorID == author.ID);
        }

        /// <summary>
        /// Hämtar en bok utifrån dess ID
        /// </summary>
        /// <param name="id">ID på boken som ska hämtas</param>
        /// <returns></returns>
        public Book Get(int? id)
        {
            return _context.Books.FirstOrDefault(m => m.ID == id);
        }

        /// <summary>
        /// Lägger till en bok
        /// </summary>
        /// <param name="book">Boken som ska läggas till</param>
        public void Add(Book book)
        {
            book.Author = _context.Authors.Find(book.Author.ID);
            _context.Add(book);
            _context.SaveChanges();
        }

        /// <summary>
        /// Uppdaterar en bok
        /// </summary>
        /// <param name="book">Boken som ska uppdateras</param>
        public void Update(Book book)
        {
            _context.Update(book);
            _context.SaveChanges();
        }

        /// <summary>
        /// Tar bort alla exemplar av boken sen tas även bokenbort givet dess ID
        /// </summary>
        /// <param name="id">ID på boken som ska tas bort</param>
        public void Delete(int id)
        {
            var book = _context.Books.Find(id);
            var bookCopies = _context.BookCopies
                .Where(x => x.BookID == book.ID);
            _context.BookCopies.RemoveRange(bookCopies);
            _context.Books.Remove(book);
            _context.SaveChanges();
        }

        /// <summary>
        /// Kollar om boken i fråga är tillgänlig eller ej
        /// </summary>
        /// <param name="book">boken i fråga</param>
        /// <returns>true om boken finns tillgänglig</returns>
        public bool IsAvailable(Book book)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Kollar om boken finns eller ej
        /// </summary>
        /// <param name="id">bokens ID</param>
        /// <returns>true om boken finns</returns>
        public bool BookExists(int id)
        {
            return _context.Books.Any(e => e.ID == id);
        }

        public string RemoveCopy(int id)
        {
            string success;
            try
            {
                var bookCopy = _context.BookCopies
                    .Where(x => x.BookID == id && x.IsAvailable == 1)
                    .FirstOrDefault();
                    _context.BookCopies.Remove(bookCopy);
                    _context.SaveChanges();
                success = "true";
                return success;
            }
            catch (ArgumentNullException)
            {
                success = "false";
                return success;
            }

        }

        public string AddCopy(int id)
        {
            string adSuccess;
            BookCopy copy = new BookCopy { BookID = id, IsAvailable = 1 };
            _context.BookCopies.Add(copy);
            _context.SaveChanges();
            adSuccess = "true";
            return adSuccess;
        }
    }
}
