using DataIslemler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Core.Interface;
using Tacmin.Core.Utilities;
using Tacmin.Model.Tebligat;
using Tacmin.Model.Whatsapp;

namespace Tacmin.Service.Tebligat
{
    public class IcisleriService
    {
        private readonly IVirtualContext vctx;
        private DataIslemler.Tebligat.Icisleri.ListeIslem listeIslem;
        private DataIslemler.Tebligat.DosyaIslem dosyaIslem;
        private DataIslemler.Tebligat.Icisleri.DataIslem dataIslem;

        public IcisleriService(IVirtualContext _vctx)
        {
            vctx = _vctx;
            listeIslem = new DataIslemler.Tebligat.Icisleri.ListeIslem(vctx.DataAdi);
            dosyaIslem = new DataIslemler.Tebligat.DosyaIslem(vctx.DataAdi);
            dataIslem = new DataIslemler.Tebligat.Icisleri.DataIslem(vctx.DataAdi);
        }

        public List<TebligatDataList> GetTebligatListesi(string durum) 
        {

            var liste = listeIslem.GetTebligatListesi(durum);

            return liste;
        }

        public List<TebligatDataList> SorguGetTebligatListesi(string durum,DateTime? tarih)
        {

            var liste = listeIslem.GetTebligatListesi(durum,tarih);

            return liste;
        }

        public TebligatDosyaModel GetPdfDokuman(int? tebligatId)
        {

            var pdfFile = dosyaIslem.GetDosya(tebligatId, "asil", TebligatModulEnums.ICISLERI_BAKANLIGI);

            

            return pdfFile;
        }

        public (byte[] file, string fileName) EkZipIndir(int? tebligatId,string tur)
        {
            var zipfiles = new Dictionary<string, byte[]>();

            var dosyalar = dosyaIslem.GetDosyalar(tebligatId, tur, TebligatModulEnums.ICISLERI_BAKANLIGI);

            if (dosyalar!=null && dosyalar.Count>0)
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

            var fName = $"File-{Guid.NewGuid()}.zip";

            return (zip, fName);
        }

        public void OkumaDurumGuncelle(int? tebligatId)
        {

            dataIslem.OkumaDurumGuncelle(tebligatId); 
        }

        public void TopluOkumaDurumGuncelle(List<int> tebligatListesi)
        {

            dataIslem.OkumaDurumlariGuncelle(tebligatListesi);
        }

        public List<TebligatDataList> TebligatDataSorguIslem(ListeleRequestModel clienSorguModel)
        {

            return listeIslem.GetTebligatListeleSorgu(clienSorguModel);
        }

        public List<IsisleriResponseListe> MaliyeListeKayitKontrolIslem(List<IsisleriResponseListe> clientListe)
        {

            var result = listeIslem.MaliyeListeKayitKontrolIslem(clientListe);

            return result;
        }

        public List<FirmaModel> WhatsAppGonderimIcerikOlustur(List<TebligatDataListModel> clientList)
        {
            var islem = new DataIslemler.Whatsapp.TebligatIslem(vctx.DataAdi);

            var result = islem.GonderimIcerikOlustur(clientList, TebligatModulEnums.VERGI_DENETIM, EvrakTurEnum.TEBLIGAT_ICISLERI_BASKANLIGI);

            return result;

        }

        public void WhatsappGonderimDataKayit(List<FirmaModel> clientList)
        {
            var islem = new DataIslemler.Whatsapp.TebligatIslem(vctx.DataAdi);

            islem.GonderimDataKayit(clientList, EvrakTurEnum.TEBLIGAT_ICISLERI_BASKANLIGI);

        }

        public IcisleriResModel TebligatSorguIslem(List<Firma> firmaListesi)
        {
            var islem = new Sorgulamalar.Tebligat.İcisleriBakanligi.SorguDataIslem(vctx.DataAdi, firmaListesi);

            var result = islem.TebligatSorguIslem();

            return result;
        }
    }
}
