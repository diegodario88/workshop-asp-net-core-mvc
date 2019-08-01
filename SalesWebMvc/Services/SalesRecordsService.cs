using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class SalesRecordsService
    {
        private readonly SalesWebMvcContext _context;

        public SalesRecordsService(SalesWebMvcContext context) => _context = context;

        //Busca Simples
        public async Task<List<SalesRecord>> FindAllAsync()
        {
            return await _context.SalesRecord.OrderBy(x => x.Seller.Name).ToListAsync();
        }
    }
}
