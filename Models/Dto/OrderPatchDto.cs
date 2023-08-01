namespace Ticket_Management_System_API.Models.Dto
{
    public class OrderPatchDto
    {
        public int OrderId { get; set; }

        public int TicketCategoryId { get; set; }

        public int NumberOfTickets { get; set; }


    }
}
