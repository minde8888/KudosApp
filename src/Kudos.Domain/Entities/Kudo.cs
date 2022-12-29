using System.ComponentModel.DataAnnotations;

namespace Kudos.Domain.Entities
{
    public class Kudo
    {
        public int Id { get; set; }
        [Required]
        public string Reason { get; set; }
        public string Description { get; set; }
        public bool Exchanged { get; set; } = false;
        [Required]
        public int SenderId { get; set; }
        [Required]
        public int ReceiverId { get; set; }
        public DateTime DateCreated { get; set; } = DateTime.Now;
        public Employee Sender { get; set; }
        public Employee Receiver { get; set; }
    }
}
