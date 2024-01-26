using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Whatsapp
{
    public class KisiModel
    {
        public int Id { get; set; }
        public string Adi { get; set; }
        public string Telefon { get; set; }
        public string Mail { get; set; }
        public string Aciklama { get; set; }
        public List<BelgeModel> BelgeListesi { get; set; }
    }
}
