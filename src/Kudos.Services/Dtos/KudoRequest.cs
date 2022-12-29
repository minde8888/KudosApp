using System.ComponentModel.DataAnnotations;

namespace Kudos.Services.Dtos
{
    public class KudoRequest
    {
        [Required]
        public string Reason { get; set; }
        public string Description { get; set; }
        [Required]
        public int SenderId { get; set; }
        [Required]
        public int ReceiverId { get; set; }
    }
}
