using CsvHelper.Configuration;
using Mix.Library.Entities.Databases.HouseSites;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mix.Desktop.Modules.DataTransmission.Models
{
    public class JcxxMap : ClassMap<Jcxx>
    {
        public JcxxMap()
        {
            Map(m => m.Id).Index(0).Name("ID");
        }
    }
}