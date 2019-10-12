﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebMVC.Services;
using WebMVC.ViewModels;

namespace WebMVC.Controllers
{
    public class CatalogController : Controller
    {
        private readonly ICatalogService _service;
        public CatalogController(ICatalogService service) => _service = service;

        public async Task<IActionResult> About()
        {
            return View();
        }

        public async Task<IActionResult> Index(int? typesFilterApplied, int? categoryFilterApplied, int? locationFilterApplied, int? page)
        {
            var eventsOnPage = 10;
            var catalog = await _service.GetEventsCatalogAsync(page ?? 0, eventsOnPage, typesFilterApplied, categoryFilterApplied, locationFilterApplied);
                       
               var vm = new CatalogIndexViewModel
                {
                    PaginationInfo = new PaginationInfo
                    {
                        ActualPage = page ?? 0,
                        EventsPerPage =catalog.Data.Count,
                        TotalEvents = catalog.Count,
                        TotalPages = (int)Math.Ceiling((decimal)catalog.Count / eventsOnPage)
                    },
                    CatalogEvents = catalog.Data,
                    Categories = await _service.GetEventCategoriesAsync(),
                    Types = await _service.GetEventTypesAsync(),
                    Locations = await _service.GetEventLocationsAsync(),
                    TypesFilterApplied = typesFilterApplied ?? 0,
                    CategoryFilterApplied = categoryFilterApplied ?? 0,
                    LocationFilterApplied = locationFilterApplied ?? 0
                };
                vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";
                vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
            
            return View(vm);
        }
    }
}