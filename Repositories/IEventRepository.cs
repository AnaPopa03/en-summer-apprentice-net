using Ticket_Management_System_API.Models;

namespace Ticket_Management_System_API.Repositories
{
    public interface IEventRepository
    {
        IEnumerable<Event> GetAll(); 

        Task<Event> GetById(int id);

        int Add(Event @event);

        void Update(Event @event);

        void Delete(Event @event);
    }
}
