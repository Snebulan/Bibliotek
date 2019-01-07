using System.Collections.Generic;
using Bibliotek.Models;

namespace Bibliotek.Services.Interfaces
{
    public interface ILoanService
    {
        IEnumerable<Loan> GetAllLoansForMember(int? id);
    }
}
