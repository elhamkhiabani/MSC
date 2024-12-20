using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Core.Presentations.Base
{
    public class ResultViewModel<T>
    {
        /// <summary>
        /// خروجی به صورت تکی
        /// </summary>
        public T Result { get; set; }

        /// <summary>
        /// خروجی به صورت لیست
        /// </summary>
        public List<T> List { get; set; }

        /// <summary>
        /// پیام بازگشت داده شده
        /// </summary>
        public MessageViewModel Message { get; set; }
    }
}
