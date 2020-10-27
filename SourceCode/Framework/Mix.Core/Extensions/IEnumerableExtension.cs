using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Reflection;

namespace Mix.Core
{
    /// <summary>
    /// 可枚举扩展
    /// </summary>
    public static class IEnumerableExtension
    {
        #region Methods

        /// <summary>
        /// ToList 的安全版本，如果发生异常并不会导致程序崩溃，而是返回null。
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static List<T> TryToList<T>(this IEnumerable<T> source)
        {
            if (source == null)
                return null;

            try { return source.ToList(); }
            catch { return null; }
        }

        /// <summary>
        /// Determines whether [is null or empty].
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns>
        ///   <c>true</c> if [is null or empty] [the specified source]; otherwise, <c>false</c>.
        /// </returns>
        public static bool IsNullOrEmpty(this IEnumerable source)
        {
            if (source == null)
                return true;

            bool has = source.GetEnumerator().MoveNext();
            if (!has)
                return true;

            return false;
        }

        /// <summary>
        /// 克隆集合
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static IList Clone(this IList source)
        {
            if (source == null)
                return null;

            MethodInfo mi = source.GetType().GetMethod("Clone");
            if (mi != null)
                return (IList)mi.Invoke(source, null);

            IList list = Activator.CreateInstance(source.GetType()) as IList;
            MethodInfo addMethod = list.GetType().GetMethod("Add");

            foreach (object item in source)
                addMethod.Invoke(list, new object[] { ObjectBase.TryClone(item) });

            return list;
        }

        /// <summary>
        /// 数据塑形
        /// </summary>
        /// <typeparam name="TSource">The type of the source.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="fields">The fields.</param>
        /// <returns></returns>
        /// <exception cref="Exception">Not found property {propertyName} in {typeof(TSource)}</exception>
        public static IEnumerable<ExpandoObject> ShapeData<TSource>(
            this IEnumerable<TSource> source,
            string fields)
        {
            Guards.ThrowIfNull(source);

            var expandoObjectList = new List<ExpandoObject>(source.Count());

            var propertyInfoList = new List<PropertyInfo>();

            if (fields.IsNullOrWhiteSpace())
            {
                var propertyInfos = typeof(TSource).GetProperties(
                    BindingFlags.IgnoreCase |
                    BindingFlags.Public |
                    BindingFlags.Instance);

                propertyInfoList.AddRange(propertyInfos);
            }
            else
            {
                var splitedFields = fields.Split(",");
                foreach (var field in splitedFields)
                {
                    var propertyName = field.Trim();
                    var propertyInfo = typeof(TSource).GetProperty(propertyName,
                        BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                    if (propertyInfo is null)
                    {
                        throw new Exception($"Not found property {propertyName} in {typeof(TSource)}");
                    }

                    propertyInfoList.Add(propertyInfo);
                }
            }

            foreach (TSource obj in source)
            {
                var shapeObj = new ExpandoObject();

                foreach (var propertyInfo in propertyInfoList)
                {
                    var propertyValue = propertyInfo.GetValue(obj);

                    ((IDictionary<string, object>)shapeObj).Add(propertyInfo.Name, propertyValue);
                }

                expandoObjectList.Add(shapeObj);
            }

            return expandoObjectList;
        }

        #endregion Methods
    }
}