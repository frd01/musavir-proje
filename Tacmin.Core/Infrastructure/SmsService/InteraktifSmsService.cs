using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Tacmin.Core.Extensions;

namespace Tacmin.Core.Infrastructure
{
    public class InteraktifSmsService : ISmsService
    {
        private readonly string API_URL = "http://panel.1sms.com.tr:8080/api";

        public Dictionary<string, string> BaslikKrediResponseMessages = new Dictionary<string, string>
        {
            { "-99", "Bilinmeyen hata" },
            { "00", "İşlem başarıyla tamamlandı" },
            { "87", "GET parametrelerinde eksik var" },
            { "93", "Kullanıcı adı veya şifre hatalı" },
            { "95", "İsteği HTTP GET ile yollayınız" },
        };

        public Dictionary<string, string> SendResponseMessage = new Dictionary<string, string>
        {
            { "-1", "Telefon numarası doğrulanamadı" },
            { "-99", "Bilinmeyen hata" },
            { "00", "SMS gönerildi" },
            { "77", "Aynı mesajı göndermeyiniz" },
            { "83", "Metin mesajı boş" },
            { "87", "Kullanıcı adı veye şifre hatalı" },
            { "89", "POST verisi XML olarak parse edilemedi" },
            { "91", "POST verisi okunamadı veya yok" },
        };

        public string Gonder(string number, string message)
        {
            if (number.IsValidPhoneNumber())
            {
                var requestXml = $@"
                    <sms>
                        <username>KullaniciAdi</username>
                        <password>Sifre</password>
                        <header>Baslik</header>
                        <validity>2880</validity>
                        <message>
                            <gsm>
                                <no>{number}</no>
                            </gsm>
                            <msg>{message}</msg>
                        </message>
                    </sms>
                ";

                var url = API_URL + "/smspost/v1";
                var response = RequestExtension.PostXML(url, requestXml);
                var responseArr = response.Split(" ");
                if (responseArr.Count() >= 1)
                {
                    var retmessage = SendResponseMessage.Keys.Where(x => x == responseArr.First()).FirstOrDefault();
                    if (retmessage != null)
                        return SendResponseMessage[retmessage];
                }

                return SendResponseMessage["-99"];
            }
            else
            {
                return SendResponseMessage["-1"];
            }
        }

        public string BaslikKontrol(string header)
        {
            var url = API_URL + $"/originator/v1?username={""}&password={""}";
            var request = WebRequest.Create(url);
            var response = request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            var data = reader.ReadToEnd();
            var dataArr = data.Split(" ");

            if (dataArr.Count() >= 1)
            {
                if (dataArr.Last().Contains(header))
                    return BaslikKrediResponseMessages["00"];

                var retmessage = BaslikKrediResponseMessages.Keys.Where(x => x == dataArr.First()).FirstOrDefault();
                if (retmessage != null)
                    return BaslikKrediResponseMessages[retmessage];
            }

            return BaslikKrediResponseMessages["-99"];
        }

        public string KrediKontrol()
        {
            var url = API_URL + $"/credit/v1?username={""}&password={""}";
            var request = WebRequest.Create(url);
            var response = request.GetResponse();
            var reader = new StreamReader(response.GetResponseStream());
            var data = reader.ReadToEnd();
            var dataArr = data.Split(" ");

            if (dataArr.Count() >= 1)
            {
                var retmessage = BaslikKrediResponseMessages.Keys.Where(x => x == dataArr.First()).FirstOrDefault();
                if (retmessage != null)
                    return dataArr.Last();
            }

            return BaslikKrediResponseMessages["-99"];
        }
    }
}