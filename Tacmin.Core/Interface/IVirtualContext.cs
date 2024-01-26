namespace Tacmin.Core.Interface
{
    public interface IVirtualContext
    {
        int UserId { get; }

        string UserKodu { get; }

        string Token { get; }

        string AdSoyad { get; }

        string DataAdi { get; }
        bool IsBuyukHarf { get; }

        bool IsLoged();

        bool yetkiKontrol(params string[] yetkiler);

        (string kullaniciKodu, string sifre) NtbBilgileri { get; }

        (string kullaniciKodu, string sifre) GibBilgileri { get; }
    }
}