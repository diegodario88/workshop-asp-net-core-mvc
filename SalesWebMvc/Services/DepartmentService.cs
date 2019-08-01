using Microsoft.EntityFrameworkCore;
using SalesWebMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesWebMvc.Services
{
    public class DepartmentService
    {
        private readonly SalesWebMvcContext _context;

        public DepartmentService(SalesWebMvcContext context) => _context = context;

        public async Task<List<Department>> FindAllAsync() => await _context.Department.OrderBy(x => x.Name).ToListAsync();

        public async Task<Department> FindByIdAsync(Department department) => await _context.Department.FindAsync(department.Id);

        public void Remove(Department department)
        {
            _context.Department.Remove(department);
        }

    }
}
