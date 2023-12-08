using Microsoft.AspNetCore.Identity;

namespace Core.Entities.Identity
{
    public class ShopUser : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
         public CustomerBasket Basket { get; set; }
    }
}
