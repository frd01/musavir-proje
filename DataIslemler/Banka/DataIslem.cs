using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model;
using Tacmin.Model.Banka;

namespace DataIslemler.Banka
{
    public class DataIslem
    {
        private Musavir_DbDataContext data;
        private List<Banka_Kartlari> data_banka_kart_listesi;

        public DataIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            data_banka_kart_listesi = data.Banka_Kartlaris.ToList();
        }

        public KayitResModel YeniBankaKartiKaydi(BankaKartModel item)
        {

            var res_model = kayitKontrol(item);

            if (res_model.IslemDurum == false)
            {
                return res_model;
            }

            try
            {
                var model = new Banka_Kartlari();

                model.BankaAdi = item.BankaAdi;
                model.BankaKodu = item.BankaKodu;
                model.HesapAdi = item.HesapAdi;
                model.HesapNo = item.HesapNo;
                model.HesapTurId = item.HesapTurId;
                model.IbanNo = item.IbanNo;
                model.ParaBirimId = item.ParaBirimId;
                model.SubeAdi = item.SubeAdi;
                model.SubeKodu = item.SubeKodu;

                data.Banka_Kartlaris.InsertOnSubmit(model);
                data.SubmitChanges();

                res_model.IslemDurum = true;
                res_model.Mesaj = "Banka Kartı Oluşturuldu.";
            }
            catch (Exception ex)
            {

                res_model.IslemDurum = false;
                res_model.Mesaj = ex.Message;
            }

            return res_model;
        }

        public KayitResModel BankaKartGuncelleme(BankaKartModel item)
        {

            var res_model = kayitKontrol(item);



            try
            {
                var model = data.Banka_Kartlaris.Where(x => x.Id == item.Id).SingleOrDefault();

                model.BankaAdi = item.BankaAdi;
                model.BankaKodu = item.BankaKodu;
                model.HesapAdi = item.HesapAdi;
                model.HesapNo = item.HesapNo;
                model.HesapTurId = item.HesapTurId;
                model.IbanNo = item.IbanNo;
                model.ParaBirimId = item.ParaBirimId;
                model.SubeAdi = item.SubeAdi;
                model.SubeKodu = item.SubeKodu;


                data.SubmitChanges();

                res_model.IslemDurum = true;
                res_model.Mesaj = "Banka Kartı Oluşturuldu.";
            }
            catch (Exception ex)
            {

                res_model.IslemDurum = false;
                res_model.Mesaj = ex.Message;
            }

            return res_model;
        }



        public KayitResModel BankaIslemKayit(BankaIslemModel item, string islemTur)
        {

            var res_model = new KayitResModel();

            if (islemTur == "havale-gonderme" || islemTur == "Giden Havale")
            {
                res_model = CariOdemeIslem(item);
            }

            if (islemTur == "havale-giris" || islemTur == "Gelen Havale")
            {
                res_model = CariTahsilatIslem(item);
            }

            if (islemTur == "para-yatirma" || islemTur == "Nakit Yatan")
            {
                res_model = KasaParaYatirmaIslem(item);
            }

            if (islemTur == "para-cekme" || islemTur == "Nakit Çekilen")
            {
                res_model = KasaParaCekme(item);
            }



            return res_model;
        }

        public KayitResModel BankaKayitSil(BankaIslemModel item, string islemTur)
        {
            var res_model = new KayitResModel();



            if (islemTur == "havale-gonderme" || islemTur == "Giden Havale" || islemTur == "para-cekme" || islemTur == "Nakit Çekilen")
            {
                res_model = BankaOdemeSil(item);


            }

            if (islemTur == "havale-giris" || islemTur == "Gelen Havale" || islemTur == "para-yatirma" || islemTur == "Nakit Yatan")
            {
                res_model = BankaTahsilatSil(item);
            }



            return res_model;

        }

        private KayitResModel BankaOdemeSil(BankaIslemModel item)
        {
            var res_model = new KayitResModel();

            try
            {
                var model = data.Banka_Odemelers.Where(x => x.Id == item.Id).SingleOrDefault();



                data.Banka_Odemelers.DeleteOnSubmit(model);
                data.SubmitChanges();

                res_model.IslemDurum = true;
            }
            catch (Exception ex)
            {

                res_model.IslemDurum = false;
                res_model.Mesaj = ex.Message;
            }

            return res_model;
        }

