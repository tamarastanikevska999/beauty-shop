using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Core.Util
{
    [Serializable]
    [DataContract(Name = "paged-list", Namespace = "offers")]
    public partial class PagedList
    {
        /// <summary>Total number of items in collection</summary>
        [DataMember(Name = "total-count", EmitDefaultValue = false)]
        public int? TotalCount { get; set; }

        /// <summary>Size of the page</summary>
        [DataMember(Name = "page-size", EmitDefaultValue = false)]
        public int? PageSize { get; set; }

        /// <summary>Index of current page</summary>
        [DataMember(Name = "page", EmitDefaultValue = false)]
        public int? Page { get; set; }

        /// <summary>Total number of pages of set size</summary>
        [DataMember(Name = "total-pages", EmitDefaultValue = false)]
        public int? TotalPages { get; set; }

        /// <summary>Sort order (`asc` or `desc`). Default is asc</summary>
        [DataMember(Name = "sort-order", EmitDefaultValue = false)]
        // [AllowedValues("asc,desc")]
        public string SortOrder { get; set; }

        /// <summary>Attribute of the collection item to sort by</summary>
        [DataMember(Name = "sort-by", EmitDefaultValue = false)]
        public string SortBy { get; set; }
    }
}