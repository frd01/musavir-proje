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
    public class RaporIslem
    {
        private Musavir_DbDataContext data;
        private BuyukHarfIslem harfIslem;

        public RaporIslem(string dataAdi,bool _harfIslem)
        {
            var con_str = new Baglanti(dataAdi).ConStr;

            data = new Musavir_DbDataContext(con_str);

            harfIslem = new BuyukHarfIslem(_harfIslem);
        }

        public List<KasaIslemListeModel> KasaIslemListesi()
        {

            var liste = new List<KasaIslemListeModel>();

            //tahsilatları yükle
            foreach (var item in data.KasaTahsilatListesi())
            {
                var model = new KasaIslemListeModel();

                model.Aciklama = item.UNVAN + " - " + item.Aciklama;
                model.Aciklama = harfIslem.GetValueStr(model.Aciklama);
                model.CariId = item.CariId;
                model.GirenTutar = item.Tutar;
                model.Id = item.Id;
                model.KasaAdi = harfIslem.GetValueStr(item.KasaAdi);
                model.KasaId = item.KasaId;
                model.KasaKodu = item.KasaKodu;
                model.Tarih = item.Tarih;

                liste.Add(model);
            }

            //ödemeleri yükle
            foreach (var item in data.KasaOdemeListesi())
            {
                var model = new KasaIslemListeModel();

                model.Aciklama = item.UNVAN + " - " + item.Aciklama;
                model.Aciklama = harfIslem.GetValueStr(model.Aciklama);
                model.CariId = item.CariId;
                model.CikanTutar = item.Tutar;
                model.Id = item.Id;
                model.KasaAdi = harfIslem.GetValueStr(item.KasaAdi);
                model.KasaId = item.KasaId;
                model.KasaKodu = item.KasaKodu;
                model.Tarih = item.Tarih;

                liste.Add(model);
            }

            // banka çekilenleri ekle

            foreach (var item in data.BankaKasaCekilenler())
            {
                var model = new KasaIslemListeModel();

                model.Aciklama = item.BankaAdi + " " + item.SubeAdi + " Şubesi " + item.HesapNo + " - " + item.IslemAciklama;
                model.Aciklama = harfIslem.GetValueStr(model.Aciklama);
                model.GirenTutar = item.Tutar;
                model.Id = item.Id;
                model.KasaAdi = harfIslem.GetValueStr(item.KasaAdi);
                model.KasaId = item.KasaId;
                model.KasaKodu = item.KasaKodu;
                model.Tarih = item.Tarih;

                liste.Add(model);
            }

            // Banka Yatırılanları Ekle

            foreach (var item in data.BankaKasaYatirilanlar())
            {
                var model = new KasaIslemListeModel();

                model.Aciklama = item.BankaAdi + " " + item.SubeAdi + " Şubesi " + item.HesapNo + " - " + item.IslemAciklama;
                model.Aciklama = harfIslem.GetValueStr(model.Aciklama);
                model.CikanTutar = item.Tutar;
                model.Id = item.Id;
                model.KasaAdi = harfIslem.GetValueStr(item.KasaAdi);
                model.KasaId = item.KasaId;
                model.KasaKodu = item.KasaKodu;
                model.Tarih = item.Tarih;

                liste.Add(model);
            }

            decimal? bakiye = null;

            //bakiye hesapla

            liste = (from m in liste orderby m.Tarih ascending select m).ToList();

            foreach (var item in liste)
            {
                if (item.CikanTutar != null)
                {
                    if (bakiye == null)
                        bakiye = 0;

                    bakiye -= item.CikanTutar;
                }

                if (item.GirenTutar != null)
                {
                    if (bakiye == null)
                        bakiye = 0;

                    bakiye += item.GirenTutar;
                }

                item.Bakiye = bakiye;
            }

            return liste;
        }

        public List<KasaIslemListeModel> KasaAyrintiListe(int? kasaId)
        {

            var liste = new List<KasaIslemListeModel>();

            //tahsilatları yükle
            foreach (var item in data.KasaTahsilatListesi_KasaId(kasaId))
            {
                var model = new KasaIslemListeModel();

                model.Aciklama = item.UNVAN + " - " + item.Aciklama;
                model.Aciklama = harfIslem.GetValueStr(model.Aciklama);
                model.CariId = item.CariId;
                model.GirenTutar = item.Tutar;
                model.Id = item.Id;
                model.KasaAdi = harfIslem.GetValueStr(item.KasaAdi);
                model.KasaId = item.KasaId;
                model.KasaKodu = item.KasaKodu;
                model.Tarih = item.Tarih;
                model.IslemTur = harfIslem.GetValueStr("Cari Tahsilat");
                model.Modul = "cari";

                liste.Add(model);
            }

            //ödemeleri yükle
            foreach (var item in data.KasaOdemeListesi_KasaId(kasaId))
            {
                var model = new KasaIslemListeModel();

                model.Aciklama = item.UNVAN + " - " + item.Aciklama;
                model.Aciklama = harfIslem.GetValueStr(model.Aciklama);
                model.CariId = item.CariId;
                model.CikanTutar = item.Tutar;
                model.Id = item.Id;
                model.KasaAdi = harfIslem.GetValueStr(item.KasaAdi);
                model.KasaId = item.KasaId;
                model.KasaKodu = item.KasaKodu;
                model.Tarih = item.Tarih;
                model.IslemTur = harfIslem.GetValueStr("Cari Ödeme");
                model.Modul = "cari";

                liste.Add(model);
            }

            // banka çekilenleri ekle

            foreach (var item in data.BankaKasaCekilenler_KasaId(kasaId))
            {
                var model = new KasaIslemListeModel();

                model.Aciklama = item.BankaAdi + " " + item.SubeAdi + " Şubesi " + item.HesapNo + " - " + item.IslemAciklama;
                model.Aciklama = harfIslem.GetValueStr(model.Aciklama);
                model.GirenTutar = item.Tutar;
                model.Id = item.Id;
                model.KasaAdi = harfIslem.GetValueStr(item.KasaAdi);
                model.KasaId = item.KasaId;
                model.KasaKodu = item.KasaKodu;
                model.Tarih = item.Tarih;
                model.IslemTur = harfIslem.GetValueStr("Banka Çekilen");
                model.Modul = "banka";

                liste.Add(model);
            }

            // Banka Yatırılanları Ekle

            foreach (var item in data.BankaKasaYatirilanlar_KasaId(kasaId))
            {
                var model = new KasaIslemListeModel();

                model.Aciklama = item.BankaAdi + " " + item.SubeAdi + " Şubesi " + item.HesapNo + " - " + item.IslemAciklama;
                model.Aciklama = harfIslem.GetValueStr(model.Aciklama);
                model.CikanTutar = item.Tutar;
                model.Id = item.Id;
                model.KasaAdi = harfIslem.GetValueStr(item.KasaAdi);
                model.KasaId = item.KasaId;
                model.KasaKodu = item.KasaKodu;
                model.Tarih = item.Tarih;
                model.IslemTur = harfIslem.GetValueStr("Banka Yatan");
                model.Modul = "banka";

                liste.Add(model);
            }

            decimal? bakiye = null;

            //bakiye hesapla

            liste = (from m in liste orderby m.Tarih ascending select m).ToList();

            foreach (var item in liste)
            {
                if (item.CikanTutar != null)
                {
                    if (bakiye == null)
                        bakiye = 0;

                    bakiye -= item.CikanTutar;
                }

                if (item.GirenTutar != null)
                {
                    if (bakiye == null)
                        bakiye = 0;

                    bakiye += item.GirenTutar;
                }

                item.Bakiye = bakiye;
            }

            return liste;
        }

        public List<KasaIslemListeModel> KasaAyrintiListe_Tarih(int? kasaId, DateTime? ilkTarih, DateTime? sonTarih)
        {

            var liste = new List<KasaIslemListeModel>();

            //tahsilatları yükle
            foreach (var item in data.KasaTahsilatListesi_KasaId_Tarih(kasaId, ilkTarih, sonTarih))
            {
                var model = new KasaIslemListeModel();

                model.Aciklama = item.UNVAN + " - " + item.Aciklama;
                model.Aciklama = harfIslem.GetValueStr(model.Aciklama);
                model.CariId = item.CariId;
                model.GirenTutar = item.Tutar;
                model.Id = item.Id;
                model.KasaAdi = harfIslem.GetValueStr(item.KasaAdi);
                model.KasaId = item.KasaId;
                model.KasaKodu = item.KasaKodu;
                model.Tarih = item.Tarih;
                model.IslemTur = harfIslem.GetValueStr("Cari Tahsilat");
                model.Modul = "cari";

                liste.Add(model);
            }

            //ödemeleri yükle
            foreach (var item in data.KasaOdemeListesi_KasaId_Tarih(kasaId, ilkTarih, sonTarih))
            {
                var model = new KasaIslemListeModel();

                model.Aciklama = item.UNVAN + " - " + item.Aciklama;
                model.Aciklama = harfIslem.GetValueStr(model.Aciklama);
                model.CariId = item.CariId;
                model.CikanTutar = item.Tutar;
                model.Id = item.Id;
                model.KasaAdi = harfIslem.GetValueStr(item.KasaAdi);
                model.KasaId = item.KasaId;
                model.KasaKodu = item.KasaKodu;
                model.Tarih = item.Tarih;
                model.IslemTur = harfIslem.GetValueStr("Cari Ödeme");
                model.Modul = "cari";

                liste.Add(model);
            }

            // banka çekilenleri ekle

            foreach (var item in data.BankaKasaCekilenler_KasaId_Tarih(kasaId, ilkTarih, sonTarih))
            {
                var model = new KasaIslemListeModel();

                model.Aciklama = item.BankaAdi + " " + item.SubeAdi + " Şubesi " + item.HesapNo + " - " + item.IslemAciklama;
                model.Aciklama = harfIslem.GetValueStr(model.Aciklama);
                model.GirenTutar = item.Tutar;
                model.Id = item.Id;
                model.KasaAdi = harfIslem.GetValueStr(item.KasaAdi);
                model.KasaId = item.KasaId;
                model.KasaKodu = item.KasaKodu;
                model.Tarih = item.Tarih;
                model.IslemTur = harfIslem.GetValueStr("Banka Çekilen");
                model.Modul = "banka";

                liste.Add(model);
            }

            // Banka Yatırılanları Ekle

            foreach (var item in data.BankaKasaYatirilanlar_KasaId_Tarih(kasaId, ilkTarih, sonTarih))
            {
                var model = new KasaIslemListeModel();

                model.Aciklama = item.BankaAdi + " " + item.SubeAdi + " Şubesi " + item.HesapNo + " - " + item.IslemAciklama;
                model.Aciklama = harfIslem.GetValueStr(model.Aciklama);
                model.CikanTutar = item.Tutar;
                model.Id = item.Id;
                model.KasaAdi = harfIslem.GetValueStr(item.KasaAdi);
                model.KasaId = item.KasaId;
                model.KasaKodu = item.KasaKodu;
                model.Tarih = item.Tarih;
                model.IslemTur = harfIslem.GetValueStr("Banka Yatan");
                model.Modul = "banka";

                liste.Add(model);
            }

            decimal? bakiye = null;

            //bakiye hesapla

            liste = (from m in liste orderby m.Tarih ascending select m).ToList();

            foreach (var item in liste)
            {
                if (item.CikanTutar != null)
                {
                    if (bakiye == null)
                        bakiye = 0;

                    bakiye -= item.CikanTutar;
                }

                if (item.GirenTutar != null)
                {
                    if (bakiye == null)
                        bakiye = 0;

                    bakiye += item.GirenTutar;
                }

                item.Bakiye = bakiye;
            }

            return liste;
        }

        public List<KasaBakiyeModel> KasaBakiyeRaporu(RaporRequestModel args)
        {
            var liste = new List<KasaBakiyeModel>();

            foreach (var item in data.Kasa_Kartlaris)
            {
                var model = new KasaBakiyeModel();

                model.Id = item.Id;
                model.KasaAdi = item.KasaAdi;
                model.KasaKodu = item.Kodu;
                model.Tahsilat = GetTahsilatToplam(item.Id, args);
                model.Odeme = GetOdemeToplam(item.Id, args);

                if (model.Tahsilat == null)
                    model.Tahsilat = 0;
                if (model.Odeme == null)
                    model.Odeme = 0;

                model.Bakiye = model.Tahsilat - model.Odeme;

                liste.Add(model);
            }

            return liste;
        }

        private decimal? GetTahsilatToplam(int kasaId, RaporRequestModel args)
        {

            decimal? toplam = 0;

            foreach (var item in data.KasaTahsilatListesi_KasaId_Tarih(kasaId, args.IlkTarih, args.SonTarih))
            {
                if (item.Tutar != null)
                {
                    toplam += item.Tutar;
                }
            }

            foreach (var item in data.BankaKasaCekilenler_KasaId_Tarih(kasaId, args.IlkTarih, args.SonTarih))
            {
                if (item.Tutar != null)
                {
                    toplam += item.Tutar;
                }
            }

            return toplam;
        }

        private decimal? GetOdemeToplam(int kasaId, RaporRequestModel args)
        {

            decimal? toplam = 0;

            foreach (var item in data.KasaOdemeListesi_KasaId_Tarih(kasaId, args.IlkTarih, args.SonTarih))
            {
                if (item.Tutar != null)
                {
                    toplam += item.Tutar;
                }
            }

            foreach (var item in data.BankaKasaYatirilanlar_KasaId_Tarih(kasaId, args.IlkTarih, args.SonTarih))
            {
                if (item.Tutar != null)
                {
                    toplam += item.Tutar;
                }
            }

            return toplam;
        }

        public RaporParamsModel<KasaEkstraModel, KasaEkstraParamsModel> KasaEsktraListesi(RaporRequestModel args)
        {

            var rapor_model = new RaporParamsModel<KasaEkstraModel, KasaEkstraParamsModel>();

            var rapor_liste = new List<KasaEkstraModel>();
            var params_liste = new List<KasaEkstraParamsModel>();

            var index = 0;

            foreach (var kasa in data.Kasa_Kartlaris)
            {
                var data_liste = KasaAyrintiListe_Tarih(kasa.Id, args.IlkTarih, args.SonTarih);

                if (index == 0)
                {
                    params_liste.Add(new KasaEkstraParamsModel { KasaAdi = kasa.KasaAdi, KasaKodu = kasa.Kodu });
                }

                foreach (var item in data_liste)
                {
                    var model = new KasaEkstraModel();

                    model.Id = item.Id;
                    model.Aciklama = item.Aciklama;
                    model.Bakiye = item.Bakiye;
                    model.CikanTutar = item.CikanTutar;
                    model.GirenTutar = item.GirenTutar;
                    model.IslemTur = item.IslemTur;
                    model.Tarih = item.Tarih.Value.ToString("dd/MM/yyyy");

                    rapor_liste.Add(model);

                }

                index++;

            }

            rapor_model.RaporListe = rapor_liste;
            rapor_model.ParamsListe = params_liste;

            return rapor_model;
        }
    }
}
