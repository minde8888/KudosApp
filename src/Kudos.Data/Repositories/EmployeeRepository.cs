using Kudos.Data.Context;
using Kudos.Domain.Entities;
using Kudos.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kudos.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Employee> Add(Employee employee)
        {
            _context.Add(employee);
            await _context.SaveChangesAsync();

            return employee;
        }

        public async Task<List<Employee>> GetAll()
        {
            return await _context.Employee.ToListAsync();
        }
    }
}
