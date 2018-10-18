using Newtonsoft.Json.Linq;
using STHtmlToPdf.STHtmlToPdf;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace STRenderWebService
{
    public interface ISTPdfServiceApiProxy
    {
        void bindTo(string address);
        byte[] HtmlToPdf(byte[] htmlheader, byte[] htmlbody, byte[] htmlfooter);
        byte[] HtmlToPdf(string htmlheader, string htmlbody, string htmlfooter);
        byte[] ImageToPdf(byte[] image);
        byte[] CombinePdfs(List<byte[]> pdfs);
        string GetLastError();
    }
    public class STPdfServiceApiProxy : ISTPdfServiceApiProxy
    {
        private string IpHost = "http://192.168.200.151:5002/";
        private string LastError = "";

        

        public void bindTo(string adress)
        {
            //http://localhost:5002/
            IpHost = adress;
        }

        public byte[] CombinePdfs(List<byte[]> pdfs)
        {
            JArray jarrayObj = new JArray();

            foreach (byte[] pdf in pdfs)
            {
                if (STValidateBytes.IsPdf(pdf))
                {
                    jarrayObj.Add(Convert.ToBase64String(pdf));
                }
                else
                {
                    LastError = "CombinePdfs: bad input file format";
                    return null;
                }
            }
            var myObject = (dynamic)new JObject();
            myObject.Method = "CombinePdfs";
            myObject.pdfs = jarrayObj;
            byte[] res = PostJObject(myObject);
            return res;
        }

        public byte[] HtmlToPdf(byte[] htmlheader, byte[] htmlbody, byte[] htmlfooter)
        {
            string htmlHeader = System.Text.Encoding.UTF8.GetString(htmlheader);
            string htmlBody = System.Text.Encoding.UTF8.GetString(htmlbody);
            string htmlFooter = System.Text.Encoding.UTF8.GetString(htmlfooter);
            return HtmlToPdf(htmlHeader, htmlFooter, htmlBody);
        }

        public byte[] HtmlToPdf(string htmlheader, string htmlbody, string htmlfooter)
        {
            var myObject = (dynamic)new JObject();
            myObject.htmlHeader = Convert.ToBase64String(Encoding.UTF8.GetBytes(htmlheader));
            myObject.htmlBody = Convert.ToBase64String(Encoding.UTF8.GetBytes(htmlbody));
            myObject.htmlFooter = Convert.ToBase64String(Encoding.UTF8.GetBytes(htmlfooter));
            myObject.Method = "HtmlToPdf";
            return PostJObject(myObject);
        }

        private byte[] PostJObject(dynamic myObject)
        {
            var baseAddress = string.Format("{0}api/STRender", IpHost);
            var httpClient = new HttpClient();
            var content = new StringContent(myObject.ToString(), Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            string sresult = httpClient.PostAsync(baseAddress, content).Result.Content.ReadAsStringAsync().Result;
            JObject objRes = JObject.Parse(sresult);
            return ProccessResponce(objRes);
        }
        //if returns null check GetLastError()
        public byte[] ImageToPdf(byte[] image)
        {
            if(!STValidateBytes.IsImage(image))
            {
                LastError="ImageToPdf:bad image input";
            }
            var myObject = (dynamic)new JObject();
            myObject.image = Convert.ToBase64String(image);
            myObject.Method = "ImageToPdf";
            byte[] res = PostJObject(myObject);
            return res;
        }

        private byte[] ProccessResponce(JObject objRes)
        {
            if (objRes["Data"]["metaData"]?.ToString() == "error")
            {
                LastError = objRes["Data"]["Message"]?.ToString();
                return null;
            }
            byte[] res = Convert.FromBase64String(objRes["Data"]["DataBytes"].ToString());
            return res;
        }

        public string GetLastError()
        {
            return LastError;
        }
    }
}
