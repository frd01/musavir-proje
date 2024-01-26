using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Tebligat
{
    public class GelirMlyResponseModel_Data_1
    {
        public GelirMlyResponseModel_Data_2 data { get; set; }
    }

    public class GelirMlyResponseModel_Data_2
    {
        public List<GelirMlyResponseModel> list { get; set; }
    }
    public class GelirMlyResponseModel
    {
        public string etebBelgeTuru { get; set; }
        public string belgeDurumu { get; set; }
        public string okumaDurum { get; set; }
        public string vdKodu { get; set; }
        public string birimAdi { get; set; }
        public string etebBelgeNo { get; set; }
        public string dizin { get; set; }
        public string zarfOid { get; set; }
        public string vdGondermeTarihi { get; set; }
        public string tebellugTarihi { get; set; }
        public string belgeOid { get; set; }
        public string belgeTuru { get; set; }
        public string ustYaziBase64Str { get; set; }
        public int firmaId { get; set; }
        public byte[] ustYaziIcerik { get; set; }
    }
}
