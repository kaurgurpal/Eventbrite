using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Domain
{
    public class EventsCatalog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string EventImageUrl { get; set; }
        public string OrganizerName { get; set; }
        public string OrganizerDescription { get; set; }
        
        //setting up Foreign key relation with EventCategory
        public int EventCategoryId { get; set; }
        public virtual EventCategory EventCategory { get; set; }

        ////setting up Foreign key relation with EventType
        public int EventTypeId { get; set; }
        public virtual EventType EventType { get; set; }

        //setting up Foreign key relation with EventLocation
        public int EventLocationId { get; set; }
        public virtual EventLocation EventLocation { get; set; }

    }
}
