using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Widget.Presentations
{
    public class ResultViewModel<T>
    {
        public T Result { get; set; }

        public List<T> List { get; set; }

        public MessageViewModel Message { get; set; }
    }
}
