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
        /// <summary>
        /// Hämtar alla författare
        /// </summary>
        /// <returns>en lista av alla författare</returns>
        IList<Author> GetAll();
        /// <summary>
        /// Lägger till en författare
        /// </summary>
        /// <param name="author">Författaren som ska läggas till</param>
        void Add(Author author);
        /// <summary>
        /// Uppdaterar en författare
        /// </summary>
        /// <param name="author">Författaren som ska uppdateras</param>
        void Update(Author author);
        bool Any(int id);
    }
}