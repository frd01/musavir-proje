using System.IO;
using System.Net;
using System.Text;

namespace Tacmin.Core.Extensions
{
    public static class RequestExtension
    {
        public static string PostXML(string url, string xmlData, string contentType = "text/xml; encoding='utf-8'") => PostData(url, xmlData, contentType);

        public static string PostData(string url, string data, string contentType)
        {
            var request = WebRequest.Create(url);
            byte[] bytes;
            bytes = Encoding.ASCII.GetBytes(data);
            request.ContentType = contentType;
            request.ContentLength = bytes.Length;
            request.Method = "POST";
            var requestStream = request.GetRequestStream();
            requestStream.Write(bytes, 0, bytes.Length);
            requestStream.Close();
            var response = request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            var veri = reader.ReadToEnd();
            return veri;
        }
    }
}
