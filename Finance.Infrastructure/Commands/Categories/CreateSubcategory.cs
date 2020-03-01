using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Infrastructure.Commands.Categories
{
    public class CreateSubcategory: AuthenticatedCommandBase
    {
        public int ParentId { get; set; }
        public string CategoryName { get; set; }
        public string IconName { get; set; }
        public string HexColor { get; set; }
    }
}
