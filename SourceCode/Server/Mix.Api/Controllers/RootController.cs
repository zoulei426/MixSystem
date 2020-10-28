using Microsoft.AspNetCore.Mvc;
using Mix.Data.Dtos;
using System.Collections.Generic;

namespace Mix.Api.Controllers
{
    /// <summary>
    /// 根文档
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [Route("api")]
    [ApiController]
    public class RootController : ControllerBase
    {
        /// <summary>
        /// Gets the root.
        /// </summary>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetRoot))]
        public IActionResult GetRoot()
        {
            var links = new List<LinkDto>();

            links.Add(new LinkDto(Url.Link(nameof(GetRoot), new { }), "self", "GET"));

            links.Add(new LinkDto(
                Url.Link(nameof(CompaniesController.GetCompanies), new { }),
                "companies", "GET"));

            links.Add(new LinkDto(
                Url.Link(nameof(CompaniesController.CreateCompany), new { }),
                "create_company", "POST"));

            return Ok(links);
        }
    }
}