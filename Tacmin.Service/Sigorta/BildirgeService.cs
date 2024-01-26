using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Web;
using Tacmin.Core.Extensions;
using Tacmin.Core.Interface;
using Tacmin.Core.Utilities;
using Tacmin.Data;
using Tacmin.Data.DbModel;
using Tacmin.Data.Repositories;
using Tacmin.Model;
using Tacmin.Model.Ortak;
using System.Linq;
using Tacmin.Model.Whatsapp;
using Tacmin.Model.Bildirge;
using Tacmin.Model.Maliye;

namespace Tacmin.Service
{
    public class BildirgeService
    {
        private readonly MainContext _ctx;
        private readonly MainRepository<BILDIRGE_ICERIKLERI> _bildirgeService;
        private readonly MainRepository<FIRMA_BAGLANTILARI> _firmaBaglantiService;
        private readonly IVirtualContext _vctx;
        private readonly MainRepository<DOSYA> _dosyaService;
        private DataIslemler.Bildirge.ListeIslem listeIslem;
        private DataIslemler.Bildirge.DataIslem dataIslem;
        

        public BildirgeService(
            MainContext ctx,
            MainRepository<BILDIRGE_ICERIKLERI> bildirgeService,
            MainRepository<FIRMA_BAGLANTILARI> firmaBaglantiService,
            IVirtualContext vctx,
            MainRepository<DOSYA> dosyaService
            
            )
        {
            _ctx = ctx;
            _bildirgeService = bildirgeService;
            _firmaBaglantiService = firmaBaglantiService;
            _vctx = vctx;
            _dosyaService = dosyaService;

            dataIslem = new DataIslemler.Bildirge.DataIslem(vctx.DataAdi);
            listeIslem = new DataIslemler.Bildirge.ListeIslem(vctx.DataAdi);
           
        }

        public DataSourceResult GetBildirge<T>(DataSourceRequest req)
        {
            var sql = $@"
                SELECT B.*
                FROM BILDIRGE_ICERIKLERI B
                INNER JOIN FIRMA_BAGLANTILARI F ON F.FIRMA_ID = B.FIRMA_ID
                WHERE 1 = 1
                    AND F.USER_ID = {_vctx.UserId}
            ";

            var result = _ctx.Database.SqlQuery<T>(sql).ToDataSourceResult(req, x => x);

            return result;
        }

        public (byte[] file, string fileName) HizmetListesiPdfGor(int? id) 
        {
            var bildirge = new DataIslemler.Islemler.Beyanname.PdfIslem(_vctx.DataAdi).GetPdfDosya(id.Value);
            if (bildirge != null)
            {
                var unzipfile = ZipUtils.DecompressZlib(bildirge.ICERIK);
                return (unzipfile, bildirge.DOSYA_ADI);
            }

            return (new byte[] { }, "");
        }

        public (byte[] file, string fileName) TahakkukPdfGor(int? id)
        {
            var bildirge = new DataIslemler.Islemler.Beyanname.PdfIslem(_vctx.DataAdi).GetPdfDosya(id.Value);
            if (bildirge != null)
            {
                var unzipfile = ZipUtils.DecompressZlib(bildirge.ICERIK);
                return (unzipfile, bildirge.DOSYA_ADI);
            }

            return (new byte[] { }, "");
        }

