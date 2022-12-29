
namespace Kudos.Domain.Exceptions
{
    public class EmployeeNotFoundException : Exception
    {
        public EmployeeNotFoundException() : base("Employee was not found")
        { 
        }
    }
}
