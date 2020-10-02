﻿using System;
using System.ComponentModel.DataAnnotations;

namespace Mix.Library.Entity.Database
{
    /// <summary>
    /// 账户
    /// </summary>
    public class Account
    {
        /// <summary>
        /// 标识
        /// </summary>
        [Key]
        public Guid ID { get; set; }

        /// <summary>
        /// 用户名
        /// </summary>
        [Required]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime { get; set; }

        /// <summary>
        /// 构造
        /// </summary>
        public Account()
        {
            ID = Guid.NewGuid();
            CreateTime = DateTime.Now;
            UpdateTime = CreateTime;
        }
    }
}