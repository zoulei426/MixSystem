﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Entity.Dto
{
    public class LoginDto
    {
        /// <summary>
        /// 登录名：admin
        /// </summary>
        [Required(ErrorMessage = "登录名为必填项")]
        public string Username { get; set; }

        /// <summary>
        /// 密码：123456
        /// </summary>
        [Required(ErrorMessage = "密码为必填项")]
        public string Password { get; set; }
    }
}