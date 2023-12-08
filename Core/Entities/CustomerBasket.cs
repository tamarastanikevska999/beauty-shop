using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;

namespace Core.Entities
{
    public class CustomerBasket
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public ShopUser User { get; set; }
        public List<BasketItem> Items { get; set; }
    }
}