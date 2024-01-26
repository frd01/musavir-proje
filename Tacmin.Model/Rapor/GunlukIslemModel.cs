using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Rapor
{
    public class GunlukIslemModel
    {
        public int Id { get; set; }
        public DateTime? Tarih { get; set; }
        [DisplayName("E-Beyanname")]
        public int Beyaname { get; set; }
        public string BeyanameUrl { get; set; }
        [DisplayName("E-Bildirge")]
        public int Bildirge { get; set; }
        public string BildirgeUrl { get; set; }
        [DisplayName("Gelir İdaresi")]
        public int GelirIdaresiTebligat { get; set; }
        public string GelirIdaresiTebligatUrl { get; set; }
        [DisplayName("Vergi Denetim")]
        public int VergiDenetimTebligat { get; set; }
        public string VergiDenetimTebligatUrl { get; set; }
        [DisplayName("Ticaret Bakanlığı")]
        public int TicaretBakanligiTebligat { get; set; }
        public string TicaretBakanligiTebligatUrl { get; set; }
        [DisplayName("İçişleri Bakanlığı")]
        public int IcisleriTebligat { get; set; }
        public string IcisleriTebligatUrl { get; set; }
        [DisplayName("Gelen Fatura")]
        public int GelenFatura { get; set; }
        public string GelenFaturaUrl { get; set; }
        [DisplayName("Giden Fatura")]
        public int GidenFatura { get; set; }
        public string GidenFaturaUrl { get; set; }
        [DisplayName("İşe Girişler")]
        public int IseGiris { get; set; }
        public string IseGirisUrl { get; set; }
        [DisplayName("İşe Çıkışlar")]
        public int IseCikis { get; set; }
        public string IseCikisUrl { get; set; }
        [DisplayName("İş Kazası")]
        public int IsKazasi { get; set; }
        public string IsKazasiUrl { get; set; }
        [DisplayName("Hastalık")]
        public int Hastalik { get; set; }
        public string HastalikUrl { get; set; }
        [DisplayName("Analık")]
        public int Analik { get; set; }
        public string AnalikUrl { get; set; }
        [DisplayName("Mes.Has.")]
        public int MeslekHastalik { get; set; }
        public string MeslekHastalikUrl { get; set; }


    }
}
