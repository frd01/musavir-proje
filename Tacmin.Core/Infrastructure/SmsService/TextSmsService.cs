using System.IO;
using System.Web.Hosting;

namespace Tacmin.Core.Infrastructure
{
    public class TextSmsService : ISmsService
    {
        public string Gonder(string number, string message)
        {
            File.WriteAllText(HostingEnvironment.MapPath("~/GonderilenSms.txt"), $"{number} - {message}");
            return "İşlem başarılı";
        }

        public string BaslikKontrol(string header)
        {
            return "İşlem başarılı";
        }

        public string KrediKontrol()
        {
            return "23";
        }
    }
}
