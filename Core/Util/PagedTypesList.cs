using System.Runtime.Serialization;
using Core.Entities;
using Core.Util;

namespace Core.Util
{
    public class PagedTypesList: PagedList
    {
        [DataMember(Name = "types", EmitDefaultValue = false)]
        public System.Collections.Generic.List<ProductType> Items { get; set; }
    }
}