
namespace Kudos.Services.Dtos
{
    public class KudoResponse
    {
        public bool IsSuccessfull { get; set; } = true;
        public List<string> Error { get; set; }
        public KudoResult KudoResult { get; set; }
    }
}
