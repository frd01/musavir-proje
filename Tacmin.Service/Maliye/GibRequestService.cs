using System.IO;
using System.Net;

namespace Tacmin.Service
{
    public static class GibRequestService
    {
        public static string PostJsonRequest(string url, string data)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Method = "POST";
           

            var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            streamWriter.Write(data);
            streamWriter.Flush();
            streamWriter.Close();

            var response = httpWebRequest.GetResponse() as HttpWebResponse;


            var reader = new StreamReader(response.GetResponseStream());
            return reader.ReadToEnd();
        }

        public static string PostJsonRequestNoContent(string url, string data)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
            //httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.Method = "POST";

            var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream());
            streamWriter.Write(data);
            streamWriter.Flush();
            streamWriter.Close();

            var response = httpWebRequest.GetResponse() as HttpWebResponse;


            var reader = new StreamReader(response.GetResponseStream());
            return reader.ReadToEnd();
        }

        public static Stream GetResponseStream(string url)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            var response = (HttpWebResponse)request.GetResponse();
            var stream = response.GetResponseStream();

            return stream;
        }


    }


}
