using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Infrastructure.Commands.Categories
{
    public class CreateCategory : AuthenticatedCommandBase
    {
        public string Name { get; protected set; }
        public string IconName { get; protected set; }
        public string ColorHex { get; protected set; }

    }
}
