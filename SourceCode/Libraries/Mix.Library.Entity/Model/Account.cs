using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using Mix.Core;

namespace Mix.Library.Entity.Model
{
    /// <summary>
    /// 账户
    /// </summary>
    public class Account : ValidableObject
    {
        /// <summary>
        /// 标识
        /// </summary>
        [DisplayName("标识")]
        public Guid ID
        {
            get { return _ID; }
            set { SetProperty(ref _ID, value); }
        }
        private Guid _ID;

        /// <summary>
        /// 用户名
        /// </summary>
        [DisplayName("用户名")]
        public string UserName
        {
            get { return _UserName; }
            set { SetProperty(ref _UserName, value); }
        }
        private string _UserName;

        /// <summary>
        /// 密码
        /// </summary>
        [DisplayName("密码")]
        public string Password
        {
            get { return _Password; }
            set { SetProperty(ref _Password, value); }
        }
        private string _Password;

        /// <summary>
        /// 创建时间
        /// </summary>
        [DisplayName("创建时间")]
        public DateTime CreateTime
        {
            get { return _CreateTime; }
            set { SetProperty(ref _CreateTime, value); }
        }
        private DateTime _CreateTime;

        /// <summary>
        /// 更新时间
        /// </summary>
        [DisplayName("更新时间")]
        public DateTime UpdateTime
        {
            get { return _UpdateTime; }
            set { SetProperty(ref _UpdateTime, value); }
        }
        private DateTime _UpdateTime;

    }
}
