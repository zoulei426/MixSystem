using FreeSql;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics;
using System.Linq;

namespace Mix.Data
{
    /// <summary>
    /// FreeSql扩展
    /// </summary>
    public static class FreeSqlExtension
    {
        public static ISelect<T> AsTable<T>(this ISelect<T> @this, string tableName, int count) where T : class
        {
            string[] tableNames = new string[] { };
            for (int i = 0; i < count; i++)
            {
                tableNames.AddIfNotContains($"{tableName}_{i}");
            }
            @this.AsTable(tableNames);
            return @this;
        }

        public static ISelect<T> AsTable<T>(this ISelect<T> @this, params string[] tableNames) where T : class
        {
            tableNames?.ToList().ForEach(tableName =>
            {
                @this.AsTable((type, oldname) =>
                {
                    if (type == typeof(T)) return tableName;
                    return null;
                });
            });
            return @this;
        }

        /// <summary>
        /// 使用配置文件中的连接字符串
        /// </summary>
        /// <param name="this"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static FreeSqlBuilder UseConnectionString(this FreeSqlBuilder @this, IConfiguration configuration)
        {
            IConfigurationSection dbTypeCode = configuration.GetSection("ConnectionStrings:DefaultDB");
            if (Enum.TryParse(dbTypeCode.Value, out DataType dataType))
            {
                if (!Enum.IsDefined(typeof(DataType), dataType))
                {
                    Trace.WriteLine($"数据库配置ConnectionStrings:DefaultDB:{dataType}无效");
                    //Log.Error($"数据库配置ConnectionStrings:DefaultDB:{dataType}无效");
                }
                IConfigurationSection configurationSection = configuration.GetSection($"ConnectionStrings:{dataType}");
                @this.UseConnectionString(dataType, configurationSection.Value);
            }
            else
            {
                Trace.WriteLine($"数据库配置ConnectionStrings:DefaultDB:{dbTypeCode.Value}无效");
                //Log.Error($"数据库配置ConnectionStrings:DefaultDB:{dbTypeCode.Value}无效");
            }
            return @this;
        }
    }
}