using Ticket_Management_System_API.Models;

namespace Ticket_Management_System_API.Repositories
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetAll();

        Task<Order> GetById(int id);

        void Update(Order order);

        void Delete(Order order);


    }
}
