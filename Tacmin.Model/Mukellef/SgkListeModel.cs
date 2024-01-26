using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Mukellef
{
    public class SgkListeModel
    {
        public int? Id { get; set; }
        [DisplayName("Şube Adı")]
        public string SubeAdi { get; set; }
        [DisplayName("Adres")]
        public string Adres { get; set; }
        public string KullaniciAdi_1 { get; set; }
        public string KullaniciAdi_2 { get; set; }
        public string SistemSifresi { get; set; }
        public string IsyeriSifresi { get; set; }
        public DateTime? AcilisTarihi { get; set; }
        public DateTime? KapanisTarihi { get; set; }
        public string Notlar { get; set; }
        public bool? Aktif { get; set; }
        public List<BildirgeTakipModel> TakipListesi { get; set; }
        public string M { get; set; }
        public string IsKoluKodu { get; set; }
        public string Yeni { get; set; }
        public string Eski { get; set; }
        public string IsYeriSiraNo { get; set; }
        public string IlKodu { get; set; }
        public string IlceKodu { get; set; }
        public string KontrolNo { get; set; }
        public string AraciKodu { get; set; }


    }
}
