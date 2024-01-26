using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Collections.Generic;
using System.IO;

namespace Tacmin.Core.Utilities
{
    public class PdfUtils
    {
        public static byte[] MergePdfs(List<byte[]> files)
        {
            using (var stream = new MemoryStream())
            {
                using (var doc = new Document())
                {
                    var pdf = new PdfCopy(doc, stream)
                    {
                        CloseStream = false
                    };
                    doc.Open();

                    foreach (var file in files)
                    {
                        var reader = new PdfReader(file);

                        for (var i = 0; i < reader.NumberOfPages; i++)
                        {
                            var page = pdf.GetImportedPage(reader, i + 1);
                            pdf.AddPage(page);
                        }

                        pdf.FreeReader(reader);
                        reader.Close();
                    }
                }

                stream.Position = 0;
                return stream.ToArray();
            }
        }
    }
}
