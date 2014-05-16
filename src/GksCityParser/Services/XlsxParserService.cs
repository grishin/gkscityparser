using GksCityParser.Models;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GksCityParser.Services
{
    /// <summary>
    /// Parse Gks cities file in XLXS format
    /// </summary>
    class XlsxParserService
    {
        private string FileName {get;set;}

        public XlsxParserService(string filename)
        {
            FileName = filename;
        }


        /// <summary>
        /// Read file content 
        /// </summary>
        public IEnumerable<City> ParseCities(int startFromLine)
        {
            using (var pck = new ExcelPackage(new FileInfo(FileName))) {
                // some basic checks
                if (pck.Workbook == null) { throw new Exception("Corrupted XLSX file"); }
                if (pck.Workbook.Worksheets.Count == 0) { throw new Exception("Corrupted XLSX file");  }

                int currentLine = startFromLine;
                var ws = pck.Workbook.Worksheets[0];

                var data = new List<City>();
                while (!String.IsNullOrEmpty(ws.Cells[currentLine, 1].Value.ToString())) {
                    var item = new City {
                        City = ws.Cells[currentLine, 2].Value.ToString(),
                        Subject = ws.Cells[currentLine, 3].Value.ToString(),
                        Population = Convert.ToInt32(ws.Cells[currentLine, 4].Value)
                    };

                    data.Add(item);
                }

                return data;
            }
        }
    }
}
