﻿using Mix.Library.Entities.Databases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Entities.Dtos
{
    public class EmployeeDto
    {
        public Guid Id { get; set; }

        public Guid CompanyId { get; set; }

        public string EmployeeNo { get; set; }

        public string Name { get; set; }

        public string GenderDisplay { get; set; }

        public int Age { get; set; }
    }
}