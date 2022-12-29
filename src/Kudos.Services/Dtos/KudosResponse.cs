
namespace Kudos.Services.Dtos
{
    public class KudosResponse
    {
        public bool IsSuccessfull { get; set; } = true;
        public List<string> Error { get; set; }
        public List<KudoResult> KudoResult { get; set; }
    }
}
