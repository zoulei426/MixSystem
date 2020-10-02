using Mix.Library.Entity.Database;
using System;
using System.Diagnostics;

namespace Mix.Library.Repository
{
    /// <summary>
    /// 账户仓储
    /// </summary>
    public class AccountRepository
    {
        private static readonly IFreeSql fsql = new FreeSql.FreeSqlBuilder()
            .UseConnectionString(FreeSql.DataType.Sqlite, @"data source=D:\Database\Sqlite\test.db")
            .UseMonitorCommand(cmd => Trace.WriteLine($"线程：{cmd.CommandText}\r\n"))
            .UseAutoSyncStructure(true) //自动创建、迁移实体表结构
            .UseNoneCommandParameter(true)
            .Build();

        /// <summary>
        /// 添加账户
        /// </summary>
        /// <param name="model"></param>
        public void AddAccount(Account model)
        {
            var repo = fsql.GetRepository<Account>();
            repo.Insert(model);
        }

        /// <summary>
        /// 获取账户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Account Get(Guid id)
        {
            var repo = fsql.GetRepository<Account>();

            //var query = repo.Select.AsQueryable();

            return (from account in repo.Select
                    where account.ID.Equals(id)
                    select account).First();
        }
    }
}