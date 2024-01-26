using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Sms
{
    public class SmsBilgiModel
    {
        public int? UserId { get; set; }
        public string SmsKullaniciAdi { get; set; }
        public string SmsSifre { get; set; }
        public int? SmsFirmaId { get; set; }
        public string Baslik { get; set; }
    }
}
