using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataIslemler.Models
{
    public class UserModel
    {
        public int Id { get; set; }
        public bool? Yetkili { get; set; }
        public string Adi { get; set; }
        public string SoyAdi { get; set; }
        public string Yetkiler { get; set; }
        public string KullaniciKodu { get; set; }
        public string GibKodu { get; set; }
        public string GibSifre { get; set; }
        public string NtbKodu { get; set; }
        public string NtbSifre { get; set; }
        public string DataAdi { get; set; }
        public string Sifre { get; set; }
    }
}
