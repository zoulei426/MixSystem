using Mix.Data.Repositories;
using Mix.Library.Entities.Databases;

namespace Mix.Library.Repositories
{
    /// <summary>
    /// ICompanyRepository
    /// </summary>
    /// <seealso cref="Mix.Data.Repositories.IAuditBaseRepository{T}" />
    public interface ICompanyRepository : IAuditBaseRepository<Company>
    {
    }
}