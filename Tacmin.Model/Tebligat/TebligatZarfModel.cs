using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Tebligat
{
    class TebligatZarfModel
    {
        public TebligatZarfDetayModel data { get; set; }
    }

    public class TebligatZarfDetayModel
    {

        public string belgeDurumu { get; set; }
        public string otomatikOkunmaOptime { get; set; }
        public string dizin { get; set; }
        public string zarfDurumu { get; set; }
        public string zarfAciklama { get; set; }
        public string dosyaAdi { get; set; }
        public string belgeOid { get; set; }
        public string vdGondermeTarihi { get; set; }
        public string belgeTuru { get; set; }
        public string birimAdi { get; set; }
        public string zarfKonu { get; set; }
        public string belgeNo { get; set; }
        public string okunmaOptime { get; set; }
        public string zarfOid { get; set; }
        public string tebellugTarihi { get; set; }
        public List<TebligatZarfEklist> ekList { get; set; }
    }

    public class TebligatZarfEklist
    {

        public string ekAdi { get; set; }
        public string ekOid { get; set; }
        public string ekBelgeAdi { get; set; }
        public string oidBelge { get; set; }
        public string ekAciklama { get; set; }
    }
}
