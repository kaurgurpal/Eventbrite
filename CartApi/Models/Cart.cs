using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CartApi.Models
{
    public class Cart
    {
        public string BuyerId { get; set; }
        public List<CartItem> TicketItems { get; set; }
        public Cart(string cartId)
        {
            BuyerId = cartId;
            TicketItems = new List<CartItem>();
        } 

    }
}
