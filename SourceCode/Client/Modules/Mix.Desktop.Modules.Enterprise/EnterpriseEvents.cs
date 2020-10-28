using Mix.Library.Entities.Dtos;
using Prism.Events;

namespace Mix.Desktop.Modules.Enterprise
{
    internal class GetEmployeesForCompanyEvent : PubSubEvent<CompanyDto> { }
}