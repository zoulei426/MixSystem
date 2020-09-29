using Prism.Mvvm;
using System;
using System.Collections;

namespace Mix.Core
{
    /// <summary>
    /// INotifyPropertyChanged实现类
    /// </summary>
    [Serializable]
    public class BindableObject : BindableBase, ICloneable, IDisposable
    {
        #region Methods

        #region Methods - Static

        /// <summary>
        /// TryClone
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static object TryClone(object obj)
        {
            if (obj is ICloneable ic)
                return ic.Clone();

            if (obj is IList list)
                return list.Clone();

            return obj;
        }

        #endregion Methods - Static

        #region Methods - Virtual

        /// <summary>
        /// 克隆
        /// </summary>
        /// <returns></returns>
        public virtual object Clone()
        {
            object newObj = MemberwiseClone();

            return newObj;
        }

        /// <summary>
        /// 回收
        /// </summary>
        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion Methods - Virtual

        #region Methods - Private



        #endregion Methods - Private

        #endregion Methods
    }
}
