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
        /// Hämtar medlem på angivet ID
        /// </summary>
        /// <param name="id">ID på medlem som ska hämtas</param>
        Member GetMember(int? id);
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
        /// <summary>
        /// Hämtar alla medlemmar
        /// </summary>
        /// <returns>en lista av alla medlemmar</returns>
        IList<Member> GetAll();
        /// <summary>
        /// Lägger till en medlem
        /// </summary>
        /// <param name="member">Medlemen som ska läggas till</param>
        void Add(Member member);
        /// <summary>
        /// Uppdaterar en medlem
        /// </summary>
        /// <param name="member">Medlemmen som ska uppdateras</param>
        void Update(Member member);
        //Raderar Medlem och alla tillhörande lån
        void DeleteMemberAndConnectedItems(int id);
        bool Any(int id);
        bool ComparePersonNumber(Member member);
    }
}
