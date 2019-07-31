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
        private readonly SellerService _sellerService;
        private readonly DepartmentService _departmentService;

        public SellersController(SellerService sellerService, DepartmentService departmentService)
        {
            _sellerService = sellerService;
            _departmentService = departmentService;
        }

        // GET: Sellers
        public IActionResult Index()
        {
            var list = _sellerService.FindAll();
            return View(list);
        }

        // GET: Departments/Details
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var seller = _sellerService.FindById(id.Value);
            if (seller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(seller);
        }

        // GET: Sellers/Create
        public IActionResult Create()
        {
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments };
            return View(viewModel);
        }

        // POST: Sellers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Seller seller)
        {
            if (ModelState.IsValid)
            {
                _sellerService.Insert(seller);
                return RedirectToAction(nameof(Index));
            }
            return View(seller);
        }

        // GET: Sellers/Edit
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var editedSeller = _sellerService.FindById(id.Value);
            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments, Seller = editedSeller };

            if (editedSeller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(viewModel);
        }

        //POST: Sellers/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Seller seller)
        {
            if (id != seller.Id)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }

            var departments = _departmentService.FindAll();
            var viewModel = new SellerFormViewModel { Departments = departments, Seller = seller };

            if (ModelState.IsValid)
            {
                try
                {
                    _sellerService.Update(seller);
                    return RedirectToAction(nameof(Index));
                }

                catch (ApplicationException e)
                {
                    if (!_sellerService.SellerExists(seller.Id))
                    {
                        return RedirectToAction(nameof(Error), new { message = e.Message });
                    }

                    else
                    {
                        throw;
                    }

                }

            }

            return View(viewModel);

        }
        // GET: Sellers/Delete
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not provided" });
            }
            var removedSeller = _sellerService.FindById(id.Value);
            if (removedSeller == null)
            {
                return RedirectToAction(nameof(Error), new { message = "Id not found" });
            }
            return View(removedSeller);
        }

        // POST: Sellers/Delete
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            return RedirectToAction(nameof(Index));
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