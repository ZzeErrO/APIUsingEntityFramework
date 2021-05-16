using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace CommonLayer.Models
{
    // Custom exception class for throwing application specific exceptions (e.g. for validation) 
    // that can be caught and handled within the application
    public class FundooCustomExceptions : Exception
    {
        public enum ExceptionType
        {
            USER_EXIST,
            INVALID_EMAIL
        }

        private readonly ExceptionType type;
        public FundooCustomExceptions(ExceptionType Type, String message) : base(message)
        {
            this.type = Type;
        }
    }
}
