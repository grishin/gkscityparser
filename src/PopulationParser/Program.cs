using CsvHelper;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PopulationParser
{
    class City
    {
        public string CityName { get; set; }
        public string SubjectName { get; set; }
        public int Population { get; set; }
    }

    class Program
    {
        const string FileName = "Tabl-22-23-24-25-18.xlsx";
        const int StartFromLine = 6;

        static void Main()
        {
            var cities = new List<City>();
            using (var pck = new ExcelPackage(new FileInfo(FileName)))
            {
                // some basic checks
                if (pck.Workbook == null) throw new Exception("Corrupted XLSX file");
                if (pck.Workbook.Worksheets.Count == 0) throw new Exception("Corrupted XLSX file");

                int currentLine = StartFromLine;

                bool firstWorksheet = true;
                foreach (var ws in pck.Workbook.Worksheets)
                {
                    if (firstWorksheet) currentLine = StartFromLine + 1;
                    while (!string.IsNullOrEmpty(ws.Cells[currentLine, 1].Value?.ToString() ?? ""))
                    {
                        var item = new City
                        {
                            CityName = ws.Cells[currentLine, 2].Value.ToString().Trim().Replace("г. ", ""),
                            SubjectName = ws.Cells[currentLine, 3].Value.ToString().Trim(),
                            Population = Convert.ToInt32(ws.Cells[currentLine, 4].Value)
                        };

                        cities.Add(item);
                        currentLine++;
                    }
                    firstWorksheet = false;
                }
            }

            using (var file = new StreamWriter("population.csv", false, Encoding.GetEncoding(1251)))
            {
                var csv = new CsvWriter(file);
                csv.WriteRecords(cities);
            }
        }
    }
}
