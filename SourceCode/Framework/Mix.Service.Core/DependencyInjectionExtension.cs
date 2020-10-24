using FreeSql;
using FreeSql.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mix.Core;
using Mix.Data;
using Mix.Data.Entities;
using Serilog;
using System;
using System.Diagnostics;
using System.Threading;
using ToolGood.Words;

namespace Mix.Service.Core
{
    public static class DependencyInjectionExtension
    {
        #region FreeSql

        /// <summary>
        /// FreeSql
        /// </summary>
        /// <param name="services"></param>
        public static void AddFreeSql(this IServiceCollection services, IConfiguration configuration)
        {
            IFreeSql fsql = new FreeSqlBuilder()
                   .UseConnectionString(configuration)
                   .UseNameConvert(NameConvertType.PascalCaseToUnderscoreWithLower)
                   .UseAutoSyncStructure(true)
                   .UseNoneCommandParameter(true)
                   .UseMonitorCommand(cmd =>
                   {
                       Trace.WriteLine(cmd.CommandText + ";");
                   }
                   )
                   .Build()
                   .SetDbContextOptions(opt => opt.EnableAddOrUpdateNavigateList = false);//联级保存功能开启（默认为关闭）

            fsql.Aop.AuditValue += (s, e) =>
            {
            };

            fsql.Aop.CurdAfter += (s, e) =>
            {
                Log.Debug($"ManagedThreadId:{Thread.CurrentThread.ManagedThreadId}: FullName:{e.EntityType.FullName}" +
                          $" ElapsedMilliseconds:{e.ElapsedMilliseconds}ms, {e.Sql}");

                if (e.ElapsedMilliseconds > 200)
                {
                    //记录日志
                    //发送短信给负责人
                }
            };

            //敏感词处理
            if (configuration["AuditValue:Enable"].ToBoolean())
            {
                IllegalWordsSearch illegalWords = ToolGoodUtils.GetIllegalWordsSearch();

                fsql.Aop.AuditValue += (s, e) =>
                {
                    if (e.Column.CsType == typeof(string) && e.Value != null)
                    {
                        string oldVal = (string)e.Value;
                        string newVal = illegalWords.Replace(oldVal);
                        //第二种处理敏感词的方式
                        //string newVal = oldVal.ReplaceStopWords();
                        if (newVal != oldVal)
                        {
                            e.Value = newVal;
                        }
                    }
                };
            }

            services.AddSingleton(fsql);
            services.AddFreeRepository();
            services.AddScoped<UnitOfWorkManager>();
            fsql.GlobalFilter.Apply<IDeleteAduitEntity>("IsDeleted", a => a.IsDeleted == false);
            try
            {
                using var objPool = fsql.Ado.MasterPool.Get();
            }
            catch (Exception e)
            {
                Log.Logger.Error(e + e.StackTrace + e.Message + e.InnerException);
                return;
            }
        }

        #endregion FreeSql
    }
}