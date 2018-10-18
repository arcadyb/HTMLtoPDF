
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




