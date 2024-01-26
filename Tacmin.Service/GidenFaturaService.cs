using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Core.Extensions;
using Tacmin.Core.Interface;
using Tacmin.Core.Utilities;
using Tacmin.Model.EArsiv;
using Tacmin.Model.Mukellef;

namespace Tacmin.Service
{
    public class GidenFaturaService
    {
        private readonly IVirtualContext vctx;
        private DataIslemler.GidenFatura.DataIslem dataIslem;
        private DataIslemler.GidenFatura.BelgeIslem belgeIslem;
        private object beyanname;
        private DataIslemler.GidenFatura.RaporIslem raporIslem;

        public GidenFaturaService(IVirtualContext _vctx)
        {

            vctx = _vctx;
            dataIslem = new DataIslemler.GidenFatura.DataIslem(vctx.DataAdi);
            belgeIslem = new DataIslemler.GidenFatura.BelgeIslem(vctx.DataAdi);
            raporIslem = new DataIslemler.GidenFatura.RaporIslem(vctx.DataAdi);
        }

        public List<ArsivFaturaModel> FaturaKayitIslem(List<ArsivFaturaModel> clientFaturaListesi)
        {

            var result = dataIslem.FaturaKayitIslem(clientFaturaListesi);

            return result;
        }
         
        public List<ArsivFaturaModel> GetFaturaListesi(List<FirmaGibModel> firmaListesi,DateTime? ilkTarih,DateTime? sonTarih)
        {
            var islem = new DataIslemler.GidenFatura.ListeIslem(vctx.DataAdi, firmaListesi, ilkTarih, sonTarih);

            return islem.GetFaturaListesi;
        }

        public List<ArsivFaturaModel> SorguFaturaListesi(DateTime? tarih)
        {
            var result = raporIslem.SorguFaturaListesi(tarih);

            return result;
        }

        public (byte[] file, string fileName) HtmlDosyaAc(int? id)
        {
            var belge = belgeIslem.GetBelge("html", id);
            if (belge != null)
            {
                var byteFile = belge.Icerik.ToArray();
                return (byteFile, belge.DosyaAdi);
            }


            return (new byte[] { }, "");
        }

        public (byte[] file, string fileName) PdfDosyaAc(int? id)
        {
            var belge = belgeIslem.GetBelge("pdf", id);
            if (belge != null)
            {
                var byteFile = belge.Icerik.ToArray(); 
                return (byteFile, belge.DosyaAdi);
            }


            return (new byte[] { }, "");
        }

        public (byte[] file, string fileName) FaturaZipDosyaOlustur(List<ArsivFaturaModel> faturaListesi,string tur)
        {

            var zipfiles = new Dictionary<string, byte[]>();

            foreach (var item in faturaListesi)
            {
                var belgeFile = belgeIslem.GetBelge(tur, item.Id);

                var fileName = $"{item.Unvan.SafeSubstring(20)}/{item.belgeNo}-{item.aliciUnvan.SafeSubstring(20)}.{tur}";
                if (!zipfiles.ContainsKey(fileName))
                    zipfiles.Add(fileName, belgeFile.Icerik);
            }

            var zip = ZipUtils.CreateZipFolder(zipfiles);

            var fName = $"File-{Guid.NewGuid()}.zip";

            return (zip, fName);
        }

        public List<Tacmin.Model.Gib.AyModel> GetAyListesi(int? yil)
        {

            return null;
        }





    }
}
