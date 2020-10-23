using Mix.Data.Entities;
using System.Collections.Generic;

namespace Mix.Library.Entities.Databases
{
    public class Company : AduitEntity
    {
        public string Name { get; set; }

        public string Introduction { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}