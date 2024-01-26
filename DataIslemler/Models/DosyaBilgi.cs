namespace DataIslemler.Models
{
    public class DosyaBilgi
    {
        public int BelgeId { get; set; }
        public string DosyaAdi { get; set; }
        public float? Boyut { get; set; }
        public string BelgeTipi { get; set; }
        public byte[] Dosya { get; set; }
    }
}
