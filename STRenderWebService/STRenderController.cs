
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
            result.Content = new StreamContent(workStream);
            result.Content.Headers.ContentType =
                new MediaTypeHeaderValue("application/pdf");
            return result;
        }


 
        [System.Web.Http.HttpPostAttribute]
        [HttpOptions]
        public HttpResponseMessage Post(JObject json)
        {
            try
            {
                string method = json["Method"].ToString();
                if (method == "HtmlToPdf")
                {
                    IBasicHtmlToPdfConverter stht = new NRecoHtmlToPdf();
                    byte[] htmlheader = Convert.FromBase64String(json["htmlHeader"].ToString());
                    byte[] htmlbody = Convert.FromBase64String(json["htmlBody"].ToString());
                    byte[] htmlfooter = Convert.FromBase64String(json["htmlFooter"].ToString());
                    byte[] bytesPdf = stht.HtmlToPdf(htmlheader, htmlbody, htmlfooter);
                    return CreateResponce(json["Method"].ToString(), bytesPdf);
                }
                else if (method == "ImageToPdf")
                {
                    byte[] imageBytes = Convert.FromBase64String(json["image"].ToString());
                    if (!STValidateBytes.IsImage(imageBytes))
                    {
                        return CreateBadResponce(String.Format("{0}:Input is not valid image", method));
                    }
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
                    int i = 0;
                    foreach (string pdfbase64 in pdfsBase64)
                    {

                        byte[] inbytes = Convert.FromBase64String(pdfbase64);
                        if(!STValidateBytes.IsPdf(inbytes))
                        {
                           return CreateBadResponce(String.Format("{0}:Input {1} is not Pdf:", method,i));
                        }
                        pdfsBytes.Add(Convert.FromBase64String(pdfbase64));
                        i++;
                    }
                    byte[] combinedbytes = convertorPdf.CombinePdfs(pdfsBytes);
                    return CreateResponce(json["Method"].ToString(), combinedbytes);
                }
                else
                    return CreateBadResponce("Uncknown Method:"+ method);
            }
            catch (Exception ex)
            {
                return CreateBadResponce(ex.ToString());
            }
            
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
        public HttpResponseMessage CreateBadResponce(string resptext)
        {

            var ResponceObj = new JObject
            {
                new JProperty("Data",
                                new JObject
                                {
                                new JProperty("metaData", "error"),
                                new JProperty("DataBytes", ""),
                                }
                          ),
                new JProperty("Message", resptext)
            };
            var response = Request.CreateResponse(HttpStatusCode.OK);
            response.Content = new StringContent(ResponceObj.ToString(), Encoding.UTF8, "application/json");
            return response;
        }
    }
}
