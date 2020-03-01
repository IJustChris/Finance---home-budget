using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Core.Domain.Exceptions
{
    public class DomainException : FinanceException
    {
        public DomainException()
        {
        }

        public DomainException(string code) : base(code)
        {
        }

        public DomainException(string message, params object[] args) : base(string.Empty, message, args)
        {
        }

        public DomainException(string code, string message, params object[] args) : base(null, code, message, args)
        {
        }

        public DomainException(Exception innerException, string code, string message, params object[] args) : base(string.Format(message, args), innerException)
        {
        }
    }
}
