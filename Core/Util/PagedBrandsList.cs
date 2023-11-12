using System.Runtime.Serialization;
using Core.Entities;
using Core.Util;

namespace Core.Util
{
    public class PagedBrandsList: PagedList
    {
        [DataMember(Name = "brands", EmitDefaultValue = false)]
        public System.Collections.Generic.List<ProductBrand> Items { get; set; }
    }
}