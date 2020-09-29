using System;
using System.Collections;
using System.Collections.Generic;
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
        /// <param name="source"></param>
        /// <returns></returns>
        public static List<T> TryToList<T>(this IEnumerable<T> source)
        {
            if (source == null)
                return null;

            try { return source.ToList(); }
            catch { return null; }
        }

        /// <summary>
        /// 集合是否为空
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
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
        /// <param name="source"></param>
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

       


        #endregion Methods
    }
}
