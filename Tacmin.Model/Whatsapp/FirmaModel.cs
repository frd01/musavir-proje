using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Whatsapp
{
    public class FirmaModel
    {
        public int Id { get; set; }
        public string FirmaUnvan { get; set; }
        public List<KisiModel> KisiListesi { get; set; }
        
    }
}
