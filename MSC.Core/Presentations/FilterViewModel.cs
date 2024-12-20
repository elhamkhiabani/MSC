using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Core.Presentations
{
    public class FilterViewModel
    {
        /// <summary>
        /// نام کارمند
        /// </summary>
        public string FirstName { get; set; }
        /// <summary>
        /// نام خانوادگی کارمند
        /// </summary>
        public string LastName { get; set; }
        /// <summary>
        /// ماه/تاریخ مورد نظر
        /// </summary>
        public string? FromDate { get; set; }
        /// <summary>
        /// ماه/تاریخ مورد نظر
        /// </summary>
        public string? ToDate { get; set; }
        /// <summary>
        /// شماره صفحه
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// تعداد نمایش در هر صفحه
        /// </summary>
        public int PageSize { get; set; }
    }
}