        public (byte[] file, string fileName) BildirgeZipOlustur(List<BildirgeAraModel> bildirge_listesi, BildirgeBelgeModel belgeModel)
        {
            //var bildirgeler = GetBildirge<BILDIRGE_ICERIKLERI>(req).Data as List<BILDIRGE_ICERIKLERI>;

            var dosyaIslem = new DataIslemler.Islemler.Beyanname.DosyaIslem(_vctx.DataAdi);

            var zipfiles = new Dictionary<string, byte[]>();

            try
            {
                if (belgeModel.isKod == true)
                {
                    // var kodGroupList = (from m in beyannameListesi group m by m.KODU into gr select new { Kodu = gr.Key }).ToList();
                    var group_liste = (from m in bildirge_listesi group m by m.KANUN_NO into gr select new { Kodu = gr.Key }).ToList();

                    foreach (var item in group_liste)
                    {
                        var liste = bildirge_listesi.Where(x => x.KANUN_NO == item.Kodu).ToList();

                        foreach (var bildirge in liste)
                        {
                            var hizlistfile = dosyaIslem.GetDosya(bildirge.FILE_ID.Value);

                            var tahakfile = dosyaIslem.GetDosya(bildirge.TAH_FILE_ID.Value);

                            var donemstr = bildirge.DONEM.ToDateTime().Month.ToString().PadLeft(2, '0');

                            if (hizlistfile != null && hizlistfile.ICERIK.Length > 0 && belgeModel.isBildirge)
                            {
                                var filename = $"{item.Kodu}/{bildirge.UNVAN.SafeSubstring(20)}-{donemstr}-Bil.pdf";
                                if (!zipfiles.ContainsKey(filename))
                                    zipfiles.Add(filename, ZipUtils.DecompressZlib(hizlistfile.ICERIK));
                            }

                            if (hizlistfile != null && hizlistfile.ICERIK.Length > 0 && belgeModel.isTahakkuh)
                            {
                                var filename = $"06111/{bildirge.UNVAN.SafeSubstring(20)}-{donemstr}-Tah.pdf";
                                if (!zipfiles.ContainsKey(filename))
                                    zipfiles.Add(filename, ZipUtils.DecompressZlib(tahakfile.ICERIK));
                            }
                        }
                    }
                }


                if (belgeModel.isKod == false)
                {
                    foreach (var bildirge in bildirge_listesi)
                    {

                        var hizlistfile = dosyaIslem.GetDosya(bildirge.FILE_ID.Value);

                        var tahakfile = dosyaIslem.GetDosya(bildirge.TAH_FILE_ID.Value);

                        var donemstr = bildirge.DONEM.ToDateTime().Month.ToString().PadLeft(2, '0');

                        if (hizlistfile != null && hizlistfile.ICERIK.Length > 0 && belgeModel.isBildirge)
                        {
                            
                            var filename = bildirge.UNVAN.SafeSubstring(25) + "-" + bildirge.KANUN_NO + "-" + bildirge.SICIL_NO + "-" + bildirge.OID + "-HizmetListesi.pdf";

                            if (!zipfiles.ContainsKey(filename))
                                zipfiles.Add(filename, ZipUtils.DecompressZlib(hizlistfile.ICERIK));
                        }

                        if (tahakfile != null && tahakfile.ICERIK.Length > 0 && belgeModel.isTahakkuh)
                        {
                            
                            var filename = bildirge.UNVAN.SafeSubstring(25) + "-" + bildirge.KANUN_NO + "-" + bildirge.SICIL_NO + "-" + bildirge.TAH_OID + "-Tahakkuk.pdf";

                            if (!zipfiles.ContainsKey(filename))
                                zipfiles.Add(filename, ZipUtils.DecompressZlib(tahakfile.ICERIK));
                        }
                    }
                }
            }
            catch
            {

                throw;
            }

            var zip = ZipUtils.CreateZipFolder(zipfiles);
            var fName = $"File-{Guid.NewGuid()}.zip";

            return (zip, fName);
        }

