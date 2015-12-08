using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevMikroblog.Domain.Model
{
    public class Result
    {
        public virtual bool IsSuccess { get; set; } = false;
        public virtual bool IsError { get; set; } = false;
        public virtual bool IsWarning { get; set; } = false;
        public virtual List<string> Messages { get; set; }

        public static List<string> CreateMessagesList(params string[] messages)
        {
            return messages.ToList();
        }
    }

    public class Result<T> : Result
    {
        private T _value;

        public T Value
        {
            get { return _value; }
            set
            {
                _value = value;
                IsSuccess = true;
            }
        }

        public static Result<T> ErrorWhenNoData(T data, List<string> messages = null)
        {
            var result = new Result<T>();

            if (data == null)
            {
                result.IsError = true;
                result.Messages = messages;

            }
            else
            {
                result.IsSuccess = true;
                result.Value = data;
            }
            return result;
        }

        public static Result<T> WarningWhenNoData(T data, List<string> messages = null)
        {
            var result = new Result<T>();

            if (data == null)
            {
                result.IsWarning = true;
                result.Messages = messages;

            }
            else
            {
                result.IsSuccess = true;
                result.Value = data;
            }
            return result;
        }
    }
}
