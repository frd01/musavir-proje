using System.Collections.Generic;

namespace Tacmin.Core.Model
{
    public class BaseUserModel
    {
        public int UserId { get; set; }

        public string Adi { get; set; }

        public string Soyadi { get; set; }

        public string KullaniciKodu { get; set; }

        public string KullaniciAdi { get; set; }

        public string AdSoyad => Adi + " " + Soyadi;

        public string Token { get; set; }

        public IEnumerable<string> Yetkiler { get; set; }

        public bool Yetkili { get; set; }

        public IEnumerable<int> AltKullaniciIds { get; set; }
    }
}
