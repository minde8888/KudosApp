
namespace Kudos.Services.Dtos
{
    public class KudoReport
    {
        public bool IsSuccessfull { get; set; } = true;
        public List<string> Error { get; set; }
        public List<KudoResult> Given { get; set; }
        public List<KudoResult> Received { get; set; }
        public int Total { get; set; }
    }
}
