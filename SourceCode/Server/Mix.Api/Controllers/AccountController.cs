using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Localization;
using StackExchange.Redis;

namespace Mix.Api.Controllers
{
    /// <summary>
    /// 账户控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        //private readonly IConnectionMultiplexer redis;
        private readonly IStringLocalizer localizer;

        //private readonly IDatabase db;

        /// <summary>
        /// 构造
        /// </summary>2
        public AccountController(IStringLocalizer localizer)
        {
            //this.redis = redis;
            this.localizer = localizer;
            //this.db = redis.GetDatabase();
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            //db.StringSet("name", "Alice");
            //str = db.StringGet("name");

            return Ok(localizer["Hello"].Value);
        }
    }
}