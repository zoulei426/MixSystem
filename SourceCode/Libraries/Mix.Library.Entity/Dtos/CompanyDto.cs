using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Library.Entities.Dtos
{
    public class CompanyDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string Introduction { get; set; }
    }
}