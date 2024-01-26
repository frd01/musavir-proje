using System;
using System.ComponentModel;

namespace Tacmin.Model
{
    public class BeyannameRequestModel
    {
        [DisplayName("İlk Tarih")]
        public DateTime? TARIH_ILK { get; set; }

        [DisplayName("Son Tarih")]
        public DateTime? TARIH_SON { get; set; }
        [DefaultValue("")]
        public bool? Is_Vergi_No { get; set; }

        public int? Vergi_Tur_Id { get; set; }
        public bool? Is_Vergi_Tur { get; set; }

        public bool? Is_Vergi_Dairesi { get; set; }

        public string Vergi_No { get; set; }

        public string TcTanim { get; set; }

        public string VergiTanim { get; set; }

        public string BeyannameTurTanim { get; set; }

        public string VergiDairesiTanim { get; set; }

        public string BeyannameKodu { get; set; }
        public int? Firma_Id { get; set; }
        public int? Vergi_Daire_Id { get; set; }
        public string Vergi_Daire_Kodu { get; set; }
    }
}
