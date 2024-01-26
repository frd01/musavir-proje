using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Service
{
    public class ServiceIslem
    {
        const string ProxyUrl = "http://146.185.207.3:3081";
        const string ProxyUsername = "PPR02GZHE86";
        const string ProxyPassword = "A8hXewke5GKJE3";
        private WebProxy GetProxy
        {
            get
            {
                var proxy = new WebProxy
                {
                    Address = new Uri(ProxyUrl),
                    Credentials = new NetworkCredential(ProxyUsername, ProxyPassword)
                };

                return proxy;
            }
        }

        private HttpClient client;

        public ServiceIslem()
        {
            var httpClientHandler = new HttpClientHandler
            {
                Proxy = GetProxy,
                UseProxy = true
            };

            client = new HttpClient(httpClientHandler);
        }

        public string PostJsonRequest(string url, string data)
        {

            var content = new StringContent(data, System.Text.Encoding.UTF8, "application/x-www-form-urlencoded");

            var response =  client.PostAsync(url, content);

            var responseString =  response.Result.Content.ReadAsStringAsync();

            return responseString.Result;
        }

        public string PostJsonRequestNoContent(string url, string data)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            //httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Method = "POST";
            httpWebRequest.Proxy = GetProxy;
            httpWebRequest.PreAuthenticate = true;
            var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            streamWriter.Write(data);
            streamWriter.Flush();
            streamWriter.Close();

            var response = httpWebRequest.GetResponse() as HttpWebResponse;


            var reader = new StreamReader(response.GetResponseStream());
            return reader.ReadToEnd();
        }

        public async Task<byte[]> GetFileByte(string url)
        {

            var response = await client.GetAsync(url);

            response.EnsureSuccessStatusCode();

            var stream = await response.Content.ReadAsStreamAsync();

            var memStream = new MemoryStream();

            stream.CopyTo(memStream, 16384);

            //System.Threading.Thread.Sleep(3000);

            return memStream.ToArray();
        }

        public Stream GetResponseStream(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            
            request.Proxy = GetProxy;
            request.PreAuthenticate = true;
            var response = (HttpWebResponse)request.GetResponse();
            var stream = response.GetResponseStream();

            return stream;
        }
    }
}
