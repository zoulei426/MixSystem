using FreeSql;
using System;

namespace Mix.Data.Repositories
{
    /// <summary>
    /// 审计仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IAuditBaseRepository<TEntity> : IBaseRepository<TEntity, Guid> where TEntity : class
    {
    }

    /// <summary>
    /// 审计仓储
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    /// <typeparam name="TKey"></typeparam>
    public interface IAuditBaseRepository<TEntity, TKey> : IBaseRepository<TEntity, TKey> where TEntity : class
    {
    }
}