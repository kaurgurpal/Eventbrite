using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishListApi.Models
{
    public class FavouriteEvent
    {
        public string Id { get; set; }
        public string EventId { get; set; }
        public string EventName { get; set; }
        public decimal TicketPrice { get; set; }
        public decimal OldTicketPrice { get; set; }
        public int NumberOfTickets { get; set; }
        public string PictureUrl { get; set; }
    }
}
