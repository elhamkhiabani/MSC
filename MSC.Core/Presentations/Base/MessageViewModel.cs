using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Core.Presentations.Base
{
    public class MessageViewModel
    {
        /// <summary>
        /// شناسه
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// وضعیت
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// پیام
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// مقدار
        /// </summary>
        public string Value { get; set; }
    }
}
