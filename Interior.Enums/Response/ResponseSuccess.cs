using System;
using System.Collections.Generic;
using System.Text;

namespace Interior.Enums

{
    public class ResponseSuccess : Response
    {
        public object Data { get; set; }
        public object Message { get; set; }
        public static ResponseSuccess Create(object data, string message)
        {
            var response = new ResponseSuccess();
            response.Success = true;
            response.Data = data;
            response.Message = message;
            return response;
        }

        public static ResponseSuccess Create(object data)
        {
            return Create(data, null);
        }

        public static ResponseSuccess Create(string message)
        {
            return Create(null, message);
        }
    }
}
