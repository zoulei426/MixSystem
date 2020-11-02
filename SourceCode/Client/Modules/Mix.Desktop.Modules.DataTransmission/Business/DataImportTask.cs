using MaterialDesignColors.Recommended;
using Mix.Core;
using Mix.Data.Excel;
using Mix.Library.Entities.Databases.HouseSites;
using Mix.Library.Repositories.HouseSites;
using Mix.Library.Services;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Mix.Desktop.Modules.DataTransmission.Business
{
    public class DataImportTask
    {
        public static IFreeSql fsql { get; } =
        new FreeSql.FreeSqlBuilder().UseConnectionString(FreeSql.DataType.PostgreSQL, @"Host=;Port=5432;Database=zjddc;Username=postgres;Password=123456;")
        .UseAutoSyncStructure(false) //自动同步实体结构到数据库
            .UseMonitorCommand(cmd =>
            {
                Trace.WriteLine(cmd.CommandText + ";");
            }
                   )
        .Build();

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
                        Id = Guid.NewGuid(),
                        //Dzxq = row[1].ToStringSafe().Trim(),
                        Hzxm = row[1].ToStringSafe().Trim(),
                        Zjhm = row[5].ToStringSafe().Trim(),
                        Sjhm = row[6].ToStringSafe().Trim()
                    };
                    if (currentJcxx.Hzxm.IsNullOrEmpty() || currentJcxx.Zjhm.IsNullOrEmpty()) continue;
                    jcxxList.Add(currentJcxx);

                    jtrs = 0;
                }

                var cyxx = new Cyxx
                {
                    Id = Guid.NewGuid(),
                    JcxxId = currentJcxx.Id,
                    Xm = row[1].ToStringSafe().Trim(),
                    Xb = ToGender(row[2].ToStringSafe().Trim()),
                    Zjhm = row[5].ToStringSafe().Trim()
                };
                if (cyxx.Xm.IsNullOrEmpty() || cyxx.Zjhm.IsNullOrEmpty()) continue;

                if ("是".Equals(row[3].ToStringSafe().Trim()))
                {
                    currentJcxx.Hzxm = cyxx.Xm;
                    currentJcxx.Zjhm = cyxx.Zjhm;
                }

                cyxxList.Add(cyxx);

                jtrs++;
            }
            if (currentJcxx is not null)
                currentJcxx.Jtrs = jtrs;

            foreach (var jcxx in jcxxList)
            {
                if (await fsql.Select<Jcxx>().Where(t => t.Hzxm.Equals(jcxx.Hzxm) && t.Zjhm.Equals(jcxx.Zjhm)).AnyAsync())
                {
                    fsql.Update<Jcxx>().Where(t => t.Hzxm.Equals(jcxx.Id) && t.Zjhm.Equals(jcxx.Zjhm))
                        //.Set(t => t.Dzxq, jcxx.Dzxq)
                        .Set(t => t.Jtrs, jcxx.Jtrs)
                        .Set(t => t.Sjhm, jcxx.Sjhm).ExecuteAffrows();
                }
                else
                {
                    fsql.Insert(jcxx).ExecuteAffrows();
                }
            }
            foreach (var cyxx in cyxxList)
            {
                if (await fsql.Select<Cyxx>().Where(t => t.Xm.Equals(cyxx.Xm) && t.Zjhm.Equals(cyxx.Zjhm)).AnyAsync())
                {
                    fsql.Update<Cyxx>().Where(t => t.Xm.Equals(cyxx.Xm) && t.Zjhm.Equals(cyxx.Zjhm))
                        .Set(t => t.Xb, cyxx.Xb).ExecuteAffrows();
                }
                else
                {
                    fsql.Insert(cyxx).ExecuteAffrows();
                }
            }

            //await houseSiteService.InsertOrUpdateJcxxAndCyxx(jcxxList, cyxxList);
        }

        private int? ToGender(string v)
        {
            if ("男".Equals(v)) return 1;
            if ("女".Equals(v)) return 0;
            return null;
        }
    }
}
