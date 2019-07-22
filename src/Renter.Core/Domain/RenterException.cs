using System;

namespace Renter.Core.Domain
{
    public class RenterException : Exception
    {
        public string Code { get; }

        public RenterException()
        {
        }

        public RenterException(string code)
        {
            Code = code;
        }

        public RenterException(string message, params object[] args)
            : this(string.Empty, message, args)
        {
        }

        public RenterException(string code, string message, params object[] args)
            : this(null, code, message, args)
        {
        }

        public RenterException(Exception innerException, string message, params object[] args)
            : this(innerException, string.Empty, message, args)
        {
        }

        public RenterException(Exception innerException, string code, string message, params object[] args)
            : base(string.Format(message, args), innerException)
        {
            Code = code;
        }
    }
}