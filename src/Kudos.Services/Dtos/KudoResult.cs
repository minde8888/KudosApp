
namespace Kudos.Services.Dtos
{
    public class KudoResult
    {
        public int Id { get; set; }
        public string Reason { get; set; }
        public string Description { get; set; }
        public bool Exchanged { get; set; } = false;
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
    }
}
