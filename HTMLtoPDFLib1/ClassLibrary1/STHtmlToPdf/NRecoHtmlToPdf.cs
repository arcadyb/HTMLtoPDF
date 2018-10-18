
using NReco.PdfGenerator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace STHtmlToPdf.STHtmlToPdf
{
    public class NRecoHtmlToPdf: IBasicHtmlToPdfConverter
    {
        public byte[] HtmlToPdf(byte[] htmlheader, byte[] htmlbody, byte[] htmlfooter)
        {
            string htmlHeader = System.Text.Encoding.UTF8.GetString(htmlheader);
            string htmlBody = System.Text.Encoding.UTF8.GetString(htmlbody);
            string htmlFooter = System.Text.Encoding.UTF8.GetString(htmlfooter);
            return Html2Pdf(htmlHeader, htmlFooter, htmlBody);
        }

        public  byte[] Html2Pdf(string headerHtml, string footerHtml, string body)
        {
            var htmlToPdf = new HtmlToPdfConverter
            {

                // various parameters get set here
                PageHeaderHtml = headerHtml,
                PageFooterHtml = footerHtml
            };
            byte[] bytes =htmlToPdf.GeneratePdf(body);    // form["htmlcontent"] holds the document body
            return bytes;
        }

        public byte[] HtmlToPdf(string htmlheader, string htmlbody, string htmlfooter)
        {
            byte[] ret =  Html2Pdf(htmlheader, htmlfooter, htmlbody);
            return ret;
        }

        public byte[] ImageToPdf(byte[] image)
        {
            throw new NotImplementedException();
        }

        public byte[] CombinePdfs(List<byte[]> pdfs)
        {
            throw new NotImplementedException();
        }

        public byte[] HtmlToPdfS(string htmlheader, string htmlbody, string htmlfooter)
        {
            throw new NotImplementedException();
        }
    }
}
