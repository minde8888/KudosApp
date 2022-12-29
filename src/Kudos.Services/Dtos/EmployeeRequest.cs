using System.ComponentModel.DataAnnotations;


namespace Kudos.Services.Dtos
{
    public class EmployeeRequest
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
    }
}
