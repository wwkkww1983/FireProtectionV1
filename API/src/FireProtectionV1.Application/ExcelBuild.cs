using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace FireProtectionV1
{
    class WorkSheet
    {
        ExcelWorksheet _worksheet;
        int _row = 1;
        public WorkSheet(ExcelWorksheet worksheet)
        {
            _worksheet = worksheet;
        }
        //public void AddColumnHeads(string[] headNames)
        //{
        //    for (int i=1;i<=headNames.Length;i++)
        //    {
        //        _worksheet.Cells[1, i].Value = headNames[i];
        //    }
        //}
        public void AddRowValues(string[] values)
        {
            for (int i = 0; i < values.Length; i++)
            {
                _worksheet.Cells[_row, i+1].Value = values[i];
            }
            _row++;
        }
    }
    class ExcelBuild :IDisposable
    {
        MemoryStream _stream;
        ExcelPackage _package;
        public ExcelBuild()
        {
            _stream = new MemoryStream();
            _package = new ExcelPackage(_stream);
        }
        public WorkSheet BuildWorkSheet(string sheetName)
        {
            return new WorkSheet(_package.Workbook.Worksheets.Add(sheetName));
        }
        /// <summary>
        /// 构建文件字节数据
        /// </summary>
        /// <returns></returns>
        public byte[] BuildFileBytes()
        {
            _package.Save();
            return _stream.ToArray();
        }

        public void Dispose()
        {
            _package.Dispose();
            _stream.Dispose();
        }
    }
}
