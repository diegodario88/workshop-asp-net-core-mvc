using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;

namespace SalesWebMvc.Controllers
{
    public class SalesRecordsController : Controller
    {
        private readonly SalesRecordsService _salesRecordsService;

        public SalesRecordsController(SalesRecordsService salesRecordsService)
        {
            _salesRecordsService = salesRecordsService;
        }

        public async Task<IActionResult> Index()
        {
            var list = await _salesRecordsService.FindAllAsync();
            return View(list);
        }

        public async Task<IActionResult> SimpleSearch(Seller seller)
        {
           
            return View();
        }

        public async Task<IActionResult> GroupingSearch(Seller seller)
        {
            return View();
        }
    }
}