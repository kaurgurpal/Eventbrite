using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WishListApi.Models
{
    public class WishList
    {
        public string BuyerId { get; set; }
        public List<FavouriteEvent> FavouriteEvents { get; set; }

        public WishList(string WishListId)
        {
            BuyerId = WishListId;
            FavouriteEvents = new List<FavouriteEvent>();
        }


    }
}
