using Mix.Library.Entities.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Entities.Dtos
{
    public class EmployeeAddDto
    {
        public string EmployeeNo { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender Gender { get; set; }

        public DateTime DateOfBirth { get; set; }
    }
}