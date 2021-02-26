using System;
using System.Collections.Generic;
using System.Text;

namespace Database.Exceptions
{
    public class EmptyDatabaseException : Exception
    {
        public EmptyDatabaseException(string message) : base(message)
        {
        }
    }
}
