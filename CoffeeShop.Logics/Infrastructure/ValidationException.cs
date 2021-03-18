using System;

namespace CoffeeShop.Logics.Infrastructure
{
    public class ValidationException : Exception
    {
        private const int DefaultStatusCode = 400;

        public int StatusCode { get; set; } = DefaultStatusCode;

        public ValidationException() : base()
        {
        }

        public ValidationException(string message) : base(message)
        {
        }

        public ValidationException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }
    }
}
