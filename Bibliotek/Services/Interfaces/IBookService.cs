using Bibliotek.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace Bibliotek.Services.Interfaces
{
    public interface IBookService
    {
        /// <summary>
        /// Lägger till en bok
        /// </summary>
        /// <param name="book">Boken som ska läggas till</param>
        void Add(Book book);
        /// <summary>
        /// Kollar om boken finns eller ej
        /// </summary>
        /// <param name="id">bokens ID</param>
        /// <returns>true om boken finns</returns>
        bool BookExists(int id);
        /// <summary>
        /// Tar bort en bok givet dess ID
        /// </summary>
        /// <param name="id">ID på boken som ska tas bort</param>
        void Delete(int id);
        /// <summary>
        /// Hämtar en bok utifrån dess ID
        /// </summary>
        /// <param name="id">ID på boken som ska hämtas</param>
        /// <returns></returns>
        Book Get(int? id);
        /// <summary>
        /// Hämtar alla böcker
        /// </summary>
        /// <returns>en lista av alla böcker</returns>
        IList<Book> GetAll();
        /// <summary>
        /// Hämtar alla böcker av en författare
        /// </summary>
        /// <returns>en lista av alla böcker från vald författare</returns>
        IEnumerable<Book> GetAllByAuthor(Author author);
        /// <summary>
        /// Hämtar alla tillgängliga böcker från angiven författare
        /// </summary>
        /// <param name="author">Författare vars tillgängliga böcker ska hämtas</param>
        /// <returns></returns>
        IEnumerable<Book> GetAvailableByAuthor(Author author);
        /// <summary>
        /// Listar alla böcker som är tillgängliga
        /// </summary>
        /// <returns></returns>
        IEnumerable<SelectListItem> GetAvailableListItems(int? id = null);
        /// <summary>
        /// Hämtar alla böcker som är tillgängliga
        /// </summary>
        /// <returns></returns>
        IEnumerable<Book> GetAvailable();
        /// <summary>
        /// Kollar om boken i fråga är tillgänlig eller ej
        /// </summary>
        /// <param name="book">boken i fråga</param>
        /// <returns>true om boken finns tillgänglig</returns>
        bool IsAvailable(Book book);
        /// <summary>
        /// Uppdaterar en bok
        /// </summary>
        /// <param name="book">Boken som ska uppdateras</param>
        void Update(Book book);
        /// <summary>
        /// Tar bort en kopia av en bok
        /// </summary>
        /// <param name="id">ID på kopian som ska tas bort</param>
        string RemoveCopy(int id);
        /// <summary>
        /// Lägger till en kopia av en bok
        /// </summary>
        /// <param name="id">ID på kopian som ska läggas till</param>
        string AddCopy(int id);
        /// <summary>
        /// Tar bort vald bok och alla lån knutna till vald bok
        /// </summary>
        /// <param name="id">ID på boken som ska tas bort</param>
        void RemoveBookAndLoans(int id);
    }
}