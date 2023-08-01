using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Ticket_Management_System_API.Exceptions;
using Ticket_Management_System_API.Models;
using Ticket_Management_System_API.Models.Dto;
using Ticket_Management_System_API.Repositories;

namespace Ticket_Management_System_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ITicketCategoryRepository _ticketCategoryRepository;
        private readonly IMapper _mapper;

        public OrderController(IOrderRepository orderRepository, IMapper mapper, ITicketCategoryRepository ticketCategoryRepository)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
            _ticketCategoryRepository = ticketCategoryRepository;
        }

        [HttpGet]
        public ActionResult<List<Order>> GetAll()
        {
            var orders = _orderRepository.GetAll();

            var dtoOrders = orders.Select(o => _mapper.Map<OrderDto>(o));

            return Ok(dtoOrders);
        }

        [HttpGet]
        public async Task<ActionResult<OrderDto>> GetById(int id)
        {
            var order = await _orderRepository.GetById(id);

            if (order == null)
            {
                throw new EntityNotFoundException(id, nameof(Order));
            }

            var orderDto = _mapper.Map<OrderDto>(order);

            return Ok(orderDto);
        }

        [HttpPatch]
        public async Task<ActionResult<OrderPatchDto>> Patch(OrderPatchDto orderPatchDto)
        {
            var orderEntity = await _orderRepository.GetById(orderPatchDto.OrderId);


            if (orderEntity == null)
            {
                throw new EntityNotFoundException(orderPatchDto.OrderId, nameof(Order));
            }

            if (orderPatchDto.NumberOfTickets != 0) orderEntity.NumberOfTickets = orderPatchDto.NumberOfTickets;

            if (orderPatchDto.TicketCategoryId != 0) orderEntity.TicketCategoryId = orderPatchDto.TicketCategoryId;

            var ticketCategory = await _ticketCategoryRepository.GetById(orderEntity.TicketCategoryId);

            orderEntity.TotalPrice = orderPatchDto.NumberOfTickets * ticketCategory.Price;

            _orderRepository.Update(orderEntity);

            return NoContent();
        }


        [HttpDelete]
        public async Task<ActionResult> DeleteById(int id)
        {
            var orderEntity = await _orderRepository.GetById(id);
            if (orderEntity == null)
            {
                throw new EntityNotFoundException(id, nameof(Order));
            }

            _orderRepository.Delete(orderEntity);

            return NoContent();
        }
    }
}
