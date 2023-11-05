using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Core.Util
{
        public interface IPageable
    {
        //
        // Summary:
        //     Page size for paged result.
        [DataMember(Name = "page-size")]
        int PageSize { get; set; }
        //
        // Summary:
        //     Page number for paged result.
        [DataMember(Name = "page-number")]
        int PageNumber { get; set; }
    }
        [DataContract(Name = "paging", Namespace = "common")]
    public class Paging : IPageable
    {
        public Paging(int pageSize, int pageNumber)
        {
            PageSize = pageSize;
            PageNumber = pageNumber;
        }

        //
        // Summary:
        //     Page size for paged result.
        [DataMember(Name = "page-size")]
        [DefaultValue(10)]
        public int PageSize { get; set; }
        //
        // Summary:
        //     Page number for paged result.
        [DataMember(Name = "page-number")]
        [DefaultValue(1)]
        public int PageNumber { get; set; }
    }
}