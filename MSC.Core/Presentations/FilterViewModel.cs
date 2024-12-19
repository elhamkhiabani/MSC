using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Core.Presentations
{
    public class FilterViewModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? FromDate { get; set; }
        public string? ToDate { get; set; }
        public int PageSize { get; set; }
        public int PageNumber { get; set; }
    }
}
