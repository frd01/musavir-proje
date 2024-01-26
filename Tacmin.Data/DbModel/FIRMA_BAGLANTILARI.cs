using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tacmin.Data.DbModel
{
    public class FIRMA_BAGLANTILARI
    {
        [Key]
        public int ID { get; set; }

        public int? USER_ID { get; set; }

        public int? FIRMA_ID { get; set; }

        [Required]
        [ForeignKey("USER_ID")]
        public virtual KULLANICI_TANIMLARI KULLANICI { get; set; }

        [Required]
        [ForeignKey("FIRMA_ID")]
        public virtual FIRMA_TANIMLARI FIRMA { get; set; }
    }
}