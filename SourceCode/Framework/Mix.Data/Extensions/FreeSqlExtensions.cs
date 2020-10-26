using FreeSql;
using Microsoft.Extensions.Configuration;
using Mix.Core;
using System;
using System.Diagnostics;
using System.Linq;

namespace Mix.Data
{
    /// <summary>
    /// FreeSql扩展
    /// </summary>
    public static class FreeSqlExtensions
    {
        /// <summary>
        /// Ases the table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this">The this.</param>
        /// <param name="tableName">Name of the table.</param>
        /// <param name="count">The count.</param>
        /// <returns></returns>
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

        /// <summary>
        /// Ases the table.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this">The this.</param>
        /// <param name="tableNames">The table names.</param>
        /// <returns></returns>
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

        /// <summary>
        /// 应用排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this">The this.</param>
        /// <param name="orderBy">The order by.</param>
        /// <param name="mappingDictionary">The mapping dictionary.</param>
        public static ISelect<T> ApplySort<T>(this ISelect<T> @this, string orderBy, System.Collections.Generic.Dictionary<string, Core.Mapping.PropertyMappingValue> mappingDictionary) where T : class
        {
            Guards.ThrowIfNull(@this);
            Guards.ThrowIfNull(mappingDictionary);

            if (orderBy.IsNullOrWhiteSpace())
            {
                return @this;
            }

            var orderBySplited = orderBy.Split(",");
            orderBySplited.Reverse();
            foreach (var orderByClause in orderBySplited)
            {
                var orderByClauseTrimmed = orderByClause.Trim();
                var orderDescending = orderByClauseTrimmed.EndsWith(" desc"); // 是否倒序
                var indexOfFirstSpace = orderByClauseTrimmed.IndexOf(" ");

                var propertyName = indexOfFirstSpace == -1
                    ? orderByClauseTrimmed
                    : orderByClauseTrimmed.Remove(indexOfFirstSpace);

                if (!mappingDictionary.ContainsKey(propertyName))
                {
                    //throw new Exception($"未找到Key为{propertyName}的映射");
                    @this = @this.OrderBy(propertyName.ToSnakeCase() + (orderDescending
                        ? " desc" : ""));
                    continue;
                }

                var propertyMappingValue = mappingDictionary[propertyName];

                Guards.ThrowIfNull(propertyMappingValue);

                foreach (var destinationProperty in propertyMappingValue.DestinationProperties.Reverse())
                {
                    if (propertyMappingValue.Revert)
                    {
                        orderDescending = !orderDescending;
                    }

                    @this = @this.OrderBy(destinationProperty.ToSnakeCase() + (orderDescending
                        ? " desc" : ""));
                }
            }

            return @this;
        }
    }
}