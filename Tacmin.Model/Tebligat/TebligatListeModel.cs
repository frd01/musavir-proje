using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Tebligat
{
    public class TebligatListeModel
    {
        public int? cariId { get; set; }
        public string gib_kodu { get; set; }
        public string gib_sifre { get; set; }
        public string gib_parola { get; set; }
        public List<TebligatListeModelListe> tebligatListe { get; set; }
        public bool? isHata { get; set; }
        public string mesaj { get; set; }
        public string token { get; set; }
    }

    public class TebligatListeModelListe
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
        public string dosyaAdi { get; set; }
        public List<TebligatEkList> ekListesi { get; set; }
        public List<TebligatDosyaModel> Dosyalar { get; set; }

    }

    public class TebligatEkList
    {
        public string ekAdi { get; set; }
        public string ekOid { get; set; }
        public string ekBelgeAdi { get; set; }
        public string oidBelge { get; set; }
        public string ekAciklama { get; set; }
    }
}
