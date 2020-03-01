using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Infrastructure.Commands.Categories
{
    public class DeleteCategory: AuthenticatedCommandBase
    {
        public int CategoryId { get; set; }
    }
}
