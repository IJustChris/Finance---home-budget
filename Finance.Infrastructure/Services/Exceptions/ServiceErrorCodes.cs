using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Infrastructure.Services.Exceptions
{
    public static class ServiceErrorCodes
    {
        public static string EmailInUse => "email_in_use";

        public static string InvalidCredentails  => "invalid_credentials";

    }
}