        public (byte[] file, string fileName) BildirgeTopluPdfOlustur(DataSourceRequest req, string pdftype)
        {
            var bildirgeler = GetBildirge<BILDIRGE_ICERIKLERI>(req).Data as List<BILDIRGE_ICERIKLERI>;
            var files = new List<byte[]>();

            foreach (var bildirge in bildirgeler)
            {
                if (pdftype == "hizmetlistesi")
                {
                    var hizlistFile = _dosyaService.Get(x => x.ID == bildirge.FILE_ID);

                    if (hizlistFile != null && hizlistFile.ICERIK.Length > 0)
                        files.Add(ZipUtils.DecompressZlib(hizlistFile.ICERIK));
                }
                else
                {
                    var tahakfile = _dosyaService.Get(x => x.ID == bildirge.TAH_FILE_ID);

                    if (tahakfile != null && tahakfile.ICERIK.Length > 0)
                        files.Add(ZipUtils.DecompressZlib(tahakfile.ICERIK));
                }
            }

            var pdf = PdfUtils.MergePdfs(files);
            var fName = $"File-{Guid.NewGuid()}.pdf";

            return (pdf, fName);
        }

        public List<FirmaModel> GonderimIcerikOlustur(List<BildirgeAraModel> clientList, string gonderimTur)
        {

            var islem = new DataIslemler.Whatsapp.BildirgeIslem(_vctx.DataAdi);

            return islem.GonderimIcerikOlustur(clientList, gonderimTur);

        }

        public List<BildirgeAraModel> GonderimDataKayit(List<FirmaModel> clientList, List<BildirgeAraModel> bildirgeListesi)
        {

            var islem = new DataIslemler.Whatsapp.BildirgeIslem(_vctx.DataAdi);

            return islem.GonderimDataKayit(clientList,bildirgeListesi); 
        }

        public KayitResModel ViziteKayitIslem(List<ViziteResponseModel> clientList)
        {
            var result = dataIslem.ViziteKayitIslem(clientList);

            return result;
        } 

        public List<ViziteListeModel> ViziteListesi(bool? durum,DateTime? ilkTarih,DateTime? sonTarih)
        {
            var result = listeIslem.ViziteListesi(durum,ilkTarih,sonTarih);

            return result;
        }

        public List<ViziteListeModel> SorguViziteListe(DateTime? tarih, string vaka)
        {
            var result = listeIslem.SorguViziteListe(tarih, vaka);

            return result;
        }

        public List<ViziteFirmaSubeModel> FirmaSubeListesi()
        {
            var result = listeIslem.FirmaSubeListesi();

            return result;
        }

        public List<TarihListeModel> IseGirisTarihAralikListesi(DateTime? _ilkTarih, DateTime? _sonTarih)
        {

            var liste = new List<TarihListeModel>();

            var ilkTarih = _ilkTarih.Value;
            var sonTarih = _sonTarih.Value;

            while (ilkTarih < sonTarih)
            {
                var model = new TarihListeModel();
                model.BaslangicTarih = ilkTarih.ToString("dd.MM.yyyy");
                var tarih = ilkTarih.AddDays(14);
                if (tarih > sonTarih)
                {
                    model.BitisTarih = sonTarih.ToString("dd.MM.yyyy");
                    liste.Add(model);
                    break;
                }
                if (tarih < sonTarih)
                {
                    model.BitisTarih = tarih.ToString("dd.MM.yyyy.");
                    liste.Add(model);
                    ilkTarih = tarih;
                }
                else
                {
                    model.BitisTarih = sonTarih.ToString("dd.MM.yyyy");
                    liste.Add(model);
                    break;
                }

                ilkTarih = ilkTarih.AddDays(1);
            }

            return liste;
        }

        public KayitResModel IseGirisCikisKaydet(List<IseGirisResModel> clientList)
        {
            var result = dataIslem.IseGirisCikisKaydet(clientList);

            return result;
        }

        public List<IseGirisListeModel> IseGirisCikisListesi(DateTime? ilkTarih, DateTime? sonTarih)
        {
            var result = listeIslem.IseGirisCikisListesi(ilkTarih, sonTarih);

            return result; 
        }

        public List<IseGirisListeModel> SorguListe(DateTime? tarih, string durum)
        {
            var result = listeIslem.SorguListe(tarih, durum);

            return result;
        }

        public Tacmin.Model.DosyaModel GetPdfEvrak(int id)
        {
            var result = listeIslem.GetPdfEvrak(id);

            return result;
        }
    }
}
