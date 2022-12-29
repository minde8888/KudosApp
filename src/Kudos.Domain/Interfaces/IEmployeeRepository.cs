using Kudos.Domain.Entities;

namespace Kudos.Domain.Interfaces;

public interface IEmployeeRepository
{
    public Task<Employee> Add(Employee employee);
    public Task<List<Employee>> GetAll();
}
