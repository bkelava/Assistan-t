using Accountant_s_Assistant.Database.Tables;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Accountant_s_Assistant.App
{
    class PDFCreator
    {
        private string path;
        public PDFCreator(string path)
        {
            this.path = path;
        }

        public string generateContractOnDefinitiveTime(List<KeyValuePair<string, string>> list, Employer employer, Employee employee)
        {
            string filename = "Ugovor" + employee.Name + " " + employer.Name;
            string path = Path.Combine(this.path, filename);

            PdfDocument pdfDocument = new PdfDocument();
            PdfPage page = pdfDocument.AddPage();

            page.Orientation = PdfSharp.PageOrientation.Portrait;
            page.Size = PdfSharp.PageSize.A4;
            XFont xfont = new XFont("Arial", 20);
            XGraphics xGraphics = XGraphics.FromPdfPage(page);

            pdfDocument.Save(path);
            return path;
        }
    }
}
