using Mix.Data.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Entities.Databases
{
    public class Company : AduitEntity
    {
        public string Name { get; set; }

        public string Introduction { get; set; }

        public ICollection<Employee> Employees { get; set; }
    }
}