using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Whatsapp
{
    public class BelgeModel
    {
        public int Id { get; set; }
        public string BelgeNo { get; set; }
        public string DosyaAdi { get; set; }
        public string DosyaTuru { get; set; }
        public EvrakTurEnum EvrakTur { get; set; }
        public bool GonderimDurum { get; set; }
        public string Base64Str { get; set; }
        public string Aciklama { get; set; }
        public string FisNo { get; set; }
        public string MimeType { get; set; }
        public string EvrakLink { get; set; }

    }
}
