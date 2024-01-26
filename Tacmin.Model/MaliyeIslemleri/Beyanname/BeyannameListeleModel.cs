using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.MaliyeIslemleri.Beyanname
{
    public class BeyannameListeleModel
    {
        public int? Data_Firma_Id { get; set; }
        public bool? Data_Is_Vergi_No { get; set; }
        public bool? Data_Is_Vergi_Tur { get; set; }
        public int? Data_Vergi_Tur_Id { get; set; }
        public bool? Data_Is_Vergi_Dairesi { get; set; }
        public int? Data_Vergi_Daire_Id { get; set; }
        public DateTime? Data_Ilk_Tarih { get; set; }
        public DateTime? Data_Son_Tarih { get; set; }
    }
}
