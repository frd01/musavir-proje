using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataIslemler.Data;
using DataIslemler.Helpers;
using Tacmin.Model.Genel;
using Tacmin.Model.Kasa;

namespace DataIslemler.Kasa
{
    public class ListeIslem
    {
        private Musavir_DbDataContext data;

        public ListeIslem(string dataAdi)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);
        }

        private List<Para_Birimleri> data_para_birim_listesi;
        private List<Kasa_Odemeler> data_odeme_listesi;
        private List<Kasa_Tahsilatlar> data_tahsilat_listesi;
        private BuyukHarfIslem harfIslem;

        public ListeIslem(string dataAdi, bool is_buyuk_harf)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            data_para_birim_listesi = data.Para_Birimleris.ToList();

            data_tahsilat_listesi = data.Kasa_Tahsilatlars.ToList();
            data_odeme_listesi = data.Kasa_Odemelers.ToList();
            harfIslem = new BuyukHarfIslem(is_buyuk_harf);
        }

        public List<KartListeModel> KasaKartListesi()
        {

            var liste = new List<KartListeModel>();

            foreach (var item in data.Kasa_Kartlaris)
            {
                var model = new KartListeModel();


                model.Id = item.Id;
                model.KasaAdi = harfIslem.GetValueStr(item.KasaAdi);
                model.KasaKodu = item.Kodu;
                model.ParaBirimi = harfIslem.GetValueStr(GetParaBirimi(item.ParaBirimId));
                model.ParaBirimId = item.ParaBirimId;
                model.MuhasebeKodu = item.MuhasebeKodu;
                model.OzelKod = item.OzelKod;
                model.Aciklama = harfIslem.GetValueStr(item.Aciklama);
                model.Tahsilat = TahsilatToplam(item.Id);
                model.Odeme = OdemeToplam(item.Id);

                if (model.Tahsilat != null)
                {
                    if (model.Bakiye == null)
                        model.Bakiye = 0;

                    model.Bakiye += model.Tahsilat;

                }

                if (model.Odeme != null)
                {
                    if (model.Bakiye == null)
                        model.Bakiye = 0;

                    model.Bakiye -= model.Odeme;
                }

                liste.Add(model);
            }

            return liste;
        }

        public Tacmin.Model.Kasa.KartModel GetKasaKart(int? kasaId)
        {
            var model = new Tacmin.Model.Kasa.KartModel();

            var item = data.Kasa_Kartlaris.Where(x => x.Id == kasaId).SingleOrDefault();

            if (item != null)
            {

                model.Id = item.Id;

                model.KasaAdi = harfIslem.GetValueStr(item.KasaAdi);
                model.KasaKodu = item.Kodu;
                model.ParaBirimId = item.ParaBirimId;
                model.OzelKod = item.OzelKod;
                model.Aciklama = harfIslem.GetValueStr(item.Aciklama);
                model.MuhasebeKodu = item.MuhasebeKodu;


            }

            return model;
        }

        public List<KasaTakipModel> KasaIslemTakipListesi()
        {

            var liste = new List<KasaTakipModel>();

            var model = new KasaTakipModel();
            model.Id = (int)KasaTakipEnum.ISLEME_DEVAM_ET;
            model.IslemTanim = "İşleme Devam Et";
            liste.Add(model);

            model = new KasaTakipModel();
            model.Id = (int)KasaTakipEnum.ISLEME_DURDUR;
            model.IslemTanim = "İşlemi Durdur";
            liste.Add(model);

            model = new KasaTakipModel();
            model.Id = (int)KasaTakipEnum.ONAY_AL;
            model.IslemTanim = "Onay Al";
            liste.Add(model);

            return liste;

        }

        private string GetParaBirimi(int? paraBirimId)
        {

            var birim = "";

            var model = data_para_birim_listesi.Where(x => x.Id == paraBirimId).SingleOrDefault();

            if (model != null)
            {
                birim = model.Birim_Kod;
            }

            return birim;
        }

        public List<BirimModel> KasaParaBirimListesi()
        {

            var liste = new List<BirimModel>();

            foreach (var item in data_para_birim_listesi)
            {
                var model = new BirimModel();

                model.Id = item.Id;
                model.BirimAdi = harfIslem.GetValueStr(item.Birim_Adi);

                liste.Add(model);
            }

            return liste;
        }

        public List<Tacmin.Model.Genel.KartModel> CariFilterList()
        {

            var liste = new List<Tacmin.Model.Genel.KartModel>();

            foreach (var item in data.FIRMA_TANIMLARIs)
            {
                var model = new Tacmin.Model.Genel.KartModel();

                model.Id = item.ID;
                model.Tanim = item.CariKodu + " - " + harfIslem.GetValueStr(item.UNVAN);
                model.Kod = item.CariKodu;

                liste.Add(model);
            }

            return liste;
        }

        public KasaIslemModel GetKasaIslem(int? islemId, string islemTur)
        {

            var model = new KasaIslemModel();

            if (islemTur == "Nakit Ödeme" || islemTur == "NAKİT ÖDEME")
            {
                var item = data.Kasa_Tahsilatlars.Where(x => x.Id == islemId).SingleOrDefault();

                if (item != null)
                {
                    model.Aciklama = item.Aciklama;
                    model.CariId = item.CariId;
                    model.FisNo = item.FisNo;
                    model.Id = item.Id;
                    model.KasaId = item.KasaId;
                    model.Tarih = item.Tarih;
                    model.Tutar = item.Tutar;
                }
            }

            if (islemTur == "Nakit Alınan" || islemTur == "NAKİT ALINAN")
            {
                var item = data.Kasa_Odemelers.Where(x => x.Id == islemId).SingleOrDefault();

                if (item != null)
                {
                    model.Aciklama = item.Aciklama;
                    model.CariId = item.CariId;
                    model.FisNo = item.FisNo;
                    model.Id = item.Id;
                    model.KasaId = item.KasaId;
                    model.Tarih = item.Tarih;
                    model.Tutar = item.Tutar;
                }
            }

            return model;
        }

        public List<Tacmin.Model.Genel.KartModel> KasaKartFilterList()
        {

            var liste = new List<Tacmin.Model.Genel.KartModel>();

            foreach (var item in data.Kasa_Kartlaris)
            {
                var model = new Tacmin.Model.Genel.KartModel();

                model.Id = item.Id;
                model.Kod = item.Kodu;
                model.Tanim = item.Kodu + " - " + harfIslem.GetValueStr(item.KasaAdi);

                liste.Add(model);
            }

            return liste;


        }

        private decimal? TahsilatToplam(int? kasaId)
        {
            decimal? toplam = null;

            var liste = data_tahsilat_listesi.Where(x => x.KasaId == kasaId).ToList();



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

            foreach (var item in data.BankaKasaCekilenler_KasaId(kasaId))
            {
                if (toplam == null)
                {
                    toplam = 0;
                }

                toplam += item.Tutar;
            }

            return toplam;
        }

        private decimal? OdemeToplam(int? kasaId)
        {
            decimal? toplam = null;

            var liste = data_odeme_listesi.Where(x => x.KasaId == kasaId).ToList();

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

            foreach (var item in data.BankaKasaYatirilanlar_KasaId(kasaId))
            {
                if (toplam == null)
                {
                    toplam = 0;
                }

                toplam += item.Tutar;
            }

            return toplam;
        }
    }
}
