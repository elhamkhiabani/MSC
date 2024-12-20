using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Core.Presentations
{
    public class RequestSalaryViewModel
    {
        /// <summary>
        /// اطلاعات فرد که به صورت xml/Json/Custom
        /// </summary>
        public string Data { get; set; }

        /// <summary>
        /// نحوه محاسبه حقوق و دستمزد
        /// </summary>
        public string OverTimeCalculator { get; set; }
    }
}
