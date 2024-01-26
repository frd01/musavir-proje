using Kendo.Mvc.UI;
using System.Collections.Generic;
using System.Linq;
using Tacmin.Core;
using Tacmin.Core.Interface;
using Tacmin.Data;
using Tacmin.Data.DbModel;
using Tacmin.Data.Repositories;
using Tacmin.Model;
using Tacmin.Model.Sms;

namespace Tacmin.Service
{
    public class HesapService
    {
        private readonly MainRepository<KULLANICI_TANIMLARI> _userRep;
        private readonly IVirtualContext _vctx;
       
        private DataIslemler.Hesap.ListeIslem listeIslem;
        private DataIslemler.Hesap.DataIslem dataIslem;

        public HesapService(
            MainRepository<KULLANICI_TANIMLARI> userRep,
            IVirtualContext vctx
           
            )
        {
            _userRep = userRep;
            _vctx = vctx;
           
            listeIslem = new DataIslemler.Hesap.ListeIslem(_vctx.DataAdi);
            dataIslem = new DataIslemler.Hesap.DataIslem();

        }

        public Tacmin.Model.Hesap.KullaniciModel GetUser()
        {
            var result = listeIslem.GetKullanici(_vctx.UserId);

            return result;
        }

        public KayitResModel KullaniciBilgiGuncelle(Tacmin.Model.Hesap.KullaniciModel kullanici)
        {
            var result = dataIslem.KullaniciBilgiGuncelle(kullanici);

            return result;
        }

        public List<SmsFirmaModel> SmsFirmaListesi()
        {
            var result = listeIslem.SmsFirmaListesi();

            return result;
        }


    }
}
