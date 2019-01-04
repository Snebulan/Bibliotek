using System.Collections.Generic;
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
    }
}