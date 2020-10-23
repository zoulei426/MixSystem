using Mix.Data.Repositories;
using Mix.Library.Entities.Databases;

namespace Mix.Library.Repositories
{
    public interface ICompanyRepository : IAuditBaseRepository<Company>
    {
    }
}