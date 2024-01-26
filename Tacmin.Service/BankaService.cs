using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Core.Interface;
using Tacmin.Model;
using Tacmin.Model.Banka;
using Tacmin.Model.Ortak;

namespace Tacmin.Service
{
    public class BankaService
    {
        private readonly IVirtualContext vctx;
        private DataIslemler.Banka.ListeIslem listeIslem;
        private DataIslemler.Banka.DataIslem dataIslem;
        private DataIslemler.Banka.RaporIslem raporIslem;

        public BankaService(IVirtualContext _vctx)
        {
            vctx = _vctx;

            listeIslem = new DataIslemler.Banka.ListeIslem(vctx.DataAdi, vctx.IsBuyukHarf);
            dataIslem = new DataIslemler.Banka.DataIslem(vctx.DataAdi);
            raporIslem = new DataIslemler.Banka.RaporIslem(vctx.DataAdi, vctx.IsBuyukHarf);

        }

        public List<BankaKartListeModel> BankaKartListesi()
        {

            var result = listeIslem.BankaKartListesi();

            return result;
        }

        public List<ListeModel> HesapTurListesi()
        {
            var result = listeIslem.HesapTurListesi();

            return result;
        }

        public List<ListeModel> ParaBirimListesi()
        {
            var result = listeIslem.ParaBirimListesi();

            return result;
        }

        public KayitResModel YeniBankaKartKaydi(BankaKartModel clientData)
        {

            var result = dataIslem.YeniBankaKartiKaydi(clientData);

            return result;
        }

        public KayitResModel BankaIslemKayit(BankaIslemModel clientData, string islemTur)
        {

            var result = dataIslem.BankaIslemKayit(clientData, islemTur);

            return result;
        }

        public List<Tacmin.Model.Genel.KartModel> GetBankaKartListesi()
        {

            var result = listeIslem.BankaListesi();

            return result;
        }

        public List<Tacmin.Model.Genel.KartModel> GetCariListesi()
        {

            var result = listeIslem.CariListesi();

            return result;
        }

        public List<Tacmin.Model.Genel.KartModel> GetKasaListesi()
        {

            var result = listeIslem.GetKasaListesi();

            return result;
        }

        public List<BankaIslemListeModel> BankaIslemListesi(int? bankaId)
        {

            var result = raporIslem.BankaIslemListesi(bankaId);

            return result;
        }

        public KayitResModel BankaKayitSil(BankaIslemModel clientData, string islemTur)
        {

            var result = dataIslem.BankaKayitSil(clientData, islemTur);

            return result;
        }

        public KayitResModel BankaKartGuncelleme(BankaKartModel clientData)
        {

            var result = dataIslem.BankaKartGuncelleme(clientData);

            return result;
        }

        public BankaIslemModel GetBankaIslem(int? islemId, string islemTur)
        {
            var result = listeIslem.GetBankaIslem(islemId, islemTur);

            return result;
        }

        public KayitResModel BankaIslemGuncelleme(BankaIslemModel clientData, string islemTur)
        {
            var result = dataIslem.BankaIslemGuncelleme(clientData, islemTur);

            return result;
        }

        public List<BankaBakiyeRaporModel> BankaBakiyeRaporu(Model.RaporRequestModel clientData)
        {

            var result = raporIslem.BankaBakiyeRaporu(clientData);

            return result;
        }

        public BankaResponseModel<BankaEsktraModel, BankaEkstraParamsModel> GetBankaEkstraRapor(RaporRequestModel clientData)
        {

            var islem = new DataIslemler.Banka.BankaEsktraRapor(vctx.DataAdi, vctx.IsBuyukHarf);

            return islem.GetBankaEkstraRapor(clientData);
        }
    }
}
