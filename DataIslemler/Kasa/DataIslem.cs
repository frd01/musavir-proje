using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model;
using Tacmin.Model.Kasa;

namespace DataIslemler.Kasa
{
    public class DataIslem
    {
        private Musavir_DbDataContext data;

        public DataIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        public KayitResModel YeniKasaKaydi(KartModel item)
        {
            var res_model = kayitKontrol(item);

            if (res_model.IslemDurum == false)
            {
                return res_model;
            }

            try
            {
                var model = new Kasa_Kartlari();

                model.KasaAdi = item.KasaAdi;
                model.Kodu = item.KasaKodu;
                model.ParaBirimId = item.ParaBirimId;
                model.MuhasebeKodu = item.MuhasebeKodu;
                model.OzelKod = item.MuhasebeKodu;
                model.Aciklama = item.Aciklama;

                data.Kasa_Kartlaris.InsertOnSubmit(model);
                data.SubmitChanges();

                res_model.IslemDurum = true;
                res_model.Mesaj = "Kasa Kartı Kaydı Yapıldı";
            }
            catch (Exception ex)
            {

                res_model.IslemDurum = false;
                res_model.Mesaj = ex.Message;
            }

            return res_model;

        }

        private KayitResModel kayitKontrol(KartModel item)
        {

            var model = new KayitResModel();

            try
            {
                var data_model_list = data.Kasa_Kartlaris.ToList();

                if (data_model_list.Count > 0)
                {
                    var kod_kontrol = data_model_list.Where(x => x.Kodu == item.KasaKodu).SingleOrDefault();

                    if (kod_kontrol != null)
                    {
                        model.IslemDurum = false;
                        model.Mesaj = item.KasaKodu + " Bu Kod Daha Önce Başka Kartta Kullanılmış.Kontrol Ediniz.";

                        return model;
                    }

                    var tanim_kontrol = data_model_list.Where(x => x.KasaAdi == item.KasaAdi).SingleOrDefault();

                    if (tanim_kontrol != null)
                    {
                        model.IslemDurum = false;
                        model.Mesaj = item.KasaAdi + " Bu Kasa Adı Başka Bir Kartta Kullanılmış. Kontrol Ediniz.";
                        return model;
                    }


                }

                model.IslemDurum = true;
                model.Mesaj = "";


            }
            catch (Exception ex)
            {

                model.IslemDurum = false;
                model.Mesaj = ex.Message;
            }

            return model;
        }

        public KayitResModel KasaNakitIslem(string islemTur, KasaIslemModel item)
        {
            var model = new KayitResModel();

            try
            {
                if (islemTur == "tahsilat")
                {
                    TahsilatYap(item);
                }
                if (islemTur == "odeme")
                {
                    OdemeYap(item);
                }

                model.IslemDurum = true;
                model.Mesaj = "Kasa Kaydı Yapıldı.";
            }
            catch (Exception ex)
            {

                model.IslemDurum = false;
                model.Mesaj = ex.Message;
            }

            return model;

        }



        private void TahsilatYap(KasaIslemModel item)
        {
            var model = new Kasa_Tahsilatlar();

            model.Aciklama = item.Aciklama;
            model.CariId = item.CariId;
            model.KasaId = item.KasaId;
            model.Tarih = item.Tarih;
            model.Tutar = item.Tutar;
            model.FisNo = item.FisNo;

            data.Kasa_Tahsilatlars.InsertOnSubmit(model);
            data.SubmitChanges();
        }

        private void OdemeYap(KasaIslemModel item)
        {
            var model = new Kasa_Odemeler();

            model.Aciklama = item.Aciklama;
            model.CariId = item.CariId;
            model.KasaId = item.KasaId;
            model.Tarih = item.Tarih;
            model.Tutar = item.Tutar;
            model.FisNo = item.FisNo;

            data.Kasa_Odemelers.InsertOnSubmit(model);
            data.SubmitChanges();
        }

