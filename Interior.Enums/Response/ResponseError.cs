using System;
using System.Collections.Generic;
using System.Text;

namespace Interior.Enums
{
    public class ResponseError : Response
    {
        public Error Error { get; set; }
        public static ResponseError Create(string displayMessage, string internalMessage = null)
        {
            var response = new ResponseError();
            response.Success = false;
            response.Error = new Error()
            {
                InternalMessage = internalMessage,
                DisplayMessage = displayMessage
            };
            return response;
        }
    }
}
