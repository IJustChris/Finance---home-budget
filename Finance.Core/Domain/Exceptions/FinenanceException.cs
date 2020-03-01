using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Core.Domain.Exceptions
{
    public abstract class FinanceException: Exception
    {
        public string Code { get; }

        protected FinanceException()
        {
        }

        protected FinanceException(string code)
        {
            Code = code;
        }

        protected FinanceException(string message, params object[] args): this(string.Empty,message,args)
        {
        }

        protected FinanceException(string code, string message, params object[] args): this(null, code, message, args)
        {
        }

        protected FinanceException(Exception innerException, string code, string message, params object[] args): base(string.Format(message,args),innerException)
        {
            Code = code;
        }



    }
}