        public KayitResModel KasaIslemGuncelleme(KasaIslemModel item, string islemTur)
        {

            var res_model = new KayitResModel();
            res_model.IslemDurum = true;

            if (islemTur == "Nakit Ödeme" || islemTur == "NAKİT ÖDEME")
            {
                res_model = KasaTahsilatGuncelle(item);
            }
            if (islemTur == "Nakit Alınan" || islemTur == "NAKİT ALINAN")
            {
                res_model = KasaOdemeGuncelle(item);
            }

            return res_model;
        }

        private KayitResModel KasaTahsilatGuncelle(KasaIslemModel item)
        {
            var res_model = new KayitResModel();

            try
            {
                var model = data.Kasa_Tahsilatlars.Where(x => x.Id == item.Id).SingleOrDefault();

                if (model != null)
                {
                    model.Aciklama = item.Aciklama;
                    model.CariId = item.CariId;
                    model.FisNo = item.FisNo;
                    model.KasaId = item.KasaId;
                    model.Tarih = item.Tarih;
                    model.Tutar = item.Tutar;

                    data.SubmitChanges();
                }

                res_model.IslemDurum = true;
                res_model.Mesaj = "Kasa İşlem Bilgileri Değiştirildi.";
            }
            catch (Exception ex)
            {

                res_model.IslemDurum = false;
                res_model.Mesaj = ex.Message;
            }

            return res_model;
        }

        private KayitResModel KasaOdemeGuncelle(KasaIslemModel item)
        {
            var res_model = new KayitResModel();

            try
            {
                var model = data.Kasa_Odemelers.Where(x => x.Id == item.Id).SingleOrDefault();

                if (model != null)
                {
                    model.Aciklama = item.Aciklama;
                    model.CariId = item.CariId;
                    model.FisNo = item.FisNo;
                    model.KasaId = item.KasaId;
                    model.Tarih = item.Tarih;
                    model.Tutar = item.Tutar;

                    data.SubmitChanges();
                }

                res_model.IslemDurum = true;
                res_model.Mesaj = "Kasa İşlem Bilgileri Değiştirildi.";
            }
            catch (Exception ex)
            {

                res_model.IslemDurum = false;
                res_model.Mesaj = ex.Message;
            }

            return res_model;
        }

        public KayitResModel KasaIslemSil(int? islemId, string islemTur)
        {

            var res_model = new KayitResModel();

            if (islemTur == "Nakit Ödeme" || islemTur == "NAKİT ÖDEME")
            {
                res_model = KasaTahsilatIslemSil(islemId);
            }
            if (islemTur == "Nakit Alınan" || islemTur == "NAKİT ALINAN")
            {
                res_model = KasaOdemeIslemSil(islemId);
            }

            return res_model;
        }

        private KayitResModel KasaTahsilatIslemSil(int? islemId)
        {

            var res_model = new KayitResModel();

            try
            {
                var model = data.Kasa_Tahsilatlars.Where(x => x.Id == islemId).SingleOrDefault();

                if (model != null)
                {
                    data.Kasa_Tahsilatlars.DeleteOnSubmit(model);
                    data.SubmitChanges();
                }

                res_model.IslemDurum = true;
                res_model.Mesaj = "Kasa Kaydı Silindi";
            }
            catch (Exception ex)
            {

                res_model.IslemDurum = false;
                res_model.Mesaj = ex.Message;
            }

            return res_model;

        }

        private KayitResModel KasaOdemeIslemSil(int? islemId)
        {

            var res_model = new KayitResModel();

            try
            {
                var model = data.Kasa_Odemelers.Where(x => x.Id == islemId).SingleOrDefault();

                if (model != null)
                {
                    data.Kasa_Odemelers.DeleteOnSubmit(model);
                    data.SubmitChanges();
                }

                res_model.IslemDurum = true;
                res_model.Mesaj = "Kasa Kaydı Silindi";
            }
            catch (Exception ex)
            {

                res_model.IslemDurum = false;
                res_model.Mesaj = ex.Message;
            }

            return res_model;

        }

    }
}
