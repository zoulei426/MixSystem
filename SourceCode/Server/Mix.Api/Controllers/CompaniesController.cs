using AutoMapper;
using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
using Microsoft.Extensions.Localization;
using Mix.Core;
using Mix.Library.Entities.Databases;
using Mix.Library.Entities.DtoParameters;
using Mix.Library.Entities.Dtos;
using Mix.Library.Repositories;
using Mix.Library.Services;
using Mix.Service.Core;
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
    [Route("api/companies")]
    public class CompaniesController : ControllerBase
    {
        private readonly IStringLocalizer localizer;
        private readonly ICompanyService companyService;
        private readonly ICompanyRepository companyRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// 构造
        /// </summary>
        /// <param name="localizer"></param>
        /// <param name="companyService"></param>
        /// <param name="companyRepository"></param>
        /// <param name="mapper"></param>
        public CompaniesController(IStringLocalizer localizer, ICompanyService companyService, ICompanyRepository companyRepository, IMapper mapper)
        {
            this.localizer = localizer;
            this.companyService = companyService;
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
        [HttpGet("{companyId}", Name = nameof(GetCompany))]
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
        /// 根据ids获取公司列表（xxx,xxx,xxx）
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpGet("batch/({ids})", Name = nameof(GetCompanyCollection))]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> GetCompanyCollection(
            [FromRoute]
            [ModelBinder(BinderType = typeof(ArrayModelBinder))]
            IEnumerable<Guid> ids)
        {
            if (ids is null) return BadRequest();

            var entities = await companyRepository.Select.Where(t => ids.Contains(t.Id)).ToListAsync();

            if (entities.Count() != ids.Count()) return NotFound();

            return Ok(mapper.Map<IEnumerable<CompanyDto>>(entities));
        }

        /// <summary>
        /// 添加
        /// </summary>
        /// <param name="company"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<CompanyDto>> CreateCompany(CompanyAddDto company)
        {
            var resultDto = await companyService.CreateCompanyAsync(company);
            return CreatedAtRoute(nameof(GetCompany), new { companyId = resultDto.Id }, resultDto);
        }

        /// <summary>
        /// 批量添加
        /// </summary>
        /// <param name="companieCollection"></param>
        /// <returns></returns>
        [HttpPost("batch")]
        public async Task<ActionResult<IEnumerable<CompanyDto>>> CreateCompanyCollection(
            IEnumerable<CompanyAddDto> companieCollection)
        {
            var resultDtos = await companyService.CreateCompanyCollectionAsync(companieCollection);

            var idsString = string.Join(",", resultDtos.Select(t => t.Id));

            return CreatedAtRoute(nameof(GetCompanyCollection), new { ids = idsString }, resultDtos);
        }

        /// <summary>
        /// 选项
        /// </summary>
        /// <returns></returns>
        [HttpOptions]
        public IActionResult GetCompaniesOptions()
        {
            Response.Headers.Add("Allow", "GET,POST,OPTIONS");
            return Ok();
        }
    }
}