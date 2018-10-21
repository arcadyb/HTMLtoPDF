using System;
using System.Net.Http;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json.Linq;
using STHtmlToPdf.STHtmlToPdf;
using STRenderWebService;

namespace UnitTestProject1
{
    [TestClass]
    public class TestRenderControllerClass
    {
        [TestMethod]
        public void TestRenderController()
        {
            var controller = new STRenderController();
            var myObject = (dynamic)new JObject();
            myObject["htmlHeader"] = Convert.ToBase64String(Encoding.UTF8.GetBytes("<html>test Head1</html>"));
            myObject.htmlBody = Convert.ToBase64String(Encoding.UTF8.GetBytes("<html>test Body1</html>"));
            myObject.htmlFooter = Convert.ToBase64String(Encoding.UTF8.GetBytes("<html>test Footer1</html>"));
            myObject.Method = "HtmlToPdf";
            HttpResponseMessage resp = controller.Post(myObject);
            
            JObject objRes = JObject.Parse(resp.Content.ReadAsStringAsync().Result);
            byte[] pPdf =  new STPdfServiceApiProxy().ProccessResponce(objRes);
            Assert.AreEqual(true, STValidateBytes.IsPdf(pPdf));
        }
        [TestMethod]
        public void TestRenderEmptyBodyHtml()
        {
            var controller = new STRenderController();
            var myObject = (dynamic)new JObject();
            myObject["htmlHeader"] = Convert.ToBase64String(Encoding.UTF8.GetBytes("<html>test Head1</html>"));
            myObject.htmlBody = Convert.ToBase64String(Encoding.UTF8.GetBytes(""));
            myObject.htmlFooter = Convert.ToBase64String(Encoding.UTF8.GetBytes("<html>test Footer1</html>"));
            myObject.Method = "HtmlToPdf";
            HttpResponseMessage resp = controller.Post(myObject);

            JObject objRes = JObject.Parse(resp.Content.ReadAsStringAsync().Result);
            byte[] pPdf = new STPdfServiceApiProxy().ProccessResponce(objRes);
            Assert.AreEqual("Html Body is empty", objRes["Message"].ToString());
            Assert.AreEqual("error", objRes["Data"]["metaData"].ToString());
        }
        [TestMethod]
        public void TestRenderEmptyHeaderOrFooterHtml()
        {
            var controller = new STRenderController();
            var myObject = (dynamic)new JObject();
            myObject["htmlHeader"] = Convert.ToBase64String(Encoding.UTF8.GetBytes(""));
            myObject.htmlBody = Convert.ToBase64String(Encoding.UTF8.GetBytes("<html> Hello from body</html>"));
            myObject.htmlFooter = Convert.ToBase64String(Encoding.UTF8.GetBytes(""));
            myObject.Method = "HtmlToPdf";
            HttpResponseMessage resp = controller.Post(myObject);

            JObject objRes = JObject.Parse(resp.Content.ReadAsStringAsync().Result);
            byte[] pPdf = new STPdfServiceApiProxy().ProccessResponce(objRes);
            Assert.AreEqual(true, STValidateBytes.IsPdf(pPdf));
        }
    }
}
