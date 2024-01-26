using System.ComponentModel.DataAnnotations;

namespace Tacmin.Model
{
    public class UserLoginModel
    {
        [StringLength(50)]
        public string kullaniciKodu { get; set; }

        [StringLength(50)]
        public string kullaniciAdi { get; set; }

        [Required]
        public string sifre { get; set; }

        public bool beniHatirla { get; set; }

        public string returl { get; set; }

        public string token { get; set; }
    }
}
