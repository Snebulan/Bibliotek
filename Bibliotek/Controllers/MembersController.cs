using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bibliotek.Models.ViewModels;
using Bibliotek.Data;
using Bibliotek.Models;
using Bibliotek.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Internal;

namespace Bibliotek.Controllers
{
    public class MembersController : Controller
    {
        private readonly IMembersService _membersService;
        private readonly ILoanService _loanService;
        //private readonly ILoanService _loanService;

        public MembersController(LibraryContext context, IMembersService membersService, ILoanService loanService)
        {
            this._membersService = membersService;
            this._loanService = loanService;
            _context = context;
        }
        private readonly LibraryContext _context;

        /// <summary>
        /// Visa en dashboard för medlemmar
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            return View(model: await _context.Members.ToListAsync());
        }

        /// <summary>
        /// Visar detaljer om vald medlem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new MemberDetailsVM();
            vm.Member = _membersService.GetDetails(id);
            vm.Books = _membersService.GetAllMembersLoans(vm.Member.Loans);
            vm.Loans = _loanService.GetAllLoansForMember(id).ToList();
            ViewBag.Debt = _loanService.LoanOverdue(vm.Loans);
            vm.TotalDebt = _loanService.GetTotalDebt(vm.Loans);
            return View(vm);
        }

        /// <summary>
        /// Visar en sida för att skapa en medlem
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            return View();
        }

        /// <summary>
        /// Skapar en ny medlem
        /// </summary>
        /// <param name="member"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,FirstName,LastName,PersonNumber")] Member member)
        {
            if (_context.Members.Any(x => x.PersonNumber.Equals(member.PersonNumber)))
            {
                TempData["fail"] = "fail";
                return View(member);

            }
            if (ModelState.IsValid)
            {
                _context.Add(member);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(member);
        }

        /// <summary>
        /// Visar en sida för att redigera en medlem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members.FindAsync(id);
            if (member == null)
            {
                return NotFound();
            }
            return View(member);
        }

        /// <summary>
        /// Redigerar en medlem
        /// </summary>
        /// <param name="id"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,FirstName,LastName,LoanID,PersonNumber")] Member member)
        {
            if (id != member.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    if (_membersService.CheckPersonNumber(id, member) == true)
                    {
                        TempData["Fail"] = "Fail";
                        return RedirectToAction(nameof(Edit));
                    }

                    _context.Update(member);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MemberExists(member.ID))
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
            return View(member);
        }

        /// <summary>
        /// Visar en sida för att ta bort en medlem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var member = await _context.Members
                .FirstOrDefaultAsync(m => m.ID == id);
            if (member == null)
            {
                return NotFound();
            }

            return View(member);
        }

        /// <summary>
        /// Tar bort en medlem
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var member = await _context.Members.FindAsync(id);
            _context.Members.Remove(member);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Kontrollerar om en medlem finns
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool MemberExists(int id)
        {
            return _context.Members.Any(e => e.ID == id);
        }
    }
}
