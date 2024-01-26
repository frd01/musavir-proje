using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.KontrolIslem
{
    public class FirmaKontrolModel
    {
        public int Id { get; set; }
        [DisplayName("Ünvan")]
        public string Unvan { get; set; }
        [DisplayName("Gib Kodu")]
        public string GibKodu { get; set; }
        [DisplayName("Gib Şifre")]
        public string GibSifre { get; set; }
        [DisplayName("Gib Parola")]
        public string GibParola { get; set; }
        [DisplayName("Durum")]
        public bool IslemDurum { get; set; }
        [DisplayName("Açıklama")]
        public string Aciklama { get; set; }
    }
}
