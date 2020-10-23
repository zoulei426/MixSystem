using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mix.Library.Entities.Models;
using Mix.Library.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mix.Api.Controllers
{
    /// <summary>
    /// 员工控制器
    /// </summary>
    [ApiController]
    [Route("api/companies/{companyId}/employees")]
    public class EmployeesController : ControllerBase
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="companyRepository"></param>
        /// <param name="employeeRepository"></param>
        /// <param name="mapper"></param>
        public EmployeesController(ICompanyRepository companyRepository, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.companyRepository = companyRepository;
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// 获取公司下所有员工
        /// </summary>
        /// <param name="companyId"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesForCompany(Guid companyId)
        {
            if (!await companyRepository.Where(t => t.Id.Equals(companyId)).AnyAsync())
                return NotFound();

            var employees = await employeeRepository.Select.Where(t => t.CompanyId.Equals(companyId)).ToListAsync();
            return Ok(mapper.Map<IEnumerable<EmployeeDto>>(employees));
        }
    }
}