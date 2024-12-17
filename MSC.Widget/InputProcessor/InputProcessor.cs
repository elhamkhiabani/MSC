using MSC.Widget.Presentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSC.Widget.InputProcessor
{
    public class InputProcessor<T> : IInputProcessor<T>
    {
        public InputProcessor()
        {

        }
        public ResultViewModel<T> Process(string dataType, string data)
        {
            ResultViewModel<T> result = new ResultViewModel<T>();
            switch (dataType)
            {
                case "Json":
                   result= JsonProcess(data);
                    break;
                case "XML":
                    result = XMLProcess(data);
                    break ;
                case "CSV":
                    result = CSVProcess(data);
                    break;
                default:
                    result = CustomeProcess(data);
                    break;
            }
            return result;
        }


        private ResultViewModel<T> JsonProcess(string data)
        {
            ResultViewModel<T> result = new ResultViewModel<T>();

            return result;
        }

        private ResultViewModel<T> XMLProcess(string data)
        {
            ResultViewModel<T> result = new ResultViewModel<T>();

            return result;
        }


        private ResultViewModel<T> CSVProcess(string data)
        {
            ResultViewModel<T> result = new ResultViewModel<T>();

            return result;
        }

        private ResultViewModel<T> CustomeProcess(string data)
        {
            ResultViewModel<T> result = new ResultViewModel<T>();

            return result;
        }
    }
}
