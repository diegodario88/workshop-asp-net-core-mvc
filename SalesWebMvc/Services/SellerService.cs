using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using SalesWebMvc.Services.Exceptions;

namespace SalesWebMvc.Services
{
    public class SellerService
    {
        //Propriedade
        private readonly SalesWebMvcContext _context;

        //Construtor
        public SellerService(SalesWebMvcContext context) => _context = context;

        //Buscar Todos
        public async Task<List<Seller>> FindAllAsync() => await _context.Seller.OrderBy(x => x.Name).ToListAsync();

        //Inserir na BD
        public async Task InsertAsync(Seller seller)
        {

           _context.Seller.Add(seller);
           await _context.SaveChangesAsync();

        }

        //Buscar por ID
        public async Task<Seller> FindByIdAsync(int id) => await _context.Seller.Include(obj => obj.Department)
            .FirstOrDefaultAsync(s => s.Id == id);

        //Remover
        public async Task RemoveAsync(int id)
        {
            try
            {
                var obj = await _context.Seller.FindAsync(id);
                _context.Seller.Remove(obj);
                await _context.SaveChangesAsync();
            }

            catch (DbUpdateException)
            {
                throw new IntegrityException("Can't delete seller because he/she has sales");
            }
        }

        //Verificar se Existe
        public async Task<bool> SellerExists(int id) => await _context.Seller.AnyAsync(s => s.Id == id);

        //Atualizar 
        public async Task UpdateAsync(Seller seller)
        {
            bool hasAny = await SellerExists(seller.Id);

            if (!hasAny)
            {
                throw new NotFoundException("Id not found"); 
            }
            try
            {
                _context.Update(seller);
                await _context.SaveChangesAsync();
            }
            catch (DbConcurrencyException e)
            {
                throw new DbConcurrencyException(e.Message);
            }
            
        }


    }
}
