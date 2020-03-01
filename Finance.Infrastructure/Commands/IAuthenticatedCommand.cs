using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Infrastructure.Commands
{
    public interface IAuthenticatedCommand : ICommand
    {
        int userId { get; set; }
    }
}
