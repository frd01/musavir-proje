using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tacmin.Model;
using Tacmin.Model.Whatsapp;
using DataIslemler.Helpers;
using DataIslemler.Data;

namespace DataIslemler.Whatsapp
{
    public class BeyannameIslem
    {
        private BelgeIslem belgeIslem;
        private DataIslemler.Islemler.Beyanname.DataIslem beyannameDataIslem;
        private DataIslemler.Data.Musavir_DbDataContext data;
        private List<BEYANNAME_ICERIKLERI> data_beyanname_listesi;

        public BeyannameIslem(string dataAdi)
        {

            belgeIslem = new BelgeIslem();

            beyannameDataIslem = new Islemler.Beyanname.DataIslem(dataAdi);

            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Data.Musavir_DbDataContext(con_str);

            data_beyanname_listesi = data.BEYANNAME_ICERIKLERIs.ToList();
        }
        public List<FirmaModel> GonderimIcerikOlustur(List<BeyannameAraModel> clientList,string gonderimTur)
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

        public List<BeyannameAraModel> GonderimDataKayit(List<FirmaModel> clientList,List<BeyannameAraModel> beyannameListesi)
        {
            var liste = new List<BeyannameAraModel>();

          
            foreach (var firma in clientList)
            {
                foreach (var kisi in firma.KisiListesi)
                {
                    foreach (var evrak in kisi.BelgeListesi)
                    {
                        if (evrak.GonderimDurum == true)
                        {
                            var data_model = new Whats_App_Gonderileri();

                            data_model.FirmaId = firma.Id;
                            data_model.KisiId = kisi.Id;
                            data_model.Tarih = DateTime.Now;
                            data_model.TarihZaman = data_model.Tarih;
                            data_model.EvrakTur = (int)evrak.EvrakTur;
                            data_model.EvrakId = evrak.Id;
                            data_model.ModulId = (int)ModulEnums.BEYANNAME;

                            data.Whats_App_Gonderileris.InsertOnSubmit(data_model);
                            data.SubmitChanges();

                            var liste_model = beyannameListesi.Where(x => x.ID == data_model.EvrakId).SingleOrDefault();

                            liste_model.Whats_App_Gonderim_Baslik = data_model.TarihZaman.Value.ToString("dd.MM.yyyy HH:mm");

                            liste.Add(liste_model);
                        }
                    }

                   
                }
            }

            return liste;
        }

        public void MailGonderimDataKayit(List<FirmaModel> clientList)
        {

            foreach (var firma in clientList)
            {
                foreach (var kisi in firma.KisiListesi)
                {
                    foreach (var evrak in kisi.BelgeListesi)
                    {
                        if (evrak.GonderimDurum == true)
                        {
                            var data_model = new Whats_App_Gonderileri();

                            data_model.FirmaId = firma.Id;
                            data_model.KisiId = kisi.Id;
                            data_model.Tarih = DateTime.Now;
                            data_model.TarihZaman = data_model.Tarih;
                            data_model.EvrakTur = (int)evrak.EvrakTur;
                            data_model.EvrakId = evrak.Id;
                            data_model.ModulId = (int)ModulEnums.BEYANNAME;

                            data.Whats_App_Gonderileris.InsertOnSubmit(data_model);
                            data.SubmitChanges();
                        }
                    }
                }
            }
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
            var pdf_file = Tacmin.Core.Utilities.ZipUtils.DecompressZlib(beyannameDataIslem.GetBeyanname(belge.ID).ICERIK);
            model.Base64Str = Convert.ToBase64String(pdf_file);
            
            var belge_tur = "";
            var donem = getBeyannameDonem(belge.ID);

            if (!string.IsNullOrEmpty(belge.TUR))
            {
                belge_tur = belge.TUR.Split('_')[0];
            }
            model.Aciklama = $"{donem} dönemine ait *{belge_tur}* beyannameniz.";
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
            var pdf_file = Tacmin.Core.Utilities.ZipUtils.DecompressZlib(beyannameDataIslem.GetTahakkuh(belge.ID).ICERIK);
            model.Base64Str = Convert.ToBase64String(pdf_file);
            model.FisNo = "Tah.Fiş No: " + belge.TAH_FIS_NO;
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
            model.Aciklama = $"Son Ödeme Tarihi: *{tarih}* Olan *{belge_tur}* Ödemeniz Toplam: *{string.Format("{0:n2}",belge.TAH_TUTAR)}* TL.dir";
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
