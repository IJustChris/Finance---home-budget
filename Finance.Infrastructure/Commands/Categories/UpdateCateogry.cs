using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Infrastructure.Commands.Categories
{
    public class UpdateCateogry: AuthenticatedCommandBase
    {
        public int CategoryId { get; set; }
        public int ParentId { get; set; }
        public string IconName { get; set; }
        public string ColorHex { get; set; }
        public string Name { get; set; }
    }
}
