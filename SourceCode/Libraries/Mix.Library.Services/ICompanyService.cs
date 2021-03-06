﻿using Mix.Data.Pagable;
using Mix.Library.Entities.Databases;
using Mix.Library.Entities.DtoParameters;
using Mix.Library.Entities.Dtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mix.Library.Services
{
    /// <summary>
    /// ICompanyService
    /// </summary>
    public interface ICompanyService
    {
        /// <summary>
        /// Gets the companies asynchronous.
        /// </summary>
        /// <param name="parameters">The parameters.</param>
        /// <returns></returns>
        Task<PagedList<Company>> GetCompaniesAsync(CompanyDtoParameters parameters);

        /// <summary>
        /// Creates the company asynchronous.
        /// </summary>
        /// <param name="company">The company.</param>
        /// <returns></returns>
        Task<CompanyDto> CreateCompanyAsync(CompanyAddDto company);

        /// <summary>
        /// Creates the company collection asynchronous.
        /// </summary>
        /// <param name="companieCollection">The companie collection.</param>
        /// <returns></returns>
        Task<IEnumerable<CompanyDto>> CreateCompanyCollectionAsync(IEnumerable<CompanyAddDto> companieCollection);

        /// <summary>
        /// Deletes the company.
        /// </summary>
        /// <param name="companyId">The company identifier.</param>
        /// <returns></returns>
        Task DeleteCompany(Guid companyId);
    }
}