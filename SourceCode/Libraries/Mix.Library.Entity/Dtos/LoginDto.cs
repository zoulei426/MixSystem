using System.ComponentModel.DataAnnotations;

namespace Mix.Library.Entities.Dtos
{
    /// <summary>
    /// LoginDto
    /// </summary>
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