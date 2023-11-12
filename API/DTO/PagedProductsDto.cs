using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;
using Core.Util;

namespace API.DTO
{
    public class PagedProductsDto: PagedList
    {
        [DataMember(Name = "products", EmitDefaultValue = false)]
        public System.Collections.Generic.List<ProductDto> Items { get; set; }
    }
}