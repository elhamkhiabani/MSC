using MSC.Widget.Presentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Widget.InputProcessor
{
    public interface IInputProcessor<T>
    {
        ResultViewModel<T> Process(string dataType, string data);
    }
}
