﻿using NPOI.HPSF;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.SS.Util;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Text;
using System.Web;

namespace Common
{
    public class NPOIHelper
    {
        public static object CellHorizontalAlignment { get; private set; }

        /// <summary>
        /// DataTable导出到Excel文件
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">保存位置</param>
        public static void Export(DataTable dtSource, string strHeaderText, string strFileName)
        {
            using (MemoryStream ms = Export(dtSource, strHeaderText))
            {
                using (FileStream fs = new FileStream(strFileName, FileMode.Create, FileAccess.Write))
                {
                    byte[] data = ms.ToArray();
                    fs.Write(data, 0, data.Length);
                    fs.Flush();
                }
            }
        }

        /// <summary>
        /// DataTable导出到Excel的MemoryStream
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        public static MemoryStream Export(DataTable dtSource, string strHeaderText, Dictionary<string, string> map = null)
        {
            HSSFWorkbook workbook = new HSSFWorkbook();
            ISheet sheet = workbook.CreateSheet();

            #region 右击文件 属性信息

            {
                DocumentSummaryInformation dsi = PropertySetFactory.CreateDocumentSummaryInformation();
                dsi.Company = "NPOI";
                workbook.DocumentSummaryInformation = dsi;

                SummaryInformation si = PropertySetFactory.CreateSummaryInformation();
                si.Author = "文件作者信息"; //填加xls文件作者信息
                si.ApplicationName = "创建程序信息"; //填加xls文件创建程序信息
                si.LastAuthor = "最后保存者信息"; //填加xls文件最后保存者信息
                si.Comments = "作者信息"; //填加xls文件作者信息
                si.Title = "标题信息"; //填加xls文件标题信息

                si.Subject = "主题信息";//填加文件主题信息
                si.CreateDateTime = DateTime.Now;
                workbook.SummaryInformation = si;
            }

            #endregion 右击文件 属性信息

            ICellStyle dateStyle = workbook.CreateCellStyle();
            IDataFormat format = workbook.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy-mm-dd");

            //取得列宽
            int[] arrColWidth = new int[dtSource.Columns.Count];
            if(map != null)
            {
                dtSource = MappingColumn(dtSource, map);
            }
            foreach (DataColumn item in dtSource.Columns)
            {
                arrColWidth[item.Ordinal] = Encoding.GetEncoding(936).GetBytes(item.ColumnName.ToString()).Length;
            }
            for (int i = 0; i < dtSource.Rows.Count; i++)
            {
                for (int j = 0; j < dtSource.Columns.Count; j++)
                {
                    int intTemp = Encoding.GetEncoding(936).GetBytes(dtSource.Rows[i][j].ToString()).Length;
                    if (intTemp > arrColWidth[j])
                    {
                        arrColWidth[j] = intTemp;
                    }
                }
            }
            int rowIndex = 0;
            foreach (DataRow row in dtSource.Rows)
            {
                #region 新建表，填充表头，填充列头，样式

                if (rowIndex == 65535 || rowIndex == 0)
                {
                    if (rowIndex != 0)
                    {
                        sheet = workbook.CreateSheet();
                    }

                    #region 表头及样式

                    if (!string.IsNullOrEmpty(strHeaderText))
                    {
                        IRow headerRow = sheet.CreateRow(0);
                        headerRow.HeightInPoints = 25;
                        headerRow.CreateCell(0).SetCellValue(strHeaderText);

                        ICellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.Center;
                        IFont font = workbook.CreateFont();
                        font.FontHeightInPoints = 20;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        headerRow.GetCell(0).CellStyle = headStyle;
                        sheet.AddMergedRegion(new CellRangeAddress(0, 0, 0, dtSource.Columns.Count - 1));
                    }

                    #endregion 表头及样式

                    #region 列头及样式

                    {
                        IRow headerRow;
                        if (!string.IsNullOrEmpty(strHeaderText))
                            headerRow = sheet.CreateRow(1);
                        else
                            headerRow = sheet.CreateRow(0);
                        ICellStyle headStyle = workbook.CreateCellStyle();
                        headStyle.Alignment = HorizontalAlignment.Center;
                        IFont font = workbook.CreateFont();
                        font.FontHeightInPoints = 10;
                        font.Boldweight = 700;
                        headStyle.SetFont(font);
                        foreach (DataColumn column in dtSource.Columns)
                        {
                            headerRow.CreateCell(column.Ordinal).SetCellValue(column.ColumnName);
                            headerRow.GetCell(column.Ordinal).CellStyle = headStyle;

                            //设置列宽
                            sheet.SetColumnWidth(column.Ordinal, (arrColWidth[column.Ordinal] + 1) * 256);
                        }
                    }

                    #endregion 列头及样式

                    if (!string.IsNullOrEmpty(strHeaderText))
                        rowIndex = 2;
                    else
                        rowIndex = 1;
                }

                #endregion 新建表，填充表头，填充列头，样式

                IRow dataRow = sheet.CreateRow(rowIndex);

                #region 填充内容

                foreach (DataColumn column in dtSource.Columns)
                {
                    ICell newCell = dataRow.CreateCell(column.Ordinal);

                    string drValue = row[column].ToString();

                    switch (column.DataType.ToString())
                    {
                        case "System.String"://字符串类型
                            newCell.SetCellValue(drValue);
                            break;

                        case "System.DateTime"://日期类型
                            DateTime dateV;
                            if(DateTime.TryParse(drValue, out dateV))
                                newCell.SetCellValue(dateV);
                            else
                                newCell.SetCellValue(drValue);
                            newCell.CellStyle = dateStyle;//格式化显示
                            break;

                        case "System.Boolean"://布尔型
                            bool boolV = false;
                            if(bool.TryParse(drValue, out boolV))
                                newCell.SetCellValue(boolV);
                            else
                                newCell.SetCellValue(drValue);
                            break;

                        case "System.Int16"://整型
                        case "System.Int32":
                        case "System.Int64":
                        case "System.Byte":
                            int intV = 0;
                            if(int.TryParse(drValue, out intV))
                                newCell.SetCellValue(intV);
                            else
                                newCell.SetCellValue(drValue);
                            break;

                        case "System.Decimal"://浮点型
                        case "System.Double":
                            double doubV = 0;
                            if(double.TryParse(drValue, out doubV))
                                newCell.SetCellValue(doubV);
                            else
                                newCell.SetCellValue(drValue);
                            break;

                        case "System.DBNull"://空值处理
                            newCell.SetCellValue("");
                            break;

                        default:
                            newCell.SetCellValue("");
                            break;
                    }
                }

                #endregion 填充内容

                rowIndex++;
            }
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                //workbook.Dispose();//一般只用写这一个就OK了，他会遍历并释放所有资源，但当前版本有问题所以只释放sheet
                return ms;
            }
        }

