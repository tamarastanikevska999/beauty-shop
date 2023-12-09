using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Identity;

namespace Core.Entities
{
    public class CustomerBasket
    {
        public CustomerBasket()
        {
            Items = new List<BasketItem>();
        }
        [Key]
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public virtual ICollection<BasketItem> Items { get; set; }
    }
}