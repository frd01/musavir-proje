﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model;
using Tacmin.Model.Tebligat;
using Tacmin.Model.Whatsapp;

namespace DataIslemler.Tebligat.TicaretBakanligi
{
    public class ListeIslem
    {
        private Musavir_DbDataContext data;
        private List<FIRMA_TANIMLARI> data_firma_listesi;
        private List<Whats_App_Gonderileri> data_gonderi_listesi;

        public ListeIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
            data_gonderi_listesi = data.Whats_App_Gonderileris.Where(x => x.ModulId == (int)ModulEnums.TEBLIGAT).ToList();
        }

        public List<TicaretMlyResponseModel> MaliteListeKayitKontrolIslem(List<TicaretMlyResponseModel> clientList)
        {
            var liste = new List<TicaretMlyResponseModel>();

            foreach (var item in clientList)
            {
                if (dataKayitKontrol(item)<=0)
                {
                    liste.Add(item);
                }
            }

            return liste;
        }

        private int dataKayitKontrol(TicaretMlyResponseModel item)
        {
            var liste = data.Tebligatlar_Gelir_Idaresis.Where(x => x.BelgeNo == item.belgeNo).ToList();

            if (liste.Count > 0)
            {
                return 1;
            }

            var kayit_tarihi = GetTarih(item.kayitZamani);
            kayit_tarihi = Convert.ToDateTime(kayit_tarihi.Value.ToString("yyyy-MM-dd"));

            var filter_2 = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd"));
            var filter_1 = Convert.ToDateTime(DateTime.Now.AddDays(-7).ToString("yyyy-MM-dd"));

            if (kayit_tarihi >= filter_1 && kayit_tarihi <= filter_2)
            {
                return 0;
            }
            else
            {
                return 1;
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

        public List<TebligatDataListModel> GetTebligatListesi(string durum="Okunmadı",DateTime? tarih=null)
        {
            var liste = new List<TebligatDataListModel>();

            var data_list = data.Tebligatlar_Ticaret_Bakanligis.ToList();

            if (durum == "Okunmadı")
            {
                data_list = data_list.Where(x => x.IsOkundu == false).ToList();
            }

            if (durum == "Okundu")
            {
                data_list = data_list.Where(x => x.IsOkundu == true).ToList();
            }

            if (tarih != null)
            {
                data_list = data_list.Where(x => x.SorguKayitTarihi == tarih).ToList();
            }

            foreach (var item in data_list)
            {
                var model = new TebligatDataListModel();

                model.BelgeNo = item.BelgeNo;
                model.BelgeTuru = item.BelgeTuru;
                model.BirimAdi = item.BirimAdi;
                model.FirmaId = item.FirmaId;
                model.GondermeTarihi = item.GondermeTarihi;
                model.Id = item.Id;
                model.TebligTarihi = item.TebligTarihi;
                model.VergiDairesi = item.BirimAdi;
                model.Unvan = GetUnvan(item.FirmaId);
                model.EkSayisi = GetEkSayisi(item.Id);
                
                if (model.EkSayisi <= 0)
                    model.cssClass = "btn btn-danger";
                if (model.EkSayisi > 0)
                    model.cssClass = "btn btn-success";
                if (item.IsOkundu == true)
                {
                    model.Durum = "Okundu";
                    model.durumCss = "okundu_css";

                }
                if (item.IsOkundu == false)
                {
                    model.Durum = "Okunmadı";
                    model.durumCss = "okunmadi_css";
                }

                if (WhasappKontrol(item.Id) > 0)
                {
                    model.Whats_App_Css = "whats_app_aktif";
                    model.Gonderim_Whats_App = true;
                    model.Whats_App_Gonderim_Baslik = GetWhatsappGonderim(item.Id);
                }
                else
                {
                    model.Whats_App_Css = "whats_app_pasif";
                    model.Gonderim_Whats_App = false;
                }


                liste.Add(model);

            }


            return liste;
        }

        private int GetEkSayisi(int? tebligatId)
        {
            var liste = data.TebligatDosyalars.Where(x => x.TebligatId == tebligatId && x.Tur == "ek" && x.ModulId == (int)TebligatModulEnums.TİCARET_BAKANLIGI).ToList();

            return liste.Count;
        }

        private int WhasappKontrol(int? tebligatId)
        {

            var liste = data_gonderi_listesi.Where(x => x.EvrakId == tebligatId && x.EvrakTur == (int)EvrakTurEnum.TEBLIGAT_TICARET_BAKANLIGI).ToList();

            return liste.Count;
        }

        private string GetWhatsappGonderim(int? evrakId)
        {
            var icerik = "";

            var liste = (from m in data_gonderi_listesi
                         where m.EvrakId == evrakId
&& m.EvrakTur == (int)EvrakTurEnum.TEBLIGAT_TICARET_BAKANLIGI
                         orderby m.TarihZaman descending
                         select m).ToList();

            if (liste.Count > 0)
            {
                var model = liste[0];

                icerik = model.TarihZaman.Value.ToString("dd-MM-yyyy HH:mm");
            }

            return icerik;
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
    }

}
