using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model;
using Tacmin.Model.Whatsapp;

namespace DataIslemler.Whatsapp
{
    public class RaporIslem
    {
        private Musavir_DbDataContext data;

        private List<FIRMA_TANIMLARI> data_firma_listesi;
        private List<Whats_App_Gonderileri> data_whats_app_gonderim_listesi;
        private List<BEYANNAME_ICERIKLERI> data_beyanname_listesi;
        private List<BILDIRGE_ICERIKLERI> data_bildirge_listesi;
        private List<Firma_Iletisim> data_firma_iletisim_listesi;

        public RaporIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            data_firma_listesi = data.FIRMA_TANIMLARIs.ToList();
            data_whats_app_gonderim_listesi = data.Whats_App_Gonderileris.ToList();
            data_beyanname_listesi = data.BEYANNAME_ICERIKLERIs.ToList();
            data_bildirge_listesi = data.BILDIRGE_ICERIKLERIs.ToList();

            data_firma_iletisim_listesi = data.Firma_Iletisims.ToList();
        }

        public List<GonderimBilgi> GonderimRaporu()
        {
            var liste = new List<GonderimBilgi>();

            foreach (var item in data_whats_app_gonderim_listesi)
            {
                var model = new GonderimBilgi();
                model.Id = item.Id;
                model.Unvan = GetUnvan(item.FirmaId);
                model.GonderimZamani = item.TarihZaman;
                var kisi = GetKisiAdi(item.KisiId);
                if(item.ModulId == (int)ModulEnums.BEYANNAME)
                {
                    model.Modul = "E-Beyanname";
                    model.GonderimIcerik = kisi + " " + GetBeyannameAciklama(item.EvrakId, item.EvrakTur.Value);
                }
                if (item.ModulId == (int)ModulEnums.BILDIRGE)
                {
                    model.Modul = "Bildirge";
                    model.GonderimIcerik = kisi + " " + GetBildirgeAciklama(item.EvrakId, item.EvrakTur.Value);
                }

                liste.Add(model);
            }

            return liste;
        }

        public List<GonderimBilgi> MailGonderimRaporu()
        {
            var liste = new List<GonderimBilgi>();

            foreach (var item in data.Mail_Gonderileris)
            {
                var model = new GonderimBilgi();
                model.Id = item.Id;
                model.Unvan = GetUnvan(item.FirmaId);
                model.GonderimZamani = item.TarihZaman;
                var kisi = GetKisiAdi(item.KisiId);
                if (item.ModulId == (int)ModulEnums.BEYANNAME)
                {
                    model.Modul = "E-Beyanname";
                    model.GonderimIcerik = kisi + " " + GetBeyannameAciklama(item.EvrakId, item.EvrakTur.Value);
                }
                if (item.ModulId == (int)ModulEnums.BILDIRGE)
                {
                    model.Modul = "Bildirge";
                    model.GonderimIcerik = kisi + " " + GetBildirgeAciklama(item.EvrakId, item.EvrakTur.Value);
                }

                liste.Add(model);
            }

            return liste;
        }

        public List<GonderimBilgi> SmsGonderimRaporu()
        {
            var liste = new List<GonderimBilgi>();

            foreach (var item in data.Sms_Gonderileris)
            {
                var model = new GonderimBilgi();
                model.Id = item.Id;
                model.Unvan = GetUnvan(item.FirmaId);
                model.GonderimZamani = item.TarihZaman;
                var kisi = GetKisiAdi(item.KisiId);
                if (item.ModulId == (int)ModulEnums.BEYANNAME)
                {
                    model.Modul = "E-Beyanname";
                    model.GonderimIcerik = kisi + " " + GetBeyannameAciklama(item.EvrakId, item.EvrakTur.Value);
                }
                if (item.ModulId == (int)ModulEnums.BILDIRGE)
                {
                    model.Modul = "Bildirge";
                    model.GonderimIcerik = kisi + " " + GetBildirgeAciklama(item.EvrakId, item.EvrakTur.Value);
                }

                liste.Add(model);
            }

            return liste;
        }

        public List<GonderimBilgi> GonderimRaporuTarih(DateTime? ilkTarih,DateTime? sonTarih)
        {
            if (ilkTarih == null)
            {
                ilkTarih = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }

            if (sonTarih == null)
            {
                sonTarih = DateTime.Now;
            }

            var liste = new List<GonderimBilgi>();

            foreach (var item in data.Whatapp_Gonderim_Listesi_Tarih(ilkTarih,sonTarih))
            {
                var model = new GonderimBilgi();
                model.Id = item.Id;
                model.Unvan = GetUnvan(item.FirmaId);
                model.GonderimZamani = item.TarihZaman;
                var kisi = GetKisiAdi(item.KisiId);
                if (item.ModulId == (int)ModulEnums.BEYANNAME)
                {
                    model.Modul = "E-Beyanname";
                    model.GonderimIcerik = kisi + " " + GetBeyannameAciklama(item.EvrakId, item.EvrakTur.Value);
                }
                if (item.ModulId == (int)ModulEnums.BILDIRGE)
                {
                    model.Modul = "Bildirge";
                    model.GonderimIcerik = kisi + " " + GetBildirgeAciklama(item.EvrakId, item.EvrakTur.Value);
                }

                liste.Add(model);
            }

            return liste;
        }

