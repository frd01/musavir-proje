using System.Collections.Generic;

namespace Tacmin.Model.Mukellef
{
    public class MaliyeBilgiModel
    {
        public MaliyeData data { get; set; }
    }

    public class MaliyeData
    {
        public int toplamsozlesmesayisi { get; set; }
        public int islemyapilanliste { get; set; }
        public List<MaliyeSozlesme> sozlesme { get; set; }
    }

    public class MaliyeSozlesme
    {
        public string oid { get; set; }
        public string sozlesmesonislemtarihi { get; set; }
        public string kayitzamani { get; set; }
        public string mukunvan { get; set; }
        public string mukvkn { get; set; }
        public string sozno { get; set; }
        public string sozlesmetipigizli { get; set; }
        public string muhvkn { get; set; }
        public string mukellefvdkodutext { get; set; }
        public string soztarih { get; set; }
        public string sozlesmetipi { get; set; }
        public string muktckn { get; set; }
        public string hizsoztarihno { get; set; }
        public string sozlesmedurum { get; set; }

    }

    public class MaliyeBilgiResponse
    {

        public bool IslemDurum { get; set; }
    }
}
