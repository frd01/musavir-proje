using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Model;
using Tacmin.Model.Whatsapp;
using DataIslemler.Data;
using DataIslemler.Helpers;

namespace DataIslemler.Sms
{
    public class BeyannameIslem
    {
        private Musavir_DbDataContext data;
        private List<BEYANNAME_ICERIKLERI> data_beyanname_listesi;

        public BeyannameIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            data_beyanname_listesi = data.BEYANNAME_ICERIKLERIs.ToList();
        }
        public List<FirmaModel> GonderimIcerikOlustur(List<BeyannameAraModel> clientList, string gonderimTur)
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
                var firma_iletisim_listesi = data.Firma_Iletisims.Where(x => x.Firma_Id == cari.cariId).ToList();
                foreach (var item in firma_iletisim_listesi)
                {
                    var kisi_model = new KisiModel();

                    kisi_model.Id = item.Id;
                    kisi_model.Adi = item.Adi;
                    kisi_model.Mail = item.Mail;
                    kisi_model.Telefon = item.Telefon;

                    kisi_model.BelgeListesi = new List<BelgeModel>();

                    foreach (var evrak in icerik_list)
                    {
                        if (gonderimTur == "beyanname")
                        {
                            kisi_model.BelgeListesi.Add(GetBeyanname(evrak));
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

                            kisi_model.BelgeListesi.Add(GetBeyanname(evrak));
                        }
                    }

                    firma_model.KisiListesi.Add(kisi_model);

                }



                liste.Add(firma_model);

            }

            return liste;
        }

        private BelgeModel GetBeyanname(BeyannameAraModel belge)
        {
            var model = new BelgeModel();
            model.BelgeNo = belge.OID;
            model.DosyaAdi = belge.OID + ".pdf";
            model.DosyaTuru = "beyanname";
            model.EvrakTur = EvrakTurEnum.BEYANNAME;
            model.GonderimDurum = false;
            model.Id = belge.ID;
            model.EvrakLink = $"https://test.emusavirim.com/beyanname/beyannamepdfgor?id={belge.ID}";

            var belge_tur = "";
            var donem = getBeyannameDonem(belge.ID);

            if (!string.IsNullOrEmpty(belge.TUR))
            {
                belge_tur = belge.TUR.Split('_')[0];
            }
            model.Aciklama = $"{donem} dönemine ait {belge_tur} beyannameniz.";
            return model;
        }

        private BelgeModel GetTahakkuk(BeyannameAraModel belge)
        {
            var model = new BelgeModel();
            model.BelgeNo = belge.TAH_OID;
            model.DosyaAdi = belge.TAH_OID + ".pdf";
            model.DosyaTuru = "tahakkuk";
            model.EvrakTur = EvrakTurEnum.BEYANNAME_TAHAKKUH;
            model.GonderimDurum = false;
            model.Id = belge.ID;
            model.FisNo = "Tah.Fiş No: " + belge.TAH_FIS_NO;
            model.EvrakLink = $"https://test.emusavirim.com/beyanname/tahakkukpdfgor?id={belge.ID}";

            var tarih = "";
            var belge_tur = "";
            if (!string.IsNullOrEmpty(belge.TUR))
            {
                belge_tur = belge.TUR.Split('_')[0];
            }
            if (belge.TAH_TUTAR == null)
            {
                belge.TAH_TUTAR = 0;
            }
            if (belge.TAH_VADE != null)
            {
                tarih = belge.TAH_VADE.Value.ToString("dd-MM-yyyy");
            }
            model.Aciklama = $"Son Ödeme Tarihi: {tarih} Olan {belge_tur} Ödemeniz Toplam: {string.Format("{0:n2}", belge.TAH_TUTAR)} TL.dir";
            return model;
        }

        private string getBeyannameDonem(int? id)
        {
            var donem = "";

            try
            {
                var model = data_beyanname_listesi.Where(x => x.ID == id).SingleOrDefault();

                donem = $"{model.DONEM_BAS.Value.ToString("MM-yyyy")}-{model.DONEM_SON.Value.ToString("MM-yyyy")}";
            }
            catch
            {

                donem = "";
            }

            return donem;
        }
    }
}
