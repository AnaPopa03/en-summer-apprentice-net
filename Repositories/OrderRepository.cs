using Microsoft.EntityFrameworkCore;
using Ticket_Management_System_API.Models;

namespace Ticket_Management_System_API.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly TicketManagementSystemContext _dbContext;

        public OrderRepository()
        {
            _dbContext = new TicketManagementSystemContext();
        }

        public void Delete(Order order)
        {
            _dbContext.Remove(order);
            _dbContext.SaveChanges();
        }

        public IEnumerable<Order> GetAll()
        {
            var orders = _dbContext.Orders;
            return orders;
        }

        public Task<Order> GetById(int id)
        {
            var order = _dbContext.Orders.Where(o => o.OrderId == id).FirstOrDefaultAsync();
            return order;
        }

        public void Update(Order order)
        {
            _dbContext.Entry(order).State = EntityState.Modified;
            _dbContext.SaveChanges();
        }
    }
}
