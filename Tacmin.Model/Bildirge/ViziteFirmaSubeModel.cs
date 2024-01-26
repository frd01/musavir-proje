using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Bildirge
{
    public class ViziteFirmaSubeModel
    {
        public int Id { get; set; }
        public int? FirmaId { get; set; }
        public string KullaniciKodu { get; set; }
        public string IsYeriKodu { get; set; }
        public string IsYeriSifresi { get; set; }
        public string SistemSifresi { get; set; }
    }
}
