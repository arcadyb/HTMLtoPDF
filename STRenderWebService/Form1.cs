using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using STText;
using STHtmlToPdf;

namespace STRenderWebService
{
    public partial class Form1 : Form
    {
        private ILogger STlog;
        public Form1()
        {
            InitializeComponent();
        }

        private void btnHtmlToPdf_Click(object sender, EventArgs e)
        {
            ISTPdfServiceApiProxy ppxy = new STPdfServiceApiProxy();
            //bool isLocalHost = bool.Parse(Program.ReadSetting("IsLocalHost"));
            BindToLocalWebApi(ppxy);
            string textH = System.IO.File.ReadAllText(@"C:\Servi-Tech\htmls\header.html");
            string textF = System.IO.File.ReadAllText(@"C:\Servi-Tech\htmls\footer.html");
            string textB = System.IO.File.ReadAllText(@"C:\Servi-Tech\htmls\body.html");
            byte[] resbytes = ppxy.HtmlToPdf(textH, textB, textF);
            File.WriteAllBytes("c:\\servi-tech\\HtmlToPdf115.pdf", resbytes);
            "c:\\servi-tech\\HtmlToPdf115.pdf".OpenPath();
        }

        private static void BindToLocalWebApi(ISTPdfServiceApiProxy ppxy)
        {
            int port = int.Parse(Program.ReadSetting("port"));
            string baseAddress = string.Format("http://{0}:{1}/", Program.GetIPAddress(), port);
            ppxy.bindTo(baseAddress);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            if (openFileDialog1.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                byte[] res = File.ReadAllBytes(openFileDialog1.FileName);
                string Base64 = Convert.ToBase64String(res);
                File.WriteAllText(openFileDialog1.FileName+".base64", Base64);

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            labelIP.Text = Program.GetIPAddress();
            labelIsLocalHost.Text = Program.ReadSetting("IsLocalHost");
            labelPort.Text = Program.ReadSetting("port");
        }

        private void btnImageToPdf_Click(object sender, EventArgs e)
        {
            ISTPdfServiceApiProxy ppxy = new STPdfServiceApiProxy();
            BindToLocalWebApi(ppxy);
            byte[] img = System.IO.File.ReadAllBytes(@"C:\Servi-Tech\images\avatar.png");
   
            byte[] resbytes = ppxy.ImageToPdf(img);
            File.WriteAllBytes("c:\\servi-tech\\ImgToPdf.pdf", resbytes);
            "c:\\servi-tech\\ImgToPdf.pdf".OpenPath();
        }

        private void btnCombine_Click(object sender, EventArgs e)
        {
            List<byte[]> pdfs = new List<byte[]>();
            byte[] pdf = System.IO.File.ReadAllBytes(@"C:\Servi-Tech\Images\pdfs\imagepdf0.pdf");
            pdfs.Add(pdf);
            pdf = System.IO.File.ReadAllBytes(@"C:\Servi-Tech\Images\pdfs\imagepdf1.pdf");
            pdfs.Add(pdf);
            List<byte[]> pdfs1 = new List<byte[]>();

            if (pdfs.Count() > 0)
            {
                ISTPdfServiceApiProxy ppxy = new STPdfServiceApiProxy();
                BindToLocalWebApi(ppxy);
                byte[] combinedbytes = ppxy.CombinePdfs(pdfs);
                File.WriteAllBytes(string.Format("c:\\servi-tech\\images\\pdfs\\combinedproxy.pdf"), combinedbytes);
            }
        }
    }
}
