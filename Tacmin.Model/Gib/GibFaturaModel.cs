using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Gib
{
    public class GibFaturaModel
    {
        public int Id { get; set; }
        [DisplayName("Fatura No")]
        public string FaturaNo { get; set; }
        public DateTime? Tarih { get; set; }
        public int? CariId { get; set; }
        [DisplayName("Ünvan")]
        public string Unvan { get; set; }
        [DisplayName("Firma Ünvan")]
        public string FirmaUnvan { get; set; }
        [DisplayName("Vergi No")]
        public string VergiNo { get; set; }
        [DisplayName("Para Birimi")]
        public string ParaBirimi { get; set; }
        [DisplayName("Tesisat No")]
        public string TesisatNo { get; set; }
        [DisplayName("Gönderim Şekli")]
        public string GonderimSekli { get; set; }
        public decimal? Toplam { get; set; }
        public decimal? Vergi { get; set; }
        [DisplayName("Ödenecek")]
        public decimal? Odenecek { get; set; }
    }
}
