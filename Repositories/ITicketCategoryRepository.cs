using Ticket_Management_System_API.Models;

namespace Ticket_Management_System_API.Repositories
{
    public interface ITicketCategoryRepository
    {
        Task<TicketCategory> GetById(int id);
    }
}
