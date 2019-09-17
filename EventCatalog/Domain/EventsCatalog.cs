using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalog.Domain
{
    public class EventsCatalog
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public string OrganizerName { get; set; }
        public string OrganizerDescription { get; set; }

        //ForiegnKey relation with EventType table
        public int EventTypeId { get; set; }
        public virtual EventType EventType { get; set; }

        //ForiegnKey relation with EventCategory table
        public int EventCategoryId { get; set; }
        public virtual EventCategory EventCategory { get; set; }

        //ForiegnKey relation with Location table
        public int LocationId { get; set; }
        public virtual Location Location { get; set; }

       
    }
}
