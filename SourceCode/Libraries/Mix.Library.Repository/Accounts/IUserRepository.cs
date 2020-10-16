using Mix.Data.Repositories;
using Mix.Library.Entity.Databases.Accounts;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mix.Library.Repository.Accounts
{
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
