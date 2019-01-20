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

        /// <summary>
        /// Visar detaljer för vald medlem
        /// </summary>
        /// <returns></returns>
        Member GetDetails(int? id);
        /// <summary>
        /// Hämtar alla böcker för vald medlem
        /// </summary>
        /// <returns></returns>
        List<Book> GetAllMembersLoans(List<Loan> loans);
        /// <summary>
        /// Hämtar en SelectList av alla medlemmar
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetSelectListItems();
        /// <summary>
        /// Kollar om en medlems personnummer är ändrat
        /// </summary>
        /// <returns></returns
        bool CheckPersonNumber(int id, Member member);
    }
}
