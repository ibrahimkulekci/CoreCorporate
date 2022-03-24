using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Models
{
    public class BaseQueryModel
    {
        // paging
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }

        // sorting
        public string SortOn { get; set; }
        public string SortDirection { get; set; }
    }
}
