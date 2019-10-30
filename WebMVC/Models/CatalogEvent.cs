using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Models
{
    public class CatalogEvent
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public decimal Price { get; set; }
        public string PictureUrl { get; set; }
        public string OrganizerName { get; set; }
        public string OrganizerDescription { get; set; }


        public int EventTypeId { get; set; }

        //To get the Event Type
        public string EventType { get; set; }

        //ForiegnKey relation with EventCategory table
        public int EventCategoryId { get; set; }

        //To get the Event Category
        public string EventCategory { get; set; }

        //ForiegnKey relation with Location table
        public int LocationId { get; set; }

        //To get the Event Location
        public string Location { get; set; }
    }
}

