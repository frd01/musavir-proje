namespace Tacmin.Data.DbModel
{
    public class Pos_Bilgileri
    {
        public int Id { get; set; }
        public int? Firma_Id { get; set; }
        public int? Yil { get; set; }
        public int? Ay { get; set; }
        public string Banka_Adi { get; set; }
        public string Uye_Isyeri_No { get; set; }
        public string Banka_Vergi_Kimlik_No { get; set; }
        public float? Tutar { get; set; }

    }
}
