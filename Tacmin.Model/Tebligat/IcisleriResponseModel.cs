using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Tebligat
{
    public class IcisleriResponseModel
    {
        public string gib_kodu { get; set; }
        public string gib_sifre { get; set; }
        public string gib_parola { get; set; }
        public int? cariId { get; set; }
        public string mesaj { get; set; }
        public bool? isHata { get; set; }
        public string token { get; set; }
        public List<IsisleriResponseListe> tebligatListesi { get; set; }
    }

    public class IsisleriResponseListe_Data_1
    {
        public IsisleriResponseListe_Data_2 data { get; set; }
    }

    public class IsisleriResponseListe_Data_2
    {
        public List<IsisleriResponseListe> data { get; set; }
    }

    public class IsisleriResponseListe
    {
        public string kurumKodu { get; set; }
        public string gerceklesenOtomatikOkunmaZamani { get; set; }
        public string tarafOid { get; set; }
        public string altKurumKodu { get; set; }
        public string belgeNo { get; set; }
        public string mukellefOkumaZamani { get; set; }
        public string dizin { get; set; }
        public string kayitZamani { get; set; }
        public string oid { get; set; }
        public string belgeTuru { get; set; }
        public string tebligZamani { get; set; }
        public string ustYaziPdfBaseStr { get; set; }
        public List<IcisleriDosyaModel> ekListesi { get; set; }
        public int? firmaId { get; set; }
        public byte[] ustYaziIcerik { get; set; }
    }

    public class IcisleriDosyaModel
    {
        public int? Id { get; set; }
        public int? TebligatId { get; set; }
        public string DosyaAdi { get; set; }
        public string Tur { get; set; }
        public string MimeType { get; set; }
        public byte[] Icerik { get; set; }
        public string belgeOid { get; set; }
        public string DosyaBaseStr { get; set; }
    }
}
