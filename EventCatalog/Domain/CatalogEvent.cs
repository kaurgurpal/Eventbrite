using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EventCatalogAPI.Domain
{
    public class CatalogEvent
    {
        public int Id { get; set; }
        public string Title { get; set; }

        public int LocationId { get; set; }
        public virtual EventLocation EventLocation { get; set; }
        public bool ShowMap { get; set; }
        public DateTime StartDateTime { get; set; }
        public DateTime EndDateTime { get; set; }
        public string ImageUrl { get; set; }
        public string Description { get; set; }
        public string OrganizerName { get; set; }
        public string OrganizerDescription { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }

        public int EventTypeId { get; set; }
        public virtual CatalogType EventType { get; set; }

        public int EventCatagoryId { get; set; }
        public virtual CatalogCategory EventCategory { get; set; }

        public decimal Price { get; set; }

    }
}
