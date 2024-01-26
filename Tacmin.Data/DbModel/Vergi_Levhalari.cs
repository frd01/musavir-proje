using System.ComponentModel.DataAnnotations;

namespace Tacmin.Data.DbModel
{
    public class Vergi_Levhalari
    {
        [Key]
        public int Id { get; set; }
        public int FirmaId { get; set; }
        public int? Yil { get; set; }
        public string Beyan { get; set; }
        public string Matrah { get; set; }
        public byte[] Dosya { get; set; }
    }
}
