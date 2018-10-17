using System;
using System.IO;
using System.Linq;
using iTextSharp.text;
using iTextSharp.text.pdf;

namespace PdfUtils
{
    public class PdfUtils
    {
        public static void CombineMultiplePdfs(string[] p_fileNames, string p_outFile)
        {
            string uniqueOutputFile = Path.GetTempFileName();
            Console.WriteLine($"Generated {uniqueOutputFile}");

            // step 1: creation of a document-object
            Document document = new Document();

            // step 2: we create a writer that listens to the document
            PdfCopy writer = new PdfCopy(document, new FileStream(uniqueOutputFile, FileMode.Create));

            // step 3: we open the document
            document.Open();

            foreach (string fileName in p_fileNames)
            {
                // we create a reader for a certain document
                PdfReader reader = new PdfReader(fileName);
                reader.ConsolidateNamedDestinations();

                // step 4: we add content
                for (int i = 1; i <= reader.NumberOfPages; i++)
                {
                    PdfImportedPage page = writer.GetImportedPage(reader, i);
                    writer.AddPage(page);
                }

                PRAcroForm form = reader.AcroForm;
                if (form != null)
                {
                    //writer.CopyAcroForm(reader);
                }

                reader.Close();
            }

            // step 5: we close the document and writer
            writer.Close();
            document.Close();

            File.Copy(uniqueOutputFile, p_outFile, true);
            File.Delete(uniqueOutputFile);
            Console.WriteLine($"Removed {uniqueOutputFile}");
        }

        public static void ImageToPdf(string p_imgFile, string p_outputPdfFile)
        {
            using (FileStream fs = new FileStream(p_outputPdfFile, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                using (Document doc = new Document())
                {
                    using (PdfWriter writer = PdfWriter.GetInstance(doc, fs))
                    {
                        doc.Open();
                        Image image = Image.GetInstance(p_imgFile);
                        image.ScaleToFit(doc.PageSize);
                        image.SetAbsolutePosition(0, 0);
                        //doc.SetPageSize(new Rectangle(0, 0, image.Width, image.Height, 0));
                        doc.NewPage();
                        writer.DirectContent.AddImage(image);
                        doc.Close();
                    }
                }
            }
        }
    }
}