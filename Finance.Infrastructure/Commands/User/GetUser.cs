using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Infrastructure.Commands.User
{

    public class GetUser : ICommand
    {
        public string Email { get; set; }
    }
}
