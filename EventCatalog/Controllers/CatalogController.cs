using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventCatalogAPI.Data;
using EventCatalogAPI.Domain;
using EventCatalogAPI.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace EventCatalogAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CatalogController : ControllerBase
    {
        private readonly CatalogContext _context;
        private readonly IConfiguration _config;
        public CatalogController(CatalogContext context, IConfiguration config)
        {
            _context = context;
            _config = config;
        }

        //Helper methods to get the events in alphabetical order and returning PaginatedViewModel
        private List<EventsCatalog> EventsByAlphabeticalOrder(IQueryable<EventsCatalog> root,int pageIndex,int pageSize)
        {
            return root.OrderBy(e => e.Name)
                 .Skip(pageIndex * pageSize)
                 .Take(pageSize).ToList();            

        }
        private PaginatedEventsViewModel<EventsCatalog> CreateViewModel(int PageIndex,int PageSize,long Count, List<EventsCatalog> events)
        {
            return new PaginatedEventsViewModel<EventsCatalog> { PageIndex = PageIndex, PageSize = PageSize, Count = Count, Data = events };
        }

        //Getting Events in alphabetic order of Events Name 
        //with given Pagesize and PageIndex
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> Events(
            [FromQuery]int pageIndex = 0,
            [FromQuery]int pageSize = 6)
        {
            var eventsCount = await _context.Events.LongCountAsync();        
            var events = ChangePictureUrl(EventsByAlphabeticalOrder(_context.Events, pageIndex, pageSize));
            return Ok(CreateViewModel(pageIndex, pageSize, eventsCount, events));
        }

        // Filtering Events according to Type, Category and Location

        [HttpGet]
        [Route("[action]/type/{EventTypeId}/category/{EventCategoryId}/location/{LocationId}")]
        public async Task<IActionResult> Events(           
            int? EventTypeId,
            int? EventCategoryId,
            int? LocationId,
           [FromQuery]int pageIndex = 0,
           [FromQuery]int pageSize = 6)
        {
            var root = (IQueryable<EventsCatalog>)_context.Events;
            if (EventTypeId.HasValue && EventTypeId!=null)
            {
                root = root.Where(e => e.EventTypeId == EventTypeId);
            }
            if (EventCategoryId.HasValue && EventCategoryId!=null)
            {
                root = root.Where(e => e.EventCategoryId == EventCategoryId);
            }
            if (LocationId.HasValue && LocationId!=null)
            {
                root = root.Where(e => e.LocationId == LocationId);
            }

            var eventsCount = await root.LongCountAsync();            
            var events = ChangePictureUrl(EventsByAlphabeticalOrder(root, pageIndex, pageSize));      

            return Ok(CreateViewModel(pageIndex, pageSize, eventsCount, events));
        }
        //Events filtered on location
        [HttpGet]
        [Route("[action]/location/{LocationId}")]
        public async Task<IActionResult> EventsbyLocation(int? LocationId, 
           [FromQuery]int pageIndex = 0,
           [FromQuery]int pageSize = 6)
        {
            var root = (IQueryable<EventsCatalog>)_context.Events;
            if (LocationId.HasValue && LocationId != null)
            {
                root = root.Where(e => e.LocationId == LocationId);
            }
            var eventsCount = await root.LongCountAsync();

            var events = ChangePictureUrl(EventsByAlphabeticalOrder(root, pageIndex, pageSize));
            return Ok(CreateViewModel(pageIndex, pageSize, eventsCount, events));
        }

        //Events filtered by category
        [HttpGet]
        [Route("[action]/category/{EventCategoryId}")]
        public async Task<IActionResult> EventsbyCategory(int? EventCategoryId,
                   [FromQuery]int pageIndex = 0,
                   [FromQuery]int pageSize = 6)
        {
            var root = (IQueryable<EventsCatalog>)_context.Events;
            if (EventCategoryId.HasValue && EventCategoryId != null)
            {
                root = root.Where(e => e.EventCategoryId == EventCategoryId);
            }
            var eventsCount = await root.LongCountAsync();

            var events = ChangePictureUrl(EventsByAlphabeticalOrder(root, pageIndex, pageSize));
            return Ok(CreateViewModel(pageIndex, pageSize, eventsCount, events));
        }

        //Events filtered by Type
        [HttpGet]
        [Route("[action]/type/{EventTypeId}")]
        public async Task<IActionResult> EventsbyType(int? EventTypeId,
                   [FromQuery]int pageIndex = 0,
                   [FromQuery]int pageSize = 6)
        {
            var root = (IQueryable<EventsCatalog>)_context.Events;
            if (EventTypeId.HasValue && EventTypeId != null)
            {
                root = root.Where(e => e.EventTypeId == EventTypeId);
            }
            var eventsCount = await root.LongCountAsync();

            var events = ChangePictureUrl(EventsByAlphabeticalOrder(root, pageIndex, pageSize));
            return Ok(CreateViewModel(pageIndex, pageSize, eventsCount, events));
        }

        //Events filtered by TypeId and CategoryId
        [HttpGet]
        [Route("[action]/type/{EventTypeId}/category/{EventCategoryId}")]
        public async Task<IActionResult> EventsbyTypeandCategory(
           int? EventTypeId,
           int? EventCategoryId,           
          [FromQuery]int pageIndex = 0,
          [FromQuery]int pageSize = 6)
        {
            var root = (IQueryable<EventsCatalog>)_context.Events;
            if (EventTypeId.HasValue && EventTypeId != null)
            {
                root = root.Where(e => e.EventTypeId == EventTypeId);
            }
            if (EventCategoryId.HasValue && EventCategoryId != null)
            {
                root = root.Where(e => e.EventCategoryId == EventCategoryId);
            }
            var eventsCount = await root.LongCountAsync();
            var events = ChangePictureUrl(EventsByAlphabeticalOrder(root, pageIndex, pageSize));

            return Ok(CreateViewModel(pageIndex, pageSize, eventsCount, events));
        }

        // Event API for adding new event
        // Using HttpPost to get new event information
        [HttpPost]
        [Route("[action]")]
        public async Task<ActionResult<EventsCatalog>> CreateEvent(EventsCatalog eventsCatalog)
        {
            try
            {
                _context.Events.Add(eventsCatalog);
                await _context.SaveChangesAsync();

                // calling "Events" api with new Event's id to get the Event object
                return CreatedAtAction(nameof(Events), new { EventId = eventsCatalog.Id }, eventsCatalog);
            }
            catch(Exception)
            {
                return BadRequest("Event not created !!!");
            }
            
        }
        // Changing Picture Url
        private List<EventsCatalog> ChangePictureUrl(List<EventsCatalog> events)
        {
            events.ForEach(
                c => c.PictureUrl = c.PictureUrl
                       .Replace("http://externalcatalogbaseurltobereplaced",
                       _config["ExternalCatalogBaseUrl"]));

            return events;
        }

        //Getting all the Event Types
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> EventTypes()
        {
            var events = await _context.EventTypes.ToListAsync();
            return Ok(events);
        }

        //Getting all the Event Categories
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> EventCategories()
        {
            var events = await _context.EventCategories.ToListAsync();
            return Ok(events);
            
        }

        //Getting all the Event Loacations
        [HttpGet]
        [Route("[action]")]
        public async Task<IActionResult> EventLocations()
        {
            var events = await _context.Locations.ToListAsync();
            return Ok(events);
        }

        //Update an Event method
       [HttpPut]
       [Route("[action]")]
        public async Task<IActionResult> UpdateEvent([FromBody] EventsCatalog eventObj)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    if (_context != null)
                    {
                        _context.Events.Update(eventObj);
                        await _context.SaveChangesAsync();
                        return Ok();
                    }
                  return NotFound();
                }
                catch (Exception ex)
                {
                    if (ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }
                    return BadRequest();
                }
            }
            else
            {
                return BadRequest();
            }
            
        }

        //Delete an Event method
        [HttpDelete]
        [Route("[action]")]
        public async Task<IActionResult> DeleteEvent(int id)
        {
            try
            {
                var itemObj = await _context.Events.FindAsync(id);
                if (itemObj == null)
                {
                    return NotFound();
                }
                _context.Events.Remove(itemObj);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}