using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Core.Util
{
        public interface ISortable
    {
        //
        // Summary:
        //     Sort order - direction (ASC or DESC).
        [DataMember(Name = "sort-order")]
        string SortOrder { get; set; }
        //
        // Summary:
        //     Name of the column for sorting.
        [DataMember(Name = "sort-type")]
        string SortType { get; set; }
    }

        public class Sorting : ISortable
    {
        public Sorting(string sortOrder, string sortType)
        {
            SortOrder = sortOrder;
            SortType = sortType;
        }
        //
        // Summary:
        //     Sort order - direction (ASC or DESC).
        [DataMember(Name = "sort-order")]
        public string SortOrder { get; set; }
        //
        // Summary:
        //     Name of the column for sorting.
        [DataMember(Name = "sort-by")]
        public string SortType { get; set; }
    }
}