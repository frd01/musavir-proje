using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model;
using Tacmin.Model.Tebligat;

namespace DataIslemler.Tebligat.VergiDenetim
{
    public class DataIslem
    {
        private Musavir_DbDataContext data;
        private List<FIRMA_TANIMLARI> data_firma_listesi;

        public DataIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public List<TebligatDataListModel> TebligatKayitIslem(List<VergiDenetimMlyListeModel> clientList,int firmaId) 
        {

            var liste = new List<TebligatDataListModel>();

            try
            {
                foreach (var item in clientList)
                {
                    var model = new Tebligat_Vergi_Denetim();
                    
                    model.BelgeNo = item.belgeNo;
                    model.BelgeTuru = item.belgeTuru;
                    model.BirimAdi = item.altKurumKodu;
                    model.GondermeTarihi = GetTarih(item.kayitZamani);
                    model.IsOkundu = false;
                    model.Oid = item.oid;
                    model.TarafOid = item.tarafOid;
                    model.TebligTarihi = GetTarih(item.tebligZamani);
                    model.IsOkundu = false;
                    model.FirmaId = firmaId;

                    data.Tebligat_Vergi_Denetims.InsertOnSubmit(model);
                    data.SubmitChanges();

                    if (item.ustYaziIcerik != null)
                    {
                        var dosya_model = new TebligatDosyalar();

                        dosya_model.DosyaAdi = model.Oid + ".pdf";
                        dosya_model.Icerik = item.ustYaziIcerik;
                        dosya_model.MimeType = "application/pdf";
                        dosya_model.Tur = "asil";
                        dosya_model.TebligatId = model.Id;
                        dosya_model.ModulId = (int)TebligatModulEnums.VERGI_DENETIM;

                        data.TebligatDosyalars.InsertOnSubmit(dosya_model);
                        data.SubmitChanges();
                    }

                    if (item.ekListesi != null)
                    {
                        foreach (var ek_item in item.ekListesi)
                        {
                            if (ek_item.icerik !=null)
                            {
                                var dosya_model = new TebligatDosyalar();
                                dosya_model.DosyaAdi = ek_item.oid + ".pdf";
                                dosya_model.MimeType= "application/pdf";
                                dosya_model.Tur = "ek";
                                dosya_model.TebligatId = model.Id;
                                dosya_model.ModulId = (int)TebligatModulEnums.VERGI_DENETIM;
                                dosya_model.Icerik = ek_item.icerik;

                                data.TebligatDosyalars.InsertOnSubmit(dosya_model);
                                data.SubmitChanges();
                            }
                        }
                    }

                    var liste_model = new TebligatDataListModel();

                    liste_model.BelgeNo = model.BelgeNo;
                    liste_model.BelgeTuru = model.BelgeTuru;
                    liste_model.BirimAdi = model.BirimAdi;
                    liste_model.FirmaId = model.FirmaId;
                    liste_model.TebligTarihi = model.TebligTarihi;
                    liste_model.GondermeTarihi = model.GondermeTarihi;
                    liste_model.Unvan = GetUnvan(model.FirmaId);
                    liste_model.Id = model.Id;
                    liste_model.VergiDairesi = model.BirimAdi;
                    liste_model.EkSayisi = 0;

                    if (item.ekListesi != null)
                    {
                        liste_model.EkSayisi = item.ekListesi.Count;
                    }
                    if (liste_model.EkSayisi <= 0)
                        liste_model.cssClass = "btn btn-danger";
                    if (liste_model.EkSayisi > 0)
                        liste_model.cssClass = "btn btn-success";
                    if (model.IsOkundu == true)
                    {
                        liste_model.Durum = "Okundu";
                        liste_model.durumCss = "okundu_css";

                    }
                    if (model.IsOkundu == false)
                    {
                        liste_model.Durum = "Okunmadı";
                        liste_model.durumCss = "okunmadi_css";
                    }
                    liste_model.Gonderim_Whats_App = false;
                    liste_model.Whats_App_Css = "whats_app_pasif";

                    liste.Add(liste_model);


                }

                

            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
            }

            return liste;
        }

        public void OkunduDurumGuncelle(List<int> clientList)
        {
            foreach (var id in clientList)
            {
                var model = data.Tebligat_Vergi_Denetims.Where(x => x.Id == id).SingleOrDefault();

                if (model != null)
                {
                    model.IsOkundu = true;
                    data.SubmitChanges();
                }
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
    }
}
