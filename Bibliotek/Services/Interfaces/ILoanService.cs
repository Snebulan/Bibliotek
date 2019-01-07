using System.Collections.Generic;
using Bibliotek.Models;

namespace Bibliotek.Services.Interfaces
{
    public interface ILoanService
    {
        IEnumerable<Loan> GetAllLoansForMember(int? id);

        /// <summary>
        /// Hämtar alla lån
        /// </summary>
        /// <returns>en lista av alla lån</returns>
        IList<Loan> GetAll();
    }
}
