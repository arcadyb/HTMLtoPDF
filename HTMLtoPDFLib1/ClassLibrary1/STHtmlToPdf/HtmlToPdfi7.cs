
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using iTextSharp.text.pdf;


namespace STHtmlToPdf.STHtmlToPdf
{
    public interface IBasicHtmlToPdfConverter
    {
        byte[] HtmlToPdf(byte[] htmlheader, byte[] htmlbody, byte[] htmlfooter);
        byte[] HtmlToPdfS(string htmlheader, string htmlbody, string htmlfooter);
        byte[] ImageToPdf(byte[] image);
        byte[] CombinePdfs(List<byte[]> pdfs);
    }
    public class HtmlToPdfi7: IBasicHtmlToPdfConverter
    {
        public byte[] HtmlToPdf(byte[] htmlheader, byte[] htmlbody, byte[] htmlfooter)
        {
            throw new NotImplementedException();
        }

        public byte[] HtmlToPdf(string htmlheader, string htmlbody, string htmlfooter)
        {
            throw new NotImplementedException();
        }
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
                    throw new NotImplementedException();
                    // writer.CopyAcroForm(reader);
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
     



        public bool IsImage(string p_fileName)
        {
            using (FileStream stream = File.Open(p_fileName, FileMode.Open))
            {
                stream.Seek(0, SeekOrigin.Begin);
                List<string> jpg = new List<string> { "FF", "D8" };
                List<string> bmp = new List<string> { "42", "4D" };
                List<string> gif = new List<string> { "47", "49", "46" };
                List<string> png = new List<string> { "89", "50", "4E", "47", "0D", "0A", "1A", "0A" };
                List<List<string>> imgTypes = new List<List<string>> { jpg, bmp, gif, png };

                List<string> bytesIterated = new List<string>();

                for (int i = 0; i < 8; i++)
                {
                    string bit = stream.ReadByte().ToString("X2");
                    bytesIterated.Add(bit);

                    bool isImage = imgTypes.Any(img => !img.Except(bytesIterated).Any());
                    if (isImage)
                    {
                        return true;
                    }
                }
                return false;
            }
        }

        public bool IsPdf(string p_fileName)
        {
            var pdfString = "%PDF-";
            var pdfBytes = Encoding.ASCII.GetBytes(pdfString);
            var len = pdfBytes.Length;
            var buf = new byte[len];
            var remaining = len;
            var pos = 0;
            using (var f = File.OpenRead(p_fileName))
            {
                while (remaining > 0)
                {
                    var amtRead = f.Read(buf, pos, remaining);
                    if (amtRead == 0) return false;
                    remaining -= amtRead;
                    pos += amtRead;
                }
            }
            return pdfBytes.SequenceEqual(buf);
        }

        public byte[] ImageToPdf(byte[] imageIn)
        {
            Image imageIText = Image.GetInstance(imageIn);
            using (MemoryStream mstream = new MemoryStream())
            {
                using (Document doc = new Document())
                {
                    using (PdfWriter writer = PdfWriter.GetInstance(doc, mstream))
                    {
                        doc.Open();
                        Image image = Image.GetInstance(imageIn);
                        image.ScaleToFit(doc.PageSize);
                        image.SetAbsolutePosition(0, 0);
                        //doc.SetPageSize(new Rectangle(0, 0, image.Width, image.Height, 0));
                        doc.NewPage();
                        writer.DirectContent.AddImage(image);
                        doc.Close();
                    }
                }
                return mstream.ToArray();
            }

        }

        public byte[] CombinePdfs(List<byte[]> pdfs)
        {
            //Document document = new Document();

            //// step 2: we create a writer that listens to the document
            //PdfCopy writer = new PdfCopy(document, new FileStream(uniqueOutputFile, FileMode.Create));

            //// step 3: we open the document
            //document.Open();
            using (MemoryStream mstream = new MemoryStream())
            {
                using (Document doc = new Document())
                {
                    using (PdfCopy writer = new PdfCopy(doc, mstream))
                    {
                        writer.SetMergeFields();
                        doc.Open();
                        foreach (var bytes in pdfs)
                        {
                            PdfReader reader = new PdfReader(bytes);
                            writer.AddDocument(reader);
                            
                        }
                    }
                    doc.Close();
                }
                return mstream.ToArray();
            }
           
        }

        public byte[] HtmlToPdfS(string htmlheader, string htmlbody, string htmlfooter)
        {
            throw new NotImplementedException();
        }
    }
}




