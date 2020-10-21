using Mix.Data.Repositories;
using Mix.Library.Entities.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Repositories
{
    public interface ICompanyRepository : IAuditBaseRepository<Company>
    {
    }
}