using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
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
        private readonly IConnectionMultiplexer redis;

        private readonly IDatabase db;

        /// <summary>
        /// 构造
        /// </summary>
        public AccountController(IConnectionMultiplexer redis)
        {
            this.redis = redis;
            this.db = redis.GetDatabase();
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var str = "123";

            db.StringSet("name", "Alice");
            str = db.StringGet("name");

            return Ok(str);
        }
    }
}