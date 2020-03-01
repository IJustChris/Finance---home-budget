using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Infrastructure.Services
{
    public interface IDataInitializer: IService
    {
        Task SeedAsync();
    }
}
