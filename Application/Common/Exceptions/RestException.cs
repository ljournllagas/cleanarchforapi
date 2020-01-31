using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Common.Exceptions
{
    public class RestException : Exception
    {
        public RestException(HttpStatusCode code, string message, object errors = null)
        {
            Code = code;
            Errors = errors;
            Response = code.ToString();
            Message = message;
        }

        public HttpStatusCode Code { get; }
        public object Errors { get; }
        public string Response { get; set; }
        public string Message { get; set; }
        public string ErrorDateTime { get; set; } = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fffffff");
    }
}
