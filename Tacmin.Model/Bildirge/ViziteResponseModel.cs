using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Bildirge
{
    public class ViziteResponseModel
    {
        public string Is_Kazasi_Tarihi { get; set; }
        public string a_baslangic_tarihi { get; set; }
        public string a_bitis_tarihi { get; set; }
        public string ad_soyad { get; set; }
        public string adi { get; set; }
        public string dogum_baslangic_tarihi { get; set; }
        public string isbasi_tarihi { get; set; }
        public string medula_rapor_id { get; set; }
        public bool? onayli { get; set; }
        public string poliklinik_tarihi { get; set; }
        public string rapor_sira_no { get; set; }
        public string rapor_takip_no { get; set; }
        public string soy_adi { get; set; }
        public string tc_no { get; set; }
        public string vaka { get; set; }
        public string vaka_adi { get; set; }
        public string yatis_rapor_baslangic_tarihi { get; set; }
        public string yatis_rapor_bitis_tarihi { get; set; }
        public int? firmaId { get; set; }
        public int? subeId { get; set; }
    }
}
