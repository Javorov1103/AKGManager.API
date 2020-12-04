namespace AutoService.API.Utilities
{
    using System;

    public class ArgumentNullOrWhiteSpaceException : ArgumentException
    {
        // Can be retrieved fro
        private const string ERROR_MESSAGE = "Value cannot be null or whitespace.";

        public ArgumentNullOrWhiteSpaceException()
            : base(ERROR_MESSAGE)
        {
        }

        public ArgumentNullOrWhiteSpaceException(string paramName)
            : base(ERROR_MESSAGE, paramName)
        {
        }
    }
}
