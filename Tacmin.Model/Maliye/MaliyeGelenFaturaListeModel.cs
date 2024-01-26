using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Maliye
{
    public class MaliyeGelenFaturaListeModel
    {
        public int Id { get; set; }
        [DisplayName("Fatura No")]
        public string FaturaNo { get; set; }
        [DisplayName("Vergi No")]
        public string VergiNo { get; set; }
        public DateTime? Tarih { get; set; }
        [DisplayName("Döviz Cinsi")]
        public string ParaBirimi { get; set; }
        [DisplayName("Tesisat No")]
        public string TesisatNo { get; set; }
        [DisplayName("Ödenecek")]
        public decimal? Odenecek { get; set; }
        public decimal? Vergi { get; set; }
        public decimal? Toplam { get; set; }
        [DisplayName("Ünvan")]
        public string Unvan { get; set; }
        [DisplayName("Gönderim Şekli")]
        public string GonderimSekli { get; set; }
        [DisplayName("Mükellef Ünvan")]
        public string MukellefUnvan { get; set; }
        public int? FirmaId { get; set; }
    }
}
