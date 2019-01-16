using Bibliotek.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bibliotek.Services.Interfaces
{
    public interface IMembersService
    {

        //object Details(int? id);
        Member GetDetails(int? id);
        List<Book> GetAllMembersLoans(List<Loan> loans);
        /// <summary>
        /// Hämtar en SelectList av alla medlemmar
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetSelectListItems();
        bool CheckPersonNumber(int id, Member member);
    }
}
