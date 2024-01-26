using System.ComponentModel;

namespace DataIslemler.Models.Mukellef
{
    public class VergiLevhaListeModel
    {
        [Key]
        public int Id { get; set; }
        [DisplayName("Ünvan")]
        public string UNVAN { get; set; }
        [DisplayName("Vergi Dairesi")]
        public string VERGI_DAIRESI { get; set; }
        [DisplayName("Vergi No")]
        public decimal? VERGI_NO { get; set; }
        [DisplayName("Takvim")]
        public int? Yil { get; set; }
        [DisplayName("Beyan Olunan Matrah")]
        public string Beyan { get; set; }
        [DisplayName("Tahakkuh Eden Vergi")]
        public string Matrah { get; set; }

    }
}
