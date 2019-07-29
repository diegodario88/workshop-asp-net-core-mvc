using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        private readonly SalesWebMvcContext _context;

        public SellerService(SalesWebMvcContext context) => _context = context;

        public List<Seller> FindAll() => _context.Seller.ToList();

        public void Insert(Seller seller)
        {

            _context.Seller.Add(seller);
            _context.SaveChangesAsync();

        }

        public Seller FindById(int id) => _context.Seller.Include(obj => obj.Department)
            .FirstOrDefault(s => s.Id == id);

        public void Remove(int id)
        {
            var obj = _context.Seller.Find(id);
            _context.Seller.Remove(obj);
            _context.SaveChanges();
        }


    }
}
