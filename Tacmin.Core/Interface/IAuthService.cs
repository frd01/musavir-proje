namespace Tacmin.Core.Interface
{
    public interface IAuthService
    {
        bool IsValidAuth(string kullaniciKodu, string kullaniciAdi, string sifreMd5);

        void SignIn(string kullaniciKodu, string kullaniciAdi, bool hatirla);
    }
}
