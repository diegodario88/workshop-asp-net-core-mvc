using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesWebMvc.Models;
using SalesWebMvc.Services;
using SalesWebMvc.Models.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Controllers
{
    public class SellersController : Controller
    {
        //Propriedades
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        //Construtor
        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        // GET: Sellers
        public async Task<IActionResult> Index()
        {
            var list = await _sellerService.FindAllAsync();
            return View(list);
        }

        // GET: Departments/Details
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var seller = await _sellerService.FindByIdAsync(id.Value);
            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(seller);
        }

        // GET: Sellers/Create
        public async Task<IActionResult> Create()
        {
            List<Department> departments = await _departmentService.FindAllAsync();
            SellerFormViewModel viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        // POST: Sellers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Seller seller)
        {
            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Departments = departments, Seller = seller };
                return View(viewModel);

            }

            await _sellerService.InsertAsync(seller);
            return RedirectToAction(nameof(Index));

        }

        // GET: Sellers/Edit
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var editedSeller = _sellerService.FindByIdAsync(id.Value);
            var departments = await _departmentService.FindAllAsync();
            var viewModel = new SellerFormViewModel { Departments = departments, Seller = await editedSeller };

            if (editedSeller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(viewModel);
        }

        //POST: Sellers/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Seller seller)
        {

            if (!ModelState.IsValid)
            {
                var departments = await _departmentService.FindAllAsync();
                var viewModel = new SellerFormViewModel { Departments = departments, Seller = seller };
                return View(viewModel);

            }

            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            try
            {
                await _sellerService.UpdateAsync(seller);
                return RedirectToAction(nameof(Index));
            }

            catch (ApplicationException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }

        // GET: Sellers/Delete
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var removedSeller = await _sellerService.FindByIdAsync(id.Value);
            if (removedSeller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(removedSeller);
        }

        // POST: Sellers/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _sellerService.RemoveAsync(id);
                return RedirectToAction(nameof(Index));
            }
            catch (IntegrityException e)
            {
                return RedirectToAction(nameof(Error), new { message = e.Message });
            }

        }

        //Error
        public IActionResult Error(string message)
        {
            var viewModel = new ErrorViewModel
            {
                Message = message,
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            };

            return View(viewModel);
        }

    }
}