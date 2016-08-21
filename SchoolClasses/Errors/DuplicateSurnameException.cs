using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SchoolClasses.Errors
{
    public class DuplicateSurnameException : Exception
    {
        public DuplicateSurnameException()
        {
        }
        public DuplicateSurnameException(string message) : base(message)
        {
        }
        public DuplicateSurnameException(string message, Exception inner) : base(message,inner)
        {
        }
    }
}