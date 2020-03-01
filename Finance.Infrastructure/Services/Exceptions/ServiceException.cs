using Finance.Core.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Infrastructure.Services.Exceptions
{
    public class ServiceException: FinanceException
    {
        public ServiceException()
        {
        }

        public ServiceException(string code) : base(code)
        {
        }

        public ServiceException(string message, params object[] args) : base(string.Empty, message, args)
        {
        }

        public ServiceException(string code, string message, params object[] args) : base(null, code, message, args)
        {
        }

        public ServiceException(Exception innerException, string code, string message, params object[] args) : base(string.Format(message, args), innerException)
        {
        }
    }
}
