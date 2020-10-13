using Microsoft.AspNetCore.Mvc;

namespace Mix.Api.Controllers
{
    /// <summary>
    /// 账户控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// 构造
        /// </summary>
        public AccountController()
        {
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var str = "123";
            return Ok(str);
        }
    }
}