        private KayitResModel BankaTahsilatSil(BankaIslemModel item)
        {
            var res_model = new KayitResModel();

            try
            {
                var model = data.Banka_Tahsilatlars.Where(x => x.Id == item.Id).SingleOrDefault();

                data.Banka_Tahsilatlars.DeleteOnSubmit(model);
                data.SubmitChanges();

                res_model.IslemDurum = true;
            }
            catch (Exception ex)
            {

                res_model.IslemDurum = false;
                res_model.Mesaj = ex.Message;
            }

            return res_model;
        }

        private KayitResModel CariOdemeIslem(BankaIslemModel item)
        {

            var res_model = new KayitResModel();

            try
            {
                var model = new Banka_Odemeler();

                if (item.Id != null)
                {
                    model = data.Banka_Odemelers.Where(x => x.Id == item.Id).SingleOrDefault();
                }

                model.BankaId = item.BankaId;
                model.CariId = item.CariId;
                model.FisNo = item.FisNo;
                model.Islem = "cari";
                model.IslemAciklama = item.Aciklama;
                model.Tarih = item.Tarih;
                model.Tutar = item.Tutar;

                if (item.Id == null)
                {
                    data.Banka_Odemelers.InsertOnSubmit(model);
                    data.SubmitChanges();
                }
                if (item.Id != null)
                {
                    data.SubmitChanges();
                }

                res_model.IslemDurum = true;
            }
            catch (Exception ex)
            {

                res_model.IslemDurum = false;
                res_model.Mesaj = ex.Message;
            }

            return res_model;
        }

        private KayitResModel CariTahsilatIslem(BankaIslemModel item)
        {

            var res_model = new KayitResModel();

            try
            {
                var model = new Banka_Tahsilatlar();

                if (item.Id != null)
                {
                    model = data.Banka_Tahsilatlars.Where(x => x.Id == item.Id).SingleOrDefault();
                }

                model.BankaId = item.BankaId;
                model.CariId = item.CariId;
                model.FisNo = item.FisNo;
                model.Islem = "cari";
                model.IslemAciklama = item.Aciklama;
                model.Tarih = item.Tarih;
                model.Tutar = item.Tutar;

                if (item.Id == null)
                {
                    data.Banka_Tahsilatlars.InsertOnSubmit(model);
                    data.SubmitChanges();
                }

                if (item.Id != null)
                {
                    data.SubmitChanges();
                }

                res_model.IslemDurum = true;
            }
            catch (Exception ex)
            {

                res_model.IslemDurum = false;
                res_model.Mesaj = ex.Message;
            }

            return res_model;
        }

        private KayitResModel KasaParaYatirmaIslem(BankaIslemModel item)
        {

            var res_model = new KayitResModel();

            try
            {
                var model = new Banka_Tahsilatlar();

                if (item.Id != null)
                {
                    model = data.Banka_Tahsilatlars.Where(x => x.Id == item.Id).SingleOrDefault();
                }

                model.BankaId = item.BankaId;
                model.KasaId = item.KasaId;
                model.FisNo = item.FisNo;
                model.Islem = "kasa";
                model.IslemAciklama = item.Aciklama;
                model.Tarih = item.Tarih;
                model.Tutar = item.Tutar;

                if (item.Id == null)
                {
                    data.Banka_Tahsilatlars.InsertOnSubmit(model);
                    data.SubmitChanges();
                }
                if (item.Id != null)
                {
                    data.SubmitChanges();
                }

                res_model.IslemDurum = true;
            }
            catch (Exception ex)
            {

                res_model.IslemDurum = false;
                res_model.Mesaj = ex.Message;
            }

            return res_model;
        }

        private KayitResModel KasaParaCekme(BankaIslemModel item)
        {

            var res_model = new KayitResModel();

            try
            {
                var model = new Banka_Odemeler();

                if (item.Id != null)
                {
                    model = data.Banka_Odemelers.Where(x => x.Id == item.Id).SingleOrDefault();
                }

                model.BankaId = item.BankaId;
                model.KasaId = item.KasaId;
                model.FisNo = item.FisNo;
                model.Islem = "kasa";
                model.IslemAciklama = item.Aciklama;
                model.Tarih = item.Tarih;
                model.Tutar = item.Tutar;

                if (item.Id == null)
                {
                    data.Banka_Odemelers.InsertOnSubmit(model);
                    data.SubmitChanges();
                }

                if (item.Id != null)
                {
                    data.SubmitChanges();
                }

                res_model.IslemDurum = true;
            }
            catch (Exception ex)
            {

                res_model.IslemDurum = false;
                res_model.Mesaj = ex.Message;
            }

            return res_model;
        }

