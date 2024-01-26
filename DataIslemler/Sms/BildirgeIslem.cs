using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model;
using Tacmin.Model.Whatsapp;

namespace DataIslemler.Sms
{
    public class BildirgeIslem
    {
        private Musavir_DbDataContext data;
        private List<BILDIRGE_ICERIKLERI> data_bildirge_listesi;

        public BildirgeIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            data_bildirge_listesi = data.BILDIRGE_ICERIKLERIs.ToList();
        }

        public List<FirmaModel> GonderimIcerikOlustur(List<BildirgeAraModel> clientList, string gonderimTur)
        {
            var cari_list = (from m in clientList group m by m.FIRMA_ID into gr select new { cariId = gr.Key }).ToList();

            var liste = new List<FirmaModel>();

            foreach (var cari in cari_list)
            {
                var icerik_list = clientList.Where(x => x.FIRMA_ID == cari.cariId).ToList();

                var firma_model = new FirmaModel();

                firma_model.FirmaUnvan = icerik_list[0].UNVAN;
                firma_model.Id = cari.cariId.Value;


                firma_model.KisiListesi = new List<KisiModel>();
                foreach (var item in data.Firma_Iletisims)
                {
                    var kisi_model = new KisiModel();

                    kisi_model.Id = item.Id;
                    kisi_model.Adi = item.Adi;
                    kisi_model.Mail = item.Mail;
                    kisi_model.Telefon = item.Telefon;

                    kisi_model.BelgeListesi = new List<BelgeModel>();

                    foreach (var evrak in icerik_list)
                    {
                        if (gonderimTur == "bildirge")
                        {
                            kisi_model.BelgeListesi.Add(GetBildirge(evrak));
                        }

                        if (gonderimTur == "tahakkuk")
                        {
                            if (!string.IsNullOrEmpty(evrak.TAH_OID))
                            {
                                kisi_model.BelgeListesi.Add(GetTahakkuk(evrak));
                            }
                        }

                        if (gonderimTur == "hepsi")
                        {
                            if (!string.IsNullOrEmpty(evrak.TAH_OID))
                            {
                                kisi_model.BelgeListesi.Add(GetTahakkuk(evrak));
                            }

                            kisi_model.BelgeListesi.Add(GetBildirge(evrak));
                        }
                    }

                    firma_model.KisiListesi.Add(kisi_model);

                }



                liste.Add(firma_model);

            }

            return liste;
        }

        private BelgeModel GetBildirge(BildirgeAraModel belge)
        {
            var model = new BelgeModel();
            model.BelgeNo = belge.OID;
            model.DosyaAdi = belge.OID + ".pdf";
            model.DosyaTuru = "beyanname";
            model.EvrakTur = EvrakTurEnum.BILDIRGE;
            model.GonderimDurum = false;
            model.Id = belge.ID;
            var donem = GetBildirgeDonem(belge.ID);
            model.EvrakLink = $"https://test.emusavirim.com/bildirge/hizmetlistesipdfgor?id={belge.ID}";

            model.Aciklama = $"{donem} dönemine ait  bildirgeniz.";
            return model;
        }

        private BelgeModel GetTahakkuk(BildirgeAraModel belge)
        {
            var model = new BelgeModel();
            model.BelgeNo = belge.TAH_OID;
            model.DosyaAdi = belge.TAH_OID + ".pdf";
            model.DosyaTuru = "tahakkuk";
            model.EvrakTur = EvrakTurEnum.BILDIRGE_TAHAKKUK;
            model.GonderimDurum = false;
            model.Id = belge.ID;
            model.FisNo = "Sicil No: " + belge.SICIL_NO;
            var tarih = getSonOdemeTarihi(belge.ID);
            model.EvrakLink = $"https://test.emusavirim.com/bildirge/tahakkukpdfgor?id={belge.ID}";


            if (belge.TUTAR == null)
            {
                belge.TUTAR = 0;
            }

            model.Aciklama = $"Son Ödeme Tarihi: *{tarih}* Olan  Ödemeniz Toplam: *{string.Format("{0:n2}", belge.TUTAR)}* TL.dir";
            return model;
        }

        private string GetBildirgeDonem(int id)
        {

            var donem = "";

            try
            {
                var model = data_bildirge_listesi.Where(x => x.ID == id).SingleOrDefault();

                donem = model.DONEM.Value.ToString("MM-yyyy");
            }
            catch
            {

                donem = "";
            }

            return donem;
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

    }
}
