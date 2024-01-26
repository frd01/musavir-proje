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

namespace DataIslemler.Tebligat.GelirIdaresi
{
    public class ListeIslem
    {
        Musavir_DbDataContext data;
        private DataIslem dataIslem;
        private List<Whats_App_Gonderileri> data_gonderi_listesi;
        public ListeIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
            dataIslem = new DataIslem(dataAdi);

            data_gonderi_listesi = data.Whats_App_Gonderileris.Where(x => x.ModulId == (int)ModulEnums.TEBLIGAT).ToList();
        }

        public List<GelirMlyResponseModel> ListeKayitKontrolIslem(List<GelirMlyResponseModel> clientList)
        {

            var liste = new List<GelirMlyResponseModel>();

            foreach (var item in clientList)
            {
                if (dataKayitKontrol(item) <=0)
                {
                    liste.Add(item);
                }
            }

            return liste;
        }

        private int dataKayitKontrol(GelirMlyResponseModel item)
        {
            var liste = data.Tebligatlar_Gelir_Idaresis.Where(x => x.BelgeNo == item.etebBelgeNo).ToList();

            if (liste.Count>0)
            {
                return 1;
            }

            var kayit_tarihi = GetTarih(item.vdGondermeTarihi);
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

        public List<TebligatDataListModel> GetTebligatListesi(string durum = "Okunmadı",DateTime? sorguTarihi=null)
        {
            var liste = new List<TebligatDataListModel>();

            var data_list = data.Tebligatlar_Gelir_Idaresis.ToList();

            if (durum == "Okunmadı")
            {
                data_list = data_list.Where(x => x.IsOkundu == false).ToList();
            }

            if (durum == "Okundu")
            {
                data_list = data_list.Where(x => x.IsOkundu == true).ToList();
            }

            if (sorguTarihi != null)
            {
                data_list = data_list.Where(x => x.SorguKayitTarihi == sorguTarihi).ToList();

            }

            foreach (var item in data_list)
            {
                var model = new TebligatDataListModel();

                model.BelgeNo = item.BelgeNo;
                model.BelgeTuru = dataIslem.GetBelgeTur(item.BelgeTurId);
                model.BirimAdi = item.BirimAdi;
                model.FirmaId = item.FirmaId;
                model.GondermeTarihi = item.GondermeTarihi;
                model.Id = item.Id;
                model.TebligTarihi = item.TebligTarihi;
                model.Unvan = dataIslem.GetUnvan(item.FirmaId);
                model.VergiDairesi = model.BirimAdi;
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

        private int WhasappKontrol(int? tebligatId)
        {

            var liste = data_gonderi_listesi.Where(x => x.EvrakId == tebligatId && x.EvrakTur == (int)EvrakTurEnum.TEBLIGAT_GELIR_IDARESI_BASKANLIGI).ToList();

            return liste.Count;
        }

        private string GetWhatsappGonderim(int? evrakId)
        {
            var icerik = "";

            var liste = (from m in data_gonderi_listesi
                         where m.EvrakId == evrakId
&& m.EvrakTur == (int)EvrakTurEnum.TEBLIGAT_GELIR_IDARESI_BASKANLIGI
                         orderby m.TarihZaman descending
                         select m).ToList();

            if (liste.Count > 0)
            {
                var model = liste[0];

                icerik = model.TarihZaman.Value.ToString("dd-MM-yyyy HH:mm");
            }

            return icerik;
        }


    }
}