        private KayitResModel kayitKontrol(BankaKartModel item)
        {

            var res_model = new KayitResModel();

            var banka_kodu = data_banka_kart_listesi.Where(x => x.BankaKodu == item.BankaKodu).ToList();

            if (banka_kodu.Count > 0)
            {
                res_model.IslemDurum = false;

                res_model.Mesaj = item.BankaKodu + " Bu Kod Daha Önce Başka Bankada Kullanılmış Kontrol Ediniz.";

                return res_model;
            }

            var sube_banka_adi = data_banka_kart_listesi.Where(x => x.BankaAdi == item.BankaAdi && x.SubeAdi == item.SubeAdi).ToList();

            if (sube_banka_adi.Count > 0)
            {
                res_model.IslemDurum = false;
                res_model.Mesaj = item.BankaAdi + " " + item.SubeAdi + " Bu Tanımlar Daha Önce Başka Banka Kartında Kullanılmış Kontrol Ediniz.";

                return res_model;
            }

            var hesap_no = data_banka_kart_listesi.Where(x => x.HesapNo == item.HesapNo).ToList();

            if (hesap_no.Count > 0)
            {
                res_model.IslemDurum = false;

                res_model.Mesaj = item.HesapNo + " Bu Hesapno Başka Banka Kartında Kullanılmış Kontrol Ediniz.";
                return res_model;
            }

            var iban_no = data_banka_kart_listesi.Where(x => x.IbanNo == item.IbanNo).ToList();

            if (iban_no.Count > 0)
            {
                res_model.IslemDurum = false;
                res_model.Mesaj = item.IbanNo + " Bu Ibanno Başka Banka Kartında Kullanılmış Kontrol Ediniz.";
                return res_model;
            }

            res_model.IslemDurum = true;
            res_model.Mesaj = "";

            return res_model;
        }

        public KayitResModel BankaIslemGuncelleme(BankaIslemModel item, string islemTur)
        {
            var res_model = new KayitResModel();
            if (islemTur == "cari_tahsilat")
            {
                res_model = BankaTahsilatGuncelleme(item);
            }

            if (islemTur == "cari_odeme")
            {
                res_model = BankaOdemeGuncelleme(item);
            }

            return res_model;
        }

        private KayitResModel BankaOdemeGuncelleme(BankaIslemModel item)
        {
            var res_model = new KayitResModel();

            try
            {
                var model = data.Banka_Odemelers.Where(x => x.Id == item.Id).SingleOrDefault();

                if (model != null)
                {
                    model.Tarih = item.Tarih;
                    model.FisNo = item.FisNo;
                    model.BankaId = item.BankaId;
                    model.CariId = item.CariId;
                    model.IslemAciklama = item.Aciklama;
                    model.Tutar = item.Tutar;
                    model.KasaId = item.KasaId;
                    model.Islem = item.Islem;

                    data.SubmitChanges();
                }

                res_model.IslemDurum = true;
                res_model.Mesaj = "Banka Bilgileri Güncellendi";
            }
            catch (Exception ex)
            {

                res_model.IslemDurum = false;
                res_model.Mesaj = ex.Message;
            }

            return res_model;
        }

        private KayitResModel BankaTahsilatGuncelleme(BankaIslemModel item)
        {
            var res_model = new KayitResModel();

            try
            {
                var model = data.Banka_Tahsilatlars.Where(x => x.Id == item.Id).SingleOrDefault();

                if (model != null)
                {
                    model.Tarih = item.Tarih;
                    model.FisNo = item.FisNo;
                    model.BankaId = item.BankaId;
                    model.CariId = item.CariId;
                    model.IslemAciklama = item.Aciklama;
                    model.Tutar = item.Tutar;
                    model.KasaId = item.KasaId;
                    model.Islem = item.Islem;

                    data.SubmitChanges();
                }

                res_model.IslemDurum = true;
                res_model.Mesaj = "Banka Bilgileri Güncellendi";
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
