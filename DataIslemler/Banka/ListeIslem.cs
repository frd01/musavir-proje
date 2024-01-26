using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model.Banka;
using Tacmin.Model.Ortak;

namespace DataIslemler.Banka
{
    public class ListeIslem
    {
        private Musavir_DbDataContext data;

        private List<Banka_Tahsilatlar> data_banka_tahsilat_listesi;
        private List<Banka_Odemeler> data_banka_odeme_listesi;
        private BuyukHarfIslem harfIslem;

        public ListeIslem(string dataAdi, bool is_buyuk_harf)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            data_banka_odeme_listesi = data.Banka_Odemelers.ToList();

            data_banka_tahsilat_listesi = data.Banka_Tahsilatlars.ToList();

            harfIslem = new BuyukHarfIslem(is_buyuk_harf);
        }

        public List<BankaKartListeModel> BankaKartListesi()
        {

            var liste = new List<BankaKartListeModel>();

            foreach (var item in data.BankaKartListesi())
            {
                var model = new BankaKartListeModel();

                model.BankaAdi = harfIslem.GetValueStr(item.BankaAdi);
                model.BankaKodu = harfIslem.GetValueStr(item.BankaKodu);
                model.HesapAdi = harfIslem.GetValueStr(item.HesapAdi);
                model.HesapNo = item.HesapNo;
                model.HesapTurAdi = harfIslem.GetValueStr(item.HesapTurAdi);
                model.HesapTurId = item.HesapTurId;
                model.IbanNo = item.IbanNo;
                model.ParaBirimAdi = harfIslem.GetValueStr(item.Birim_Adi);
                model.ParaBirimId = item.ParaBirimId;
                model.SubeAdi = item.SubeAdi;
                model.SubeKodu = item.SubeKodu;
                model.Tahsilat = BankaTahsilatToplam(item.Id);
                model.Odeme = BankaOdemeToplam(item.Id);
                model.Id = item.Id;



                if (model.Tahsilat != null)
                {
                    if (model.Bakiye == null)
                    {
                        model.Bakiye = 0;
                    }

                    model.Bakiye += model.Tahsilat;
                }

                if (model.Odeme != null)
                {
                    if (model.Bakiye == null)
                    {
                        model.Bakiye = 0;

                    }

                    model.Bakiye -= model.Odeme;
                }

                liste.Add(model);
            }

            return liste;
        }

        public BankaKartListeModel GetBankaKart(int? kasaId)
        {

            var data_model = data.BankaKartDetay(kasaId).SingleOrDefault();

            var model = new BankaKartListeModel();

            model.BankaAdi = harfIslem.GetValueStr(data_model.BankaAdi);
            model.BankaKodu = data_model.BankaKodu;
            model.HesapAdi = harfIslem.GetValueStr(data_model.HesapAdi);
            model.HesapNo = data_model.HesapNo;
            model.HesapTurAdi = harfIslem.GetValueStr(data_model.HesapTurAdi);
            model.HesapTurId = data_model.HesapTurId;



            return model;
        }

        public BankaIslemModel GetBankaIslem(int? islemId, string islemTur)
        {
            var model = new BankaIslemModel();

            if (islemTur == "cari_odeme")
            {
                var item = data.BankaIslemFis_CariOdeme(islemId).SingleOrDefault();

                if (item != null)
                {
                    model.Aciklama = item.IslemAciklama;
                    model.BankaId = item.BankaId;
                    model.CariId = item.CariId;
                    model.FisNo = item.FisNo;
                    model.Id = islemId;
                    model.Islem = "cari";
                    model.Tarih = item.Tarih;
                    model.Tutar = item.Tutar;
                    model.CariKodu = item.CariKodu;
                    model.BankaKodu = item.BankaKodu;
                }
            }

            if (islemTur == "cari_tahsilat")
            {
                var item = data.BankaIslemFis_CariTahsilat(islemId).SingleOrDefault();

                if (item != null)
                {
                    model.Aciklama = item.IslemAciklama;
                    model.BankaId = item.BankaId;
                    model.CariId = item.CariId;
                    model.FisNo = item.FisNo;
                    model.Id = islemId;
                    model.Islem = "cari";
                    model.Tarih = item.Tarih;
                    model.Tutar = item.Tutar;
                    model.CariKodu = item.CariKodu;
                    model.BankaKodu = item.BankaKodu;
                }
            }

            if (islemTur == "kasa_tahsilat")
            {
                var item = data.BankaIslemFis_KasaTahsilat(islemId).SingleOrDefault();

                if (item != null)
                {
                    model.Aciklama = item.IslemAciklama;
                    model.BankaId = item.BankaId;
                    model.KasaId = item.KasaId;
                    model.FisNo = item.FisNo;
                    model.Id = islemId;
                    model.Islem = "kasa";
                    model.Tarih = item.Tarih;
                    model.Tutar = item.Tutar;
                    model.KasaKodu = item.KasaKodu;
                    model.BankaKodu = item.BankaKodu;
                }
            }

            if (islemTur == "kasa_odeme")
            {
                var item = data.BankaIslemFis_KasaOdeme(islemId).SingleOrDefault();

                if (item != null)
                {
                    model.Aciklama = item.IslemAciklama;
                    model.BankaId = item.BankaId;
                    model.KasaId = item.KasaId;
                    model.FisNo = item.FisNo;
                    model.Id = islemId;
                    model.Islem = "kasa";
                    model.Tarih = item.Tarih;
                    model.Tutar = item.Tutar;
                    model.KasaKodu = item.KasaKodu;
                    model.BankaKodu = item.BankaKodu;
                }
            }

            return model;
        }

