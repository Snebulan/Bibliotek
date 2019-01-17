using System.Collections.Generic;
using Bibliotek.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Bibliotek.Services.Interfaces
{
    public interface IAuthorService
    {
        /// <summary>
        /// Hämtar en SelectList av alla författare
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetSelectListItems();
        /// <summary>
        /// Hämtar författare på angivet ID
        /// </summary>
        /// <returns></returns>
        Author GetAuthor(int? id);
        /// <summary>
        /// Raderar Författare och alla object tillhörande författare
        /// </summary>
        /// <returns></returns>
        void DeleteAuthorAndConnectedItems(int id);
    }
}