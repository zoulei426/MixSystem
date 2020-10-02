using Microsoft.AspNetCore.Mvc;
using Mix.Core;
using Mix.Library.Entity;
using Mix.Library.Entity.Model;
using Mix.Library.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mix.Server.Controllers
{
    /// <summary>
    /// 账户控制器
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class AccountController : Controller
    {
        private IAccountRepository _AccountRepository;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="accountRepository"></param>
        public AccountController(IAccountRepository accountRepository)
        {
            _AccountRepository = accountRepository;
        }

        /// <summary>
        /// 添加账户
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Add(Account model)
        {
            //var account = new Account();
            //account.ID = new Guid("{472a6ac0-28f6-4f73-8b8e-48bc2408c41d}");
            //account.UserName = "Alice";
            //account.Password = "123456";

            _AccountRepository.AddAccount(model.ConvertTo<Mix.Library.Entity.Database.Account>());
            return Ok("添加成功！");
        }

        /// <summary>
        /// 获取账户
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public ActionResult<Account> Get(Guid id)
        {
            return _AccountRepository.Get(id).ConvertTo<Account>();
        }
    }
}