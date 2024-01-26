using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Gib
{
    public class AyModel
    {
        public int Id { get; set; }
        public int Ay { get; set; }
        public string AyStr { get; set; }
        public DateTime IlkTarih { get; set; }
        public DateTime SonTarih { get; set; }
        public List<TarihListModel> TarihListesi { get; set; }
    }
}
