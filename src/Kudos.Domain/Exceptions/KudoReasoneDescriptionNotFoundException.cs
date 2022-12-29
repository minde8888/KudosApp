
namespace Kudos.Domain.Exceptions
{
    public class KudoReasonDescriptionNotFoundException : Exception
    {
        public KudoReasonDescriptionNotFoundException() : base("reason description not found")
        { }
    }
}
