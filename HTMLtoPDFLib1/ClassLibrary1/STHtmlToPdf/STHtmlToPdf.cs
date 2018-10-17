using System;
using System.Collections.Generic;
using System.Drawing.Printing;
//using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using TuesPechkin;


namespace HTMLtoPDF.STHtmlToPdfNS
{

    public class STHtmlToPdf : IBasicHtmlToPdfConverter
    {
        public byte[] CombinePdfs(List<byte[]> pdfs)
        {
            throw new NotImplementedException();
        }

        //public byte[] IT7HtmlToPdf(string pathHtml,string pathPdf)
        //{
        // //   LicenseKey.LoadLicenseFile(
        // //System.getenv("ITEXT7_LICENSEKEY") + "/itextkey-html2pdf_typography.xml");
        // //   File file = new File(TARGET);
        // //   file.mkdirs();
        // //   new C01E01_HelloWorld().createPdf(HTML, DEST);
        //    HtmlConverter.ConvertToPdf(new FileInfo(@"C:\Servi-Tech\htmls\header1.html"), new FileInfo(@"C:\Servi-Tech\htmls\itext7.pdf"));
        //}
        public byte[] convertHTMLTOPDF(string html)
        {
            TuesPechkin.HtmlToPdfDocument document = new TuesPechkin.HtmlToPdfDocument
            {
                GlobalSettings =
                    {
                        ProduceOutline = false,
                        DocumentTitle = "Pretty Websites",
                        PaperSize = System.Drawing.Printing.PaperKind.Letter, // Implicit conversion to PechkinPaperSize
                        Margins =
                        {
                        All = 1.375,
                        Unit = Unit.Centimeters
                        }
                    },
                Objects = {
                        new ObjectSettings { HtmlText = html},
                    }
            };
            TuesPechkin.IPechkin sc2 = TuesPechkin.Factory.Create();
            var buf = sc2.Convert(document);
            byte[] result = sc2.Convert(document);
            return result;
        }

        public byte[] HtmlToPdf(byte[] html)
        {
            string strhtml = System.Text.Encoding.UTF8.GetString(html);
            return convertHTMLTOPDF(strhtml);
        }

        public byte[] HtmlToPdf(byte[] htmlheader, byte[] htmlbody, byte[] htmlfooter)
        {
            string htmlHeader = System.Text.Encoding.UTF8.GetString(htmlheader);
            string htmlBody = System.Text.Encoding.UTF8.GetString(htmlbody);
            string htmlFooter = System.Text.Encoding.UTF8.GetString(htmlfooter);

            var document = new HtmlToPdfDocument
            {
                GlobalSettings =
            {
            ProduceOutline = true,
            DocumentTitle = "My Website",
            PaperSize = PaperKind.A4,
            Margins =
            {
            All = 1.375,
            Unit = Unit.Centimeters
            }
            },
                Objects = {
            new ObjectSettings
            {
            HtmlText = htmlBody,
            HeaderSettings = new HeaderSettings{CenterText = htmlHeader},
            FooterSettings = new FooterSettings{CenterText = htmlFooter, LeftText = "[page]"}
            }
            }
            };

            IPechkin converter = Factory.Create();
            byte[] result = converter.Convert(document);
            System.IO.File.WriteAllBytes(@"c:\servi-tech\tuespechkinTest.pdf", result);
            return result;
        }

        public byte[] HtmlToPdfS(string htmlHeader, string htmlBody, string htmlFooter)
        {
            //string /*htmlHeader*/ = System.Text.Encoding.UTF8.GetString(htmlheader);
            //string htmlBody = System.Text.Encoding.UTF8.GetString(htmlbody);
            //string htmlFooter = System.Text.Encoding.UTF8.GetString(htmlfooter);

            var document = new HtmlToPdfDocument
            {
                GlobalSettings =
                    {
                        ProduceOutline = true,
                        DocumentTitle = "My Website",
                        PaperSize = PaperKind.A4,
                        Margins =
                            {
                                All = 1.375,
                                Unit = Unit.Centimeters
                            }
                        },
                        Objects = {
                        new ObjectSettings
                        {
                            HtmlText = htmlBody,
                            HeaderSettings = new HeaderSettings{CenterText = htmlHeader},
                            FooterSettings = new FooterSettings{CenterText = htmlFooter, LeftText = "[page]"}
                        }
                    }
            };

            IPechkin converter = Factory.Create();
            byte[] result = converter.Convert(document);
            System.IO.File.WriteAllBytes(@"c:\servi-tech\tuespechkinTest.pdf", result);
            return result;

        }

        public byte[] ImageToPdf(byte[] image)
        {
            throw new NotImplementedException();
        }
    }
}
