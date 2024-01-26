using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Bildirge
{
    public class IseGirisListeModel
    {
        public int Id { get; set; }
        public int? FirmaId { get; set; }
        public int? SubeId { get; set; }
        [DisplayName("Refarans Kodu")]
        public string ReferansKodu { get; set; }
        [DisplayName("Tc No")]
        public string TcNo { get; set; }
        [DisplayName("Adı Soyadı")]
        public string AdiSoyadi { get; set; }
        public DateTime? Tarih { get; set; }
        [DisplayName("İslem Tarihi")]
        public DateTime? IslemTarihi { get; set; }
        [DisplayName("Giriş/Çıkış")]
        public string Durum { get; set; }
        [DisplayName("Ünvan")]
        public string Unvan { get; set; }
        [DisplayName("Şube Adı")]
        public string SubeAdi { get; set; }
    }
}
