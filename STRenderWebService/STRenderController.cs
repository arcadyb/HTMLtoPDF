
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using STHtmlToPdf.STHtmlToPdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Windows.Forms;

namespace STRenderWebService
{

    public class STRenderController : ApiController
    {
        [Route("Blabla")]
        public HttpResponseMessage Get()
        {
            MemoryStream workStream = new MemoryStream();
            IBasicHtmlToPdfConverter stht = new NRecoHtmlToPdf();
            byte[] bytesH = Encoding.UTF8.GetBytes("<html>hello header here</html>");
            byte[] bytesB = Encoding.UTF8.GetBytes("<html>hello body here</html>");
            byte[] bytesF = Encoding.UTF8.GetBytes("<html>hello Footer here</html>");
            byte[] bytesPdf = stht.HtmlToPdf(bytesH, bytesB, bytesF);
            

            workStream.Write(bytesPdf, 0, bytesPdf.Length);
            workStream.Position = 0;
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            //var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            result.Content = new StreamContent(workStream);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/pdf");
            return result;
        }


    /*
        public HttpResponseMessage PostOLD(JObject json)
        {

            byte[] htmlheader = Convert.FromBase64String(json["htmlHeader"].ToString());
            byte[] htmlbody = Convert.FromBase64String(json["htmlBody"].ToString());
            byte[] htmlfooter = Convert.FromBase64String(json["htmlFooter"].ToString());
            MemoryStream workStream = new MemoryStream();
            IBasicHtmlToPdfConverter stht = new HTMLtoPDF.STHtmlToPdfNS.STHtmlToPdf();
            //byte[] bytes = Encoding.ASCII.GetBytes("<html>hello here</html>");
            byte[] bytesPdf = stht.HtmlToPdf(htmlheader, htmlbody, htmlfooter);

            workStream.Write(bytesPdf, 0, bytesPdf.Length);
            workStream.Position = 0;
            HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
            //var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
            result.Content = new StreamContent(workStream);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/pdf");
            return result;
        }
        */
        [System.Web.Http.HttpPostAttribute]
        [HttpOptions]
        public HttpResponseMessage Post(JObject json)
        {
            // JObject json = JObject.Parse(jsons);

            //MemoryStream workStream = new MemoryStream();
            try
            {
                IBasicHtmlToPdfConverter stht = new NRecoHtmlToPdf();
                //byte[] bytes = Encoding.UTF8.GetBytes("<html>hello here</html>");
                string method = json["Method"].ToString();
                if (method == "HtmlToPdf")
                {
                    byte[] htmlheader = Convert.FromBase64String(json["htmlHeader"].ToString());
                    byte[] htmlbody = Convert.FromBase64String(json["htmlBody"].ToString());
                    byte[] htmlfooter = Convert.FromBase64String(json["htmlFooter"].ToString());
                    byte[] bytesPdf = stht.HtmlToPdf(htmlheader, htmlbody, htmlfooter);
                    return CreateResponce(json["Method"].ToString(), bytesPdf);
                }
                else if (method == "ImageToPdf")
                {
                    byte[] imageBytes = Convert.FromBase64String(json["image"].ToString());
                    IBasicHtmlToPdfConverter convertorPdf = new HtmlToPdfi7();
                    byte[] pdf = convertorPdf.ImageToPdf(imageBytes);
                    return CreateResponce(json["Method"].ToString(), pdf);
                }
                else if (method == "CombinePdfs")
                {
                    List<byte[]> pdfsBytes = new List<byte[]>();
                    IBasicHtmlToPdfConverter convertorPdf = new HtmlToPdfi7();
                    JArray pdfs = (JArray)json["pdfs"];
                    List<string> pdfsBase64 = JsonConvert.DeserializeObject<List<string>>(pdfs.ToString());
                    foreach (string pdfbase64 in pdfsBase64)
                    {

                        pdfsBytes.Add(Convert.FromBase64String(pdfbase64));                                  //here more code in order to save in a database

                    }
                    byte[] combinedbytes = convertorPdf.CombinePdfs(pdfsBytes);

                    return CreateResponce(json["Method"].ToString(), combinedbytes);
                }
                else return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
        }
        public HttpResponseMessage CreateResponce(string resptext, byte[] respbytes,string Metadata="pdf")
        {
            string b64Data = Convert.ToBase64String(respbytes);
            var ResponceObj = new JObject
            {
                new JProperty("Data",
                                new JObject
                                {
                                new JProperty("metaData", Metadata),
                                new JProperty("DataBytes", b64Data),
                                }
                          ),
                new JProperty("Message", resptext)
            };

            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(ResponceObj.ToString(), Encoding.UTF8, "application/json");
            return response;
        }
        //    public FileStreamResult pdf()
        //    {

        //        MemoryStream workStream = new MemoryStream();
        //        IBasicHtmlToPdfConverter stht = new STHtmlToPdf();
        //        byte[] bytes = Encoding.ASCII.GetBytes("<html>hello here</html>");
        //        byte[] bytesPdf = stht.HtmlToPdf(bytes);

        //        workStream.Write(bytesPdf, 0, bytesPdf.Length);
        //        workStream.Position = 0;
        //        HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
        //        //var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        //        result.Content = new StreamContent(workStream);
        //        result.Content.Headers.ContentType =
        //            new MediaTypeHeaderValue("application/octet-stream");
        //        return result;
        //        //return new FileStreamResult(workStream, "application/pdf");
        //    }
        //    public HttpResponseMessage Post(string version, string environment,
        //string filetype)
        //    {
        //        var path = @"C:\Temp\test.exe";
        //        HttpResponseMessage result = new HttpResponseMessage(HttpStatusCode.OK);
        //        var stream = new FileStream(path, FileMode.Open, FileAccess.Read);
        //        result.Content = new StreamContent(stream);
        //        result.Content.Headers.ContentType =
        //            new MediaTypeHeaderValue("application/octet-stream");
        //        return result;
        //    }
    }
}