        /// <summary>
        /// 用于Web导出
        /// </summary>
        /// <param name="dtSource">源DataTable</param>
        /// <param name="strHeaderText">表头文本</param>
        /// <param name="strFileName">文件名</param>
        public static void ExportByWeb(DataTable dtSource, string strHeaderText, string strFileName, Dictionary<string, string> map = null)
        {
            HttpContext curContext = HttpContext.Current;

            // 设置编码和附件格式
            curContext.Response.ContentType = "application/vnd.ms-excel";
            curContext.Response.ContentEncoding = Encoding.UTF8;
            curContext.Response.Charset = "";
            curContext.Response.AppendHeader("Content-Disposition",
                "attachment;filename=" + HttpUtility.UrlEncode(strFileName, Encoding.UTF8));

            curContext.Response.BinaryWrite(Export(dtSource, strHeaderText, map).GetBuffer());
            curContext.Response.End();
        }

        /// <summary>读取excel
        /// 默认第一行为标头
        /// </summary>
        /// <param name="strFileName">excel文档路径</param>
        /// <returns></returns>
        public static DataTable Import(string strFileName)
        {
            DataTable dt = new DataTable();

            HSSFWorkbook hssfworkbook;
            using (FileStream file = new FileStream(strFileName, FileMode.Open, FileAccess.Read))
            {
                hssfworkbook = new HSSFWorkbook(file);
            }
            ISheet sheet = hssfworkbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();

            IRow headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;

            for (int j = 0; j < cellCount; j++)
            {
                ICell cell = headerRow.GetCell(j);
                dt.Columns.Add(cell.ToString());
            }

            for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                DataRow dataRow = dt.NewRow();

                for (int j = row.FirstCellNum; j < cellCount; j++)
                {
                    if (row.GetCell(j) != null)
                        dataRow[j] = row.GetCell(j).ToString();
                }

                dt.Rows.Add(dataRow);
            }
            return dt;
        }

        private static DataTable MappingColumn(DataTable table, Dictionary<string, string> map)
        {
            List<string> removeList = new List<string>();
            foreach(DataColumn col in table.Columns)
            {
                if (!map.ContainsKey(col.ColumnName))
                    removeList.Add(col.ColumnName);
            }
            foreach(var colName in removeList)
            {
                table.Columns.Remove(colName);
            }
            foreach(var row in map)
            {
                if (table.Columns.Contains(row.Key))
                    table.Columns[row.Key].ColumnName = row.Value;
            }
            return table;
        }
    }
}