        public List<GonderimBilgi> MailGonderimRaporuTarih(DateTime? ilkTarih, DateTime? sonTarih)
        {
            if (ilkTarih == null)
            {
                ilkTarih = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }

            if (sonTarih == null)
            {
                sonTarih = DateTime.Now;
            }

            var liste = new List<GonderimBilgi>();

            foreach (var item in data.Mail_Gonderim_Listesi_Tarih(ilkTarih, sonTarih))
            {
                var model = new GonderimBilgi();
                model.Id = item.Id;
                model.Unvan = GetUnvan(item.FirmaId);
                model.GonderimZamani = item.TarihZaman;
                var kisi = GetKisiAdi(item.KisiId);
                if (item.ModulId == (int)ModulEnums.BEYANNAME)
                {
                    model.Modul = "E-Beyanname";
                    model.GonderimIcerik = kisi + " " + GetBeyannameAciklama(item.EvrakId, item.EvrakTur.Value);
                }
                if (item.ModulId == (int)ModulEnums.BILDIRGE)
                {
                    model.Modul = "Bildirge";
                    model.GonderimIcerik = kisi + " " + GetBildirgeAciklama(item.EvrakId, item.EvrakTur.Value);
                }

                liste.Add(model);
            }

            return liste;
        }

        public List<GonderimBilgi> SmsGonderimRaporuTarih(DateTime? ilkTarih, DateTime? sonTarih)
        {
            if (ilkTarih == null)
            {
                ilkTarih = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            }

            if (sonTarih == null)
            {
                sonTarih = DateTime.Now;
            }

            var liste = new List<GonderimBilgi>();

            foreach (var item in data.Sms_Gonderim_Listesi_Tarih(ilkTarih, sonTarih))
            {
                var model = new GonderimBilgi();
                model.Id = item.Id;
                model.Unvan = GetUnvan(item.FirmaId);
                model.GonderimZamani = item.TarihZaman;
                var kisi = GetKisiAdi(item.KisiId);
                if (item.ModulId == (int)ModulEnums.BEYANNAME)
                {
                    model.Modul = "E-Beyanname";
                    model.GonderimIcerik = kisi + " " + GetBeyannameAciklama(item.EvrakId, item.EvrakTur.Value);
                }
                if (item.ModulId == (int)ModulEnums.BILDIRGE)
                {
                    model.Modul = "Bildirge";
                    model.GonderimIcerik = kisi + " " + GetBildirgeAciklama(item.EvrakId, item.EvrakTur.Value);
                }

                liste.Add(model);
            }

            return liste;
        }

        private string GetBeyannameAciklama(int? evrakId,int evrakTur)
        {
            var model = data_beyanname_listesi.Where(x => x.ID == evrakId).SingleOrDefault();

            if (model == null)
                return "";

            if (evrakTur == (int)EvrakTurEnum.BEYANNAME)
            {
                var donem = $"{model.DONEM_BAS.Value.ToString("MM-yyyy")}-{model.DONEM_SON.Value.ToString("MM-yyyy")}";
                return $"{donem} dönemine ait *{model.TUR.Split('_')[0]}* beyannameniz.";
            }
            if (evrakTur == (int)EvrakTurEnum.BEYANNAME_TAHAKKUH)
            {
                var tarih = "";
                if (model.TAH_VADE!=null)
                {
                    tarih = model.TAH_VADE.Value.ToString("dd-MM-yyyy");
                }
                return $"Son Ödeme Tarihi: {tarih} Olan {model.TUR.Split('_')[0]} Ödemeniz Toplam: {string.Format("{0:n2}", model.TAH_TUTAR)} TL.dir";
            }

            return "";
        }

        private string GetBildirgeAciklama(int? evrakId,int evrakTur)
        {
            var model = data_bildirge_listesi.Where(x => x.ID == evrakId).SingleOrDefault();

            if (model == null)
                return "";

            if (evrakTur == (int)EvrakTurEnum.BILDIRGE)
            {
                var donem = model.DONEM.Value.ToString("MM-yyyy");

                return $"{donem} dönemine ait  bildirgeniz.";
            }
            if (evrakTur == (int)EvrakTurEnum.BILDIRGE_TAHAKKUK)
            {
                var sonOdemeTarihi = getSonOdemeTarihi(model.ID);
                return $"Son Ödeme Tarihi: {sonOdemeTarihi} Olan  Ödemeniz Toplam: {string.Format("{0:n2}", model.TUTAR)} TL.dir";
            }

            return "";
        }


