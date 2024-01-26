using System.ComponentModel.DataAnnotations;

namespace Tacmin.Data.DbModel
{
    public class Tebligat_Dosyalar
    {
        [Key]
        public int Id { get; set; }
        public int BelgeId { get; set; }
        public float? Boyut { get; set; }
        public string BelgeTipi { get; set; }
        public byte[] Dosya { get; set; }

    }
}
