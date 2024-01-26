using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Tacmin.Data.DbModel
{
    public class Firma_Iletisim
    {
        [Key]
        public int? Id { get; set; }
        public int? Firma_Id { get; set; }
        public string Adi { get; set; }
        public string Telefon { get; set; }
        public string Mail { get; set; }

        [Required]
        [ForeignKey("Firma_Id")]
        public virtual FIRMA_TANIMLARI FIRMA { get; set; }
    }
}
