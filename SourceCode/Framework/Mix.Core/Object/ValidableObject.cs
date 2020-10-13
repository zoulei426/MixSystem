using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;

namespace Mix.Core
{
    /// <summary>
    /// 可验证对象
    /// </summary>
    [Serializable]
    public class ValidableObject : BindableObject, IDataErrorInfo
    {
        #region Fields

        /// <summary>
        /// 验证错误集合
        /// </summary>
        private readonly Dictionary<string, string> _DataErrors = new Dictionary<string, string>();

        private string _Error;

        /// <summary>
        /// 验证器
        /// </summary>
        private object _Validator;

        private MethodInfo _ValidateMI;

        #endregion Fields

        #region Properties

        /// <summary>
        /// 错误信息
        /// </summary>
        public virtual string Error { get { return _Error; } }

        /// <summary>
        /// Gets the error message for the property with the given name.
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public virtual string this[string columnName] { get { return Validate(columnName); } }

        #endregion Properties

        #region Ctor

        /// <summary>
        /// 构造
        /// </summary>
        public ValidableObject()
        {
        }

        #endregion Ctor

        #region Methods

        #region Methods - Virtual

        /// <summary>
        /// 验证方法
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        protected virtual string Validate(string columnName)
        {
            try
            {
                if (_Validator == null || _ValidateMI == null)
                {
                    var type = GetType();
                    var className = type.Name;
                    var assembly = type.Assembly;
                    var validatorCls = assembly.GetTypes().FirstOrDefault(t => t.Name.Equals(className + "Validator"));
                    _Validator = Activator.CreateInstance(validatorCls, true);//根据类型创建实例
                    _ValidateMI = validatorCls.GetMethods().FirstOrDefault(t => t.Name.Equals("Validate"));
                }

                var result = _ValidateMI.Invoke(_Validator, new object[] { this }) as FluentValidation.Results.ValidationResult;

                var errors = result.Errors.Where(lol => lol.PropertyName == columnName).ToList();

                if (errors.Count > 0)
                {
                    AddDic(_DataErrors, columnName);
                    _Error = string.Join(Environment.NewLine, errors.Select(r => r.ErrorMessage).ToArray());
                    return _Error;
                }
                RemoveDic(_DataErrors, columnName);
                return null;
                //return firstOrDefault?.ErrorMessage;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        #endregion Methods - Virtual

        #region Methods - private

        /// <summary>
        /// 移除字典
        /// </summary>
        /// <param name="dics"></param>
        /// <param name="dicKey"></param>
        private static void RemoveDic(Dictionary<string, string> dics, string dicKey)
        {
            dics.Remove(dicKey);
        }

        /// <summary>
        /// 添加字典
        /// </summary>
        /// <param name="dics"></param>
        /// <param name="dicKey"></param>
        private static void AddDic(Dictionary<string, string> dics, string dicKey)
        {
            if (!dics.ContainsKey(dicKey)) dics.Add(dicKey, string.Empty);
        }

        #endregion Methods - private

        #endregion Methods
    }
}