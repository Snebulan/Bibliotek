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
        /// Returnerar ett lån
        /// </summary>
        /// <param name="loan">Lånet som ska returneras</param>
        void ReturnLoan(int id);
        /// <summary>
        /// Hämtar alla lån
        /// </summary>
        /// <returns>en lista av alla lån</returns>
        IList<Loan> GetAll();
        IEnumerable<SelectListItem> GetMemberLoanListItems();
    }
}
