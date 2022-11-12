using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace CalcTest
{
    public class ExcelHelper
    {
        public static void GetExcelValuesFromFile(string path, ref List<double> xValues, ref List<double> yValues)
        {
            string fileName = @"C:\Users\simon\source\repos\CalcTest\CalcTest\assets\values.xlsx";
            //string fileName = @"values.xlsx";

            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (SpreadsheetDocument doc = SpreadsheetDocument.Open(fs, false))
                {
                    WorkbookPart bkPart = doc.WorkbookPart;
                    Workbook workbook = bkPart.Workbook;
                    Sheet s = workbook.Descendants<DocumentFormat.OpenXml.Spreadsheet.Sheet>().Where(sht => sht.Name == "Sheet1").FirstOrDefault();
                    WorksheetPart wsPart = (WorksheetPart)bkPart.GetPartById(s.Id);
                    SheetData sheetdata = wsPart.Worksheet.Elements<DocumentFormat.OpenXml.Spreadsheet.SheetData>().FirstOrDefault();
                    foreach (Row row in sheetdata.Elements<DocumentFormat.OpenXml.Spreadsheet.Row>())
                    {
                        foreach (Cell c in row.Elements<Cell>())
                        {
                            if (c.CellValue != null)
                            {
                                if (c.CellReference.InnerText.IndexOf("A") != -1)
                                {
                                    var value = c.CellValue.Text.Replace(".", ",");
                                    xValues.Add(Double.Parse(value));
                                }
                                else if (c.CellReference.InnerText.IndexOf("B") != -1)
                                {
                                    var value = c.CellValue.Text.Replace(".", ",");
                                    yValues.Add(Double.Parse(value));
                                }
                            }

                            else return;
                        }
                    }
                }
            }
        }
    }
}
