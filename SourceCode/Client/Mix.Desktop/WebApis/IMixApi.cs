﻿using Mix.Library.Entities.Dtos;
using Refit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mix.Desktop.WebApis
{
    public interface IMixApi
    {
        [Get("/api/companies")]
        Task<IEnumerable<CompanyDto>> GetCompaniesAsync();

        [Get("/api/companies/{companyId}/employees")]
        Task<IEnumerable<EmployeeDto>> GetEmployeesForCompany(Guid companyId);
    }
}