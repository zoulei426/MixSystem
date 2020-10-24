using FreeSql;
using Mix.Data.Aop.Attributes;
using Mix.Data.Repositories;
using Mix.Data.Services;
using Mix.Library.Entities.Databases;
using Mix.Library.Entities.Dtos;
using Mix.Library.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Services
{
    public class CompanyService : ApplicationService, ICompanyService
    {
        private readonly IAuditBaseRepository<Company> companyRepository;
        private readonly IEmployeeRepository employeeRepository;

        public CompanyService(IAuditBaseRepository<Company> companyRepository, IEmployeeRepository employeeRepository)
        {
            this.companyRepository = companyRepository;
            this.employeeRepository = employeeRepository;
        }

        //[Transactional]
        public async Task<CompanyDto> CreateCompanyAsync(CompanyAddDto company)
        {
            var entity = Mapper.Map<Company>(company);

            var result = companyRepository.Insert(entity);
            result.Employees.ForEach(t => t.CompanyId = result.Id);
            await employeeRepository.InsertAsync(result.Employees);

            return Mapper.Map<CompanyDto>(result);
        }

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