using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Tebligat
{
    public class TicaretMlyResponseModel_Data_1
    {
        public TicaretMlyResponseModel_Data_2 data { get; set; }
    }

    public class TicaretMlyResponseModel_Data_2
    {
        public List<TicaretMlyResponseModel> data { get; set; }
    }
    public class TicaretMlyResponseModel
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
        public int? firmaId { get; set; }
        public string ustYaziBaseStr { get; set; } 

        public List<TicaretMlyDosyaModel> ekListesi { get; set; }

        public byte[] ustYaziIcerik { get; set; }

    }
}
