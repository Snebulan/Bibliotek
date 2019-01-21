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
        /// <summary>
        /// Hämtar alla aktiva lån i en lista
        /// </summary>
        /// <returns>en lista av alla aktiva lån</returns>
        IList<Loan> GetActiveLoans();
        /// <summary>
        /// Hämtar alla aktiva lån på vald medlem
        /// </summary>
        /// <returns>en lista av alla aktiva lån på vald medlem</returns>
        IEnumerable<Loan> GetAllActiveLoansForMember(int? id);
        /// <summary>
        /// Lån som är försenade, returnerar antal dagar
        /// </summary>
        /// <returns>Lån som är försenade, returnerar antal dagar</returns>
        IEnumerable<string> LoanOverdue(IEnumerable<Loan> loans);
        /// <summary>
        /// Hämtar total skuld
        /// </summary>
        /// <returns>den totala skulden</returns>
        double GetTotalDebt(IEnumerable<Loan> loans);
        bool Any(int id);
        //IEnumerable<SelectListItem> GetMemberLoanListItems();
    }
}
