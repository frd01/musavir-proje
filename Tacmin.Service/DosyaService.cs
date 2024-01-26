using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Core.Interface;


namespace Tacmin.Service
{
    public class DosyaService
    {
        private readonly IVirtualContext vctx;

        public DosyaService(IVirtualContext _vctx)
        {
            vctx = _vctx;
        }

        public (byte[] file, string fileName) KurulumDosyasiIndir()
        {

            var dosya_path = @"C:\masaustu-servis-programlar\tacminsoft.rar";

            var file = File.ReadAllBytes(dosya_path);

            return (file, "kurulum.rar");
        }

        public (byte[] file, string fileName) MukellefBilgiGuncellemeExcelIndir()
        {
            var kaynak_path = @"C:\Sablonlar\Mükellef Bilgi Yükleme Kartı.xls";

            var dosya_adi = vctx.AdSoyad + ".xls";

            var hedef_path = @"C:\Sablonlar\" + dosya_adi;

            File.Copy(kaynak_path, hedef_path,true);

            var file = File.ReadAllBytes(hedef_path);

            return (file, dosya_adi);
        }

        private void MukellefExcelDosyaBilgiYaz()
        {
            var dosya_adi = vctx.AdSoyad + ".xls";

            var hedef_path = @"C:\Sablonlar\" + dosya_adi;
       

            var firma_listesi = new DataIslemler.LocalData.ListeIslem(vctx.DataAdi).LocalListeler().FirmaListesi;

            var index = 1;

            foreach (var item in firma_listesi)
            {
                
            }
        }
    }
}
