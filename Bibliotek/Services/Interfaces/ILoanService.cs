using System;
using System.Collections.Generic;
using Bibliotek.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bibliotek.Services.Interfaces
{
    public interface ILoanService
    {
        IEnumerable<Loan> GetAllLoansForMember(int? id);

        /// <summary>
        /// Lägger till ett lån
        /// </summary>
        /// <param name="loan">Lånet som ska läggas till</param>
        void Add(Loan loan);
        /// <summary>
        /// Tar bort ett lån givet dess ID
        /// </summary>
        /// <param name="id">ID på lånet som ska tas bort</param>
        void Delete(int id);
        /// <summary>
        /// Uppdaterar ett lån
        /// </summary>
        /// <param name="loan">Lånet som ska uppdateras</param>
        void Update(Loan loan);
        /// <summary>
        /// Returnerar ett lån
        /// </summary>
        /// <param name="loan">Lånet som ska returneras</param>
        void ReturnLoan(int id);
        /// <summary>
        /// Hämtar alla lån
        /// </summary>
        /// <returns>en lista av alla lån</returns>
        IList<Loan> GetAll();
        /// <summary>
        /// Hämtar ett lån utifrån dess ID
        /// </summary>
        /// <param name="id">ID på lånet som ska hämtas</param>
        /// <returns></returns>
        Loan Get(int? id);
        IList<Loan> GetActiveLoans();
        IEnumerable<SelectListItem> GetMemberLoanListItems();
        IEnumerable<Loan> GetAllActiveLoansForMember(int? id);
        IEnumerable<string> LoanOverdue(IEnumerable<Loan> loans);
        double GetTotalDebt(IEnumerable<Loan> loans);
    }
}
