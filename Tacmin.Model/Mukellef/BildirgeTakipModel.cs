using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Mukellef
{
    public class BildirgeTakipModel
    {
        public int Id { get; set; }
        public int? FirmaId { get; set; }
        public string TurKod { get; set; }
        public string TurAciklama { get; set; }
        public string BtnId { get; set; }
        public string IconId { get; set; }
        public bool? IsSecili { get; set; }
    }
}
