using System.Collections.Generic;
using System.Linq;
using Tacmin.Core.Extensions;

namespace Tacmin.Core.Infrastructure
{
    public class MutluSmsService : ISmsService
    {
        private readonly string API_URL = "https://smsgw.mutlucell.com/smsgw-ws";

        public Dictionary<string, string> ResponseMessages = new Dictionary<string, string>
        {
            { "-99", "Bilinmeyen hata" },
            { "-1", "Hatalı telefon numarası" },
            { "0", "İşlem başarılı" },
            { "20", "Post edilen Xml eksik yada hatalı" },
            { "23", "Kullanıcı adı yada parolanız hatalı" },
            { "24", "Şuanda size ait başka bir işlem aktif" },
            { "25", "İşlemi 1-2 dk sonra tekrar deneyin" },
            { "30", "Hesap aktivasyonu sağlanmamış" },
        };

        public string Gonder(string number, string message)
        {
            if (number.IsValidPhoneNumber())
            {
                var url = API_URL + "/sndblkex";
                var xmlString = $@"
                    <?xml version='1.0' encoding='UTF-8'?>
                    <smspack ka='{""}' pwd='{""}' org='{""}'>
                        <mesaj>
                            <metin>{message}</metin>
                            <nums>{number}</nums>
                        </mesaj>
                    </smspack>
                ";

                var data = RequestExtension.PostXML(url, xmlString);
                var retmessage = ResponseMessages.Keys.Where(x => x == data).FirstOrDefault();
                if (retmessage != null)
                    return ResponseMessages[retmessage];

                return ResponseMessages["-99"];
            }
            else
            {
                return ResponseMessages["-1"];
            }
        }

        public string BaslikKontrol(string header)
        {
            var xmlString = $@"
                <?xml version='1.0' encoding='UTF-8' ?>
                <smsorig ka='{""}' pwd='{""}' />
            ";

            var url = API_URL + "/gtorgex";
            var data = RequestExtension.PostXML(url, xmlString);

            var retmessage = ResponseMessages.Keys.Where(x => x == data).FirstOrDefault();
            if (retmessage == null)
            {
                if (retmessage.Contains(header))
                    return ResponseMessages["0"];

                return ResponseMessages["-99"];
            }

            return ResponseMessages[retmessage];
        }

        public string KrediKontrol()
        {
            var xmlString = $@"
                <?xml version='1.0' encoding='UTF-8'?>
                <smskredi ka ='{""}' pwd ='{""}' />
            ";

            var url = API_URL + "/gtcrdtex";
            var data = RequestExtension.PostXML(url, xmlString);

            if (data.StartsWith("$"))
                return data.Substring(1);

            var retmessage = ResponseMessages.Keys.Where(x => x == data).FirstOrDefault();
            if (retmessage == null)
                return ResponseMessages["-99"];
            else
                return ResponseMessages[retmessage];
        }
    }

}
