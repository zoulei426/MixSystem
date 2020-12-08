using Mix.Core;
using Mix.Data.Excel;
using Mix.Library.Entities.Databases.HouseSites;
using Mix.Library.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace Mix.Desktop.Modules.DataTransmission.Business
{
    public class DataImportTask
    {
        public IHouseSiteService houseSiteService { get; set; }

        public DataImportTask()
        {
        }

        public async Task ImportDataAsync(string filePath)
        {
            var reader = new ExcelReader(filePath, true, 1);
            var dataTable = await reader.ExcelToDataTableAsync();
            reader.Close();
            if (dataTable is null) return;

            int jtrs = 0;
            var cyxxList = new List<Cyxx>(dataTable.Rows.Count);
            var jcxxList = new List<Jcxx>();
            Jcxx currentJcxx = null;
            foreach (DataRow row in dataTable.Rows)
            {
                if (row[0].ToStringSafe().IsNotNullOrEmpty())
                {
                    if (currentJcxx is not null)
                        currentJcxx.Jtrs = jtrs;

                    currentJcxx = new Jcxx
                    {
                        Id = row[3].ToStringSafe().Trim(),
                        Dzxq = row[1].ToStringSafe().Trim(),
                        Hzxm = row[2].ToStringSafe().Trim(),
                        Zjhm = row[3].ToStringSafe().Trim(),
                        //Sjhm = row[6].ToStringSafe().Trim()
                    };
                    if (currentJcxx.Hzxm.IsNullOrEmpty() || currentJcxx.Zjhm.IsNullOrEmpty()) continue;
                    jcxxList.Add(currentJcxx);

                    jtrs = 0;
                }

                var cyxx = new Cyxx
                {
                    Id = Guid.NewGuid().ToString(),
                    JcxxId = currentJcxx.Id,
                    Xm = row[4].ToStringSafe().Trim(),
                    Xb = row[5].ToStringSafe().Trim().GetGenderByICN(),
                    Zjhm = row[5].ToStringSafe().Trim()
                };
                if (cyxx.Xm.IsNullOrEmpty() || cyxx.Zjhm.IsNullOrEmpty()) continue;

                //if ("是".Equals(row[3].ToStringSafe().Trim()))
                //{
                //    currentJcxx.Hzxm = cyxx.Xm;
                //    currentJcxx.Zjhm = cyxx.Zjhm;
                //}

                cyxxList.Add(cyxx);

                jtrs++;
            }
            if (currentJcxx is not null)
                currentJcxx.Jtrs = jtrs;

            await houseSiteService.InsertOrUpdateJcxxAndCyxx(jcxxList, cyxxList);
        }

        private int? ToGender(string v)
        {
            if ("男".Equals(v)) return 1;
            if ("女".Equals(v)) return 0;
            return null;
        }
    }
}