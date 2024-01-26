using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Mail
{
    public class MailKullaniciModel
    {
        public int Id { get; set; }
        public string Unvan { get; set; }
        public string MailKullaniciAdi { get; set; }
        public string MailSifre { get; set; }
        public int MailTur { get; set; }
    }
}
