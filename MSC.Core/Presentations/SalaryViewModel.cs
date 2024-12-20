using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Core.Presentations
{
    public class SalaryViewModel
    {
        /// <summary>
        /// شناسه
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// تاریخ به صورت کلید
        /// </summary>
        public int  CalenderDateID { get; set; }

        /// <summary>
        /// نام کارمند
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// نام خانوادگی کارمند
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// حقوق پایه
        /// </summary>
        public int BasicSalary { get; set; }

        /// <summary>
        /// مزایای کارمند
        /// </summary>
        public int Allowance { get; set; }

        /// <summary>
        /// حق ایاب ذهاب
        /// </summary>
        public int Transportation { get; set; }

        /// <summary>
        /// تاریخ شمسی
        /// </summary>
        public string Date { get; set; }

        /// <summary>
        /// ساعت درج اطلاعات
        /// </summary>
        public string Time { get; set; }

        /// <summary>
        /// نحوه محاسبه حقوق و دستمزد
        /// </summary>
        public string OverTimeCalculatorMethod { get; set; }

        /// <summary>
        /// مبلغ محاسبه شد حقوق و دستمزد
        /// </summary>
        public int SalaryAmount { get; set; }
        /// <summary>
        /// وضعیت فیلد
        /// </summary>
        public bool IsActive { get; set; }
    }
}

