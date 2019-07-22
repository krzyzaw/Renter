using System;

namespace Renter.Core.Domain
{
    public class RenterException : Exception
    {
        public string Code { get; }

        protected RenterException()
        {
        }

        protected RenterException(string code)
        {
            Code = code;
        }

        protected RenterException(string message, params object[] args) : this(string.Empty, message, args)
        {
        }

        protected RenterException(string code, string message, params object[] args) : this(null, code, message, args)
        {
        }

        protected RenterException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        protected RenterException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}