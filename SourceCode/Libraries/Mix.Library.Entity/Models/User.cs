﻿using Mix.Core;
using System;
using System.ComponentModel;

namespace Mix.Library.Entities.Model
{
    /// <summary>
    /// 账户
    /// </summary>
    public class User : ValidableObject
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
    }
}