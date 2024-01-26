using DataIslemler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Core.Interface;
using Tacmin.Model;
using Tacmin.Model.Mukellef;
using Tacmin.Model.Tebligat;
using Tacmin.Model.Whatsapp;

namespace Tacmin.Service.Tebligat
{
    public class GelirTebService
    {
        private readonly IVirtualContext vctx;
        private DataIslemler.Tebligat.GelirIdaresi.ListeIslem listeIslem;
        private DataIslemler.Tebligat.GelirIdaresi.DataIslem dataIslem;
        private DataIslemler.Tebligat.DosyaIslem dosyaIslem;

        public GelirTebService(IVirtualContext _vctx)
        {

            vctx = _vctx;
            listeIslem = new DataIslemler.Tebligat.GelirIdaresi.ListeIslem(vctx.DataAdi);
            dataIslem = new DataIslemler.Tebligat.GelirIdaresi.DataIslem(vctx.DataAdi);
            dosyaIslem = new DataIslemler.Tebligat.DosyaIslem(vctx.DataAdi);
        }

       

        public List<GelirMlyResponseModel> ListeKayitKontrolIslem(List<GelirMlyResponseModel> clientList)
        {

            var result = listeIslem.ListeKayitKontrolIslem(clientList);

            return result;
        }

        public List<TebligatDataListModel> GetTebligatListesi(string durum) 
        {

            var result = listeIslem.GetTebligatListesi(durum); 

            return result;
        }

        public List<TebligatDataListModel> SorguGetTebligatListesi(string durum,DateTime? tarih)
        {

            var result = listeIslem.GetTebligatListesi(durum,tarih);

            return result;
        }

        public TebligatDosyaModel GetDosya(int? tebligatId)
        {
            
            var result = dosyaIslem.GetDosya(tebligatId, "asil", TebligatModulEnums.GELIR_IDARESI);

            var liste = new List<int>();

            liste.Add(tebligatId.Value);

            dataIslem.TebligatDurumGuncelle(liste);

            return result;
        }

        public void TebligatDurumGuncelle(List<int> clientList)
        {
            dataIslem.TebligatDurumGuncelle(clientList);
        }

        public List<FirmaModel> WhatsAppGonderimIcerikOlustur(List<TebligatDataListModel> clientList)
        {
            var islem = new DataIslemler.Whatsapp.TebligatIslem(vctx.DataAdi);

            var result = islem.GonderimIcerikOlustur(clientList, TebligatModulEnums.VERGI_DENETIM, EvrakTurEnum.TEBLIGAT_GELIR_IDARESI_BASKANLIGI);

            return result;

        }

        public void WhatsappGonderimDataKayit(List<FirmaModel> clientList)
        {
            var islem = new DataIslemler.Whatsapp.TebligatIslem(vctx.DataAdi);

            islem.GonderimDataKayit(clientList, EvrakTurEnum.TEBLIGAT_GELIR_IDARESI_BASKANLIGI);

        }

        public GelirIdaresiResModel TebligatSorguIslem(List<Firma> firmaListesi)
        {
            var islem = new Sorgulamalar.Tebligat.GelirIdaresi.SorguDataIslem(vctx.DataAdi, firmaListesi);

            var result = islem.TebligatSorguIslem();

            return result;
        }
    }
}
