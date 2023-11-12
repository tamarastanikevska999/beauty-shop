using System.Runtime.Serialization;
using Core.Entities;
using Core.Util;

namespace Core.Util
{
    [Serializable]
    [DataContract(Name = "paged-products-list", Namespace = "products")]
    public class PagedProductList : PagedList
    {
        [DataMember(Name = "products", EmitDefaultValue = false)]
        public System.Collections.Generic.List<Product> Items { get; set; }
    }
}