using System.ComponentModel;

namespace Tacmin.Model.MaliyeIslemleri
{
    public class SorguSonucModel
    {
        public int Id { get; set; }
        [DisplayName("Ünvan")]
        public string Unvan { get; set; }
        public string Mesaj { get; set; }
    }
}
