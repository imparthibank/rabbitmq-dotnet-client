using System;

namespace IdentityManagement.Filters
{
    internal class UnHandledException : Exception
    {
        public UnHandledException()
        { }

        public UnHandledException(string message)
            : base(message)
        { }

        public UnHandledException(string message, Exception innerException)
            : base(message, innerException)
        { }
    }
}