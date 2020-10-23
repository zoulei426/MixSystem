using Mix.Library.Entities.Models;
using Refit;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mix.Desktop.WebApis
{
    public interface IMixApi
    {
        [Get("/api/companies")]
        Task<IEnumerable<CompanyDto>> GetCompaniesAsync();
    }
}
