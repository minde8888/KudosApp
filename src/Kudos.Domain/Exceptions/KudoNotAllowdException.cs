
namespace Kudos.Domain.Exceptions
{
    public class KudoNotAllowedException : Exception
    {
        public KudoNotAllowedException() : base("Employee id is wrong")
        {
        }
    }
}
