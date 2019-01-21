using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Bibliotek.Models;
using Bibliotek.Data;
using Bibliotek.Services.Interfaces;
using Bibliotek.Models.ViewModels;

namespace Bibliotek.Controllers
{
    public class LoansController : Controller
    {
        private readonly ILoanService _loanService;
        private readonly IMembersService _memberService;
        private readonly IBookService _bookService;

        public LoansController(ILoanService loanService, IMembersService memberService, IBookService bookService)
        {
            this._loanService = loanService;
            this._memberService = memberService;
            this._bookService = bookService;
        }

        //Visar all lån
        public IActionResult Index()
        {
            var vm = new LoanIndexVM();
            vm.Loans = _loanService.GetAll();
            vm.Book = _bookService.GetAll();
            vm.Members = _memberService.GetSelectListItems();
            ViewBag.Debt = _loanService.LoanOverdue(vm.Loans);
            vm.TotalDebt = _loanService.GetTotalDebt(vm.Loans);
            return View(vm);
        }

        //Visar alla lån till en medlem
        public IActionResult FilterOnMember(LoanIndexVM vm)
        {
            vm.Loans = _loanService.GetAllLoansForMember(vm.SelectMember.ID);
            vm.Book = _bookService.GetAll();
            vm.Members = _memberService.GetSelectListItems();
            ViewBag.Debt = _loanService.LoanOverdue(vm.Loans);
            vm.TotalDebt = _loanService.GetTotalDebt(vm.Loans);
            return View("Index", vm);
        }

        /// <summary>
        /// Filtrerar lånen på vald medlem och returnerar Return
        /// </summary>
        /// <param name="vm"></param>
        /// <returns>Loans/Return</returns>
        public IActionResult FilterOnMemberReturn(LoanReturnVM vm)
        {
            vm.Loans = _loanService.GetAllActiveLoansForMember(vm.SelectMember.ID);
            vm.Book = _bookService.GetAll();
            vm.Members = _memberService.GetSelectListItems();
            return View("Return", vm);
        }

        //Visa detaljer på valt lån
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new LoanDetailsVM();
            vm.Book = _bookService.GetAll();
            vm.Loan = _loanService.Get(id);

            if (vm.Loan == null)
            {
                return NotFound();
            }

            return View(vm);
        }

        /// <summary>
        /// Visar en sida för att skapa ett lån
        /// </summary>
        /// <returns></returns>
        public IActionResult Create()
        {
            var MemberEmpty = _memberService.GetSelectListItems().Count();
            var AvailableBookEmpty = _bookService.GetAvailableListItems().Count();

            if (MemberEmpty == 0)
            {
                TempData["Fail"] = "Fail";
            }
            if (AvailableBookEmpty == 0)
            {
                TempData["BookFail"] = "BookFail";
            }

            ViewBag.Members = _memberService.GetSelectListItems();
            ViewBag.Books = _bookService.GetAvailableListItems();
            return View();
        }

        /// <summary>
        /// Skapar ett nytt lån
        /// </summary>
        /// <param name="loan"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Loan loan)
        {
            if (ModelState.IsValid)
            {
                _loanService.Add(loan);
                return RedirectToAction(nameof(Index));
            }
            return View(loan);
        }

        //Visar lån att returnera
        public IActionResult Return()
        {
            var vm = new LoanReturnVM();
            vm.Loans = _loanService.GetActiveLoans();
            vm.Book = _bookService.GetAll();
            vm.Members = _memberService.GetSelectListItems();
            return View(vm);
        }

        //Returnerar lån
        public IActionResult ReturnAction(int id)
        {
            if (ModelState.IsValid)
            {
                _loanService.ReturnLoan(id);
                return RedirectToAction(nameof(Return));
            }
            return View();
        }

        //Visar sida för redigera lån
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            ViewBag.Books = _bookService.GetAvailableListItems(id);
            ViewBag.Members = _memberService.GetSelectListItems();

            var loan = _loanService.Get(id);
            if (loan == null)
            {
                return NotFound();
            }
            return View(loan);
        }

        /// <summary>
        /// Redigerar ett lån
        /// </summary>
        /// <param name="id"></param>
        /// <param name="loan"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,MemberID,BookID,DateLoan,DateReturn")] Loan loan)
        {
            if (id != loan.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _loanService.Update(loan);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoanExists(loan.ID))
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
            return View(loan);
        }

        /// <summary>
        /// Visar en sida för att ta bort ett lån
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var vm = new LoanDeleteVM();
            vm.Book = _bookService.GetAll();
            vm.Loan = _loanService.Get(id);
            if (vm.Loan == null)
            {
                return NotFound();
            }
            return View(vm);
        }

        /// <summary>
        /// Tar bort ett lån
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _loanService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        /// <summary>
        /// Kontrollerar att ett lån existerar
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool LoanExists(int id)
        {
            return _context.Loans.Any(e => e.ID == id);
        }
    }
}
