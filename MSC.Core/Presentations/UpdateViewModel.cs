using NHibernate.Criterion;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Core.Presentations
{
    /// <summary>
    /// مدل ورودی برای به روز رسانی حقوق و مزایا
    /// </summary>
    public class UpdateViewModel
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
        /// ماه مورد نظر برای به‌روزرسانی
        /// </summary>
        public string Month { get; set; }

        /// <summary>
        /// حقوق پایه کارمند
        /// </summary>
        public int? BasicSalary { get; set; }

        /// <summary>
        /// مزایای کارمند
        /// </summary>
        public int? Allowance { get; set; }

        /// <summary>
        /// هزینه حمل و نقل
        /// </summary>
        public int? Transportation { get; set; }

        /// <summary>
        /// روش محاسبه حقوق و دستمزد
        /// </summary>
        public string? OverTimeCalculatorMethod { get; set; }

    }
}
