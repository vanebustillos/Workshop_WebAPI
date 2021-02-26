using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BusinessLogic.Exceptions
{
    public class InvalidWorkshopNameException : Exception
    {
        public int StatusCode { get; set; }
        public InvalidWorkshopNameException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
