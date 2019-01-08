using System.Collections.Generic;
using Bibliotek.Models;

namespace Bibliotek.Services.Interfaces
{
    public interface ILoanService
    {
        IEnumerable<Loan> GetAllLoansForMember(int? id);

        /// <summary>
        /// Lägger till en bok
        /// </summary>
        /// <param name="loan">Boken som ska läggas till</param>
        void Add(Loan loan);
        /// <summary>
        /// Hämtar alla lån
        /// </summary>
        /// <returns>en lista av alla lån</returns>
        IList<Loan> GetAll();
    }
}
