﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Mix.Library.Entities.Databases;
using Mix.Library.Entities.Dtos;
using Mix.Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
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
        /// <param name="gender"></param>
        /// <param name="q"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesForCompany(Guid companyId, [FromQuery] string gender, string q)
        {
            if (!await companyRepository.Where(t => t.Id.Equals(companyId)).AnyAsync())
                return NotFound();

            var employees = await employeeRepository.GetEmployeesAsync(companyId, gender, q);
            return Ok(mapper.Map<IEnumerable<EmployeeDto>>(employees));
        }

        /// <summary>
        /// 获取公司下某员工
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        [HttpGet("{employeeId}", Name = nameof(GetEmployeeForCompany))]
        public async Task<ActionResult<EmployeeDto>> GetEmployeeForCompany(Guid companyId, Guid employeeId)
        {
            if (!await companyRepository.Where(t => t.Id.Equals(companyId)).AnyAsync())
                return NotFound();

            var employee = await employeeRepository.Select.Where(t => t.CompanyId.Equals(companyId) && t.Id.Equals(employeeId)).ToOneAsync();
            return Ok(mapper.Map<EmployeeDto>(employee));
        }

        /// <summary>
        /// 添加员工
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<EmployeeDto>> CreateEmployeeForCompany(Guid companyId, EmployeeAddDto employee)
        {
            if (!await companyRepository.Where(t => t.Id.Equals(companyId)).AnyAsync())
                return NotFound();

            var entity = mapper.Map<Employee>(employee);
            var result = await employeeRepository.AddEmployeeAsync(companyId, entity);
            var resultDto = mapper.Map<EmployeeDto>(result);

            return CreatedAtRoute(nameof(GetEmployeeForCompany),
                    new { companyId, employeeId = resultDto.Id },
                    resultDto);
        }
    }
}