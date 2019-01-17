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
        private readonly LibraryContext _context;
        private readonly ILoanService _loanService;
        private readonly IMembersService _memberService;
        private readonly IBookService _bookService;

        public LoansController(ILoanService loanService, IMembersService memberService, IBookService bookService, LibraryContext context)
        {
            this._loanService = loanService;
            this._memberService = memberService;
            this._bookService = bookService;
            _context = context;
        }

        //Visar all lån
        public IActionResult Index()
        {
            var vm = new LoanIndexVM
            {
                Loans = _loanService.GetAll(),
                Book = _bookService.GetAll(),
                Members = _memberService.GetSelectListItems()
            };
            ViewBag.Debt = _loanService.LoanOverdue(vm.Loans);
            vm.TotalDebt = _loanService.GetTotalDebt(vm.Loans);
            return View(vm);
        }

        //Visat alla lån till en medlem
        public IActionResult FilterOnMember(LoanIndexVM vm)
        {
            vm.Loans = _loanService.GetAllLoansForMember(vm.SelectMember.ID);
            vm.Book = _bookService.GetAll();
            vm.Members = _memberService.GetSelectListItems();
            ViewBag.Debt = _loanService.LoanOverdue(vm.Loans);
            vm.TotalDebt = _loanService.GetTotalDebt(vm.Loans);
            return View("Index", vm);
        }


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

        // GET: Loans/Create
        public IActionResult Create()
        {
            ViewBag.Members = _memberService.GetSelectListItems();
            ViewBag.Books = _bookService.GetAvailableListItems();

            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
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

        //Redigerar lån
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            
            ViewBag.Books = _bookService.GetAvailableListItems(id);
            ViewBag.Members = _memberService.GetSelectListItems();
            var loan = await _context.Loans.FindAsync(id);
            if (loan == null)
            {
                return NotFound();
            }
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("ID,MemberID,BookID,DateLoan")] Loan loan)
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

        // GET: Loans/Delete
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

        private bool LoanExists(int id)
        {
            return _context.Loans.Any(e => e.ID == id);
        }
    }
}
