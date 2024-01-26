using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Whatsapp
{
    public class GonderimBilgi
    {
        public int Id { get; set; }
        [DisplayName("Gönderim Tarihi")]
        public DateTime? GonderimZamani { get; set; }
        [DisplayName("Ünvan")]
        public string Unvan { get; set; }
        public int? EvrakId { get; set; }
        [DisplayName("Belge Türü")]
        public string BelgeTuru { get; set; }
        [DisplayName("Belge Adı")]
        public string BelgeAdi { get; set; }
        [DisplayName("Modül")]
        public string Modul { get; set; }
        [DisplayName("Dönem")]
        public string Donem { get; set; }
        public decimal? Tutar { get; set; }
        [DisplayName("Gönderilen İçerik")]
        public string GonderimIcerik { get; set; }

    }
}
