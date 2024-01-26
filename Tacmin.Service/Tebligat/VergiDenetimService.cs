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
    public class VergiDenetimService
    {
        private readonly IVirtualContext vctx;
        private DataIslemler.Tebligat.VergiDenetim.ListeIslem listeIslem;
        private DataIslemler.Tebligat.VergiDenetim.DataIslem dataIslem;
        private DataIslemler.Tebligat.DosyaIslem dosyaIslem;

        public VergiDenetimService(IVirtualContext _vctx)
        {
            vctx = _vctx;

            listeIslem = new DataIslemler.Tebligat.VergiDenetim.ListeIslem(vctx.DataAdi);
            dataIslem = new DataIslemler.Tebligat.VergiDenetim.DataIslem(vctx.DataAdi);
            dosyaIslem = new DataIslemler.Tebligat.DosyaIslem(vctx.DataAdi);
        }

        public List<VergiDenetimMlyListeModel> MaliyeListeKayitKontrolIslem(List<VergiDenetimMlyListeModel> clientList)
        {
            var result = listeIslem.MaliyeListeKayitKontrolIslem(clientList);

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

        public TebligatDosyaModel PdfDokumanGoster(int? tebligatId)
        {
            var result = dosyaIslem.GetDosya(tebligatId, "asil", TebligatModulEnums.VERGI_DENETIM);

            var id_list = new List<int>();
            id_list.Add(tebligatId.Value);

            dataIslem.OkunduDurumGuncelle(id_list);

            return result;
        }

        public (byte[] file, string fileName) EkZipIndir(int? tebligatId, string tur)
        {
            var zipfiles = new Dictionary<string, byte[]>();

            var dosyalar = dosyaIslem.GetDosyalar(tebligatId, tur, TebligatModulEnums.VERGI_DENETIM);

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

        public void OkunduDurumGuncelle(List<int> clientList)
        {
            dataIslem.OkunduDurumGuncelle(clientList);
        }

        public List<FirmaModel> WhatsAppGonderimIcerikOlustur(List<TebligatDataListModel> clientList)
        {
            var islem = new DataIslemler.Whatsapp.TebligatIslem(vctx.DataAdi);

            var result = islem.GonderimIcerikOlustur(clientList, TebligatModulEnums.VERGI_DENETIM, EvrakTurEnum.TEBLIGAT_VERGI_DENETIM_BASKANLIGI);

            return result;

        }

        public void WhatsappGonderimDataKayit(List<FirmaModel> clientList)
        {
            var islem = new DataIslemler.Whatsapp.TebligatIslem(vctx.DataAdi);

            islem.GonderimDataKayit(clientList, EvrakTurEnum.TEBLIGAT_VERGI_DENETIM_BASKANLIGI);

        }
    }
}
