using System;
using System.Linq;
using System.Web;
using Tacmin.Core.Interface;
using Tacmin.Model.Login;

namespace TacminMusavir
{
    public class TacminVirtualContext : IVirtualContext
    {
        private readonly HttpSessionStateBase _session;        

        public TacminVirtualContext(
            HttpSessionStateBase session
           
            )
        {
            _session = session;
           

            
        }

        public UserModel User => (_session["UserModel"] != null) ? (UserModel)_session["UserModel"] : null;

        public int UserId => User.UserId;

        public string UserKodu => User.KullaniciKodu;

        public bool Yetkili => User.Yetkili;

        public string Token => User.Token;

        public string AdSoyad => User.AdSoyad;

        public bool IsBuyukHarf => User.IsBuyukHarf;

        public (string kullaniciKodu, string sifre) NtbBilgileri => (User.NtbKodu, User.NtbSifre);

        public (string kullaniciKodu, string sifre) GibBilgileri => (User.GibKodu, User.GibSifre);

        public string DataAdi => User.DataAdi;

        public bool IsLoged()
        {
            return User != null;
        }

        public bool yetkiKontrol(params string[] yetkiler)
        {
            if (!IsLoged())
                return false;

            if (User.Yetkili)
                return true;

            if (yetkiler.Any(x => User.Yetkiler.Contains(x)))
                return true;

            return false;
        }
    }
}