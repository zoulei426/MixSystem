using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using Mix.Core;
using Mix.Data.Dtos;
using Mix.Data.Pagable;
using Mix.Library.Entities.DtoParameters;
using Mix.Library.Entities.Dtos;
using Mix.Library.Repositories;
using Mix.Library.Services;
using Mix.Service.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Json;
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
        /// Gets the companies.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetCompanies))]
        public async Task<IActionResult> GetCompanies([FromQuery] CompanyDtoParameters parameters)
        {
            var companies = await companyService.GetCompaniesAsync(parameters);

            var paginationMetadata = new PaginationMetadata
            {
                TotalCount = companies.TotalCount,
                PageSize = companies.PageSize,
                CurrentPage = companies.CurrentPage,
                TotalPages = companies.TotalPages
            };

            Response.Headers.Add(PaginationMetadata.KEY,
                JsonSerializer.Serialize(paginationMetadata,
                    new JsonSerializerOptions
                    {
                        Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
                    }));

            var shapedData = companies.ShapeDatas(parameters.Fields);
            var links = CreateLinksForCompany(parameters, companies.HasPrevious, companies.HasNext);

            var shapedCompaniesWithLinks = shapedData.Select(t =>
            {
                var companyDict = t as IDictionary<string, object>;
                var companyLinks = CreateLinksForCompany((Guid)companyDict[nameof(CompanyDto.Id)], null);
                companyDict.Add("Links", companyLinks);
                return companyDict;
            });

            var linkedCompanies = new
            {
                value = shapedCompaniesWithLinks,
                links
            };

            return Ok(linkedCompanies);
        }

        /// <summary>
        /// 根据Id获取
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        [HttpGet("{companyId}", Name = nameof(GetCompany))]
        public async Task<IActionResult> GetCompany(Guid companyId, string fields)
        {
            var company = await companyRepository.GetAsync(companyId);
            if (company is null)
            {
                return NotFound();
            }

            var links = CreateLinksForCompany(companyId, fields);

            var linkedDict = mapper.Map<CompanyDto>(company).ShapeData(fields)
                as IDictionary<string, object>;

            linkedDict.Add("links", links);

            return Ok(linkedDict);
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
        [HttpPost(Name = nameof(CreateCompany))]
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
        /// Deletes the company.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <returns></returns>
        [HttpDelete("{companyId}", Name = nameof(DeleteCompany))]
        public async Task<IActionResult> DeleteCompany(Guid companyId)
        {
            var entityExists = await companyRepository.Select.AnyAsync(t => t.Id.Equals(companyId));

            if (!entityExists)
                return NotFound();

            await companyService.DeleteCompany(companyId);

            return NoContent();
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

        /// <summary>
        /// Creates the companies resource URI.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <param name="type">The type.</param>
        /// <returns></returns>
        private string CreateCompaniesResourceUri(CompanyDtoParameters parameters, ResourceUriType type)
        {
            switch (type)
            {
                case ResourceUriType.PreviousPage:
                    return Url.Link(nameof(GetCompanies), new
                    {
                        pageNumber = parameters.PageNumber - 1,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields,
                        companyName = parameters.CompanyName
                    });

                case ResourceUriType.NextPage:
                    return Url.Link(nameof(GetCompanies), new
                    {
                        pageNumber = parameters.PageNumber + 1,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields,
                        companyName = parameters.CompanyName
                    });

                case ResourceUriType.CurrentPage:
                default:
                    return Url.Link(nameof(GetCompanies), new
                    {
                        pageNumber = parameters.PageNumber,
                        pageSize = parameters.PageSize,
                        orderBy = parameters.OrderBy,
                        fields = parameters.Fields,
                        companyName = parameters.CompanyName
                    });
            }
        }

        private IEnumerable<LinkDto> CreateLinksForCompany(Guid companyId, string fields)
        {
            var links = new List<LinkDto>();

            if (fields.IsNullOrWhiteSpace())
            {
                links.Add(new LinkDto(
                        Url.Link(nameof(GetCompany), new { companyId }),
                        "self",
                        "GET"));
            }
            else
            {
                links.Add(new LinkDto(
                        Url.Link(nameof(GetCompany), new { companyId, fields }),
                        "self",
                        "GET"));
            }

            links.Add(new LinkDto(
                 Url.Link(nameof(DeleteCompany), new { companyId }),
                 "delete_company",
                 "DELETE"));

            links.Add(new LinkDto(
               Url.Link(nameof(EmployeesController.CreateEmployeeForCompany), new { companyId }),
               "create_employee_for_company",
               "POST"));

            links.Add(new LinkDto(
              Url.Link(nameof(EmployeesController.GetEmployeesForCompany), new { companyId }),
              "employees",
              "GET"));

            return links;
        }

        private IEnumerable<LinkDto> CreateLinksForCompany(CompanyDtoParameters parameters, bool hasPrevious, bool hasNext)
        {
            var links = new List<LinkDto>();

            links.Add(new LinkDto(
                CreateCompaniesResourceUri(parameters, ResourceUriType.CurrentPage),
                "self", "GET"));

            if (hasPrevious)
            {
                links.Add(new LinkDto(
                CreateCompaniesResourceUri(parameters, ResourceUriType.PreviousPage),
                "previous_page", "GET"));
            }

            if (hasNext)
            {
                links.Add(new LinkDto(
                CreateCompaniesResourceUri(parameters, ResourceUriType.NextPage),
                "next_page", "GET"));
            }

            return links;
        }
    }
}