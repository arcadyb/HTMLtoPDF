
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

            //var docTitle = form["doctitle"].ToString();

            //var headerHtml =
            //    "<div style='width:100%; margin-top:1em; display:block;'>" +
            //        "<img src='" + System.Web.HttpContext.Current.Server.MapPath("~") + "/media/images/document_banner.png' />" +
            //    "</div>" +
            //    "<div style='width:100%; bottom:110px; left:0; position:relative; display:block;'>" +
            //        "<span style='color:#fff; font-size:2.5em; font-family:georgia; margin-left:1em;'>" + docTitle + "</span>" +
            //    "</div>";

            //var footerHtml =
            //    "<div style='width:100%; text-align:center; border-top:1px solid #abc; margin-top:2em;'>Page 0 of 0</div>;

    var htmlToPdf = new HtmlToPdfConverter();

            // various parameters get set here
            htmlToPdf.PageHeaderHtml = headerHtml;
            htmlToPdf.PageFooterHtml = footerHtml;
    

   // Response.ContentType = "application/pdf";
   //         Response.AddHeader("content-disposition", "attachment;filename=MyTestDocument.pdf");
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
        //    htmlToPdf.PageHeaderHtml = "<div>Page: <span class="page"></span></div>";
        //var generator = new NReco.PdfGenerator.HtmlToPdfConverter();
        //    generator.PageFooterHtml = $@"page <span class=""page""></span> of <span class=""topage""></span>";
    }
}
