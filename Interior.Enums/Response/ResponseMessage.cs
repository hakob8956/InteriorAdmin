using System;
using System.Collections.Generic;
using System.Text;

namespace Interior.Enums
{
    public class ResponseMessage
    {
        public static ResponseMessage Create(string message, ResultCode code)
        {
            var response = new ResponseMessage();
            response.message = message;
            response.status = code;
            return response;
        }
        public ResultCode status { get; set; }
        public string message { get; set; }

    }
}
