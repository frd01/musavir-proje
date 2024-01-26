using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Main
{
    public class SorguOzetModel
    {
        public int Id { get; set; }
        public string ModulAdi { get; set; }
        public int? EvrakSayisi { get; set; }
        public string Aciklama { get; set; }
        public DateTime? Tarih { get; set; }
    }
}