        public List<ListeModel> HesapTurListesi()
        {
            var liste = new List<ListeModel>();

            foreach (var item in data.Banka_Hesap_Turleris)
            {
                var model = new ListeModel();

                model.Id = item.Id;
                model.Value = harfIslem.GetValueStr(item.HesapTurAdi);



                liste.Add(model);
            }

            return liste;
        }

        public List<ListeModel> ParaBirimListesi()
        {

            var liste = new List<ListeModel>();

            foreach (var item in data.Para_Birimleris)
            {
                var model = new ListeModel();
                model.Id = item.Id;
                model.Value = harfIslem.GetValueStr(item.Birim_Kod);



                liste.Add(model);
            }

            return liste;
        }

        public List<Tacmin.Model.Genel.KartModel> BankaListesi()
        {
            var liste = new List<Tacmin.Model.Genel.KartModel>();

            foreach (var item in data.Banka_Kartlaris)
            {
                var model = new Tacmin.Model.Genel.KartModel();

                model.Id = item.Id;
                model.Kod = item.BankaKodu;
                model.Tanim = harfIslem.GetValueStr(item.BankaAdi);



                liste.Add(model);
            }

            return liste;
        }

        public List<Tacmin.Model.Genel.KartModel> CariListesi()
        {
            var liste = new List<Tacmin.Model.Genel.KartModel>();

            foreach (var item in data.FIRMA_TANIMLARIs)
            {
                var model = new Tacmin.Model.Genel.KartModel();

                model.Id = item.ID;
                model.Kod = item.CariKodu;
                model.Tanim = harfIslem.GetValueStr(item.UNVAN);



                liste.Add(model);
            }

            return liste;
        }

        public List<Tacmin.Model.Genel.KartModel> GetKasaListesi()
        {
            var liste = new List<Tacmin.Model.Genel.KartModel>();

            foreach (var item in data.Kasa_Kartlaris)
            {
                var model = new Tacmin.Model.Genel.KartModel();

                model.Id = item.Id;
                model.Kod = item.Kodu;
                model.Tanim = harfIslem.GetValueStr(item.KasaAdi);



                liste.Add(model);
            }

            return liste;
        }

        private decimal? BankaTahsilatToplam(int bankaId)
        {
            decimal? toplam = null;


            var liste = data_banka_tahsilat_listesi.Where(x => x.BankaId == bankaId).ToList();

            if (liste.Count > 0)
            {
                toplam = 0;
                foreach (var item in liste)
                {
                    if (item.Tutar != null)
                    {
                        toplam += item.Tutar;
                    }
                }

            }

            return toplam;
        }

        private decimal? BankaOdemeToplam(int bankaId)
        {
            decimal? toplam = null;

            var liste = data_banka_odeme_listesi.Where(x => x.BankaId == bankaId).ToList();

            if (liste.Count > 0)
            {
                toplam = 0;

                foreach (var item in liste)
                {
                    if (item.Tutar != null)
                    {
                        toplam += item.Tutar;
                    }
                }
            }

            return toplam;
        }
    }
}
