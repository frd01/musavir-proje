using System;
using System.Collections.Generic;

namespace Tacmin.Model
{
    public class EBildirgeIcerik
    {
        public string Tckn { get; set; }
        public string Unvan { get; set; }
        public DateTime Donem { get; set; }
        public string SicilNo { get; set; }
        public string BelgeCesidi { get; set; }
        public string Kanun { get; set; }
        public string Durum { get; set; }
        public DateTime Vade { get; set; }
        public decimal? Tutar { get; set; }
        public string MuhSgkBeyOid { get; set; }
        public string MuhSgkTahOid { get; set; }
        public byte[] beyfile { get; set; }
        public byte[] tahfile { get; set; }
        public int FirmaId { get; set; }
        //public string DonemStr { get; set; }
    }

    public class BeyannameIcerik
    {
        public short Ihbarnamekesildi { get; set; }
        public string Onaylanabilir { get; set; }
        public short Mesajvar { get; set; }
        public string BeyannameOid { get; set; }
        public int Subeno { get; set; }
        public string TahakkukOid { get; set; }
        public string BeyannameKodu { get; set; }
        public string Donem { get; set; }
        public string VergiDairesi { get; set; }
        public string Unvan { get; set; }
        public string Tckn { get; set; }
        public string Durum { get; set; }
        public string BeyannameTuru { get; set; }
        public DateTime? YuklenmeTarihi { get; set; }
        public DateTime? Vade { get; set; }
        public string fisNo { get; set; }
        public decimal? Tutar { get; set; }
        public byte[] beyfile { get; set; }
        public byte[] tahfile { get; set; }
        public bool islemDurum { get; set; }
        public string dosyaAdi { get; set; }
    }

    public class BeyannameListesi
    {
        public int Rowcount { get; set; }
        public string Page { get; set; }
        public IList<BeyannameIcerik> Data { get; set; }
    }

    public class BeyannameResultModel
    {
        public BeyannameListesi Data { get; set; }
    }

}
