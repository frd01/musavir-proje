using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using NReco.PdfGenerator;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace DataIslemler.Islemler
{
    public class DosyaIslem
    {
        
        public byte[] HtmlToPdfKayit(string htmlFile)
        {

            var htmlToPdf = new NReco.PdfGenerator.HtmlToPdfConverter();

            var pdfBytes = htmlToPdf.GeneratePdf(htmlFile);

            return pdfBytes;
        }
    }
}
