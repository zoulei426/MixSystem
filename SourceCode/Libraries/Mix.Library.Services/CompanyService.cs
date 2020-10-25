﻿using Mix.Data.Repositories;
using Mix.Data.Services;
using Mix.Library.Entities.Databases;
using Mix.Library.Entities.Dtos;
using Mix.Library.Repositories;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mix.Library.Services
{
    /// <summary>
    /// CompanyService
    /// </summary>
    /// <seealso cref="Mix.Data.Services.ApplicationService" />
    /// <seealso cref="Mix.Library.Services.ICompanyService" />
    public class CompanyService : ApplicationService, ICompanyService
    {
        private readonly IAuditBaseRepository<Company> companyRepository;
        private readonly IEmployeeRepository employeeRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompanyService"/> class.
        /// </summary>
        /// <param name="companyRepository">The company repository.</param>
        /// <param name="employeeRepository">The employee repository.</param>
        public CompanyService(IAuditBaseRepository<Company> companyRepository, IEmployeeRepository employeeRepository)
        {
            this.companyRepository = companyRepository;
            this.employeeRepository = employeeRepository;
        }

        /// <summary>
        /// Creates the company asynchronous.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <returns></returns>
        //[Transactional]
        public async Task<CompanyDto> CreateCompanyAsync(CompanyAddDto company)
        {
            var entity = Mapper.Map<Company>(company);

            var result = companyRepository.Insert(entity);
            result.Employees.ForEach(t => t.CompanyId = result.Id);
            await employeeRepository.InsertAsync(result.Employees);

            return Mapper.Map<CompanyDto>(result);
        }

        /// <summary>
        /// Creates the company collection asynchronous.
        /// </summary>
        /// <param name="companieCollection">The companie collection.</param>
        /// <returns></returns>
        //[Transactional]
        public async Task<IEnumerable<CompanyDto>> CreateCompanyCollectionAsync(IEnumerable<CompanyAddDto> companieCollection)
        {
            var entityCollection = Mapper.Map<IEnumerable<Company>>(companieCollection);
            foreach (var entity in entityCollection)
            {
                entity.Employees.ForEach(t => t.CompanyId = entity.Id);
                await employeeRepository.InsertAsync(entity.Employees);
            }

            var resultCollection = await companyRepository.InsertAsync(entityCollection);

            return Mapper.Map<IEnumerable<CompanyDto>>(resultCollection);
        }
    }
}