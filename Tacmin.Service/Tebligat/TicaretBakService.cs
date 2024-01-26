using DataIslemler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Core.Interface;
using Tacmin.Core.Utilities;
using Tacmin.Model;
using Tacmin.Model.Tebligat;
using Tacmin.Model.Whatsapp;

namespace Tacmin.Service.Tebligat
{
    public class TicaretBakService
    {
        private readonly IVirtualContext vctx;
        private DataIslemler.Tebligat.TicaretBakanligi.ListeIslem listeIslem;
        private DataIslemler.Tebligat.TicaretBakanligi.DataIslem dataIslem;
        private DataIslemler.Tebligat.DosyaIslem dosyaIslem;

        public TicaretBakService(IVirtualContext _vctx)
        {
            vctx = _vctx;
            listeIslem = new DataIslemler.Tebligat.TicaretBakanligi.ListeIslem(vctx.DataAdi);
            dataIslem = new DataIslemler.Tebligat.TicaretBakanligi.DataIslem(vctx.DataAdi);
            dosyaIslem = new DataIslemler.Tebligat.DosyaIslem(vctx.DataAdi);
        }

        public List<TicaretMlyResponseModel> MaliteListeKayitKontrolIslem(List<TicaretMlyResponseModel> clientList)
        {
            var result = listeIslem.MaliteListeKayitKontrolIslem(clientList);

            return result;
        }

      

        public List<TebligatDataListModel> GetTebligatListesi(string durum )
        {
            var result = listeIslem.GetTebligatListesi(durum); 

            return result;
        }

        public List<TebligatDataListModel> SorguGetTebligatListesi(string durum,DateTime? tarih)
        {
            var result = listeIslem.GetTebligatListesi(durum,tarih);

            return result;
        }

        public void OkunduDurumGuncelle(List<int> clientList)
        {
            dataIslem.OkunduDurumGuncelle(clientList);
        }

        public TebligatDosyaModel PdfDokumanGoster(int? tebligatId)
        {
            var result = dosyaIslem.GetDosya(tebligatId, "asil", TebligatModulEnums.TİCARET_BAKANLIGI);

            var id_list = new List<int>();
            id_list.Add(tebligatId.Value);

            dataIslem.OkunduDurumGuncelle(id_list);

            return result;
        }

        public (byte[] file, string fileName) EkZipIndir(int? tebligatId, string tur)
        {
            var zipfiles = new Dictionary<string, byte[]>();

            var dosyalar = dosyaIslem.GetDosyalar(tebligatId, tur, TebligatModulEnums.TİCARET_BAKANLIGI);

            if (dosyalar != null && dosyalar.Count > 0)
            {
                foreach (var item in dosyalar)
                {
                    if (!zipfiles.ContainsKey(item.DosyaAdi))
                    {
                        zipfiles.Add(item.DosyaAdi, item.Icerik);
                    }
                }
            }

            var zip = ZipUtils.CreateZipFolder(zipfiles);

            var id_list = new List<int>();
            id_list.Add(tebligatId.Value);

            dataIslem.OkunduDurumGuncelle(id_list);

            var fName = $"File-{Guid.NewGuid()}.zip";

            return (zip, fName);
        }

        public List<FirmaModel> WhatsAppGonderimIcerikOlustur(List<TebligatDataListModel> clientList)
        {
            var islem = new DataIslemler.Whatsapp.TebligatIslem(vctx.DataAdi);

            var result = islem.GonderimIcerikOlustur(clientList, TebligatModulEnums.VERGI_DENETIM, EvrakTurEnum.TEBLIGAT_TICARET_BAKANLIGI);

            return result;

        }

        public void WhatsappGonderimDataKayit(List<FirmaModel> clientList)
        {
            var islem = new DataIslemler.Whatsapp.TebligatIslem(vctx.DataAdi);

            islem.GonderimDataKayit(clientList, EvrakTurEnum.TEBLIGAT_TICARET_BAKANLIGI);

        }

        public TicaretResModel TebligatSorguIslem(List<Firma> firmaListesi)
        {
            var islem = new Sorgulamalar.Tebligat.TicaretBakanligi.SorguDataIslem(vctx.DataAdi, firmaListesi);

            var result = islem.TebligatSorguIslem();

            return result;
        }
    }
}
