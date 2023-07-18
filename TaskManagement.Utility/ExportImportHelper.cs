using Microsoft.AspNetCore.Http;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace TaskManagement.Utility
{
    public class ExportImportHelper
    {
        public static void WriteData<T>(List<T> exportData, IWorkbook _workbook, ISheet _sheet, bool isExcel = true)
        {
            var _headers = new List<string>();
            var _type = new List<string>();

            var properties = TypeDescriptor.GetProperties(typeof(T));
            var table = new DataTable();
            foreach (PropertyDescriptor prop in properties)
            {
                var property = typeof(T).GetProperty(prop.Name);
                if ((!Attribute.IsDefined(property, typeof(IgnoreInExportAttribute))) &&
                    (!Attribute.IsDefined(property, typeof(IsExcelAttribute))))
                {
                    var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    _type.Add(type.Name);
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                    string name = Regex.Replace(property.GetCustomAttribute<DisplayAttribute>()?.Name ?? prop.Name, "([A-Z])", "$1").Trim();
                    _headers.Add(name);
                }

                if (isExcel && (Attribute.IsDefined(property, typeof(IsExcelAttribute))))
                {
                    var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
                    _type.Add(type.Name);
                    table.Columns.Add(prop.Name, Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType);
                    string name = Regex.Replace(property.GetCustomAttribute<DisplayAttribute>()?.Name ?? prop.Name, "([A-Z])", "$1").Trim();
                    _headers.Add(name);
                }

            }

            foreach (T item in exportData)
            {
                var row = table.NewRow();
                foreach (PropertyDescriptor prop in properties)
                {
                    var property = typeof(T).GetProperty(prop.Name);
                    if ((!Attribute.IsDefined(property, typeof(IgnoreInExportAttribute))) &&
                        (!Attribute.IsDefined(property, typeof(IsExcelAttribute))))
                    {
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    }

                    if (isExcel && (Attribute.IsDefined(property, typeof(IsExcelAttribute))))
                    {
                        row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                    }

                }
                table.Rows.Add(row);
            }

            var rowStyle = _workbook.CreateCellStyle(); //Formatting
            rowStyle.BorderBottom = rowStyle.BorderLeft =
               rowStyle.BorderRight = rowStyle.BorderTop = BorderStyle.Medium;
            for (int i = 0; i < table.Rows.Count; i++)
            {
                var sheetRow = _sheet.CreateRow(i + 1);
                for (int j = 0; j < table.Columns.Count; j++)
                {
                    var cell = sheetRow.CreateCell(j);
                    cell.CellStyle = rowStyle;
                    string cellvalue;
                    if (_type[j].ToLower() != "list`1")
                    {
                        cellvalue = Convert.ToString(table.Rows[i][j]);
                    }
                    else
                    {
                        cellvalue = JsonSerializer.Serialize(table.Rows[i][j]);
                    }

                    if (string.IsNullOrWhiteSpace(cellvalue))
                    {
                        cell.SetCellValue(string.Empty);
                    }
                    else
                    {
                        switch (_type[j].ToLower())
                        {
                            case "string":
                                cell.SetCellValue(cellvalue);
                                break;
                            case "int32":
                                cell.SetCellValue(Convert.ToInt32(table.Rows[i][j]));
                                break;
                            case "double":
                                cell.SetCellValue(Convert.ToDouble(table.Rows[i][j]));
                                break;
                            case "datetime":
                                cell.SetCellValue(Convert.ToDateTime
                                 (table.Rows[i][j]).ToString("dd/MM/yyyy"));
                                break;
                            case "list`1":
                                cell.SetCellValue(cellvalue);
                                break;
                            default:
                                cell.SetCellValue(cellvalue.ToString());
                                break;
                        }
                    }
                }
            }

            var headerStyle = _workbook.CreateCellStyle(); //Formatting
            var headerFont = _workbook.CreateFont();
            headerFont.IsBold = true;
            var columnStyle = _workbook.CreateCellStyle(); //Formatting
            var defaultDataFormat = _workbook.CreateDataFormat();
            columnStyle.DataFormat = defaultDataFormat.GetFormat("text");
            headerStyle.SetFont(headerFont);
            headerStyle.BorderBottom = headerStyle.BorderLeft =
                headerStyle.BorderRight = headerStyle.BorderTop = BorderStyle.Medium;
            var header = _sheet.CreateRow(0);
            for (var i = 0; i < _headers.Count; i++)
            {
                var cell = header.CreateCell(i);
                cell.SetCellValue(_headers[i]);
                cell.CellStyle = headerStyle;
                _sheet.AutoSizeColumn(i);
                _sheet.SetDefaultColumnStyle(i, columnStyle);
            }
        }

        public static DataTable GetDataTable(IFormFile file)
        {
            ISheet sheet;
            var fileExt = Path.GetExtension(file.FileName);
            if (fileExt == ".xls")
            {
                var hssfwb = new HSSFWorkbook(file.OpenReadStream());
                sheet = hssfwb.GetSheetAt(0);
            }
            else
            {
                var hssfwb = new XSSFWorkbook(file.OpenReadStream());
                sheet = hssfwb.GetSheetAt(0);
            }

            var table = new DataTable();
            var headerRow = sheet.GetRow(0);
            int cellCount = headerRow.LastCellNum;
            int lastRow = sheet.LastRowNum;
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                var column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                if (!string.IsNullOrEmpty(column.ColumnName))
                    table.Columns.Add(column);
                else
                    break;
            }
            for (int i = (sheet.FirstRowNum + 1); i <= lastRow; i++)
            {
                var row = sheet.GetRow(i);
                if (row != null)
                {
                    var dataRow = table.NewRow();
                    if (row.FirstCellNum >= 0)
                    {
                        for (int j = row.FirstCellNum; j < table.Columns.Count; j++)
                        {
                            if (row.GetCell(j) != null || !string.IsNullOrEmpty(row.GetCell(j).ToString()))
                            {
                                dataRow[j] = row.GetCell(j).ToString();
                            }
                        }
                        table.Rows.Add(dataRow);
                    }

                }
                else
                {
                    break;
                }
            }
            return table;
        }
    }

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class IgnoreInExportAttribute : Attribute
    {
        //Class Members
    }

    [AttributeUsage(AttributeTargets.All, Inherited = true, AllowMultiple = false)]
    public class IsExcelAttribute : Attribute
    {
        //Class Members
    }
}
