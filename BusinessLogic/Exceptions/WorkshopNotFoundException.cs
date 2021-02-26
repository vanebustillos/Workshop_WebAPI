using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Exceptions
{
    public class WorkshopNotFoundException : Exception
    {
        public int StatusCode { get; set; }
        public WorkshopNotFoundException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
