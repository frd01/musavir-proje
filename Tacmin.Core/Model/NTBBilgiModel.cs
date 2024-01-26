using System.Collections.Generic;

namespace Tacmin.Core.Model
{
    public class NtbMeslekBilgisi
    {
        public string MeslekKodu { get; set; }
        public string MeslekAdi { get; set; }
    }

    public class NTBBilgiModel
    {
        public string Adi { get; set; }
        public string Soyadi { get; set; }
        public string BabaAdi { get; set; }
        public string VergiDairesiKodu { get; set; }
        public string VergiDairesiAdi { get; set; }
        public string VergiKimlikNo { get; set; }
        public string TcKimlikNo { get; set; }
        public string Unvan { get; set; }
        public string IsMahalleSemt { get; set; }
        public string IsCaddeSokak { get; set; }
        public string IsKapiNo { get; set; }
        public string IsDaireNo { get; set; }
        public string IsIlceAdi { get; set; }
        public string IsIlKodu { get; set; }
        public string IsIlAdi { get; set; }
        public string IkametMahalleSemt { get; set; }
        public string IkametCaddeSokak { get; set; }
        public string IkametKapiNo { get; set; }
        public string IkametDaireNo { get; set; }
        public string IkametIlceAdi { get; set; }
        public string IkametIlKodu { get; set; }
        public string IkametIlAdi { get; set; }
        public string Faal { get; set; }
        public string VergiLevhaUrl { get; set; }
        public List<NtbMeslekBilgisi> MeslekBilgileri { get; set; }
    }
}
