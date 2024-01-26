using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Banka
{
    public class BankaIslemListeModel
    {
        public int Id { get; set; }
        public int? CariId { get; set; }
        public int? KasaId { get; set; }
        public int? TabloId { get; set; }
        [DisplayName("İşlem Türü")]
        public string Islem { get; set; }
        public DateTime? Tarih { get; set; }
        [DisplayName("İşlem Türü")]
        public string IslemTuru { get; set; }
        [DisplayName("Fiş No")]
        public string FisNo { get; set; }
        [DisplayName("İşlem Açıklama")]
        public string Aciklama { get; set; }
        [DisplayName("Toplam Yatan")]
        public decimal? Yatan { get; set; }
        [DisplayName("Toplam Çekilen")]
        public decimal? Cekilen { get; set; }
        public decimal? Bakiye { get; set; }
        public string BankaKodu { get; set; }
        public int? BankaId { get; set; }
        public string BankaAdi { get; set; }
        public string CariKodu { get; set; }
        public string CariUnvan { get; set; }
        public string TabloAciklama { get; set; }
        public string KasaKodu { get; set; }
        public string KasaAdi { get; set; }

    }
}
