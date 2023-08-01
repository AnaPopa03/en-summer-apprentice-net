using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Ticket_Management_System_API.Exceptions;
using Ticket_Management_System_API.Models;
using Ticket_Management_System_API.Models.Dto;
using Ticket_Management_System_API.Repositories;

namespace Ticket_Management_System_API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IEventRepository _eventRepository;
        private readonly IMapper _mapper;
        private readonly ILogger _logger;

        public EventController(IEventRepository eventRepository, IMapper mapper)
        {
            _eventRepository = eventRepository;
            _mapper = mapper;
            //_logger = logger;
        }

        [HttpGet]
        public ActionResult<List<EventDto>> GetAll()
        {
            var events = _eventRepository.GetAll();

            var dtoEvents = _mapper.Map<List<EventDto>>(events);

            return Ok(dtoEvents);
        }

        [HttpGet]
        public async Task<ActionResult<EventDto>> GetById(int id)
        {

            var @event = await _eventRepository.GetById(id);

            var eventsDto = _mapper.Map<EventDto>(@event);

            if (eventsDto == null)
            {
                return NotFound();
            }

            return Ok(eventsDto);
        }

        [HttpPatch]
        public async Task<ActionResult<EventPatchDto>> Patch(EventPatchDto eventPatch)
        {
            if (eventPatch == null) throw new ArgumentNullException(nameof(eventPatch));
            var eventEntity = await _eventRepository.GetById(eventPatch.EventId);

            if (eventEntity == null)
            {
                throw new EntityNotFoundException(eventPatch.EventId, nameof(Event));
            }

            if (!eventPatch.EventName.IsNullOrEmpty()) eventEntity.EventName = eventPatch.EventName;
            if (!eventPatch.EventDescription.IsNullOrEmpty()) eventEntity.EventDescription = eventPatch.EventDescription;
            _eventRepository.Update(eventEntity);

            return NoContent();

        }

        [HttpDelete]
        public async Task<ActionResult> DeleteById(int id)
        {
            var eventEntity = await _eventRepository.GetById(id);

            if (eventEntity == null)
            {
                throw new EntityNotFoundException(id, nameof(Event));
            }

            _eventRepository.Delete(eventEntity);

            return NoContent();
        }
    }
}
