using Newtonsoft.Json;
using Newtonsoft.Json.Bson;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace STRenderWebService
{
    public sealed class SomePostRequest
    {
        public int Id { get; set; }
        public byte[] Content { get; set; }
    }
    //var response = await PostBsonAsync<SamplePostRequest>("api/SomeData/Incoming", requestData)
    public class STRenderClient
    {
        public static  string PostBsonAsync<T>(string url, T data)
        {
            using (var client = new HttpClient())
            {
                //Specifiy 'Accept' header As BSON: to ask server to return data as BSON format
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/bson"));

                //Specify 'Content-Type' header: to tell server which format of the data will be posted
                //Post data will be as Bson format                
                var bSonData = SerializeBson<T>(data);
                var byteArrayContent = new ByteArrayContent(bSonData);
                byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/bson");

                var response = client.PostAsync(url, byteArrayContent).Result.Content.ReadAsStringAsync().Result;
                //client.SendAsync(request).Result.Content.ReadAsStringAsync().Result
               // response.EnsureSuccessStatusCode();

                return response;
            }
        }
        public static byte[] SerializeBson<T>(T obj)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (BsonWriter writer = new BsonWriter(ms))
                {
                    JsonSerializer serializer = new JsonSerializer();
                    serializer.Serialize(writer, obj);
                }

                return ms.ToArray();
            }
        }
        public async Task SendRequestAsync()
        {
            using (var stream = new MemoryStream())
            using (var bson = new BsonWriter(stream))
            {
                var jsonSerializer = new JsonSerializer();

                var request = new SomePostRequest
                {
                    Id = 20,
                    Content = new byte[] { 2, 5, 7, 10 }
                };

                jsonSerializer.Serialize(bson, request);

                var client = new HttpClient
                {
                    BaseAddress = new Uri("http://localhost/5002")
                };

                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/bson"));

                var byteArrayContent = new ByteArrayContent(stream.ToArray());
                byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/bson");

                var result = await client.PostAsync(
                        "api/STRender/PostIncomingData", byteArrayContent);

                result.EnsureSuccessStatusCode();
            }
        }
            //        await client.PostAsJsonAsync(apiUrl,
            //new
            //{
            //    message = "",
            //    content = Convert.ToBase64String(yourByteArray),
            //}
            //string base64Str = (string)postBody.data;
            //byte[] fileBytes = Convert.FromBase64String(base64Str);

        
    }
}
