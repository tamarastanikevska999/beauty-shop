using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class ProductReview
    {
        public Product Product { get; set; }
        public string Comment { get; set; }
        public int Grade { get; set; }
    }
}