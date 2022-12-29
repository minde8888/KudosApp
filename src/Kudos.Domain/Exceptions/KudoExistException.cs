

namespace Kudos.Domain.Exceptions
{
    public class KudoExistException:Exception
    {
        public KudoExistException() : base("Kudo already is given to this user") 
        { }
    }
}
