using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tacmin.Model.Musavir
{
    public class SozlesmeModel
    {
        public string oid { get; set; }
        [DisplayName("Hizmet Sözleşmesi Tarih No")]
        public string sozlesmesonislemtarihi { get; set; }
        [DisplayName("Sisteme Giriş Tarihi")]
        public string kayitzamani { get; set; }
        [DisplayName("Ünvan")]
        public string mukunvan { get; set; }
        [DisplayName("Vergi No")]
        public string mukvkn { get; set; }
        [DisplayName("Sözleşme No")]
        public string sozno { get; set; }
        [DisplayName("Vergi Dairesi")]
        public string mukellefvdkodutext { get; set; }
        [DisplayName("Sözleşme Tarihi")]
        public string soztarih { get; set; }
        [DisplayName("Sözleşme Türü")]
        public string sozlesmetipi { get; set; }
        [DisplayName("Tc No")]
        public string muktckn { get; set; }
    }
}
