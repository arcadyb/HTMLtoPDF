using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using HTMLtoPDF.STHtmlToPdfNS;
using STHtmlToPdf;
using STHtmlToPdf.STHtmlToPdf;
using STText;

namespace HTMLtoPDF
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnBasicHtmlToPdf_Click(object sender, EventArgs e)
        {
            //IBasicHtmlToPdfConverter stht = new STHtmlToPdfNS.STHtmlToPdf();
            //string textH = System.IO.File.ReadAllText(@"C:\Servi-Tech\htmls\header.html");
            //string textF = System.IO.File.ReadAllText(@"C:\Servi-Tech\htmls\footer.html");
            
            ////byte[] bytesH = Encoding.ASCII.GetBytes(textH);
            ////byte[] bytesB = Encoding.ASCII.GetBytes("<html>hello body here</html>");
            ////byte[] bytesF = Encoding.ASCII.GetBytes(textF);
     
            //byte[] bytesPdf = stht.HtmlToPdfS(textH, "<html>hello body here</html>", textF);
            ////HtmlConverter.ConvertToPdf(new FileInfo(@"C:\Servi-Tech\htmls\header1.html"), new FileInfo(@"C:\Servi-Tech\htmls\itext7.pdf"));
            //File.WriteAllBytes("c:\\servi-tech\\Pechkin.pdf", bytesPdf);
            //"c:\\servi-tech\\Pechkin.pdf".OpenPath();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            IBasicHtmlToPdfConverter stht = new NRecoHtmlToPdf();
            string textH = System.IO.File.ReadAllText(@"C:\Servi-Tech\htmls\header.html");
            string textF = System.IO.File.ReadAllText(@"C:\Servi-Tech\htmls\footer.html");
            
            byte[] bytesH = Encoding.ASCII.GetBytes(textH);
            byte[] bytesB = Encoding.ASCII.GetBytes("<html>hello body here</html>");
            byte[] bytesF = Encoding.ASCII.GetBytes(textF);
            byte[] bytesPdf = stht.HtmlToPdf(bytesH, bytesB, bytesF);
            File.WriteAllBytes("c:\\servi-tech\\Nreco.pdf", bytesPdf);
            "c:\\servi-tech\\Nreco.pdf".OpenPath();
            byte[] stringsPdf = stht.HtmlToPdfS(textH, "body text", textF);
            File.WriteAllBytes("c:\\servi-tech\\NrecoStrings.pdf", stringsPdf);
            "c:\\servi-tech\\NrecoStrings.pdf".OpenPath();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            IBasicHtmlToPdfConverter convertorPdf = new HtmlToPdfi7();
            string directorypath = @"c:\Servi-Tech\images";
            List<byte[]> pdfs = new List<byte[]>();
            List<byte[]> images = ImageUtils.FromDirectory(directorypath, false);
            int i = 0;
            foreach(var img in images)
            {
                byte[] pdf = convertorPdf.ImageToPdf(img);
                pdfs.Add(pdf);
                File.WriteAllBytes(string.Format("c:\\servi-tech\\images\\pdfs\\imagepdf{0}.pdf", i++) , pdf);
            }
            if (pdfs.Count() > 0)
            {
                byte[] combinedbytes = convertorPdf.CombinePdfs(pdfs);
                File.WriteAllBytes(string.Format("c:\\servi-tech\\images\\pdfs\\combined.pdf", i++), combinedbytes);
            }
            Process.Start("c:\\servi-tech\\images\\pdfs");

        }

        private void button3_Click(object sender, EventArgs e)
        {
            Process.Start("c:\\servi-tech\\images\\");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            Process.Start("c:\\servi-tech\\images\\pdfs");
        }
    }
}
