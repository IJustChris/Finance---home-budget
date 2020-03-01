using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Infrastructure.Commands
{
    public class AuthenticatedCommandBase : IAuthenticatedCommand
    {
        public int userId { get; set; }
    }
}
