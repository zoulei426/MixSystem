using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace Mix.Data.Excel
{
    /// <summary>
    /// ExcelReader
    /// </summary>
    public class ExcelReader
    {
        #region Properties

        /// <summary>
        /// 读取文件路径
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// 是否包含标题
        /// </summary>
        /// <value>
        ///   <c>true</c> if [contains titles]; otherwise, <c>false</c>.
        /// </value>
        public bool ContainsTitles { get; set; }

        /// <summary>
        /// Gets or sets the index of the sheet.
        /// </summary>
        /// <value>
        /// The index of the sheet.
        /// </value>
        public int SheetIndex { get; set; }

        /// <summary>
        /// 起始列
        /// </summary>
        public int StartRow { get; set; }

        #endregion Properties

        #region Fields

        private FileStream fs = null;
        private IWorkbook workbook = null;
        private IFormulaEvaluator evalor = null;

        #endregion Fields

        #region Ctor

        public ExcelReader(string filePath, bool containsTitles = false, int startRow = 0)
        {
            FilePath = filePath;
            ContainsTitles = containsTitles;
            StartRow = startRow;

            Initialize();
        }

        #endregion Ctor

        /// <summary>
        /// Initializes this instance.
        /// </summary>
        public void Initialize()
        {
            fs = File.OpenRead(FilePath);

            // 2007版本
            if (FilePath.IndexOf(".xlsx") > 0)
            {
                workbook = new XSSFWorkbook(fs);
                evalor = new XSSFFormulaEvaluator(workbook);
            }
            // 2003版本
            else if (FilePath.IndexOf(".xls") > 0)
            {
                workbook = new HSSFWorkbook(fs);
                evalor = new HSSFFormulaEvaluator(workbook);
            }
        }

        /// <summary>
        /// Flushes the and close asynchronous.
        /// </summary>
        public async Task FlushAndCloseAsync()
        {
            await fs.FlushAsync();
            fs.Close();
        }

        /// <summary>
        /// Closes this instance.
        /// </summary>
        public void Close()
        {
            fs?.Close();
        }

        /// <summary>
        /// 将excel导入到datatable
        /// </summary>
        /// <returns>返回datatable</returns>
        public Task<DataTable> ExcelToDataTableAsync()
        {
            if (workbook is null) throw new Exception($"workbook is null");

            return Task.Run(() =>
            {
                DataTable dataTable = null;
                DataColumn column = null;
                DataRow dataRow = null;
                ISheet sheet = null;
                IRow row = null;
                ICell cell = null;

                dataTable = new DataTable();
                for (int sheetIndex = 0; sheetIndex < workbook.NumberOfSheets; sheetIndex++)
                {
                    sheet = workbook.GetSheetAt(sheetIndex);//读取第一个sheet，当然也可以循环读取每个sheet
                    if (sheet is null) throw new Exception($"Not found sheet");

                    int rowCount = sheet.LastRowNum;//总行数
                    if (rowCount == 0) return null;

                    IRow firstRow = sheet.GetRow(0);//第一行
                    int cellCount = firstRow.LastCellNum;//列数

                    //构建datatable的列
                    if (ContainsTitles && sheetIndex == 0)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                if (cell.StringCellValue != null)
                                {
                                    column = new DataColumn(cell.StringCellValue);
                                    dataTable.Columns.Add(column);
                                }
                            }
                        }
                    }
                    else if (!ContainsTitles && sheetIndex == 0)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            column = new DataColumn("column" + (i + 1));
                            dataTable.Columns.Add(column);
                        }
                    }

                    //填充行
                    for (int i = StartRow; i <= rowCount; ++i)
                    {
                        row = sheet.GetRow(i);
                        if (row == null) continue;

                        dataRow = dataTable.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (j > 6) continue;
                            cell = row.GetCell(j);
                            if (cell == null)
                            {
                                continue;
                                dataRow[j] = "";
                            }
                            else
                            {
                                //CellType(Unknown = -1,Numeric = 0,String = 1,Formula = 2,Blank = 3,Boolean = 4,Error = 5,)
                                switch (cell.CellType)
                                {
                                    case CellType.Blank:
                                        dataRow[j] = "";
                                        break;

                                    case CellType.Numeric:
                                        short format = cell.CellStyle.DataFormat;
                                        //对时间格式（2015.12.5、2015/12/5、2015-12-5等）的处理
                                        if (format == 14 || format == 31 || format == 57 || format == 58)
                                            dataRow[j] = cell.DateCellValue;
                                        else
                                            dataRow[j] = cell.NumericCellValue;
                                        break;

                                    case CellType.String:
                                        dataRow[j] = cell.StringCellValue;
                                        break;

                                    case CellType.Formula:
                                        //针对公式列 进行动态计算;注意：公式暂时只支持 数值 字符串类型
                                        var formulaValue = evalor.Evaluate(cell);
                                        if (formulaValue.CellType == CellType.Numeric)
                                        {
                                            dataRow[j] = formulaValue.NumberValue;
                                        }
                                        else if (formulaValue.CellType == CellType.String)
                                        {
                                            dataRow[j] = formulaValue.StringValue;
                                        }

                                        break;
                                }
                            }
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                }

                return dataTable;
            });
        }
    }
}