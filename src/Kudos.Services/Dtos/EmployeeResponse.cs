using Kudos.Domain.Entities;

namespace Kudos.Services.Dtos
{
    public class EmployeeResponse
    {
        public bool IsSuccessfull { get; set; } = true;
        public List<string> Error { get; set; }
        public Employee Employee { get; set; }
    }
}
