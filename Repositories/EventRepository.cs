using Microsoft.EntityFrameworkCore;
using Ticket_Management_System_API.Models;

namespace Ticket_Management_System_API.Repositories
{
    public class EventRepository : IEventRepository
    {
        private readonly TicketManagementSystemContext _dbContext;

        public EventRepository()
        {
            _dbContext = new TicketManagementSystemContext();
        }

        public int Add(Event @event)
        {
            throw new NotImplementedException();
        }

        public void Delete(Event @event)
        {
            _dbContext.Remove(@event);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Event> GetAll()
        {
            var events = _dbContext.Events;
            return events;
        }

        public async Task<Event> GetById(int id)
        {
            var @event = await _dbContext.Events.Where(e => e.EventId == id).FirstOrDefaultAsync();
            return @event;
        }

        public void Update(Event @event)
        {
            _dbContext.Entry(@event).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
