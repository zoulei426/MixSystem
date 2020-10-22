using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Mix.Core;
using Mix.Library.Entities.Databases;
using Mix.Library.Entities.Models;
using Mix.Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mix.Api.Controllers
{
    /// <summary>
    /// 公司控制器
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CompaniesController : ControllerBase
    {
        private readonly IStringLocalizer localizer;
        private readonly ICompanyRepository companyRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="localizer"></param>
        /// <param name="companyRepository"></param>
        public CompaniesController(IStringLocalizer localizer, ICompanyRepository companyRepository, IMapper mapper)
        {
            this.localizer = localizer;
            this.companyRepository = companyRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// 获取所有
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanies()
        {
            var companies = await companyRepository.Select.ToListAsync();

            return Ok(mapper.Map<IEnumerable<CompanyDto>>(companies));
        }

        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [HttpGet("{companyId}")]
        public async Task<ActionResult<CompanyDto>> GetCompany(Guid companyId)
        {
            var company = await companyRepository.GetAsync(companyId);
            if (company is null)
            {
                return NotFound();
            }
            return Ok(mapper.Map<CompanyDto>(company));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddCompany(Company model)
        {
            var company = await companyRepository.InsertAsync(model);
            return new JsonResult(company);
        }
    }
}