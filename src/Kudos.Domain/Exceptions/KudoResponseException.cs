
namespace Kudos.Domain.Exceptions
{
    public class KudoResponseException : Exception
    {
        public KudoResponseException() : base("date not fount in the database")
        { }
    }
}
