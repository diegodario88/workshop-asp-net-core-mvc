﻿using SalesWebMvc.Models;
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

        public List<Department> FindAll() => _context.Department.OrderBy(x => x.Name).ToList();

        public Department FindById(Department department) => _context.Department.Find(department.Id);

        public void Remove(Department department)
        {
            _context.Department.Remove(department);
        }

    }
}