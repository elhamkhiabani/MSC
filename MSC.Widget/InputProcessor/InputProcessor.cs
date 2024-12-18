using MSC.Widget.Presentations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Xml.Serialization;

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
                    result = JsonProcess(data);
                    break;
                case "XML":
                    result = XMLProcess(data);
                    break;
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
            try
            {
                result.Result = JsonSerializer.Deserialize<T>(data);
                result.Message = new MessageViewModel
                {
                    ID = 0,
                    Message = "Data processed successfully",
                    Status = "Success",
                    Value = ""
                };
            }
            catch (Exception ex)
            {
                result.Message = new MessageViewModel
                {
                    ID = -1000,
                    Message = ex.Message,
                    Status = "Error",
                    Value = ""
                };
                return result;
            }
            return result;
        }

        private ResultViewModel<T> XMLProcess(string data)
        {
            ResultViewModel<T> result = new ResultViewModel<T>();
            try
            {
                using (var stringReader = new StringReader(data))
                {
                    XmlSerializer serializer = new XmlSerializer(typeof(T));
                    if (serializer!=null)
                    {
                        result.Result = (T)serializer.Deserialize(stringReader);
                        result.Message = new MessageViewModel
                        {
                            ID = 0,
                            Message = "Data processed successfully",
                            Status = "Success",
                            Value = ""
                        };
                    }
                   
                }

            }
            catch (Exception ex)
            {
                result.Message = new MessageViewModel
                {
                    ID = -1000,
                    Message = ex.Message,
                    Status = "Error",
                    Value = ""
                };
                return result;
            }
            return result;
        }


        private ResultViewModel<T> CSVProcess(string data)
        {
            ResultViewModel<T> result = new ResultViewModel<T>();
            try
            {

            }
            catch (Exception ex)
            {
                result.Message = new MessageViewModel
                {
                    ID = -1000,
                    Message = ex.Message,
                    Status = "Error",
                    Value = ""
                };
                return result;
            }
            return result;
        }

        private ResultViewModel<T> CustomeProcess(string data)
        {
            ResultViewModel<T> result = new ResultViewModel<T>();
            try
            {
                var lines = data.Split(new[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries);

                if (lines.Length != 2)
                {
                    throw new ArgumentException("The data must contain exactly two lines.");
                }

                // سطر اول عنوان‌ها
                var headers = lines[0].Split('/');

                // سطر دوم مقادیر
                var values = lines[1].Split('/');

                if (headers.Length != values.Length)
                {
                    throw new ArgumentException("The number of headers must match the number of values.");
                }

                var dictionary = new Dictionary<string, string>();
                for (int i = 0; i < headers.Length; i++)
                {
                    dictionary[headers[i].Trim()] = values[i].Trim();
                }

       
                result.Result = (T)(object)dictionary;

                result.Message = new MessageViewModel
                {
                    ID = 0,
                    Message = "Data processed successfully",
                    Status = "Success",
                    Value = ""
                };
            }
            catch (Exception ex)
            {
                result.Message = new MessageViewModel
                {
                    ID = -1000,
                    Message = ex.Message,
                    Status = "Error",
                    Value = ""
                };
            }
            return result;
        }
    }
}
