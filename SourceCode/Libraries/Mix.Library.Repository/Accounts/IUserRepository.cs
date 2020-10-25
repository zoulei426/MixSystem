using Mix.Data.Repositories;
using Mix.Library.Entities.Databases.Accounts;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mix.Library.Repositories.Accounts
{
    /// <summary>
    /// IUserRepository
    /// </summary>
    /// <seealso cref="Mix.Data.Repositories.IAuditBaseRepository{T}" />
    public interface IUserRepository : IAuditBaseRepository<User>
    {
        /// <summary>
        /// 根据条件得到用户信息
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<User> GetUserAsync(Expression<Func<User, bool>> expression);
    }
}