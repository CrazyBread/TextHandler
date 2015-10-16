using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using System.Collections.Generic;
using System.Text;

namespace FileLib
{
    /// <summary>
    /// Чтение .pdf файла
    /// </summary>
    public class PdfHandler : FileHandler
    {
        public PdfHandler(string path) : base(path) { }

        public override void ReadFile(out List<string> lines)
        {
            lines = new List<string>();
            using (PdfReader reader = new PdfReader(path))
            {
                int pages = reader.NumberOfPages;
                string[] rows = null;
                for (int i = 1; i <= pages; i++)
                {
                    rows = (PdfTextExtractor.GetTextFromPage(reader, i, new LocationTextExtractionStrategy())).Split('\n');
                    foreach (string row in rows)
                    {
                        if(!string.IsNullOrEmpty(row))
                            lines.Add(Encoding.UTF8.GetString(Encoding.UTF8.GetBytes(row)));
                    }   
                }
                reader.Close();
            }
        }
    }
}
