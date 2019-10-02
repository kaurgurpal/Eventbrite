using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.Services
{
    public interface ICatalogService
    {
        Task<Catalog> GetEventsCatalogAsync(int page, int size, int? type, int? category, int? location);
        Task<IEnumerable<SelectListItem>> GetEventCategoriesAsync();
        Task<IEnumerable<SelectListItem>> GetEventTypesAsync();
        Task<IEnumerable<SelectListItem>> GetEventLocationsAsync();
    }
}
