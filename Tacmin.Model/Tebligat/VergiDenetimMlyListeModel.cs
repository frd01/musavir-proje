using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Tebligat
{
    public class VergiDenetimMlyListeModel_Data_1
    {
        public VergiDenetimMlyListeModel_Data_2 data { get; set; }
    }

    public class VergiDenetimMlyListeModel_Data_2
    {
        public List<VergiDenetimMlyListeModel> data { get; set; }
    }
    public class VergiDenetimMlyListeModel
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
        public string ustYaziBaseStr { get; set; }
        public List<VergiDenetimMDosyaModel> ekListesi { get; set; }
        public int? firmaId { get; set; }
        public byte[] ustYaziIcerik { get; set; }
    }
}