        private string getSonOdemeTarihi(int id)
        {

            var odeme = "";

            try
            {
                var model = data_bildirge_listesi.Where(x => x.ID == id).SingleOrDefault();

                var donem = model.DONEM;

                var donem_bas = new DateTime(donem.Value.Year, donem.Value.Month, 1);

                donem_bas = donem_bas.AddMonths(2).AddDays(-1);

                odeme = donem_bas.ToString("dd-MM-yyyy");
            }
            catch
            {

                odeme = "";
            }

            return odeme;
        }

        private string GetUnvan(int? firmaId)
        {
            var unvan = "";

            var model = data_firma_listesi.Where(x => x.ID == firmaId).SingleOrDefault();

            if (model != null)
            {
                unvan = model.UNVAN;

            }

            return unvan;
        }

        private string GetBeyannameBelgeTur(int? evrakId, int evrakTur)
        {
            var model = data_beyanname_listesi.Where(x => x.ID == evrakId).SingleOrDefault();

            if (model != null)
            {
                var tur = model.TUR.Split('_')[0];

                if (evrakTur ==(int) EvrakTurEnum.BEYANNAME)
                    tur = tur + " Beyanname";
                if (evrakTur == (int)EvrakTurEnum.BEYANNAME_TAHAKKUH)
                    tur = tur + " Tahakkuk";

                return tur;
            }
            return "";
        }

        private string GetBildirgeTur(int? evrakId,int evrakTur)
        {
            var model = data_bildirge_listesi.Where(x => x.ID == evrakId).SingleOrDefault();

            if (model != null)
            {
                var tur = "";

                if (evrakTur == (int)EvrakTurEnum.BILDIRGE)
                    tur = "Sgk Hizmet Listesi";
                if (evrakTur == (int)EvrakTurEnum.BILDIRGE_TAHAKKUK)
                    tur = "Sgk Ödeme Tahakkuk";

                return tur;
            }

            return "";
        }

        private string GetBeyannameEvrakAdi(int? evrakId,int evrakTur)
        {
            var model = data_beyanname_listesi.Where(x => x.ID == evrakId).SingleOrDefault();

            if (model!=null)
            {
                if (evrakTur == (int) EvrakTurEnum.BEYANNAME)
                {
                    return $"{model.DONEM_BAS.Value.ToString("MM-yyyy")}-{model.DONEM_SON.Value.ToString("MM-yyyy")} {model.TUR.Split('_')[0]} Beyannamesi";
                }
                if (evrakTur == (int)EvrakTurEnum.BEYANNAME_TAHAKKUH)
                {
                    return $"{model.TAH_FIS_NO} Fiş Nolu Tahakkuk";
                }
            }

            return "";
        }

        private string GetBildirgeEvrakAdi(int evrakTur)
        {
            if (evrakTur == (int)EvrakTurEnum.BILDIRGE)
                return "Hizmet Listesi";
            if (evrakTur == (int)EvrakTurEnum.BILDIRGE_TAHAKKUK)
                return "Sgk Ödeme Tahakkuk";

            return "";
        }

        private decimal? GetBeyannameTutar(int? evrakId,int evrakTur)
        {
            decimal? tutar = null;
            if (evrakTur == (int)EvrakTurEnum.BEYANNAME)
                return tutar;

            var model = data_beyanname_listesi.Where(x => x.ID == evrakId).SingleOrDefault();

            if (model!=null)
            {
                tutar = model.TAH_TUTAR;
            }

            return tutar;
        }

        private decimal? GetBildirgeTutar(int? evrakId,int evrakTur)
        {
            decimal? tutar = null;

            if (evrakTur == (int)EvrakTurEnum.BILDIRGE)
                return tutar;

            var model = data_bildirge_listesi.Where(x => x.ID == evrakId).SingleOrDefault();

            if (model!=null)
            {
                tutar = model.TUTAR;
            }

            return tutar;
        }

        private string GetBeyannameDonem(int? evrakId)
        {
            var model = data_beyanname_listesi.Where(x => x.ID == evrakId).SingleOrDefault();

            if (model != null)
            {
                return $"{model.DONEM_BAS.Value.ToString("MM-yyyy")}-{model.DONEM_SON.Value.ToString("MM-yyyy")}";
            }

            return "";
        }

        private string GetBildirgeDonem(int? evrakId)
        {
            var model = data_bildirge_listesi.Where(x => x.ID == evrakId).SingleOrDefault();

            if (model!=null)
            {
                return model.DONEM.Value.ToString("MM-yyyy");
            }

            return "";
        }

        private string GetKisiAdi(int? kisiId)
        {
            var icerik = "";

            var model = data_firma_iletisim_listesi.Where(x => x.Id == kisiId).SingleOrDefault();

            if (model != null)
            {
                icerik = "Sayın " + model.Adi;
            }

            return icerik;
        }
    }
}
