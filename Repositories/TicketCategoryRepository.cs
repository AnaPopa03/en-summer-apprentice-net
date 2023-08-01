using Microsoft.EntityFrameworkCore;
using Ticket_Management_System_API.Exceptions;
using Ticket_Management_System_API.Models;
using Ticket_Management_System_API.Models.Dto;

namespace Ticket_Management_System_API.Repositories
{
    public class TicketCategoryRepository : ITicketCategoryRepository
    {
        private readonly TicketManagementSystemContext _dbContext;

        public TicketCategoryRepository()
        {
            _dbContext = new TicketManagementSystemContext();
        }
        public async Task<TicketCategory> GetById(int id)
        {
            var ticketCategory = _dbContext.TicketCategories.Where(tc => tc.TicketCategoryId == id).FirstOrDefault();
            if (ticketCategory == null)
            {
                throw new EntityNotFoundException(id, nameof(TicketCategory));
            }
            return ticketCategory;
        }

    }
}
