using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bibliotek.Data;
using Bibliotek.Models;
using Bibliotek.Models.ViewModels;
using Bibliotek.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bibliotek.Controllers
{
    public class AuthorsController : Controller
    {
        private readonly LibraryContext _context;
        private readonly IAuthorService _authorService;
        private readonly IBookService _bookService;

        public AuthorsController(LibraryContext context, IAuthorService authorService, IBookService bookService)
        {
            _context = context;
            this._authorService = authorService;
            this._bookService = bookService;
        }

        /// <summary>
        /// Visa en dashboard för författare
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(await _context.Authors.ToListAsync());
        }

        /// <summary>
        /// Visar detaljer om vald författare
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new AuthorDetailsVM();
            vm.Author = _authorService.GetAuthor(id);
            vm.Books = _bookService.GetAllByAuthor(vm.Author).ToList();
            return View(vm);
        }

        /// <summary>
        /// Visar en sida för att skapa en författare
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Visar en sida för att ta bort en författare
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Author author = _authorService.GetAuthor(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        /// <summary>
        /// Tar bort en författare
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _authorService.DeleteAuthorAndConnectedItems(id);

            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Visar en sida för att redigera en författare
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var author = await _context.Authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }
            return View(author);
        }

        /// <summary>
        /// Redigerar en författare
        /// </summary>
        /// <param name="id"></param>
        /// <param name="author"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Author author)
        {
            if (id != author.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (!ModelState.IsValid)
                    {
                        return View(author);
                    }
                    _context.Update(author);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AuthorExists(author.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        /// <summary>
        /// Skapar en ny författare
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName")] Author author)
        {
            if (!ModelState.IsValid)
            {
                TempData["Fail"] = "Fail";
                return View(author);
            }
            if (ModelState.IsValid)
            {
                _context.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View();
        }

        /// <summary>
        /// Kontrollerar om en författare finns
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool AuthorExists(int id)
        {
            return _context.Authors.Any(e => e.ID == id);
        }
    }
}
