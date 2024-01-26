using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model.Tebligat;

namespace DataIslemler.Tebligat.Icisleri 
{
    public class DataIslem
    {
        private Musavir_DbDataContext data;
        private List<Tebligat_Icisleri> data_tebligat_listesi;
        private List<FIRMA_TANIMLARI> data_firma_listesi;

        public DataIslem(string dataAdi)
        {

            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public List<TebligatDataList> TebligatKayitIslem(List<IsisleriResponseListe> clientListe,int firmaId)
        {
            var liste = new List<TebligatDataList>();
            foreach (var item in clientListe)
            {
                var model = new Tebligat_Icisleri();

                model.AltKurumKodu = item.altKurumKodu;
                model.BelgeNo = item.belgeNo;
                model.BelgeTuru = item.belgeTuru;
                model.FirmaId = firmaId;
                model.GondermeTarihi = GetTarih(item.kayitZamani);
                model.KurumKodu = item.kurumKodu;
                model.Oid = item.oid;
                model.TarafOid = item.tarafOid;
                model.TebligTarihi = GetTarih(item.tebligZamani);
                model.Durum = "Okunmadı";
                model.IsOkundu = false;

                data.Tebligat_Icisleris.InsertOnSubmit(model);
                data.SubmitChanges();


                if (item.ustYaziIcerik != null)
                {
                    var dosya_model = new TebligatDosyalar();
                    dosya_model.DosyaAdi = model.Oid + ".pdf";
                    dosya_model.Icerik = item.ustYaziIcerik;
                    dosya_model.MimeType = "application/pdf";
                    dosya_model.TebligatId = model.Id;
                    dosya_model.Tur = "asil";
                    dosya_model.ModulId = (int)TebligatModulEnums.ICISLERI_BAKANLIGI;

                    data.TebligatDosyalars.InsertOnSubmit(dosya_model);
                    data.SubmitChanges();
                }

                if (item.ekListesi != null)
                {
                    foreach (var ek_item in item.ekListesi)
                    {
                        if (ek_item.Icerik != null)
                        {
                            var dosya_model = new TebligatDosyalar();
                            dosya_model.DosyaAdi = ek_item.DosyaAdi;
                            dosya_model.MimeType = ek_item.MimeType;
                            dosya_model.Tur = "ek";
                            dosya_model.Icerik = ek_item.Icerik;
                            dosya_model.TebligatId = model.Id;
                            dosya_model.ModulId = (int)TebligatModulEnums.ICISLERI_BAKANLIGI;

                            data.TebligatDosyalars.InsertOnSubmit(dosya_model);
                            data.SubmitChanges();
                        }
                    }
                }

                var ek_sayisi = 0;

                if (item.ekListesi != null)
                {
                    ek_sayisi = item.ekListesi.Count;
                }

                var lsModel = new TebligatDataList();

                lsModel.BelgeNo = model.BelgeNo;
                lsModel.BelgeTuru = model.BelgeTuru;
                lsModel.BirimAdi = model.AltKurumKodu;
                lsModel.FirmaId = model.FirmaId;
                lsModel.GondermeTarihi = model.GondermeTarihi;
                lsModel.Id = model.Id;
                lsModel.TebligTarihi = model.TebligTarihi;
                lsModel.Unvan = GetUnvan(model.FirmaId);
                lsModel.VergiDairesi = "";
                lsModel.EkSayisi = ek_sayisi;
                if (model.IsOkundu == true)
                {
                    lsModel.Durum = "Okundu";
                    lsModel.durumCss = "okundu_css";

                }
                if (model.IsOkundu == false)
                {
                    lsModel.Durum = "Okunmadı";
                    lsModel.durumCss = "okunmadi_css";
                }
                lsModel.Gonderim_Whats_App = false;
                lsModel.Whats_App_Css = "whats_app_pasif";


                liste.Add(lsModel);
            }

            return liste;
        }

        private void DosyaKayit(int? tebligatId, List<IcisleriDosyaModel> dosyalar)
        {

            var data_list = new List<TebligatDosyalar>();

            foreach (var item in dosyalar)
            {
                var model = new TebligatDosyalar();

                model.DosyaAdi = item.DosyaAdi;
                model.Icerik = item.Icerik;
                model.MimeType = item.MimeType;
                model.TebligatId = tebligatId;
                model.Tur = item.Tur;

                data_list.Add(model);
            }

            data.TebligatDosyalars.InsertAllOnSubmit(data_list);
            data.SubmitChanges();
        }

        public int KayitKontrol(string belgeNo)
        {
            try
            {
                if (data_tebligat_listesi == null)
                {
                    data_tebligat_listesi = data.Tebligat_Icisleris.ToList();
                }

                var kontrol = data_tebligat_listesi.Where(x => x.BelgeNo == belgeNo).ToList();

                return kontrol.Count();
            }
            catch 
            {

                return 0;
            }
        }

        private DateTime? GetTarih(string value)
        {

            try
            {
                var yil = Convert.ToInt32(value.Substring(0, 4));
                var ay = Convert.ToInt32(value.Substring(4, 2));
                var gun = Convert.ToInt32(value.Substring(6, 2));

                var tarih = new DateTime(yil, ay, gun);

                return tarih;
            }
            catch
            {

                return null;
            }
        }

        private string GetUnvan(int? id)
        {

            if (data_firma_listesi == null)
            {
                data_firma_listesi = data.FIRMA_TANIMLARIs.ToList();
            }

            var liste = data_firma_listesi.Where(x => x.ID == id).ToList();

            if (liste.Count() > 0)
            {
                return liste[0].UNVAN;
            }

            return "";
        }

        public void OkumaDurumlariGuncelle(List<int> tebligatListesi)
        {

            foreach (var id in tebligatListesi)
            {
                var model = data.Tebligat_Icisleris.Where(x => x.Id == id).SingleOrDefault();

                model.Durum = "Okundu";

                data.SubmitChanges();
            }

        }

        public void OkumaDurumGuncelle(int? tebligatId)
        {

            var model = data.Tebligat_Icisleris.Where(x => x.Id == tebligatId).SingleOrDefault();

            model.Durum = "Okundu";

            data.SubmitChanges();
        }
    }
}
