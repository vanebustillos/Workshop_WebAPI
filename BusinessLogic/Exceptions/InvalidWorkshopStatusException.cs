using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace BusinessLogic.Exceptions
{
    public class InvalidWorkshopStatusException : Exception
    {
        public int StatusCode { get; set; }
        public InvalidWorkshopStatusException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}