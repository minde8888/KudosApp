using Kudos.Domain.Entities;

namespace Kudos.Domain.Interfaces
{
    public interface IKudoRepository
    {
        public Task<Kudo> AddAsync(Kudo kudo);
        public Task<List<Kudo>> FilterAll(int? senderId, int? receivedId);
        public Task<Kudo> ExchangeAsync(Kudo kudo);
        public Task<List<Kudo>> TotalKudosMonthAsync(DateTime mindate, DateTime maxdate);
    }
}
