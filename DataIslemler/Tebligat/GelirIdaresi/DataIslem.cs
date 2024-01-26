using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model;
using Tacmin.Model.Tebligat;

namespace DataIslemler.Tebligat.GelirIdaresi
{
    public class DataIslem
    {
        private Musavir_DbDataContext data;
        private List<Tebligatlar_Gelir_Idaresi> data_tebligat_listesi;
        private List<Tebligat_Belge_Turleri> data_belge_tur_listesi;
        private List<FIRMA_TANIMLARI> data_firma_listesi;
        private List<Vergi_Daireleri> data_vergi_daire_listesi;

        public DataIslem(string dataAdi)
        {

            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            data_tebligat_listesi = data.Tebligatlar_Gelir_Idaresis.Where(x => x.IsOkundu == false).ToList();
        }

        public List<TebligatDataListModel> TebligatKayit(List<GelirMlyResponseModel> tebligatListesi,int firmaId)
        {

            var liste = new List<TebligatDataListModel>();

            try
            {
                foreach (var item in tebligatListesi)
                {
                    var model = new Tebligatlar_Gelir_Idaresi();

                    model.BelgeNo = item.etebBelgeNo;
                    model.BelgeOid = item.belgeOid;
                    if (!string.IsNullOrEmpty(item.belgeTuru))
                    {
                        model.BelgeTurId = Convert.ToInt32(item.belgeTuru);
                    }
                    model.BirimAdi = item.birimAdi;
                    model.FirmaId = firmaId;
                    model.GondermeTarihi = GetTarih(item.vdGondermeTarihi);
                    model.TebligTarihi = GetTarih(item.tebellugTarihi);
                    model.VdKodu = item.vdKodu;
                    model.ZarfOid = item.zarfOid;
                    model.IsOkundu = false;

                    data.Tebligatlar_Gelir_Idaresis.InsertOnSubmit(model);
                    data.SubmitChanges();

                    if (item.ustYaziIcerik!=null)
                    {
                        var dosya_model = new TebligatDosyalar();

                        dosya_model.DosyaAdi = item.belgeOid + ".pdf";
                        dosya_model.Icerik = item.ustYaziIcerik;
                        dosya_model.MimeType = "application/pdf";
                        dosya_model.TebligatId = model.Id;
                        dosya_model.Tur = "asil";
                        dosya_model.ModulId = (int)TebligatModulEnums.GELIR_IDARESI;

                        data.TebligatDosyalars.InsertOnSubmit(dosya_model);
                        data.SubmitChanges();
                    }

                    var list_model = new TebligatDataListModel();

                    list_model.BelgeNo = model.BelgeNo;
                    list_model.BelgeTuru = GetBelgeTur(model.BelgeTurId);
                    list_model.BirimAdi = model.BirimAdi;
                    list_model.FirmaId = model.FirmaId;
                    list_model.GondermeTarihi = model.GondermeTarihi;
                    list_model.Id = model.Id;
                    list_model.TebligTarihi = model.TebligTarihi;
                    list_model.Unvan = GetUnvan(model.FirmaId);
                    list_model.VergiDairesi = model.BirimAdi;
                    if (model.IsOkundu == true)
                    {
                        list_model.Durum = "Okundu";
                        list_model.durumCss = "okundu_css";

                    }
                    if (model.IsOkundu == false)
                    {
                        list_model.Durum = "Okunmadı";
                        list_model.durumCss = "okunmadi_css";
                    }
                    list_model.Gonderim_Whats_App = false;
                    list_model.Whats_App_Css = "whats_app_pasif";

                    liste.Add(list_model);
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return liste;
          
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

        public string GetBelgeTur(int? id)
        {
            if(data_belge_tur_listesi == null)
            {
                data_belge_tur_listesi = data.Tebligat_Belge_Turleris.ToList();
            }

            var liste = data_belge_tur_listesi.Where(x => x.Kod == id).ToList();

            if (liste.Count > 0)
            {
                return liste[0].Belge_Turu;
            }
            return "";
        }

        public string GetUnvan(int? id)
        {

            if (data_firma_listesi == null)
            {
                data_firma_listesi = data.FIRMA_TANIMLARIs.ToList();
            }

            var liste = data_firma_listesi.Where(x => x.ID == id).ToList();

            if (liste.Count > 0)
            {
                return liste[0].UNVAN;
            }

            return "";
        }

        private string GetVergiDairesi(string kod)
        {
            if (data_vergi_daire_listesi == null)
            {
                data_vergi_daire_listesi = data.Vergi_Daireleris.ToList();
            }

            var liste = data_vergi_daire_listesi.Where(x => x.Kodu == kod).ToList();

            if (liste.Count>0)
            {
                return liste[0].Daire_Adi;
            }

            return "";

        }

        public void TebligatDurumGuncelle(List<int> clientList)
        {
            foreach (var id in clientList)
            {
                var model = data_tebligat_listesi.Where(x => x.Id == id).SingleOrDefault();

                if (model != null)
                {
                    model.IsOkundu = true;

                    data.SubmitChanges();
                }
            }
        }

        
    }
}
