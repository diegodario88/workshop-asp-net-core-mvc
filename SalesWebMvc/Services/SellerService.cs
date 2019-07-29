using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;

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


    }
}
