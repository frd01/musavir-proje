using System;

namespace DataIslemler.Models
{
    public class AyModel
    {
        public int Id { get; set; }
        public string AyStr { get; set; }
        public int Ay { get; set; }
        public DateTime IlkTarih { get; set; }
        public DateTime SonTarih { get; set; }
        public string Title { get; set; }
    }
}
