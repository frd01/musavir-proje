using System.ComponentModel.DataAnnotations;

namespace Tacmin.Data.DbModel
{
    public class DOSYA
    {
        [Key]
        public int ID { get; set; }

        public byte[] ICERIK { get; set; }

        public float? SIZE { get; set; }

        [StringLength(100)]
        public string DOSYA_ADI { get; set; }

        [StringLength(150)]
        public string MIME_TYPE { get; set; }
    }
}
