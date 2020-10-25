using FreeSql;
using Mix.Data;
using Mix.Data.Repositories;
using Mix.Library.Entities.Databases;

namespace Mix.Library.Repositories
{
    /// <summary>
    /// CompanyRepository
    /// </summary>
    /// <seealso cref="Mix.Data.Repositories.AuditBaseRepository{T}" />
    /// <seealso cref="Mix.Library.Repositories.ICompanyRepository" />
    public class CompanyRepository : AuditBaseRepository<Company>, ICompanyRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyRepository"/> class.
        /// </summary>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="currentUser"></param>
        public CompanyRepository(UnitOfWorkManager unitOfWorkManager, ICurrentUser currentUser) : base(unitOfWorkManager, currentUser)
        {
        }
    }
}