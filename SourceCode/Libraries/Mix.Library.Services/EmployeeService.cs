using Mix.Core;
using Mix.Core.Mapping;
using Mix.Data;
using Mix.Data.Pagable;
using Mix.Data.Services;
using Mix.Library.Entities.Databases;
using Mix.Library.Entities.DtoParameters;
using Mix.Library.Entities.Dtos;
using Mix.Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Services
{
    /// <summary>
    /// EmployeeService
    /// </summary>
    /// <seealso cref="Mix.Library.Services.IEmployeeService" />
    public class EmployeeService : ApplicationService, IEmployeeService
    {
        private readonly ICompanyRepository companyRepository;
        private readonly IEmployeeRepository employeeRepository;
        private readonly IPropertyMappingService propertyMappingService;

        #region Ctor

        /// <summary>
        /// Initializes a new instance of the <see cref="EmployeeService"/> class.
        /// </summary>
        /// <param name="companyRepository">The company repository.</param>
        /// <param name="employeeRepository">The employee repository.</param>
        /// <param name="propertyMappingService"></param>
        public EmployeeService(ICompanyRepository companyRepository, IEmployeeRepository employeeRepository, IPropertyMappingService propertyMappingService)
        {
            Guards.ThrowIfNull(companyRepository);
            Guards.ThrowIfNull(employeeRepository);
            Guards.ThrowIfNull(propertyMappingService);

            this.companyRepository = companyRepository;
            this.employeeRepository = employeeRepository;
            this.propertyMappingService = propertyMappingService;
        }

        #endregion Ctor

        /// <summary>
        /// Gets the employees for company.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<PagedList<EmployeeDto>> GetEmployeesForCompany(Guid companyId, EmployeeDtoParameters parameters)
        {
            Guards.ThrowIfNull(parameters);

            var query = employeeRepository.Select.Where(t=>t.CompanyId.Equals(companyId));

            var mappingDictionary = propertyMappingService.GetPropertyMapping<EmployeeDto, Employee>();

            query = query.ApplySort(parameters.OrderBy, mappingDictionary);

            var pagedEmployees = await PagedList<EmployeeDto>.CreateAsync(
                query,
                parameters.PageNumber,
                parameters.PageSize,
                Mapper);

            return pagedEmployees;
        }
    }
}