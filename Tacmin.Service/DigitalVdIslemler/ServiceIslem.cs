using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Service.DigitalVdIslemler
{
    //class ServiceIslem
    public class ServiceIslem
    {
        public T PostMethod<T>(object obj, string uri, Dictionary<string, string> headers = null)
        {
            var client = new RestClient(uri);

            var request = new RestRequest(uri, Method.Post) { RequestFormat = DataFormat.Json };


            var result = GetResult<T>(client, request, obj, headers);

            return result;
        }

        public T GetMethod<T>(string uri, Dictionary<string, string> headers = null)
        {
            var client = new RestClient(uri);
            var request = new RestRequest(uri, Method.Get) { RequestFormat = DataFormat.Json };

            var result = GetResult<T>(client, request, null, headers);

            return result;
        }



        private T GetResult<T>(RestClient client, RestRequest request, object obj, Dictionary<string, string> headers = null)
        {
            if (headers != null) //header varsa requeste headerları ekle
            {
                foreach (var header in headers)
                {
                    request.AddHeader(header.Key, header.Value);
                }
            }

            if (obj != null) //post,put,delete gibi işlemler için servise gönderilecek nesne varsa requeste ekle
            {
                request.AddJsonBody(JsonConvert.SerializeObject(obj));
            }
            //client üzerinden requesti servise yolla ve
            var response = client.Execute(request);

            return JsonConvert.DeserializeObject<T>(response.Content, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });

        }
    }

}
