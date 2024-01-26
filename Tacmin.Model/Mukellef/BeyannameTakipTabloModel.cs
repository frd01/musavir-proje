using System.Collections.Generic;
using System.ComponentModel;

namespace Tacmin.Model.Mukellef
{
    public class BeyannameTakipTabloModel
    {
        public int? Id { get; set; }
        [DisplayName("Tür")]
        public string Beyanname_Tur { get; set; }
        public int? Toplam { get; set; }
        public int? Onaylanan { get; set; }
        public int? Kalan { get; set; }
        public List<TakipFirma> ToplamFirmaListesi { get; set; }
        public List<TakipFirma> OnayFirmaListesi { get; set; }
        public List<TakipFirma> KalanFirmaListesi { get; set; }
    }

    public class TakipFirma
    {

        public int? Id { get; set; }
        public string Unvan { get; set; }
    }
}
