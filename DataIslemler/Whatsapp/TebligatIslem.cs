using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model;
using Tacmin.Model.Tebligat;
using Tacmin.Model.Whatsapp;

namespace DataIslemler.Whatsapp
{
    public class TebligatIslem
    {
        private Musavir_DbDataContext data;
        private List<FIRMA_TANIMLARI> data_firma_listesi;
        private DataIslemler.Tebligat.DosyaIslem dosyaIslem;

        public TebligatIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            data_firma_listesi = data.FIRMA_TANIMLARIs.ToList();

            dosyaIslem = new Tebligat.DosyaIslem(dataAdi);
        }

        public List<FirmaModel> GonderimIcerikOlustur(List<TebligatDataListModel> clientList,TebligatModulEnums modul,EvrakTurEnum evrakTur)
        {

            var liste = new List<FirmaModel>();

            var firmaList = (from m in clientList group m by m.FirmaId into gr select new { id = gr.Key }).ToList();

            foreach (var firma in firmaList)
            {
                var firma_bilgi = GetFirma(firma.id);

                var kisi_list = data.Firma_Iletisims.Where(x => x.Firma_Id == firma.id).ToList();

                var firma_model = new FirmaModel();

                firma_model.Id = firma.id.Value;
                firma_model.FirmaUnvan = firma_bilgi.UNVAN;
                firma_model.KisiListesi = new List<KisiModel>();

                var icerik_list = clientList.Where(x => x.FirmaId == firma.id).ToList();

                foreach (var kisi in kisi_list)
                {
                    var kisi_model = new KisiModel();

                    kisi_model.Adi = kisi.Adi;
                    kisi_model.Id = kisi.Id;
                    kisi_model.Mail = kisi.Mail;
                    kisi_model.Telefon = kisi.Telefon;
                    kisi_model.BelgeListesi = new List<BelgeModel>();
                   

                    foreach (var icerik in icerik_list)
                    {
                        var dosyalar = dosyaIslem.GetDosyalar(icerik.Id, "hepsi", modul);
                        kisi_model.Aciklama = $"{icerik.GondermeTarihi.Value.ToString("dd-MM-yyyy")} Tarihli *{icerik.BirimAdi}* tebligatınız.";
                        foreach (var dosya in dosyalar)
                        {
                            var belge_model = new BelgeModel();
                            belge_model.Aciklama = "";
                            belge_model.DosyaAdi = dosya.DosyaAdi;
                            belge_model.EvrakTur = evrakTur;
                            belge_model.GonderimDurum = false;
                            belge_model.Base64Str = Convert.ToBase64String(dosya.Icerik.ToArray());
                            belge_model.MimeType = dosya.MimeType;
                            belge_model.Id = icerik.Id;

                            kisi_model.BelgeListesi.Add(belge_model);
                        }
                    }

                    firma_model.KisiListesi.Add(kisi_model);

                }

                liste.Add(firma_model);
            }

            return liste;
        }

        public void GonderimDataKayit(List<FirmaModel> clientList,EvrakTurEnum evrakTur)
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
                            data_model.EvrakTur = (int)evrakTur;
                            data_model.EvrakId = evrak.Id;
                            data_model.ModulId = (int)ModulEnums.TEBLIGAT;

                            data.Whats_App_Gonderileris.InsertOnSubmit(data_model);
                            data.SubmitChanges();
                        }
                    }
                }
            }
        }

        public FIRMA_TANIMLARI GetFirma(int? id)
        {

            var model = data_firma_listesi.Where(x => x.ID == id).SingleOrDefault();

            return model;
        }

        private string GetAciklama(TebligatModulEnums modul)
        {
            var aciklama = "";

            if (modul == TebligatModulEnums.GELIR_IDARESI)
                aciklama = "*Gelir İdaresi Başkanlığı*";
            if (modul == TebligatModulEnums.ICISLERI_BAKANLIGI)
                aciklama = "*İçişleri Bakanlığı*";
            if (modul == TebligatModulEnums.TİCARET_BAKANLIGI)
                aciklama = "*Ticaret Bakanlığı*";
            if (modul == TebligatModulEnums.VERGI_DENETIM)
                aciklama = "*Vergi Denetim Başkanlığı*";


            return aciklama;
        }
    }
}
