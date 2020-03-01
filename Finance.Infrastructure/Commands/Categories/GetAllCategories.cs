using Finance.Infrastructure.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace Finance.Infrastructure.Commands.Categories
{
    public class GetAllCategories: AuthenticatedCommandBase
    {
        //output
        public IEnumerable<CategoryDto> categories;
    }
}
