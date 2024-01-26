using System.Collections.Generic;

namespace DataIslemler.Models.Gib
{
    public class GelenFaturaModel
    {
        public GelenFaturaListe data { get; set; }
    }
    public class GelenFaturaListe
    {

        public List<GelenFaturaResponseModel> FATURASONUC { get; set; }
    }
    public class GelenFaturaResponseModel
    {
        public string faturaNo { get; set; }
        public string mukVkn { get; set; }
        public string tarih { get; set; }
        public string tesisatNo { get; set; }
        public string unvan { get; set; }
        public decimal? vergi { get; set; }
        public decimal? odenecek { get; set; }
        public decimal? toplam { get; set; }
        public string gonderimSekli { get; set; }


    }
}
