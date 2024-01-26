using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Core.Interface;
using Tacmin.Model;
using Tacmin.Model.Genel;
using Tacmin.Model.Kasa;

namespace Tacmin.Service
{
    public class KasaService
    {
        private readonly IVirtualContext vctx;
        private DataIslemler.Kasa.DataIslem dataIslem;
        private DataIslemler.Kasa.ListeIslem listeIslem;
        private DataIslemler.Kasa.RaporIslem raporIslem;

        public KasaService(IVirtualContext _vctx)
        {
            vctx = _vctx;

            dataIslem = new DataIslemler.Kasa.DataIslem(vctx.DataAdi);
            listeIslem = new DataIslemler.Kasa.ListeIslem(vctx.DataAdi, vctx.IsBuyukHarf);
            raporIslem = new DataIslemler.Kasa.RaporIslem(vctx.DataAdi, vctx.IsBuyukHarf);
        }

        public List<KartListeModel> KasaKartListesi()
        {
            var result = listeIslem.KasaKartListesi();

            return result;
        }

        public Tacmin.Model.Kasa.KartModel GetKasaKart(int? kasaId)
        {
            var result = listeIslem.GetKasaKart(kasaId);

            return result;
        }

        public List<KasaTakipModel> KasaIslemTakipListesi()
        {

            var result = listeIslem.KasaIslemTakipListesi();

            return result;
        }

        public KayitResModel YeniKasaKaydi(Tacmin.Model.Kasa.KartModel clientData)
        {
            var result = dataIslem.YeniKasaKaydi(clientData);

            return result;
        }

        public List<BirimModel> KasaParaBirimListesi()
        {

            var result = listeIslem.KasaParaBirimListesi();

            return result;
        }

        public List<KasaIslemListeModel> KasaIslemListesi()
        {

            var result = raporIslem.KasaIslemListesi();

            return result;
        }

        public List<Tacmin.Model.Genel.KartModel> CariFilterList()
        {

            var result = listeIslem.CariFilterList();

            return result;
        }

        public List<Tacmin.Model.Genel.KartModel> KasaKartFilterList()
        {

            var result = listeIslem.KasaKartFilterList();

            return result;
        }

        public KayitResModel KasaNakitIslem(string islemTur, KasaIslemModel clientData)
        {
            var result = dataIslem.KasaNakitIslem(islemTur, clientData);

            return result;
        }

        public List<KasaIslemListeModel> KasaAyrintiListe(int? kasaId)
        {

            var result = raporIslem.KasaAyrintiListe(kasaId);

            return result;
        }

        public KasaIslemModel GetKasaIslem(int? islemId, string islemTur)
        {

            var result = listeIslem.GetKasaIslem(islemId, islemTur);

            return result;
        }

        public KayitResModel KasaIslemGuncelleme(KasaIslemModel clientData, string islemTur)
        {
            var result = dataIslem.KasaIslemGuncelleme(clientData, islemTur);

            return result;
        }

        public KayitResModel KasaIslemSil(int? islemId, string islemTur)
        {
            var result = dataIslem.KasaIslemSil(islemId, islemTur);

            return result;
        }

        public List<KasaBakiyeModel> KasaBakiyeRaporu(RaporRequestModel clientData)
        {
            var result = raporIslem.KasaBakiyeRaporu(clientData);

            return result;
        }

        public RaporParamsModel<KasaEkstraModel, KasaEkstraParamsModel> KasaEsktraListesi(RaporRequestModel clientData)
        {

            var result = raporIslem.KasaEsktraListesi(clientData);

            return result;
        }
    }
}
