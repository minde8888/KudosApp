using Kudos.Data.Context;
using Kudos.Domain.Entities;
using Kudos.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Kudos.Data.Repositories
{
    public class KudoRepository : IKudoRepository
    {
        private readonly AppDbContext _context;

        public KudoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Kudo> AddAsync(Kudo kudo)
        {
            if (!_context.Employee.Any(x => x.Id == kudo.SenderId) &&
            _context.Employee.Any(x => x.Id == kudo.ReceiverId)) return null;

            if (_context.Kudo.Any(x => x.SenderId == kudo.SenderId &&
            x.ReceiverId == kudo.ReceiverId)) return null;

            _context.Kudo.Add(kudo);
            await _context.SaveChangesAsync();

            return kudo;
        }

        public async Task<List<Kudo>> FilterAll(int? senderId, int? receivedId)
        {
            var kudos = _context.Kudo.AsQueryable();

            if (senderId.HasValue)
            {
                kudos = kudos.Where(k => k.SenderId == senderId);
            }

            if (receivedId.HasValue)
            {
                kudos = kudos.Where(k => k.ReceiverId == receivedId);
            }

            return await kudos.ToListAsync();
        }

        public async Task<Kudo> ExchangeAsync(Kudo kudo)
        {
            if (!_context.Employee.Any(x => x.Id == kudo.SenderId) &&
            _context.Employee.Any(x => x.Id == kudo.ReceiverId)) return null;

            var receivedKudo = await _context.Kudo.
                Where(x => x.SenderId == kudo.ReceiverId && x.ReceiverId == kudo.SenderId).FirstOrDefaultAsync();

            if (_context.Kudo.Where(x => x.SenderId == kudo.SenderId && x.ReceiverId == kudo.ReceiverId).Any()) return null;

            receivedKudo.Exchanged = true;
            _context.Entry(receivedKudo).State = EntityState.Modified;

            kudo.Exchanged = true;
            _context.Kudo.Add(kudo);
            await _context.SaveChangesAsync();
            return kudo;
        }

        public async Task<List<Kudo>> TotalKudosMonthAsync(DateTime mindate, DateTime maxdate)
        {
            var baseQuery = await _context.Kudo.Where(x => x.DateCreated >= mindate && x.DateCreated < maxdate).ToListAsync();

            if (!baseQuery.Any()) return null;

            return baseQuery;
        }
    }
}
