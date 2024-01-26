using DataIslemler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Core.Interface;
using Tacmin.Model;
using Tacmin.Model.Maliye;

namespace Tacmin.Service
{
    public class GelenFaturaService
    {
        private readonly IVirtualContext vctx;
        private DataIslemler.MaliyeGelenFatura.DataIslem dataIslem;
        private DataIslemler.MaliyeGelenFatura.ListeIslem listeIslem;

        public GelenFaturaService(IVirtualContext _vctx)
        {
            vctx = _vctx;
            dataIslem = new DataIslemler.MaliyeGelenFatura.DataIslem(vctx.DataAdi);
            listeIslem = new DataIslemler.MaliyeGelenFatura.ListeIslem(vctx.DataAdi);
        }
         
        public List<MaliyeGelenFaturaListeModel> GelenFaturaListesi(DateTime? ilkTarih, DateTime? sonTarih,Tacmin.Model.LocalData.FirmaModel firma)
        {

            var result = listeIslem.GelenFaturaListesi(ilkTarih, sonTarih,firma);

            return result;
        }

        public List<MaliyeGelenFaturaListeModel> SorguGelenFaturaListesi(DateTime? tarih)
        {
            var result = listeIslem.SorguGelenFaturaListesi(tarih);

            return result;
        }

        public List<TarihListeModel> TarihAralikListesi(DateTime? _ilkTarih, DateTime? _sonTarih)
        {

            var liste = new List<TarihListeModel>();

            var ilkTarih = _ilkTarih.Value;
            var sonTarih = _sonTarih.Value;

            while (ilkTarih < sonTarih)
            {
                var model = new TarihListeModel();
                model.BaslangicTarih = ilkTarih.ToString("yyyy-MM-dd");
                var tarih = ilkTarih.AddDays(7);
                if (tarih > sonTarih)
                {
                    model.BitisTarih = sonTarih.ToString("yyyy-MM-dd");
                    liste.Add(model);
                    break;
                }
                if (tarih < sonTarih)
                {
                    model.BitisTarih = tarih.ToString("yyyy-MM-dd");
                    liste.Add(model);
                    ilkTarih = tarih;
                }
                else
                {
                    model.BitisTarih = sonTarih.ToString("yyyy-MM-dd");
                    liste.Add(model);
                    break;
                }

                ilkTarih = ilkTarih.AddDays(1);
            }

            return liste;
        }

        public KayitResModel FaturaKayit(List<GelenFaturaModel> clientList,int? firmaId)
        {

            var result = dataIslem.FaturaKayit(clientList,firmaId);

            return result;
        }

        public List<Firma> GetFirmaListesi()
        {

            var islem = new DataIslemler.Listeler.FirmaListesi(vctx.DataAdi);

            return islem.GetFirmaListesi();
        }

        public List<GelenFaturaModel> FaturOtoKayit(List<GelenFaturaModel> clientList)
        {
            var result = dataIslem.FaturOtoKayit(clientList);

            return result;
        }

        public KayitResModel GelenFaturaSorguIslem(DateTime? ilkTarih,DateTime? sonTarih,Tacmin.Model.LocalData.FirmaModel firma)
        {
            var islem = new Service.Sorgulamalar.GelenFatura(vctx.DataAdi, firma, ilkTarih, sonTarih);

            var result = islem.SorguIslem();

            return result;
        }


    }
}
