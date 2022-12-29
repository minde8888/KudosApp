
namespace Kudos.Domain.Exceptions
{
    public class KudoReasonEnumNotFoundException : Exception
    {
        public KudoReasonEnumNotFoundException() : base("Reason value is wrong")
        { }
    }
}
