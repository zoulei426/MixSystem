using AutoMapper;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Mix.Library.Entities.Databases;
using Mix.Library.Entities.DtoParameters;
using Mix.Library.Entities.Dtos;
using Mix.Library.Repositories;
using Mix.Library.Services;
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
        private readonly IEmployeeService employeeService;
        private readonly ICompanyRepository companyRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IMapper mapper;

        /// <summary>
        /// 构造器
        /// </summary>
        /// <param name="employeeService"></param>
        /// <param name="companyRepository"></param>
        /// <param name="employeeRepository"></param>
        /// <param name="mapper"></param>
        public EmployeesController(IEmployeeService employeeService, ICompanyRepository companyRepository, IEmployeeRepository employeeRepository, IMapper mapper)
        {
            this.employeeService = employeeService;
            this.companyRepository = companyRepository;
            this.employeeRepository = employeeRepository;
            this.mapper = mapper;
        }

        /// <summary>
        /// Gets the employees for company.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        [HttpGet(Name = nameof(GetEmployeesForCompany))]
        public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployeesForCompany(
            Guid companyId,
            [FromQuery] EmployeeDtoParameters parameters)
        {
            if (!await companyRepository.Where(t => t.Id.Equals(companyId)).AnyAsync())
                return NotFound();

            var employees = await employeeService.GetEmployeesForCompany(companyId, parameters);
            return Ok(employees);
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
        [HttpPost(Name = nameof(CreateEmployeeForCompany))]
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

        /// <summary>
        /// 更新或新增员工
        /// </summary>
        /// <param name="companyId"></param>
        /// <param name="employeeId"></param>
        /// <param name="employee"></param>
        /// <returns></returns>
        [HttpPut("{employeeId}")]
        public async Task<ActionResult<EmployeeDto>> UpdateEmployeeForCompany(
            Guid companyId,
            Guid employeeId,
            EmployeeUpdateDto employee)
        {
            if (!await companyRepository.Where(t => t.Id.Equals(companyId)).AnyAsync())
                return NotFound();

            var entity = await employeeRepository.Where(t => t.Id.Equals(employeeId)).ToOneAsync();

            // 若不存在，则创建
            if (entity is null)
            {
                entity = mapper.Map<Employee>(employee);
                entity.Id = employeeId;
                var result = await employeeRepository.AddEmployeeAsync(companyId, entity);
                var resultDto = mapper.Map<EmployeeDto>(result);

                return CreatedAtRoute(nameof(GetEmployeeForCompany),
                        new { companyId, employeeId = resultDto.Id },
                        resultDto);
            }

            mapper.Map(employee, entity);

            await employeeRepository.UpdateAsync(entity);

            return NoContent();
        }

        /// <summary>
        /// 局部更新或新增员工
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="employeeId">The employee identifier.</param>
        /// <param name="patchDocument">The patch document.</param>
        /// <returns></returns>
        [HttpPatch("{employeeId}")]
        public async Task<IActionResult> PartiallyUpdateEmployeeForCompany(
            Guid companyId,
            Guid employeeId,
            JsonPatchDocument<EmployeeUpdateDto> patchDocument)
        {
            if (!await companyRepository.Where(t => t.Id.Equals(companyId)).AnyAsync())
                return NotFound();

            var entity = await employeeRepository.Where(t => t.Id.Equals(employeeId)).ToOneAsync();

            // 若不存在，则创建
            if (entity is null)
            {
                var employeeDto = new EmployeeUpdateDto();
                patchDocument.ApplyTo(employeeDto, ModelState);

                if (!TryValidateModel(employeeDto))
                {
                    return ValidationProblem(ModelState);
                }

                var employeeToAdd = mapper.Map<Employee>(entity);
                employeeToAdd.Id = employeeId;

                var result = await employeeRepository.AddEmployeeAsync(companyId, employeeToAdd);
                var resultDto = mapper.Map<EmployeeDto>(result);

                return CreatedAtRoute(nameof(GetEmployeeForCompany),
                        new { companyId, employeeId = resultDto.Id },
                        resultDto);
            }

            var dtoToPatch = mapper.Map<EmployeeUpdateDto>(entity);

            patchDocument.ApplyTo(dtoToPatch, ModelState);

            if (!TryValidateModel(dtoToPatch))
            {
                return ValidationProblem(ModelState);
            }

            mapper.Map(dtoToPatch, entity);

            await employeeRepository.UpdateAsync(entity);

            return NoContent();
        }

        /// <summary>
        /// Deletes the employee for company.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="employeeId">The employee identifier.</param>
        /// <returns></returns>
        [HttpDelete("{employeeId}")]
        public async Task<IActionResult> DeleteEmployeeForCompany(Guid companyId, Guid employeeId)
        {
            if (!await companyRepository.Where(t => t.Id.Equals(companyId)).AnyAsync())
                return NotFound();

            var entity = await employeeRepository.Where(t => t.Id.Equals(employeeId)).ToOneAsync();

            if (entity is null)
                return NotFound();

            await employeeRepository.DeleteAsync(employeeId);

            return NoContent();
        }

        /// <summary>
        /// Creates an <see cref="T:Microsoft.AspNetCore.Mvc.ActionResult" /> that produces a <see cref="F:Microsoft.AspNetCore.Http.StatusCodes.Status400BadRequest" /> response
        /// with validation errors from <paramref name="modelStateDictionary" />.
        /// </summary>
        /// <param name="modelStateDictionary">The <see cref="T:Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary" />.</param>
        /// <returns>
        /// The created <see cref="T:Microsoft.AspNetCore.Mvc.BadRequestObjectResult" /> for the response.
        /// </returns>
        public override ActionResult ValidationProblem([ActionResultObjectValue] ModelStateDictionary modelStateDictionary)
        {
            var options = HttpContext.RequestServices.GetRequiredService<IOptions<ApiBehaviorOptions>>();

            return (ActionResult)options.Value.InvalidModelStateResponseFactory(ControllerContext);
        }
    }
}