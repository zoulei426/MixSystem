using FreeSql;
using Mix.Data;
using Mix.Data.Repositories;
using Mix.Library.Entities.Databases.Accounts;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Mix.Library.Repositories.Accounts
{
    /// <summary>
    /// UserRepository
    /// </summary>
    /// <seealso cref="AuditBaseRepository{T}" />
    /// <seealso cref="IUserRepository" />
    public class UserRepository : AuditBaseRepository<User>, IUserRepository
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRepository"/> class.
        /// </summary>
        /// <param name="unitOfWorkManager"></param>
        /// <param name="currentUser"></param>
        public UserRepository(UnitOfWorkManager unitOfWorkManager, ICurrentUser currentUser) : base(unitOfWorkManager, currentUser)
        {
        }

        /// <summary>
        /// 根据条件得到用户信息
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public Task<User> GetUserAsync(Expression<Func<User, bool>> expression)
        {
            return Select.Where(expression).ToOneAsync();
        }
    }
}